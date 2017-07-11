using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using DataLogger.Entities;
using System.Globalization;
using DataLogger.Data;
using System.Diagnostics;
using System.IO;
using Excel = ClosedXML.Excel;
//Microsoft.Office.Interop.Excel;
using System.Reflection;
using DataLogger.Utils;
using System.Resources;
using System.Net.Sockets;
using System.Net;
using WinformProtocol;
using Npgsql;

namespace DataLogger
{
    public partial class frmNewMain : Form
    {
        LanguageService lang = new LanguageService(typeof(frmNewMain).Assembly);
        //ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        //CultureInfo cul;            //declare culture info
        public static string language_code = "en";
        public static DateTime datetime00;
        public bool is_close_form = false;
        public static Boolean isSamp;
        public static TcpListener tcpListener = null;

        private System.Threading.Timer tmrThreadingTimer;
        private System.Threading.Timer tmrThreadingTimer_HeadingTime;
        private System.Threading.Timer tmrThreadingTimerStationStatus;
        private System.Threading.Timer tmrThreadingTimerFor5Minute;
        private System.Threading.Timer tmrThreadingTimerFor60Minute;
        private System.Threading.Timer tmrThreadingTimerForFTP;
        public CalculationDataValue objCalCulationDataValue5Minute = new CalculationDataValue();
        public CalculationDataValue objCalCulationDataValue60Minute = new CalculationDataValue();

        public const int TRANSACTION_ADD_NEW = 1;
        public const int TRANSACTION_UPDATE = 2;

        public const int PERIOD_CHECK_COMMUNICATION_ERROR = 35;

        public const string ADAM_4050 = "ADAM4050";
        public const string ADAM_4051 = "ADAM4051";
        public const string ADAM_TEMP_HUMIDITY_ = "TEMP_HUMIDITY_";

        public const string CODE_MPS_PH = "CODE_MPS_PH";
        public const string CODE_MPS_EC = "CODE_MPS_EC";
        public const string CODE_MPS_DO = "CODE_MPS_DO";
        public const string CODE_MPS_TURBIDITY = "CODE_MPS_TURBIDITY";
        public const string CODE_MPS_ORP = "CODE_MPS_ORP";
        public const string CODE_MPS_TEMP = "CODE_MPS_TEMP";
        public const string CODE_TN = "CODE_TN";
        public const string CODE_TP = "CODE_TP";
        public const string CODE_TOC = "CODE_TOC";

        public const string STATUS_ERROR = "Error";
        public const string STATUS_Normal = "Normal";
        public const string STATUS_WARNING = "Warning";
        public const string STATUS_MEASURING = "Measuring";
        public const string STATUS_CALIBRATE = "Calibrate";

        public const int INT_STATUS_NORMAL = 0;
        public const int INT_STATUS_MEASURING_STOP = 1;
        public const int INT_STATUS_EMPTY_SAMPLER_RESERVOIR = 2;
        public const int INT_STATUS_CALIBRATING = 3;
        public const int INT_STATUS_MAINTENANCE = 4;
        public const int INT_STATUS_COMMUNICATION_ERROR = 5;
        public const int INT_STATUS_INSTRUMENT_ERROR = 6;

        // global dataValue
        int countingRequest = 0;
        public int firstTimeForIOControl = 0;
        //
        public static Boolean connectFlag = false;
        //public static station_status objStationStatusGlobal = new station_status();
        public static measured_data objMeasuredDataGlobal = new measured_data();
        //public static module _modules = new module();
        public static string TNComname = "COM100";
        public static string TPComname = "COM100";
        public static string TOCComname = "COM100";
        data_value obj5MinuteDataValue = new data_value();
        data_value obj60MinuteDataValue = new data_value();

        // delegate used for Invoke
        internal delegate void StringDelegate(string data);
        internal delegate void _setTextMPS(string data, TextBox box);
        internal delegate void HeadingTimerDelegate(string data);
        private delegate void ProcessDataCallback(string text);
        internal delegate void SetHeadingLoginNameDelegate(string data);

        public delegate void DataReceivedEventHandler(object sender, ReceivedEventArgs e);
        public event DataReceivedEventHandler DataReceived;
        private const int PACKET_LENGTH = 92;

        private byte[] TOC_receive_buffer = new byte[2048];

        private byte[] TP_receive_buffer = new byte[2048];

        private byte[] TN_receive_buffer = new byte[2048];
        // MPS
        private int MPS_rx_write = 0;
        private int MPS_rx_counter = 0;
        private byte[] MPS_rx_buffer = new byte[2048];

        private const int MPS_PACKET_LENGTH = 29;
        private byte[] MPS_receive_buffer = new byte[2048];
        private int MPS_buffer_counter = 0;

        private byte[] SAMP_rx_buffer = new byte[2048];

        private const int SAMP_PACKET_LENGTH = 86;
        private byte[] SAMP_receive_buffer = new byte[2048];

        private int ADAM405x_rx_write = 0;
        private int ADAM405x_rx_counter = 0;
        private byte[] ADAM405x_rx_buffer = new byte[2048];

        private const int ADAM405x_PACKET_LENGTH = 8;
        private const int ADAM_TEMP_HUMIDITY_PACKET_LENGTH = 58;
        private byte[] ADAM405x_receive_buffer = new byte[2048];

        private readonly data_5minute_value_repository db5m = new data_5minute_value_repository();
        private readonly data_60minute_value_repository db60m = new data_60minute_value_repository();
        private readonly maintenance_log_repository _maintenance_logs = new maintenance_log_repository();

        public string tooltipTOCInfo = "";
        public string tooltipTPInfo = "";
        public string tooltipTNInfo = "";

        public string tooltipTOC = "";
        public string tooltipTP = "";
        public string tooltipTN = "";

        #region Form event
        public frmNewMain()
        {
            InitializeComponent();
        }

        private void frmNewMain_Load(object sender, EventArgs e)
        {

            frmConfiguration.protocol = new Form1(this);
            GlobalVar.maintenanceLog = new maintenance_log();

            initUserInterface();
            tmrThreadingTimer = new System.Threading.Timer(new TimerCallback(tmrThreadingTimer_TimerCallback), null, System.Threading.Timeout.Infinite, 2000);
            tmrThreadingTimer.Change(0, 2000);

            tmrThreadingTimer_HeadingTime = new System.Threading.Timer(new TimerCallback(tmrThreadingTimer_HeadingTime_TimerCallback), null, System.Threading.Timeout.Infinite, 700);
            tmrThreadingTimer_HeadingTime.Change(0, 700);

            tmrThreadingTimerStationStatus = new System.Threading.Timer(new TimerCallback(tmrThreadingTimerStationStatus_TimerCallback), null, System.Threading.Timeout.Infinite, 2000);
            tmrThreadingTimerStationStatus.Change(0, 2000);

            tmrThreadingTimerFor5Minute = new System.Threading.Timer(new TimerCallback(tmrThreadingTimerFor5Minute_TimerCallback), null, System.Threading.Timeout.Infinite, 50000);
            //tmrThreadingTimerFor5Minute.Change(0, 2500);
            tmrThreadingTimerFor5Minute.Change(0,50000);
            tmrThreadingTimerFor60Minute = new System.Threading.Timer(new TimerCallback(tmrThreadingTimerFor60Minute_TimerCallback), null, System.Threading.Timeout.Infinite, 2000);
            //tmrThreadingTimerFor60Minute.Change(0, 3000);
            tmrThreadingTimerFor60Minute.Change(0, 240000);

            tmrThreadingTimerForFTP = new System.Threading.Timer(new TimerCallback(tmrThreadingTimerForFTP_TimerCallback), null, 1000 * 60, Timeout.Infinite);
            tmrThreadingTimerForFTP.Change(0, 1000 * 60 * 60 * 2);
            initConfig(true);

        }

        private void initConfig(bool isConfigCOM = false)
        {
            GlobalVar.stationSettings = new station_repository().get_info();
            GlobalVar.moduleSettings = new module_repository().get_all();

            label9.Text = Convert.ToString(GlobalVar.stationSettings.station_name);
            for (int i = 1; i <= GlobalVar.moduleSettings.Count(); i++)
            {
                foreach (var item in GlobalVar.moduleSettings)
                {
                    string currentvar = "var" + i.ToString();
                    string currentlabelname = currentvar + "Text";
                    string currentlabelunit = "Unit" + i.ToString();
                    if (item.item_name.Equals(currentvar))
                    {
                        ClearLabel(panel30, item.display_name, currentlabelname);
                        ClearLabel(panel30, item.unit, currentlabelunit);
                    }
                }
            }
            if (init(isConfigCOM))
            {
            }
            else
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                tcpListener.Stop();
            }
            catch (Exception ex)
            {
            }
            this.Close();
        }
        #endregion

        #region initial method
        /// <summary>
        /// init open comport
        /// </summary>
        /// <returns></returns>
        private bool init(bool isConfigCOM = false)
        {
            try
            {
                txtMPSpHValue.Text = "---";
                txtMPSORPValue.Text = "---";
                txtMPSTempValue.Text = "---";
                txtMPSDOValue.Text = "---";
                txtMPSTurbValue.Text = "---";
                txtMPSCondValue.Text = "---";
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// initial interface setting
        /// </summary>
        private void initUserInterface()
        {
            switch_language();
        }

        private void switch_language()
        {
            lang.nextLanguage();
            switch (lang.CurrentLanguage.Language)
            {
                case ELanguage.English:
                    language_code = "en";
                    this.btnMonthlyReport.BackgroundImage = global::DataLogger.Properties.Resources.MonthlyReportButton;
                    break;
                case ELanguage.Vietnamese:
                    language_code = "vi";
                    this.btnMonthlyReport.BackgroundImage = global::DataLogger.Properties.Resources.MonthlyReportButton;
                    break;
                default:
                    break;
            }

            this.btnLanguage.BackgroundImage = lang.CurrentLanguage.Icon;


            // heading menu
            lang.setText(lblHeaderNationName, "main_menu_language");
            lang.setText(lblMainMenuTitle, "main_menu_title");
            settingForLoginStatus();

            lang.setText(lblThaiNguyenStation, "thai_nguyen_station_text", EAlign.Center);
            lang.setText(lblAutomaticMonitoring, "automatic_monitoring_text", EAlign.Center);
            lang.setText(lblSurfaceWaterQuality, "surface_water_quality_text", EAlign.Center);

            lang.setText(this, "data_logger_system");
        }
        #endregion
        private void setText(string text)
        {
            if (this.txtData.InvokeRequired)
            {
                StringDelegate d = new StringDelegate(setText);
                this.txtData.Invoke(d, new object[] { text });
            }
            else
            {
                txtData.Text = text;
            }
        }
        private void setTextMPS(string text,TextBox box)
        {
            if (box.InvokeRequired)
            {
                _setTextMPS d = new _setTextMPS(updateTextMPS);
                box.Invoke(d, new object[] { text, box });
            }
            else
            {
                box.Text = text;
            }
        }
        private void updateTextMPS(string text, TextBox box)
        {
            box.Text = text;
        }
        private void setTextHeadingTimer(string text)
        {
            if (this.txtData.InvokeRequired)
            {
                HeadingTimerDelegate d = new HeadingTimerDelegate(setTextHeadingTimer);
                this.lblHeadingTime.Invoke(d, new object[] { text });
            }
            else
            {
                lblHeadingTime.Text = text;
            }
        }
        private void setTextHeadingLogin(string text)
        {
            if (this.txtData.InvokeRequired)
            {
                SetHeadingLoginNameDelegate d = new SetHeadingLoginNameDelegate(setTextHeadingLogin);
                this.lblLoginDisplayName.Invoke(d, new object[] { text });
            }
            else
            {
                lblLoginDisplayName.Text = text;
            }
        }

        public static ASCIIEncoding _encoder = new ASCIIEncoding();

        #region threading timer
        public int indexSelection = 0;
        private void tmrThreadingTimer_TimerCallback(object state)
        {
            if (is_close_form)
            {
                try
                {
                    this.Close();
                    //MessageBox.Show("123");
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(Environment.ExitCode);
                    }
                }
                catch
                {

                }
            }
            setText(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        private void tmrThreadingTimer_HeadingTime_TimerCallback(object state)
        {
            if (is_close_form)
            {
                try
                {
                    this.Close();
                    //MessageBox.Show("123");
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(Environment.ExitCode);
                    }
                }
                catch
                {

                }
            }
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            setTextHeadingTimer(time);
            settingForLoginStatus();

        }

        public int indexSelectionStation = 0;
        public static string station_status_data_type = "";
        private void tmrThreadingTimerStationStatus_TimerCallback(object state)
        {
            if (is_close_form)
            {
                try
                {
                    this.Close();
                    //MessageBox.Show("123");
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(Environment.ExitCode);
                    }
                }
                catch
                {

                }
            }
        }

        public static int countingIndex5Minute = 0;
        private void tmrThreadingTimerFor5Minute_TimerCallback(object state)
        {
            try
            {
                //Console.Write("Start 5min thread \n");
                Boolean saved = false;
                if (is_close_form)
                {
                    try
                    {
                        this.Close();
                        //MessageBox.Show("123");
                        if (System.Windows.Forms.Application.MessageLoop)
                        {
                            // WinForms app
                            System.Windows.Forms.Application.Exit();
                        }
                        else
                        {
                            // Console app
                            System.Environment.Exit(Environment.ExitCode);
                        }
                    }
                    catch
                    {

                    }
                }
                // 50 seconds save current time to datavalue table
                if (countingIndex5Minute < 1)
                {
                    countingIndex5Minute++;
                    Console.WriteLine("countingIndex5Minute :" + countingIndex5Minute);
                    return;
                }
                else
                {

                }
                //txtMPSCondValue.Text = "----";
                //Console.WriteLine("TRUE1");
                data_value objDataValue = new data_value();
                //data_value objRawDataValue = new data_value();

                measured_data objConnectData = new measured_data();
                measured_data objConnectRawData = new measured_data();
                //TURN ON CLIENT
                objConnectData = getsensordata(out objConnectRawData);

                objMeasuredDataGlobal.var1 = objConnectData.var1;
                objMeasuredDataGlobal.var2 = objConnectData.var2;
                objMeasuredDataGlobal.var3 = objConnectData.var3;
                objMeasuredDataGlobal.var4 = objConnectData.var4;
                objMeasuredDataGlobal.var5 = objConnectData.var5;
                objMeasuredDataGlobal.var6 = objConnectData.var6;
                objMeasuredDataGlobal.MPS_status = objConnectData.MPS_status;

                //objMeasuredDataGlobal.var1 = 1.45;
                //objMeasuredDataGlobal.var2 = 2.45;
                //objMeasuredDataGlobal.var3 = 3.45;
                //objMeasuredDataGlobal.var4 = 4.465;
                //objMeasuredDataGlobal.var5 = 5.45;
                //objMeasuredDataGlobal.var6 = 6.54;
                //objMeasuredDataGlobal.MPS_status = 0;
                objMeasuredDataGlobal.latest_update_MPS_communication = DateTime.Now;
                objConnectRawData.latest_update_MPS_communication = DateTime.Now;
                // MPS
                if (objMeasuredDataGlobal.MPS_status < 0)
                {
                    objMeasuredDataGlobal.MPS_status = CommonInfo.INT_STATUS_COMMUNICATION_ERROR;
                    objMeasuredDataGlobal.var1 = -1;
                    objMeasuredDataGlobal.var2 = -1;
                    objMeasuredDataGlobal.var3 = -1;
                    objMeasuredDataGlobal.var4 = -1;
                    objMeasuredDataGlobal.var5 = -1;
                    objMeasuredDataGlobal.var6 = -1;
                }

                if (objConnectRawData.MPS_status < 0)
                {
                    objConnectRawData.MPS_status = CommonInfo.INT_STATUS_COMMUNICATION_ERROR;
                    objConnectRawData.var1 = -1;
                    objConnectRawData.var2 = -1;
                    objConnectRawData.var3 = -1;
                    objConnectRawData.var4 = -1;
                    objConnectRawData.var5 = -1;
                    objConnectRawData.var6 = -1;
                }


                objDataValue.var1 = objMeasuredDataGlobal.var1;
                objDataValue.var1_status = objMeasuredDataGlobal.MPS_status;
                //objRawDataValue.var1_status = objRawDataValue.MPS_status;

                objDataValue.var2 = objMeasuredDataGlobal.var2;
                objDataValue.var2_status = objMeasuredDataGlobal.MPS_status;
                //objRawDataValue.var2_status = objRawDataValue.MPS_status;

                objDataValue.var3 = objMeasuredDataGlobal.var3;
                objDataValue.var3_status = objMeasuredDataGlobal.MPS_status;
                //objRawDataValue.var3_status = objRawDataValue.MPS_status;

                objDataValue.var4 = objMeasuredDataGlobal.var4;
                objDataValue.var4_status = objMeasuredDataGlobal.MPS_status;
                //objRawDataValue.var4_status = objRawDataValue.MPS_status;

                objDataValue.var5 = objMeasuredDataGlobal.var5;
                objDataValue.var5_status = objMeasuredDataGlobal.MPS_status;
                //objRawDataValue.var5_status = objRawDataValue.MPS_status;

                objDataValue.var6 = objMeasuredDataGlobal.var6;
                objDataValue.var6_status = objMeasuredDataGlobal.MPS_status;
                //objRawDataValue.var6_status = objRawDataValue.MPS_status;

                objDataValue.MPS_status = objMeasuredDataGlobal.MPS_status;
                //objRawDataValue.MPS_status = objRawDataValue.MPS_status;

                // Time
                objDataValue.stored_date = DateTime.Now;
                objDataValue.stored_hour = DateTime.Now.Hour;
                objDataValue.stored_minute = DateTime.Now.Minute;

                //objRawDataValue.stored_date = DateTime.Now;
                //objRawDataValue.stored_hour = DateTime.Now.Hour;
                //objRawDataValue.stored_minute = DateTime.Now.Minute;

                //if (new data_value_repository().add(ref objDataValue) > 0)
                //{
                //    saved = true;
                //    using (module_repository _modules = new module_repository())
                //    {
                //        IEnumerable<module> moduleConfigList = _modules.get_all();
                //        //IEnumerable<module> moduleID = moduleConfigList.Where(t => t.sensorId.Equals(ID));
                //        objDataValue.var1 = Calculator(objDataValue.var1, moduleConfigList.Where(t => t.item_name.Equals("var1")));
                //        objDataValue.var2 = objMeasuredDataGlobal.var2;
                //        objDataValue.var3 = objMeasuredDataGlobal.var3;
                //        objDataValue.var4 = objMeasuredDataGlobal.var4;
                //        objDataValue.var5 = objMeasuredDataGlobal.var5;
                //        objDataValue.var6 = objMeasuredDataGlobal.var6;
                //    }
                //}

                checkAllCommunication();
                if (new data_value_repository().add(ref objDataValue) > 0)
                {
                    saved = true;
                }
                //// save to data value table
                if (saved)
                {
                    //Console.Write("add vao duoc data_value \n");
                    data_value objAdd5Minute = objCalCulationDataValue5Minute.addNewObjFor5Minute(objDataValue);
                    if (objAdd5Minute != null)
                    {
                        // add to 60 _minute data
                        objCalCulationDataValue60Minute.addNewObjFor60Minute(objAdd5Minute);
                        //Console.Write("objAdd5Minute tra ve khac null \n");
                    }
                    else
                    {
                        // do nothing
                        //Console.Write("objAdd5Minute tra ve null \n");
                    }
                }
                else
                {
                    //Console.Write("ko add vao duoc data_value \n");
                    // fail
                }

            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }
        private void tmrThreadingTimerForFTP_TimerCallback(object state)
        {
            try
            {
                setting_repository s = new setting_repository();
                int id = s.get_id_by_key("lasted_push");
                DateTime lastedPush = s.get_datetime_by_id(id);
                GlobalVar.stationSettings = new station_repository().get_info();

                /// Send File ftp	
                if (GlobalVar.stationSettings != null)
                {
                    if (GlobalVar.stationSettings.ftpflag == 1)
                    {
                        if (Application.OpenForms.OfType<Form1>().Count() == 1)
                        {
                            //Application.Exit(Application.OpenForms.OfType<Form1>().First());
                            //Application.OpenForms.OfType<Form1>().First().;
                            Form1.control1.ClearTextBox(Form1.control1.getForm1fromControl, 1);
                        }
                        
                        //protocol = new Form1(frmConfiguration.newMain);
                        if (ManualFTP(lastedPush, DateTime.Now))
                        {
                        }
                        //protocol.Show();
                    }
                    else
                    {
                        
                        if (Application.OpenForms.OfType<Form1>().Count() == 1)
                        {
                            Application.OpenForms.OfType<Form1>().First().Close();
                            Form1.control1.ClearTextBox(Form1.control1.getForm1fromControl, 1);
                        }

                        //protocol = new Form1(frmConfiguration.newMain);
                        //protocol.Show();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        private void tmrThreadingTimerFor60Minute_TimerCallback(object state)
        {
            if (is_close_form)
            {
                try
                {
                    this.Close();
                    //MessageBox.Show("123");
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(Environment.ExitCode);
                    }
                }
                catch
                {

                }
            }
        }
        public measured_data getsensordataTCE(out measured_data objRawdata)
        {
            //saved = false;
            measured_data obj = new measured_data();
            objRawdata = new measured_data();
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            FieldInfo[] field = typeof(measured_data).GetFields(bindingFlags);
            //string variableField = field[1].Name.Substring(field[1].Name.IndexOf("<")+1, field[1].Name.IndexOf(">")- field[1].Name.IndexOf("<")-1);
            //IEnumerable<module> moduleConfigList = new module[] { };

            using (module_repository _modules = new module_repository())
            {
                IEnumerable<module> moduleConfigList = _modules.get_all();
                try
                {
                    IPAddress localAddr = IPAddress.Parse("192.168.1.168");
                    Int32 port = 14111;
                    string data;
                    TcpClient client = new TcpClient();
                    client.Connect(new IPEndPoint(localAddr, 14111));
                    if (client.Connected)
                    {
                        byte[] sender = null;
                        byte[] buffer = new byte[2048];
                        byte[] buffer2 = new byte[1];
                        byte[] dataarray = null;
                        string[] words = null;
                        string[] variable = null;
                        string[] variableData = null;
                        NetworkStream nwStream = client.GetStream();
                        int i = nwStream.Read(buffer, 0, buffer.Length);
                        if (i != 0)
                        {
                            data = null;
                            data = Encoding.ASCII.GetString(buffer, 0, i);
                            String s = "getsensordata";
                            byte[] msg = _encoder.GetBytes(s);
                            byte[] _eot = new byte[] { 0x0A };
                            sender = msg.Concat(_eot).ToArray();
                            //sendByte(nwStream, sender);
                            nwStream.Write(sender, 0, sender.Length);
                            nwStream.Flush();
                            while ((i = nwStream.Read(buffer2, 0, buffer2.Length)) != 0)
                            {
                                if (buffer2 != null)
                                {
                                    if (dataarray == null) dataarray = buffer2;
                                    else dataarray = dataarray.Concat(buffer2).ToArray();
                                }
                                if (dataarray.Length > 3 && s.Equals("?"))
                                {
                                    if (dataarray[dataarray.Length - 1].Equals(0x0A) && dataarray[dataarray.Length - 2].Equals(0x0D) && dataarray[dataarray.Length - 3].Equals(0x6E))
                                    {
                                        break;
                                    }
                                }
                                if (dataarray.Length > 4 && s.Equals("getsensordata"))
                                {
                                    if (dataarray[dataarray.Length - 1].Equals(0x0A) && dataarray[dataarray.Length - 2].Equals(0x0D) && dataarray[dataarray.Length - 3].Equals(0x38) && dataarray[dataarray.Length - 4].Equals(0x36))
                                    {
                                        break;
                                    }
                                }
                            }
                            data = System.Text.Encoding.ASCII.GetString(dataarray, 0, dataarray.Length);

                            //
                            string[] separators = { "\t", "\n" };
                            words = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            //
                            //foreach (var word in words)
                            //{
                            //    Console.WriteLine(word);
                            //}
                            //Console.ReadLine();
                            //
                            string[] separatorsENDLINE = { "\n" };
                            variable = data.Split(separatorsENDLINE, StringSplitOptions.RemoveEmptyEntries);
                            variable = variable.Skip(1).ToArray();
                            //
                            //foreach (var word in variable)
                            //{
                            //    Console.WriteLine(word);
                            //}
                            //Console.ReadLine();
                        }
                        for (int k = 0; k < variable.Length; k++)
                        {
                            string[] separatorsTAB = { "\t" };
                            variableData = variable[k].Split(separatorsTAB, StringSplitOptions.RemoveEmptyEntries);
                            //Lay ID nhan duoc tu GO
                            string ID = variableData[0].Substring(19, variableData[0].Length - 19);
                            //Tim ID giong voi ID lay tu GO trong database
                            IEnumerable<module> moduleID = moduleConfigList.Where(t => t.sensorId.Equals(ID));

                            if (moduleID.Count() == 1)
                            {
                                string varNo = moduleID.First().item_name;
                                double doubleData = double.Parse(variableData[1].Substring(0, variableData[1].IndexOf("[")).Trim());

                                Type type = typeof(measured_data);
                                //object instance = Activator.CreateInstance(type);
                                //PropertyInfo prop = type.GetProperty(property);
                                //prop.SetValue(instance, value, null);
                                //Console.WriteLine(((Foo)instance).Bar);
                                for (int m = 0; m < field.Length; m++)
                                {
                                    string variableField = field[m].Name.Substring(field[m].Name.IndexOf("<") + 1, field[m].Name.IndexOf(">") - field[m].Name.IndexOf("<") - 1);
                                    if (varNo.Equals(variableField))
                                    {
                                        PropertyInfo prop = type.GetProperty(variableField);
                                        prop.SetValue(objRawdata, doubleData, null);
                                        doubleData = Calculator(doubleData, moduleID.First());
                                        prop.SetValue(obj, doubleData, null);
                                    }
                                }
                                //if (varNo.Equals("var1"))
                                //{
                                //    obj.var1 = doubleData;
                                ////Console.WriteLine("COD ---" + obj.var1);
                                //};
                                //if (varNo.Equals("var2"))
                                //{
                                //    obj.var2 = doubleData;
                                ////Console.WriteLine("Color ---" + obj.var2);
                                //};
                                //if (varNo.Equals("var3"))
                                //{
                                //    obj.var3 = doubleData;
                                ////Console.WriteLine("TSS ---" + obj.var3);
                                //};
                                //if (varNo.Equals("var4"))
                                //{
                                //    obj.var4 = doubleData;
                                ////Console.WriteLine("LL ---" + obj.var4);
                                //};
                                //if (varNo.Equals("var5"))
                                //{
                                //    obj.var5 = doubleData;
                                ////Console.WriteLine("Temp ---" + obj.var5);
                                //};
                                //if (varNo.Equals("var6"))
                                //{
                                //    obj.var6 = doubleData;
                                ////Console.WriteLine("pH ---" + obj.var6);
                                //};
                            }
                        }
                        //obj.var1 = double.Parse(words[19].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture); //COD
                        //obj.var2 = double.Parse(words[24].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture);  //Color
                        //obj.var3 = double.Parse(words[29].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture);  //TSS
                        ////29-34-39-45-49-
                        //obj.var4 = double.Parse(words[59].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture); //Luu Luong
                        //obj.var5 = double.Parse(words[71].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture); //Temp
                        //obj.var6 = double.Parse(words[77].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture); //pH
                        Console.WriteLine("COD ---" + obj.var1);
                        Console.WriteLine("Color ---" + obj.var2);
                        Console.WriteLine("TSS ---" + obj.var3);
                        Console.WriteLine("LL ---" + obj.var4);
                        Console.WriteLine("Temp ---" + obj.var5);
                        Console.WriteLine("pH ---" + obj.var6);
                        obj.MPS_status = 0;
                        obj.latest_update_MPS_communication = DateTime.Now;
                        objRawdata.MPS_status = 0;
                        objRawdata.latest_update_MPS_communication = DateTime.Now;

                        nwStream.Close();
                    }
                    else
                    {
                        obj.MPS_status = 5;
                        objRawdata.MPS_status = 5;
                    }
                    connectFlag = client.Connected;

                    client.Close();
                }
                catch (Exception ex)
                {
                    //Console.Write(ex.StackTrace);
                    objRawdata = new measured_data();
                }
            }
            return obj;
        }
        public measured_data getsensordata(out measured_data objRawdata)
        {
            //saved = false;
            measured_data obj = new measured_data();
            objRawdata = new measured_data();
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            FieldInfo[] field = typeof(measured_data).GetFields(bindingFlags);
            //string variableField = field[1].Name.Substring(field[1].Name.IndexOf("<")+1, field[1].Name.IndexOf(">")- field[1].Name.IndexOf("<")-1);
            //IEnumerable<module> moduleConfigList = new module[] { };

            using (module_repository _modules = new module_repository())
            {
                IEnumerable<module> moduleConfigList = _modules.get_all();
                try
                {
                    IPAddress localAddr = IPAddress.Parse("192.168.1.168");
                    Int32 port = 14111;
                    string data;
                    TcpClient client = new TcpClient();
                    client.Connect(new IPEndPoint(localAddr, 14111));
                    if (client.Connected)
                    {
                        byte[] sender = null;
                        byte[] buffer = new byte[2048];
                        byte[] buffer2 = new byte[1];
                        byte[] dataarray = null;
                        string[] words = null;
                        string[] variable = null;
                        string[] variableData = null;
                        NetworkStream nwStream = client.GetStream();
                        int i = nwStream.Read(buffer, 0, buffer.Length);
                        if (i != 0)
                        {
                            data = null;
                            data = Encoding.ASCII.GetString(buffer, 0, i);
                            String s = "getsensordata";
                            byte[] msg = _encoder.GetBytes(s);
                            byte[] _eot = new byte[] { 0x0A };
                            sender = msg.Concat(_eot).ToArray();
                            //sendByte(nwStream, sender);
                            nwStream.Write(sender, 0, sender.Length);
                            nwStream.Flush();
                            while ((i = nwStream.Read(buffer2, 0, buffer2.Length)) != 0)
                            {
                                if (buffer2 != null)
                                {
                                    if (dataarray == null) dataarray = buffer2;
                                    else dataarray = dataarray.Concat(buffer2).ToArray();
                                }
                                if (dataarray.Length > 3 && s.Equals("?"))
                                {
                                    if (dataarray[dataarray.Length - 1].Equals(0x0A) && dataarray[dataarray.Length - 2].Equals(0x0D) && dataarray[dataarray.Length - 3].Equals(0x6E))
                                    {
                                        break;
                                    }
                                }
                                if (dataarray.Length > 4 && s.Equals("getsensordata"))
                                {
                                    if (dataarray[dataarray.Length - 1].Equals(0x0A) && dataarray[dataarray.Length - 2].Equals(0x0D) && dataarray[dataarray.Length - 3].Equals(0x38) && dataarray[dataarray.Length - 4].Equals(0x36))
                                    {
                                        break;
                                    }
                                }
                            }
                            data = System.Text.Encoding.ASCII.GetString(dataarray, 0, dataarray.Length);
                            Console.WriteLine("data :" + data);
                            //
                            string[] separators = { "\t", "\n" };
                            //words = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            //
                            //foreach (var word in words)
                            //{
                            //    Console.WriteLine(word);
                            //}
                            //Console.ReadLine();
                            //
                            string[] separatorsENDLINE = { "\n" };
                            variable = data.Split(separatorsENDLINE, StringSplitOptions.RemoveEmptyEntries);
                            variable = variable.Skip(1).ToArray();
                            //
                            //for (int l = 0; l < variable.Length; l++)
                            //{
                            //    Console.WriteLine("+++" + l);
                            //    Console.WriteLine(variable[l]);
                            //}
                            //Console.ReadLine();
                        }
                        for (int k = 0; k < variable.Length; k++)
                        {
                            //Console.WriteLine("---" + k);
                            //Console.WriteLine(variable[k]);
                            string[] separatorsTAB = { "\t" };
                            variableData = variable[k].Split(separatorsTAB, StringSplitOptions.RemoveEmptyEntries);
                            //

                            //Lay ID nhan duoc tu GO
                            string ID = variableData[0].Substring(19, variableData[0].Length - 19);
                            //
                            //Console.WriteLine("ID :" + ID);
                            //Tim ID giong voi ID lay tu GO trong database
                            IEnumerable<module> moduleID = moduleConfigList.Where(t => t.sensorId.Equals(ID));

                            if (moduleID.Count() == 1)
                            {
                                string varNo = moduleID.First().item_name;
                                double doubleData = double.Parse(variableData[1].Substring(0, variableData[1].IndexOf("[")).Trim());
                                //Console.WriteLine("doubleData :" + doubleData);
                                Type type = typeof(measured_data);
                                //object instance = Activator.CreateInstance(type);
                                //PropertyInfo prop = type.GetProperty(property);
                                //prop.SetValue(instance, value, null);
                                //Console.WriteLine(((Foo)instance).Bar);
                                for (int m = 0; m < field.Length; m++)
                                {
                                    //Console.WriteLine("Field m :" + m + " : " + field[m].Name);
                                    string variableField = field[m].Name.Substring(field[m].Name.IndexOf("<") + 1, field[m].Name.IndexOf(">") - field[m].Name.IndexOf("<") - 1);
                                    //Console.WriteLine("Field m substring :" + m + " : " + variableField);
                                    if (varNo.Equals(variableField))
                                    {
                                        PropertyInfo prop = type.GetProperty(variableField);
                                        prop.SetValue(objRawdata, doubleData, null);

                                        //doubleData = Calculator(doubleData, moduleID.First());

                                        prop.SetValue(obj, doubleData, null);
                                    }
                                }
                                //if (varNo.Equals("var1"))
                                //{
                                //    obj.var1 = doubleData;
                                ////Console.WriteLine("COD ---" + obj.var1);
                                //};
                                //if (varNo.Equals("var2"))
                                //{
                                //    obj.var2 = doubleData;
                                ////Console.WriteLine("Color ---" + obj.var2);
                                //};
                                //if (varNo.Equals("var3"))
                                //{
                                //    obj.var3 = doubleData;
                                ////Console.WriteLine("TSS ---" + obj.var3);
                                //};
                                //if (varNo.Equals("var4"))
                                //{
                                //    obj.var4 = doubleData;
                                ////Console.WriteLine("LL ---" + obj.var4);
                                //};
                                //if (varNo.Equals("var5"))
                                //{
                                //    obj.var5 = doubleData;
                                ////Console.WriteLine("Temp ---" + obj.var5);
                                //};
                                //if (varNo.Equals("var6"))
                                //{
                                //    obj.var6 = doubleData;
                                ////Console.WriteLine("pH ---" + obj.var6);
                                //};
                            }
                        }
                        //obj.var1 = double.Parse(words[19].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture); //COD
                        //obj.var2 = double.Parse(words[24].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture);  //Color
                        //obj.var3 = double.Parse(words[29].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture);  //TSS
                        ////29-34-39-45-49-
                        //obj.var4 = double.Parse(words[59].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture); //Luu Luong
                        //obj.var5 = double.Parse(words[71].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture); //Temp
                        //obj.var6 = double.Parse(words[77].Substring(0, 6).Trim(), System.Globalization.CultureInfo.InvariantCulture); //pH
                        Console.WriteLine("COD ---" + obj.var1);
                        Console.WriteLine("Color ---" + obj.var2);
                        Console.WriteLine("TSS ---" + obj.var3);
                        Console.WriteLine("LL ---" + obj.var4);
                        Console.WriteLine("Temp ---" + obj.var5);
                        Console.WriteLine("pH ---" + obj.var6);
                        obj.MPS_status = 0;
                        obj.latest_update_MPS_communication = DateTime.Now;
                        objRawdata.MPS_status = 0;
                        objRawdata.latest_update_MPS_communication = DateTime.Now;

                        nwStream.Close();
                    }
                    else
                    {
                        obj.MPS_status = 5;
                        objRawdata.MPS_status = 5;
                    }
                    connectFlag = client.Connected;

                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.StackTrace);
                    objRawdata = new measured_data();
                }
            }
            return obj;
        }
        #endregion
        #region Utility
        private static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            try
            {
                foreach (byte b in data)
                    sb.Append(Convert.ToString(b, 16).PadLeft(2, '0') + "");
            }
            catch (Exception)
            {
                return "Error";
            }
            return sb.ToString().ToUpper();
        }
        private static Single ConvertHexToSingle(string hexVal)
        {
            try
            {
                int i = 0, j = 0;
                byte[] bArray = new byte[4];


                for (i = 0; i <= hexVal.Length - 1; i += 2)
                {
                    bArray[j] = Byte.Parse(hexVal[i].ToString() + hexVal[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    j += 1;
                }
                Array.Reverse(bArray);
                Single s = BitConverter.ToSingle(bArray, 0);
                return (s);
            }
            catch (Exception ex)
            {
                throw new FormatException("The supplied hex value is either empty or in an incorrect format. Use the " +
                "following format: 00000000", ex);
            }
        }
        public static byte[] SubArray(byte[] data, int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        private static byte[] Combine(byte[] first, int first_length, byte[] second)
        {
            byte[] ret = new byte[first_length + second.Length];
            try
            {
                Buffer.BlockCopy(first, 0, ret, 0, first_length);
                Buffer.BlockCopy(second, 0, ret, first_length, second.Length);
            }
            catch (Exception ex)
            {

            }

            return ret;
        }
        private static bool isSensorID(string[] input, string ID)
        {
            if (input[0].Substring(19, input[0].Length - 19).Equals(ID)) { return true; }
            else
            {
                return false;
            }
        }
        public static void sendByte(NetworkStream nwStream, byte[] msg)
        {
            try
            {
                nwStream.Write(msg, 0, msg.Length);
                nwStream.Flush();
                Console.WriteLine(" Sended: {0}", _encoder.GetString(msg));
            }
            catch
            {
                Console.WriteLine("SENDING: " + "\"" + _encoder.GetString(msg) + "\"" + " BUT");
                Console.WriteLine("ERROR : CAN NOT LISTEN ANY CONNECT, CHECK CONNECT IN CENTER.");
            }
        }
        private double Calculator(double D, module mod)
        {
            double A;
            try
            {           
                A = (Double)((mod.output_min - mod.output_max) / (mod.input_min - mod.input_max)) * (Double)(D - mod.input_min) + mod.output_min + mod.off_set;
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                return -1;
            }
            return A;
        }
        public void updateMeasuredDataValue(measured_data obj)
        {
            try
            {
                // check latest update communication
                if (DateTime.Compare(objMeasuredDataGlobal.latest_update_MPS_communication, DateTime.Now.AddSeconds(-PERIOD_CHECK_COMMUNICATION_ERROR)) < 0)
                {
                    objMeasuredDataGlobal.MPS_status = INT_STATUS_COMMUNICATION_ERROR;
                    obj.MPS_status = objMeasuredDataGlobal.MPS_status;
                    objMeasuredDataGlobal.var1 = -1;
                    objMeasuredDataGlobal.var2 = -1;
                    objMeasuredDataGlobal.var3 = -1;
                    objMeasuredDataGlobal.var4 = -1;
                    objMeasuredDataGlobal.var5 = -1;
                    objMeasuredDataGlobal.var6 = -1;
                }
                setTextMPS("----", txtMPSCondValue);
                setTextMPS("----", txtMPSpHValue);
                setTextMPS("----", txtMPSDOValue);
                setTextMPS("----", txtMPSTurbValue);
                setTextMPS("----", txtMPSORPValue);
                setTextMPS("----", txtMPSTempValue);
                if (obj.MPS_status != INT_STATUS_COMMUNICATION_ERROR &&
                    obj.MPS_status != INT_STATUS_INSTRUMENT_ERROR &&
                    obj.MPS_status != INT_STATUS_EMPTY_SAMPLER_RESERVOIR)
                {
                    if (obj.var1 > -1)
                    {
                        setTextMPS(obj.var1.ToString("##0.00"), txtMPSCondValue);
                        //txtMPSCondValue.Text = obj.var1.ToString("##0.00");
                    }
                    else
                    {
                        setTextMPS("--", txtMPSCondValue);
                        //txtMPSCondValue.Text = "--";
                    }
                    if (obj.var2 > -1)
                    {
                        setTextMPS(obj.var2.ToString("##0.00"), txtMPSpHValue);
                        //txtMPSpHValue.Text = obj.var2.ToString("##0.00");
                    }
                    else
                    {
                        setTextMPS("--", txtMPSpHValue);
                        //txtMPSpHValue.Text = "--";
                    }
                    if (obj.var3 > -1)
                    {
                        setTextMPS(obj.var3.ToString("##0.00"), txtMPSDOValue);
                        //txtMPSDOValue.Text = obj.var3.ToString("##0.00");
                    }
                    else
                    {
                        setTextMPS("--", txtMPSDOValue);
                        //txtMPSDOValue.Text = "--";
                    }
                    if (obj.var4 > -1)
                    {
                        setTextMPS(obj.var4.ToString("##0.00"), txtMPSTurbValue);
                        //txtMPSTurbValue.Text = obj.var4.ToString("##0.00");
                    }
                    else
                    {
                        setTextMPS("--", txtMPSTurbValue);
                        //txtMPSTurbValue.Text = "--";
                    }
                    if (obj.var5 > -1)
                    {
                        setTextMPS(obj.var5.ToString("##0.00"), txtMPSORPValue);
                        //txtMPSORPValue.Text = obj.var5.ToString("##0.00");
                    }
                    else
                    {
                        setTextMPS("--", txtMPSORPValue);
                        //txtMPSORPValue.Text = "--";
                    }
                    if (obj.var6 > -1)
                    {
                        setTextMPS(obj.var6.ToString("##0.00"), txtMPSTempValue);
                        //txtMPSTempValue.Text = obj.var6.ToString("##0.00");
                    }
                    else
                    {
                        setTextMPS("--", txtMPSTempValue);
                        //txtMPSTempValue.Text = "--";
                    }
                }
                switch (obj.MPS_status)
                {
                    case INT_STATUS_COMMUNICATION_ERROR:
                        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Communication_Fault_status;
                        break;
                    case INT_STATUS_INSTRUMENT_ERROR:
                        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                        break;
                    case INT_STATUS_MAINTENANCE:
                        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Maintenance_status;
                        break;
                    case INT_STATUS_NORMAL:
                        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
                        break;
                    case INT_STATUS_MEASURING_STOP:
                        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                        break;
                    case INT_STATUS_CALIBRATING:
                        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Calibration_status;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void dataCSV(string firts, data_value data, string path, string date)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    var csv = new StringBuilder();
                    csv.Append(firts + "\t" + "");
                    csv.AppendLine();
                    //String connstring = "Server = localhost;Port = 5432; User Id = postgres;Password = 123;Database = DataLoggerDB";

                    if (db.open_connection())
                    {
                        string sql_command1 = "SELECT * from " + "databinding";
                        string sql_command2 = "SELECT * from " + "modules";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command1;
                            NpgsqlDataReader dr;
                            dr = cmd.ExecuteReader();
                            DataTable tbcode = new DataTable();
                            tbcode.Load(dr); // Load bang chua mapping cac truong

                            cmd.CommandText = sql_command2;
                            NpgsqlDataReader dr2;
                            dr2 = cmd.ExecuteReader();
                            DataTable tbcode2 = new DataTable();
                            tbcode2.Load(dr2); // Load bang chua mapping cac truong

                            var tableEnumerable = tbcode2.AsEnumerable();
                            var _displayList = tableEnumerable.ToArray();

                            foreach (DataRow row in tbcode.Rows)
                            {
                                string code = Convert.ToString(row["clnnamevalue"]);
                                int min_value = Convert.ToInt32(row["min_value"]);
                                for (int i = 0; i < _displayList.Count(); i++)
                                {
                                    DataRow currentRow = _displayList[i];
                                    if (currentRow["item_name"].Equals(code))
                                    {
                                        string display_name = currentRow["item_name"].ToString();
                                        Type _display_nameType = typeof(data_value);
                                        //var display_nameType = Activator.CreateInstance(_display_nameType);
                                        PropertyInfo prop = _display_nameType.GetProperty(display_name);
                                        double display_var;

                                        display_var = (double)prop.GetValue(data);

                                        if (Convert.ToDouble(String.Format("{0:0.00}", display_var)) >= min_value)
                                        {
                                            csv.Append(date + "\t" + currentRow["display_name"].ToString() + "\t" + String.Format("{0:0.00}", display_var) + "\t" + currentRow["unit"].ToString());
                                            csv.AppendLine();
                                        }

                                    }
                                }

                            }
                            using (StreamWriter swriter = new StreamWriter(path))
                            {
                                swriter.Write(csv.ToString());
                            }
                            db.close_connection();
                        }
                    }
                    else
                    {
                        db.close_connection();
                    }
                }
                catch (Exception e)
                {
                    db.close_connection();
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        public Boolean FTP(data_value data, DateTime datetime)
        {
            try
            {
                GlobalVar.stationSettings = new station_repository().get_info();
                string stationID = GlobalVar.stationSettings.station_id;
                string stationName = GlobalVar.stationSettings.station_name;
                string server = GlobalVar.stationSettings.ftpserver;
                string username = GlobalVar.stationSettings.ftpusername;
                string password = GlobalVar.stationSettings.ftppassword;
                string folder = GlobalVar.stationSettings.ftpfolder;
                String datetimeS = datetime.ToString("yyyyMMddHHmmss");
                string date = datetimeS.Substring(0, 4) + datetimeS.Substring(4, 2) + datetimeS.Substring(6, 2) + datetimeS.Substring(8, 2) + datetimeS.Substring(10, 2) + datetimeS.Substring(12, 2);
                //server = " \@" " + server + "\"" ;
                //ftp ftpClient = new ftp( @"ftp://127.0.0.1/", username, password);
                ftp ftpClient = new ftp(server, username, password);

                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                string csv = "push";

                //string tempFileName = "push.txt";
                string newFileName = stationID + "_" + stationName + "_" + date + ".txt";

                string yearFolder = datetimeS.Substring(0, 4);
                string monthFolder = datetimeS.Substring(4, 2);
                string dayFolder = datetimeS.Substring(6, 2);

                //string tempFilePath = Path.Combine(appPath, dataFolderName, tempFileName);
                string newFolderPath = Path.Combine(appPath, csv);
                string newFilePath = Path.Combine(appPath, csv, newFileName);

                /// Year Folder
                string[] simpleDirectoryYear = ftpClient.directoryListSimple(folder);
                Boolean hasFolderY = false;
                for (int i = 0; i < simpleDirectoryYear.Count(); i++)
                {
                    if (simpleDirectoryYear[i].Equals(yearFolder))
                    {
                        hasFolderY = true;
                    }
                }

                string folderPathY;
                if (hasFolderY == false)
                {
                    folderPathY = Path.Combine(folder, yearFolder);
                    ftpClient.createDirectory(folderPathY);
                }
                else
                {
                    folderPathY = Path.Combine(folder, yearFolder);
                }
                ///
                /// Month Folder
                string[] simpleDirectoryMonth = ftpClient.directoryListSimple(folderPathY);
                Boolean hasFolderM = false;
                for (int i = 0; i < simpleDirectoryYear.Count(); i++)
                {
                    if (simpleDirectoryYear[i].Equals(monthFolder))
                    {
                        hasFolderM = true;
                    }
                }

                string folderPathM;
                if (hasFolderM == false)
                {
                    folderPathM = Path.Combine(folderPathY, monthFolder);
                    ftpClient.createDirectory(folderPathM);
                }
                else
                {
                    folderPathM = Path.Combine(folderPathY, monthFolder);
                }
                /// 
                /// Day Folder
                string[] simpleDirectoryDay = ftpClient.directoryListSimple(folderPathM);
                Boolean hasFolderD = false;
                for (int i = 0; i < simpleDirectoryYear.Count(); i++)
                {
                    if (simpleDirectoryYear[i].Equals(dayFolder))
                    {
                        hasFolderD = true;
                    }
                }

                string folderPathD;
                if (hasFolderD == false)
                {
                    folderPathD = Path.Combine(folderPathM, dayFolder);
                    ftpClient.createDirectory(folderPathD);
                }
                else
                {
                    folderPathD = Path.Combine(folderPathM, dayFolder);
                }
                /// 
                if (!Directory.Exists(newFolderPath))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(newFolderPath);
                }
                string header = stationID + "_" + stationName;
                if (!File.Exists(newFilePath))
                {
                    File.Create(newFilePath).Close();
                    dataCSV(header, data, newFilePath, date);
                }
                else
                {
                    System.IO.File.WriteAllText(newFilePath, string.Empty);
                    dataCSV(header, data, newFilePath, date);
                }
                /* Upload a File */
                //ftpClient.upload("/test/2017/data_report.csv", @"C:\Users\Admin\Desktop\data_report.csv");
                string filePath = Path.Combine(folderPathD, newFileName);
                ftpClient.upload(filePath, newFilePath);
                Form1.control1.AppendTextBox("Manual/Success " + newFileName + Environment.NewLine, Form1.control1.getForm1fromControl, 1);
                return true;
            }
            catch (Exception e)
            {
                Form1.control1.AppendTextBox("Manual/Error " + Environment.NewLine, Form1.control1.getForm1fromControl, 1);
                return false;
            }
        }
        public Boolean FTP(data_value data)
        {
            try
            {
                GlobalVar.stationSettings = new station_repository().get_info();
                string stationID = GlobalVar.stationSettings.station_id;
                string stationName = GlobalVar.stationSettings.station_name;
                string server = GlobalVar.stationSettings.ftpserver;
                string username = GlobalVar.stationSettings.ftpusername;
                string password = GlobalVar.stationSettings.ftppassword;
                string folder = GlobalVar.stationSettings.ftpfolder;
                DateTime s = DateTime.Now;
                String datetimeS = s.ToString("yyyyMMddHHmmss");
                string date = datetimeS.Substring(0, 4) + datetimeS.Substring(4, 2) + datetimeS.Substring(6, 2) + datetimeS.Substring(8, 2) + datetimeS.Substring(10, 2) + datetimeS.Substring(12, 2);
                //server = " \@" " + server + "\"" ;
                //ftp ftpClient = new ftp( @"ftp://127.0.0.1/", username, password);
                ftp ftpClient = new ftp(server, username, password);

                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                string csv = "push";

                //string tempFileName = "push.txt";
                string newFileName = stationID + "_" + stationName + "_" + date + ".txt";

                string yearFolder = datetimeS.Substring(0, 4);
                string monthFolder = datetimeS.Substring(4, 2);
                string dayFolder = datetimeS.Substring(6, 2);

                //string tempFilePath = Path.Combine(appPath, dataFolderName, tempFileName);
                string newFolderPath = Path.Combine(appPath, csv);
                string newFilePath = Path.Combine(appPath, csv, newFileName);

                /// Year Folder
                string[] simpleDirectoryYear = ftpClient.directoryListSimple(folder);
                Boolean hasFolderY = false;
                for (int i = 0; i < simpleDirectoryYear.Count(); i++)
                {
                    if (simpleDirectoryYear[i].Equals(yearFolder))
                    {
                        hasFolderY = true;
                    }
                }

                string folderPathY;
                if (hasFolderY == false)
                {
                    folderPathY = Path.Combine(folder, yearFolder);
                    ftpClient.createDirectory(folderPathY);
                }
                else
                {
                    folderPathY = Path.Combine(folder, yearFolder);
                }
                ///
                /// Month Folder
                string[] simpleDirectoryMonth = ftpClient.directoryListSimple(folderPathY);
                Boolean hasFolderM = false;
                for (int i = 0; i < simpleDirectoryYear.Count(); i++)
                {
                    if (simpleDirectoryYear[i].Equals(monthFolder))
                    {
                        hasFolderM = true;
                    }
                }

                string folderPathM;
                if (hasFolderM == false)
                {
                    folderPathM = Path.Combine(folderPathY, monthFolder);
                    ftpClient.createDirectory(folderPathM);
                }
                else
                {
                    folderPathM = Path.Combine(folderPathY, monthFolder);
                }
                /// 
                /// Day Folder
                string[] simpleDirectoryDay = ftpClient.directoryListSimple(folderPathM);
                Boolean hasFolderD = false;
                for (int i = 0; i < simpleDirectoryYear.Count(); i++)
                {
                    if (simpleDirectoryYear[i].Equals(dayFolder))
                    {
                        hasFolderD = true;
                    }
                }

                string folderPathD;
                if (hasFolderD == false)
                {
                    folderPathD = Path.Combine(folderPathM, dayFolder);
                    ftpClient.createDirectory(folderPathD);
                }
                else
                {
                    folderPathD = Path.Combine(folderPathM, dayFolder);
                }
                /// 
                if (!Directory.Exists(newFolderPath))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(newFolderPath);
                }
                string header = stationID + "_" + stationName;
                if (!File.Exists(newFilePath))
                {
                    File.Create(newFilePath).Close();
                    dataCSV(header, data, newFilePath, date);
                }
                else
                {
                    System.IO.File.WriteAllText(newFilePath, string.Empty);
                    dataCSV(header, data, newFilePath, date);
                }
                /* Upload a File */
                //ftpClient.upload("/test/2017/data_report.csv", @"C:\Users\Admin\Desktop\data_report.csv");
                string filePath = Path.Combine(folderPathD, newFileName);
                ftpClient.upload(filePath, newFilePath);
                Form1.control1.AppendTextBox("Auto/Success " + newFileName + Environment.NewLine, Form1.control1.getForm1fromControl, 1);
                return true;
            }
            catch (Exception e)
            {
                Form1.control1.AppendTextBox("Auto/Error" + Environment.NewLine, Form1.control1.getForm1fromControl, 1);
                return false;
            }
        }
        public Boolean ManualFTP(DateTime dtpDateFrom, DateTime dtpDateTo)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command1 = "SELECT * from " + "databinding";
                        string sql_command2 = "SELECT * from " + "modules";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command1;
                            NpgsqlDataReader dr;
                            dr = cmd.ExecuteReader();
                            DataTable tbcode = new DataTable();
                            tbcode.Load(dr); // Load bang chua mapping cac truong

                            cmd.CommandText = sql_command2;
                            NpgsqlDataReader dr2;
                            dr2 = cmd.ExecuteReader();
                            DataTable tbcode2 = new DataTable();
                            tbcode2.Load(dr2); // Load bang chua mapping cac truong

                            List<string> _paramListForQuery = new List<string>();
                            //List<string> _codeListForQuery = new List<string>();
                            List<string> _minListForQuery = new List<string>();

                            var tableEnumerable = tbcode2.AsEnumerable();
                            var _codeListForQuery = tableEnumerable.ToArray();

                            foreach (DataRow row2 in tbcode.Rows)
                            {
                                //string code = Convert.ToString(row2["code"]);
                                //_codeListForQuery.Add(code);
                                string clnnamevalue = Convert.ToString(row2["clnnamevalue"]);
                                _paramListForQuery.Add(clnnamevalue);
                                string min_value = Convert.ToString(row2["min_value"]);
                                _minListForQuery.Add(min_value);
                            }

                            //_codeListForQuery.ToArray();
                            _paramListForQuery.ToArray();
                            DataTable dt_source = null;
                            dt_source = db5m.get_all_custom(dtpDateFrom, dtpDateTo, _paramListForQuery);
                            foreach (DataRow row3 in dt_source.Rows)
                            {
                                frmNewMain newmain = new frmNewMain();
                                data_value data = new data_value();


                                int id = Int32.Parse(Convert.ToString(row3["id"]));
                                DateTime created = (DateTime)row3["created"];
                                data.created = created;
                                for (int i = 0; i < _paramListForQuery.Count; i++)
                                {
                                    Type type = typeof(data_value);
                                    PropertyInfo prop = type.GetProperty(_paramListForQuery[i]);
                                    
                                    var variable = Convert.ToDouble(String.Format("{0:0.00}", row3[_paramListForQuery[i]]));

                                    prop.SetValue(data, variable, null);
                                    //string code = Convert.ToString(row3[_valueListForQuery[i]]);
                                    //foreach (var codeListForQuery in _codeListForQuery)
                                    //{
                                    //    DataRow currentRow = codeListForQuery;
                                    //    if (currentRow["item_name"].Equals("var1"))
                                    //    {
                                    //        data.var1 = variable;
                                    //        break;
                                    //    }
                                    //    if (currentRow["item_name"].Equals("var2"))
                                    //    {
                                    //        data.var2 = variable;
                                    //    }
                                    //    if (currentRow["item_name"].Equals("var3"))
                                    //    {
                                    //        data.var3 = variable;
                                    //    }
                                    //    if (currentRow["item_name"].Equals("var4"))
                                    //    {
                                    //        data.var4 = variable;
                                    //    }
                                    //    if (currentRow["item_name"].Equals("var5"))
                                    //    {
                                    //        data.var5 = variable;
                                    //    }
                                    //    if (currentRow["item_name"].Equals("var6"))
                                    //    {
                                    //        data.var6 = variable;
                                    //    }
                                    //}
                                }
                                if (FTP(data, created))
                                {
                                    db5m.updatePush(id, 1, DateTime.Now);
                                    //control1.AppendTextLog1Box();
                                    setting_repository s = new setting_repository();
                                    int idLasted = s.get_id_by_key("lasted_push");
                                    setting set = new setting();
                                    set.setting_key = "lasted_push";
                                    set.setting_type = "";
                                    set.setting_value = "";
                                    set.note = "";
                                    set.setting_datetime = data.created;
                                    //int id = setre.get_id_by_key("lasted_push");			
                                    s.update_with_id(ref set, idLasted);
                                }
                                else
                                {
                                    db5m.updatePush(id, 0, DateTime.Now);
                                }

                            }
                        }
                        Form1.control1.AppendTextBox("Lasted/Success " + "END" + Environment.NewLine, Form1.control1.getForm1fromControl, 1);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return false;
                }
            }
        }
        #endregion
        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
                if (!GlobalVar.isLogin)
                {
                    MessageBox.Show(lang.getText("login_before_to_do_this"));
                    return;
                }
            }
            frmConfiguration frmConfig = new frmConfiguration(lang,this);
            frmConfig.ShowDialog();
            initConfig(true);
        }
        private void frmNewMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                is_close_form = true;
                data_value obj = calculateImmediately5Minute();
                data_value obj60min = calculateImmediately60Minute();
                if (tmrThreadingTimer != null)
                {
                    tmrThreadingTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                    tmrThreadingTimer.Dispose();
                }

                if (tmrThreadingTimerStationStatus != null)
                {
                    tmrThreadingTimerStationStatus.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                    tmrThreadingTimerStationStatus.Dispose();
                }

                Process.GetCurrentProcess().Kill();

                //MessageBox.Show("123");
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(Environment.ExitCode);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Process.GetCurrentProcess().Kill();
            }
        }
        private data_value calculateImmediately5Minute()
        {
            data_value obj = objCalCulationDataValue5Minute.addNewObjFor5Minute(null, true);
            //data_value obj = null;
            if (obj == null)
            {
                obj = new data_5minute_value_repository().get_latest_info();
            }
            return obj;
        }
        private void btnMPS5Minute_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately5Minute();
            frm5MinuteMPS frm = new frm5MinuteMPS(obj, lang);
            frm.ShowDialog();
        }
        private data_value calculateImmediately60Minute()
        {
            data_value obj = objCalCulationDataValue5Minute.addNewObjFor60Minute(null, true);

            if (obj == null)
            {
                obj = new data_60minute_value_repository().get_latest_info();
            }
            return obj;
        }
        private void btnMPS1Hour_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately60Minute();
            frm1HourMPS frm = new frm1HourMPS(obj, lang);
            frm.ShowDialog();
        }
        private void btnMPSHistoryData_Click(object sender, EventArgs e)
        {
            frmHistoryMPS frm = new frmHistoryMPS(lang);
            frm.ShowDialog();
        }
        private void btnAllHistory_Click(object sender, EventArgs e)
        {
            frmHistoryAll frm = new frmHistoryAll(lang);
            frm.ShowDialog();
        }
        private void checkAllCommunication()
        {
            updateMeasuredDataValue(objMeasuredDataGlobal);
        }
        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
                if (!GlobalVar.isLogin)
                {
                    MessageBox.Show(lang.getText("login_before_to_do_this"));
                    return;
                }
            }
            frmMaintenance objMaintenance = new frmMaintenance(lang);
            //this.Hide();
            objMaintenance.ShowDialog();
            //this.Show();
        }
        private void btnUsers_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {
                frmUserManagement frmUM = new frmUserManagement(lang);
                frmUM.ShowDialog();
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }

        }
        private void btnLoginLogout_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {
                this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.logout;

                GlobalVar.isLogin = false;
                GlobalVar.loginUser = null;
            }
            else
            {
                this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.login;
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();

                if (GlobalVar.isLogin)
                {
                    this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.logout;
                }
            }
        }
        private void settingForLoginStatus()
        {
            if (GlobalVar.isLogin)
            {
                this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.logout;
                setTextHeadingLogin("" + lang.getText("main_menu_welcome") + ", " + GlobalVar.loginUser.user_name + " !");
            }
            else
            {
                this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.login;
                setTextHeadingLogin("" + lang.getText("main_menu_welcome") + ", " + lang.getText("main_menu_guest") + " !");
            }
        }
        private void btnMonthlyReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(lang.getText("monthly_report_yesno_question"), lang.getText("confirm"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                btnMonthlyReport.Enabled = false;
                vprgMonthlyReport.Value = 0;
                vprgMonthlyReport.Visible = true;

                bgwMonthlyReport.RunWorkerAsync();

                //Console.Write("1");
            }
        }
        private void btnLanguage_Click(object sender, EventArgs e)
        {
            switch_language();
            initConfig(false);
        }
        #region backgroundWorkerMonthlyReport
        private void backgroundWorkerMonthlyReport_DoWork(object sender, DoWorkEventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string dataFolderName = "data";

            string tempFileName = "monthly_report_template.xlsx";
            string newFileName = "MonthlyReport_" + DateTime.Now.ToString("yyyy (MMddHHmmssfff)");

            string tempFilePath = Path.Combine(appPath, dataFolderName, tempFileName);
            string newFilePath = Path.Combine(appPath, dataFolderName, newFileName);

            if (File.Exists(tempFilePath))
            {
                int year = DateTime.Now.Year;
                double dayOfYearTotal = (new DateTime(year, 12, 31)).DayOfYear;
                double dayOfYear = 0;
                int percent = 0;

                IEnumerable<data_value> allData = db60m.get_all_for_monthly_report(year);

                if (allData != null)
                {
                    Excel.XLWorkbook oExcelWorkbook = new Excel.XLWorkbook(tempFilePath);
                    // Excel.Application oExcelApp = new Excel.Application();
                    // Excel.Workbook oExcelWorkbook = oExcelApp.Workbooks.Open(tempFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    const int startRow = 5;
                    int row;

                    List<MonthlyReportInfo> mps_ph = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_orp = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_do = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_turbidity = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_ec = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_temp = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> tn = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> tp = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> toc = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> refrigeration_temperature = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> bottle_position = new List<MonthlyReportInfo>();

                    for (int month = 1; month <= 12; month++)
                    {
                        Excel.IXLWorksheet oExcelWorksheet = oExcelWorkbook.Worksheet(month) as Excel.IXLWorksheet;
                        // Excel.IXLWorkSheet oExcelWorksheet = oExcelWorkbook.Worksheets[month] as Excel.Worksheet;

                        //rename the Sheet name
                        oExcelWorksheet.Name = (new DateTime(year, month, 1)).ToString("MMM-yy");
                        oExcelWorksheet.Cell(2, 1).Value = "'" + (new DateTime(year, month, 1)).ToString("MM.");
                        oExcelWorksheet.Cell(2, 17).Value = (new DateTime(year, month, 1)).ToString("MMM-yy");

                        // calculate average value
                        for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                        {
                            // get maintenance by date (year, month, day)
                            string strDate = year + "-" + month + "-" + day;
                            IEnumerable<maintenance_log> onDateMaintenanceLogs = _maintenance_logs.get_all_by_date(strDate);
                            // prepare data for maintenance
                            string maintenance_operator_name = "";
                            string maintenance_start_time = "";
                            string maintenance_end_time = "";
                            string maintenance_equipments = "";

                            Color maintenance_color = StatusColorInfo.COL_STATUS_MAINTENANCE_PERIODIC;
                            if (onDateMaintenanceLogs != null && onDateMaintenanceLogs.Count() > 0)
                            {
                                foreach (maintenance_log itemMaintenanceLog in onDateMaintenanceLogs)
                                {
                                    maintenance_operator_name += itemMaintenanceLog.name + ";";
                                    maintenance_start_time += itemMaintenanceLog.start_time.ToString("HH")
                                                                + "h" + itemMaintenanceLog.start_time.ToString("mm") + ";";
                                    maintenance_end_time += itemMaintenanceLog.end_time.ToString("HH")
                                                                + "h" + itemMaintenanceLog.end_time.ToString("mm") + ";";
                                    if (itemMaintenanceLog.tn == 1)
                                    {
                                        maintenance_equipments += "TN;";
                                    }
                                    if (itemMaintenanceLog.tp == 1)
                                    {
                                        maintenance_equipments += "TP;";
                                    }
                                    if (itemMaintenanceLog.toc == 1)
                                    {
                                        maintenance_equipments += "TOC;";
                                    }
                                    if (itemMaintenanceLog.mps == 1)
                                    {
                                        maintenance_equipments += "MPS;";
                                    }
                                    if (itemMaintenanceLog.pumping_system == 1)
                                    {
                                        maintenance_equipments += "Pumping;";
                                    }
                                    if (itemMaintenanceLog.auto_sampler == 1)
                                    {
                                        maintenance_equipments += "AutoSampler;";
                                    }
                                    if (itemMaintenanceLog.other == 1)
                                    {
                                        maintenance_equipments += itemMaintenanceLog.other_para + ";";
                                    }
                                    if (itemMaintenanceLog.maintenance_reason == 1)
                                    {
                                        maintenance_color = StatusColorInfo.COL_STATUS_MAINTENANCE_INCIDENT;
                                    }
                                }
                                maintenance_operator_name = maintenance_operator_name.Substring(0, maintenance_operator_name.Length - 1);
                                maintenance_start_time = maintenance_start_time.Substring(0, maintenance_start_time.Length - 1);
                                maintenance_end_time = maintenance_end_time.Substring(0, maintenance_end_time.Length - 1);
                                try
                                {
                                    maintenance_equipments = maintenance_equipments.Substring(0, maintenance_equipments.Length - 1);
                                }
                                catch { }
                            }

                            IEnumerable<data_value> dayData = allData.Where(t => t.stored_date.Month == month && t.stored_date.Day == day);
                            mps_ph.Clear();
                            mps_orp.Clear();
                            mps_do.Clear();
                            mps_turbidity.Clear();
                            mps_ec.Clear();
                            mps_temp.Clear();
                            tn.Clear();
                            tp.Clear();
                            toc.Clear();
                            refrigeration_temperature.Clear();
                            bottle_position.Clear();
                            foreach (data_value item in dayData)
                            {
                                mps_ph.AddNewDataValue(item.var1_status, item.var1);
                                mps_orp.AddNewDataValue(item.var2_status, item.var2);
                                mps_do.AddNewDataValue(item.var3_status, item.var3);
                                mps_turbidity.AddNewDataValue(item.var4_status, item.var4);
                                mps_ec.AddNewDataValue(item.var5_status, item.var5);
                                mps_temp.AddNewDataValue(item.var6_status, item.var6);
                                //tn.AddNewDataValue(item.TN_status, item.TN);
                                //tp.AddNewDataValue(item.TP_status, item.TP);
                                //toc.AddNewDataValue(item.TOC_status, item.TOC);
                                //refrigeration_temperature.AddNewDataValue(0, item.refrigeration_temperature);
                                //bottle_position.AddNewDataValue(0, item.bottle_position);
                            }

                            // update to excel worksheet
                            row = startRow + day;

                            oExcelWorksheet.Cell(row, 2).Value = mps_ph.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 3).Value = mps_orp.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 4).Value = mps_do.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 5).Value = mps_turbidity.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 6).Value = mps_ec.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 7).Value = mps_temp.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 8).Value = tn.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 9).Value = tp.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 10).Value = toc.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 11).Value = refrigeration_temperature.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 12).Value = bottle_position.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 14).Value = maintenance_operator_name;
                            oExcelWorksheet.Cell(row, 15).Value = maintenance_start_time;
                            oExcelWorksheet.Cell(row, 16).Value = maintenance_end_time;
                            oExcelWorksheet.Cell(row, 17).Value = maintenance_equipments;


                            oExcelWorksheet.Range("b" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_ph.GetStatusColor()));
                            oExcelWorksheet.Range("c" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_orp.GetStatusColor()));
                            oExcelWorksheet.Range("d" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_do.GetStatusColor()));
                            oExcelWorksheet.Range("e" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_turbidity.GetStatusColor()));
                            oExcelWorksheet.Range("f" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_ec.GetStatusColor()));
                            oExcelWorksheet.Range("g" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_temp.GetStatusColor()));
                            oExcelWorksheet.Range("h" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(tn.GetStatusColor()));
                            oExcelWorksheet.Range("i" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(tp.GetStatusColor()));
                            oExcelWorksheet.Range("j" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(toc.GetStatusColor()));
                            oExcelWorksheet.Range("k" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(refrigeration_temperature.GetStatusColor()));
                            oExcelWorksheet.Range("l" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(bottle_position.GetStatusColor()));

                            oExcelWorksheet.Range("n" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(maintenance_color));
                            oExcelWorksheet.Range("o" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(maintenance_color));
                            oExcelWorksheet.Range("p" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(maintenance_color));
                            oExcelWorksheet.Range("q" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(maintenance_color));

                            dayOfYear = (new DateTime(year, month, day)).DayOfYear;
                            percent = (int)(dayOfYear * 100d / dayOfYearTotal);
                            bgwMonthlyReport.ReportProgress(percent);

                            //Thread.Sleep(1);
                        }
                    }
                    oExcelWorkbook.SaveAs(newFilePath + ".xlsx");
                    //oExcelWorkbook.SaveAs(newFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
            }
            FileInfo fi = new FileInfo(newFilePath + ".xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(newFilePath + ".xlsx");
            }
            else
            {
                //file doesn't exist
            }
        }
        private void backgroundWorkerMonthlyReport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            vprgMonthlyReport.Value = e.ProgressPercentage;
        }
        private void backgroundWorkerMonthlyReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnMonthlyReport.Enabled = true;
            vprgMonthlyReport.Visible = false;

            if (!e.Cancelled && e.Error == null)
            {
                MessageBox.Show(lang.getText("successfully"));
            }
            else
            {

            }
        }
        #endregion backgroundWorkerMonthlyReport
        public void ClearLabel(System.Windows.Forms.Control control, string text, string label)
        {
            if (control is Label)
            {
                Label lbl = (Label)control;
                if (lbl.Name.StartsWith(label))
                    lbl.Text = text;

            }
            else
                foreach (System.Windows.Forms.Control child in control.Controls)
                {
                    ClearLabel(child, text, label);
                }

        }
    }
    public class CalculationDataValue
    {
        public List<data_value> listDataValue = new List<data_value>();
        public int hour { get; set; }
        public int min_minute { get; set; } // start time
        public int max_minute { get; set; } // end time
        public DateTime latestCalculate5Minute = DateTime.Now;
        public DateTime latestCalculate60Minute = DateTime.Now;

        public CalculationDataValue()
        {
            hour = DateTime.Now.Hour;
            min_minute = DateTime.Now.Minute;
            max_minute = DateTime.Now.Minute;
        }
        public data_value addNewObjFor5Minute(data_value obj, bool isImmediatelyCalculation = false)
        {
            try
            {
                // checking execute transaction
                int tempHour = 0;
                if (obj != null)
                {
                    tempHour = obj.created.Hour;
                }
                else
                {
                    tempHour = DateTime.Now.Hour;

                    if (DateTime.Compare(latestCalculate5Minute, DateTime.Now.AddSeconds(-40)) < 0)
                    {
                        return null;
                    }
                }

                int tempMinute = 0;
                if (obj != null)
                {
                    tempMinute = obj.created.Minute;
                }
                else
                {
                    tempMinute = DateTime.Now.Minute;
                }

                data_value objDataValue = null;
                data_value objLatest = null;
                int status = 0;

                if (listDataValue.Count > 0)
                {
                    if ((tempHour != hour) || ((tempMinute - min_minute) > 1) || isImmediatelyCalculation)
                    {
                        if (tempMinute % 5 == 0 || isImmediatelyCalculation)
                        {
                            // ok
                            // calculate and add to database
                            objDataValue = new data_value();
                            // MPS
                            objDataValue.var1 = listDataValue[0].var1;
                            objDataValue.var1_status = listDataValue[0].MPS_status;
                            objDataValue.var2 = listDataValue[0].var2;
                            objDataValue.var2_status = listDataValue[0].MPS_status;
                            objDataValue.var3 = listDataValue[0].var3;
                            objDataValue.var3_status = listDataValue[0].MPS_status;
                            objDataValue.var4 = listDataValue[0].var4;
                            objDataValue.var4_status = listDataValue[0].MPS_status;
                            objDataValue.var5 = listDataValue[0].var5;
                            objDataValue.var5_status = listDataValue[0].MPS_status;
                            objDataValue.var6 = listDataValue[0].var6;
                            objDataValue.var6_status = listDataValue[0].MPS_status;
                            objDataValue.MPS_status = listDataValue[0].MPS_status;
                            // Time
                            objDataValue.stored_date = listDataValue[0].stored_date;
                            objDataValue.stored_hour = hour;
                            //objDataValue.stored_minute = (min_minute / 5) * 5;
                            //objDataValue.stored_minute = min_minute;
                            objDataValue.stored_minute = tempMinute;
                            int count = listDataValue.Count;

                            bool updateMPSFlag = true;
                            int countingMPSCal = 1;

                            for (int i = 0; i <= count - 1; i++)
                            {
                                // MPS
                                if (updateMPSFlag)
                                {
                                    if (listDataValue[i].MPS_status == CommonInfo.INT_STATUS_NORMAL)
                                    {
                                        objDataValue.var1 = objDataValue.var1 + listDataValue[i].var1;
                                        objDataValue.var1_status = listDataValue[i].MPS_status;
                                        objDataValue.var2 = objDataValue.var2 + listDataValue[i].var2;
                                        objDataValue.var2_status = listDataValue[i].MPS_status;
                                        objDataValue.var3 = objDataValue.var3 + listDataValue[i].var3;
                                        objDataValue.var3_status = listDataValue[i].MPS_status;
                                        objDataValue.var4 = objDataValue.var4 + listDataValue[i].var4;
                                        objDataValue.var4_status = listDataValue[i].MPS_status;
                                        objDataValue.var5 = objDataValue.var5 + listDataValue[i].var5;
                                        objDataValue.var5_status = listDataValue[i].MPS_status;
                                        objDataValue.var6 = objDataValue.var6 + listDataValue[i].var6;
                                        objDataValue.var6_status = listDataValue[i].MPS_status;
                                        objDataValue.MPS_status = listDataValue[i].MPS_status;
                                        countingMPSCal++;
                                    }
                                    else
                                    {
                                        objDataValue.var1 = -1;
                                        objDataValue.var1_status = listDataValue[i].MPS_status;
                                        objDataValue.var2 = -1;
                                        objDataValue.var2_status = listDataValue[i].MPS_status;
                                        objDataValue.var3 = -1;
                                        objDataValue.var3_status = listDataValue[i].MPS_status;
                                        objDataValue.var4 = -1;
                                        objDataValue.var4_status = listDataValue[i].MPS_status;
                                        objDataValue.var5 = -1;
                                        objDataValue.var5_status = listDataValue[i].MPS_status;
                                        objDataValue.var6 = -1;
                                        objDataValue.var6_status = listDataValue[i].MPS_status;
                                        objDataValue.MPS_status = listDataValue[i].MPS_status;
                                        updateMPSFlag = false;
                                    }
                                }
                            }
                            if (updateMPSFlag)
                            {
                                objDataValue.var1 = (double)objDataValue.var1 / (double)countingMPSCal;
                                objDataValue.var2 = (double)objDataValue.var2 / (double)countingMPSCal;
                                objDataValue.var3 = (double)objDataValue.var3 / (double)countingMPSCal;
                                objDataValue.var4 = (double)objDataValue.var4 / (double)countingMPSCal;
                                objDataValue.var5 = (double)objDataValue.var5 / (double)countingMPSCal;
                                objDataValue.var6 = (double)objDataValue.var6 / (double)countingMPSCal;
                            }
                            // get latest to check before add
                            objLatest = new data_5minute_value_repository().get_latest_info();
                            frmNewMain main = new frmNewMain();
                            if (objLatest != null &&
                                objLatest.created.Date == objDataValue.created.Date &&
                                objLatest.created.Month == objDataValue.created.Month &&
                                objLatest.created.Year == objDataValue.created.Year &&
                                objLatest.stored_hour == objDataValue.stored_hour &&
                                objLatest.stored_minute == objDataValue.stored_minute)
                            {
                                // update to
                                // MPS

                                if (objLatest.MPS_status == CommonInfo.INT_STATUS_NORMAL &&
                                    objDataValue.MPS_status == CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objLatest.var1 = (objLatest.var1 + objDataValue.var1) / 2;
                                    objLatest.var1_status = objDataValue.MPS_status;
                                    objLatest.var2 = (objLatest.var2 + objDataValue.var2) / 2;
                                    objLatest.var2_status = objDataValue.MPS_status;
                                    objLatest.var3 = (objLatest.var3 + objDataValue.var3) / 2;
                                    objLatest.var3_status = objDataValue.MPS_status;
                                    objLatest.var4 = (objLatest.var4 + objDataValue.var4) / 2;
                                    objLatest.var4_status = objDataValue.MPS_status;
                                    objLatest.var5 = (objLatest.var5 + objDataValue.var5) / 2;
                                    objLatest.var5_status = objDataValue.MPS_status;
                                    objLatest.var6 = (objLatest.var6 + objDataValue.var6) / 2;
                                    objLatest.var6_status = objDataValue.MPS_status;
                                    objLatest.MPS_status = objDataValue.MPS_status;
                                }
                                else
                                {
                                    objLatest.var1 = -1;
                                    objLatest.var1_status = objLatest.MPS_status;
                                    objLatest.var2 = -1;
                                    objLatest.var2_status = objLatest.MPS_status;
                                    objLatest.var3 = -1;
                                    objLatest.var3_status = objLatest.MPS_status;
                                    objLatest.var4 = -1;
                                    objLatest.var4_status = objLatest.MPS_status;
                                    objLatest.var5 = -1;
                                    objLatest.var5_status = objLatest.MPS_status;
                                    objLatest.var6 = -1;
                                    objLatest.var6_status = objLatest.MPS_status;
                                    objLatest.MPS_status = objLatest.MPS_status;
                                    if (objDataValue.MPS_status != CommonInfo.INT_STATUS_NORMAL)
                                    {
                                        objLatest.var1_status = objDataValue.MPS_status;
                                        objLatest.var2_status = objDataValue.MPS_status;
                                        objLatest.var3_status = objDataValue.MPS_status;
                                        objLatest.var4_status = objDataValue.MPS_status;
                                        objLatest.var5_status = objDataValue.MPS_status;
                                        objLatest.var6_status = objDataValue.MPS_status;
                                        objLatest.MPS_status = objDataValue.MPS_status;
                                    }

                                }

                                ///
                                setting_repository s = new setting_repository();
                                int id = s.get_id_by_key("lasted_push");
                                DateTime lastedPush = s.get_datetime_by_id(id);

                                /// Send File ftp			
                                if (
                                    main.FTP(objLatest))
                                {
                                    objLatest.push = 1;
                                    objLatest.push_time = DateTime.Now;

                                }
                                else
                                {
                                    objLatest.push = 0;
                                    objLatest.push_time = DateTime.Now;
                                }
                                ///
                                //// save to data value table
                                if (new data_5minute_value_repository().update(ref objLatest) > 0)
                                {
                                    //Console.Write("TRUE5" + "\n");
                                    // ok
                                }
                                else
                                {
                                    // fail
                                }
                                status = 1; // update
                            } //if TRUE4
                            else
                            {
                                ///
                                setting_repository s = new setting_repository();
                                int id = s.get_id_by_key("lasted_push");
                                DateTime lastedPush = s.get_datetime_by_id(id);

                                /// Send File ftp			
                                if (

                                    main.FTP(objDataValue))
                                {
                                    objDataValue.push = 1;
                                    objDataValue.push_time = DateTime.Now;

                                }
                                else
                                {
                                    objDataValue.push = 0;
                                    objDataValue.push_time = DateTime.Now;
                                }
                                //// save to data value table
                                if (new data_5minute_value_repository().add(ref objDataValue) > 0)
                                {
                                    // ok
                                }
                                else
                                {
                                    // fail
                                }
                                status = 2; // add
                            }
                            min_minute = tempMinute;
                            listDataValue.Clear();
                        } //if TRUE3
                        else
                        {
                            // add to list
                        }
                    }
                    else
                    {
                        // add to list
                    }
                }
                latestCalculate5Minute = DateTime.Now;
                max_minute = tempMinute;
                hour = tempHour;
                if (obj != null)
                {
                    listDataValue.Add(obj);
                    //Console.Write("da add vao list \n");
                }

                if (status == 0)
                {
                    return null;
                }
                else if (status == 1)
                {
                    return objLatest;
                }
                else
                {
                    return objDataValue;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return null;
            }

        }
        public data_value addNewObjFor60Minute(data_value obj, bool isImmediatelyCalculation = false)
        {

            // checking execute transaction
            int tempHour = 0;
            if (obj != null)
            {
                tempHour = obj.created.Hour;
            }
            else
            {
                tempHour = DateTime.Now.Hour;
            }

            int tempMinute = 0;
            if (obj != null)
            {
                tempMinute = obj.created.Minute;
            }
            else
            {
                tempMinute = DateTime.Now.Minute;

                if (DateTime.Compare(latestCalculate60Minute, DateTime.Now.AddMinutes(-1)) < 0)
                {
                    return null;
                }
            }
            data_value objDataValue = null;
            data_value objLatest = null;
            int status = 0;

            if (listDataValue.Count > 0)
            {
                if ((tempHour != hour) || isImmediatelyCalculation)
                {
                    // ok
                    // calculate and add to database
                    objDataValue = new data_value();
                    objDataValue.MPS_status = listDataValue[0].MPS_status;
                    // MPS
                    objDataValue.var1 = listDataValue[0].var1;
                    objDataValue.var1_status = listDataValue[0].MPS_status;
                    objDataValue.var2 = listDataValue[0].var2;
                    objDataValue.var2_status = listDataValue[0].MPS_status;
                    objDataValue.var3 = listDataValue[0].var3;
                    objDataValue.var3_status = listDataValue[0].MPS_status;
                    objDataValue.var4 = listDataValue[0].var4;
                    objDataValue.var4_status = listDataValue[0].MPS_status;
                    objDataValue.var5 = listDataValue[0].var5;
                    objDataValue.var5_status = listDataValue[0].MPS_status;
                    objDataValue.var6 = listDataValue[0].var6;
                    objDataValue.var6_status = listDataValue[0].MPS_status;
                    // Time
                    objDataValue.stored_date = listDataValue[0].stored_date;
                    objDataValue.stored_hour = hour;
                    objDataValue.stored_minute = 0;
                    int count = listDataValue.Count;

                    bool updateMPSFlag = true;
                    int countingMPSCal = 1;

                    for (int i = 0; i <= count - 1; i++)
                    {
                        // MPS
                        if (updateMPSFlag)
                        {
                            if (listDataValue[i].MPS_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objDataValue.var1 = objDataValue.var1 + listDataValue[i].var1;
                                objDataValue.var1_status = listDataValue[i].MPS_status;
                                objDataValue.var2 = objDataValue.var2 + listDataValue[i].var2;
                                objDataValue.var2_status = listDataValue[i].MPS_status;
                                objDataValue.var3 = objDataValue.var3 + listDataValue[i].var3;
                                objDataValue.var3_status = listDataValue[i].MPS_status;
                                objDataValue.var4 = objDataValue.var4 + listDataValue[i].var4;
                                objDataValue.var4_status = listDataValue[i].MPS_status;
                                objDataValue.var5 = objDataValue.var5 + listDataValue[i].var5;
                                objDataValue.var5_status = listDataValue[i].MPS_status;
                                objDataValue.var6 = objDataValue.var6 + listDataValue[i].var6;
                                objDataValue.var6_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_status = listDataValue[i].MPS_status;
                                countingMPSCal++;
                            }
                            else
                            {
                                objDataValue.var1 = -1;
                                objDataValue.var1_status = listDataValue[i].MPS_status;
                                objDataValue.var2 = -1;
                                objDataValue.var2_status = listDataValue[i].MPS_status;
                                objDataValue.var3 = -1;
                                objDataValue.var3_status = listDataValue[i].MPS_status;
                                objDataValue.var4 = -1;
                                objDataValue.var4_status = listDataValue[i].MPS_status;
                                objDataValue.var5 = -1;
                                objDataValue.var5_status = listDataValue[i].MPS_status;
                                objDataValue.var6 = -1;
                                objDataValue.var6_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_status = listDataValue[i].MPS_status;
                                updateMPSFlag = false;
                            }
                        }

                    }
                    if (updateMPSFlag)
                    {

                        objDataValue.var1 = (double)objDataValue.var1 / (double)countingMPSCal;
                        objDataValue.var2 = (double)objDataValue.var2 / (double)countingMPSCal;
                        objDataValue.var3 = (double)objDataValue.var3 / (double)countingMPSCal;
                        objDataValue.var4 = (double)objDataValue.var4 / (double)countingMPSCal;
                        objDataValue.var5 = (double)objDataValue.var5 / (double)countingMPSCal;
                        objDataValue.var6 = (double)objDataValue.var6 / (double)countingMPSCal;
                    }

                    // get latest to check before add
                    objLatest = new data_60minute_value_repository().get_latest_info();
                    if (objLatest != null &&
                        objLatest.created.Date == objDataValue.created.Date &&
                        objLatest.created.Month == objDataValue.created.Month &&
                        objLatest.created.Year == objDataValue.created.Year &&
                        objLatest.stored_hour == objDataValue.stored_hour &&
                        objLatest.stored_minute == objDataValue.stored_minute)
                    {
                        // update to
                        // MPS

                        if (objLatest.MPS_status == CommonInfo.INT_STATUS_NORMAL &&
                            objDataValue.MPS_status == CommonInfo.INT_STATUS_NORMAL)
                        {
                            objLatest.var1 = (objLatest.var1 + objDataValue.var1) / 2;
                            objLatest.var1_status = objDataValue.MPS_status;
                            objLatest.var2 = (objLatest.var2 + objDataValue.var2) / 2;
                            objLatest.var2_status = objDataValue.MPS_status;
                            objLatest.var3 = (objLatest.var3 + objDataValue.var3) / 2;
                            objLatest.var3_status = objDataValue.MPS_status;
                            objLatest.var4 = (objLatest.var4 + objDataValue.var4) / 2;
                            objLatest.var4_status = objDataValue.MPS_status;
                            objLatest.var5 = (objLatest.var5 + objDataValue.var5) / 2;
                            objLatest.var5_status = objDataValue.MPS_status;
                            objLatest.var6 = (objLatest.var6 + objDataValue.var6) / 2;
                            objLatest.var6_status = objDataValue.MPS_status;
                            objLatest.MPS_status = objDataValue.MPS_status;
                        }
                        else
                        {
                            objLatest.var1 = -1;
                            objLatest.var1_status = objLatest.MPS_status;
                            objLatest.var2 = -1;
                            objLatest.var2_status = objLatest.MPS_status;
                            objLatest.var3 = -1;
                            objLatest.var3_status = objLatest.MPS_status;
                            objLatest.var4 = -1;
                            objLatest.var4_status = objLatest.MPS_status;
                            objLatest.var5 = -1;
                            objLatest.var5_status = objLatest.MPS_status;
                            objLatest.var6 = -1;
                            objLatest.var6_status = objLatest.MPS_status;
                            objLatest.MPS_status = objLatest.MPS_status;
                            if (objDataValue.MPS_status != CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.var1_status = objDataValue.MPS_status;
                                objLatest.var2_status = objDataValue.MPS_status;
                                objLatest.var3_status = objDataValue.MPS_status;
                                objLatest.var4_status = objDataValue.MPS_status;
                                objLatest.var5_status = objDataValue.MPS_status;
                                objLatest.var6_status = objDataValue.MPS_status;
                                objLatest.MPS_status = objDataValue.MPS_status;
                            }

                        }

                        //// save to data value table
                        if (new data_60minute_value_repository().update(ref objLatest) > 0)
                        {
                            // ok
                        }
                        else
                        {
                            // fail
                        }
                        status = 1;
                    }
                    else
                    {
                        //if (GlobalVar.isMaintenanceStatus && GlobalVar.maintenanceLog.pumping_system == 1)
                        //{
                        //    objDataValue.pumping_system_status = CommonInfo.INT_STATUS_MAINTENANCE;
                        //    //objDataValue.station_status = CommonInfo.INT_STATUS_MAINTENANCE;
                        //}
                        //// save to data value table
                        if (new data_60minute_value_repository().add(ref objDataValue) > 0)
                        {
                            // ok
                        }
                        else
                        {
                            // fail
                        }
                        status = 2;
                    }

                    min_minute = tempMinute;
                    listDataValue.Clear();
                }
                else
                {
                    // add to list
                }

            }
            latestCalculate60Minute = DateTime.Now;
            max_minute = tempMinute;
            hour = tempHour;
            if (obj != null)
            {
                listDataValue.Add(obj);
            }
            if (status == 0)
            {
                return null;
            }
            else if (status == 1)
            {
                return objLatest;
            }
            else
            {
                return objDataValue;
            }
        }
    }
    public class ReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; set; }
    }
}
