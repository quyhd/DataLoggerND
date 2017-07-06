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
using System.Resources;
using System.Reflection;
namespace DataLogger
{
    public partial class frmMain : Form
    {
        public static data_value _data_value_obj = new data_value();

        private System.Threading.Timer tmrThreadingTimer;
        private System.Threading.Timer tmrThreadingTimerStationStatus;
        // delegate used for Invoke
        internal delegate void StringDelegate(string data);
        private delegate void ProcessDataCallback(string text);
        // TOC         
        private int TOC_rx_write = 0;
        private int TOC_rx_counter = 0;
        private byte[] TOC_rx_buffer = new byte[2048];

        private const int PACKET_LENGTH = 92;
        private byte[] TOC_receive_buffer = new byte[2048];
        private int TOC_buffer_counter = 0;

        // TP         
        private int TP_rx_write = 0;
        private int TP_rx_counter = 0;
        private byte[] TP_rx_buffer = new byte[2048];

        private byte[] TP_receive_buffer = new byte[2048];
        private int TP_buffer_counter = 0;

        // TN         
        private int TN_rx_write = 0;
        private int TN_rx_counter = 0;
        private byte[] TN_rx_buffer = new byte[2048];

        private byte[] TN_receive_buffer = new byte[2048];
        private int TN_buffer_counter = 0;

        // MPS         
        private int MPS_rx_write = 0;
        private int MPS_rx_counter = 0;
        private byte[] MPS_rx_buffer = new byte[2048];

        private const int MPS_PACKET_LENGTH = 29;
        private byte[] MPS_receive_buffer = new byte[2048];
        private int MPS_buffer_counter = 0;

        // Sampling Test         
        private int SAMP_rx_write = 0;
        private int SAMP_rx_counter = 0;
        private byte[] SAMP_rx_buffer = new byte[2048];

        private const int SAMP_PACKET_LENGTH = 86;
        private byte[] SAMP_receive_buffer = new byte[2048];
        private int SAMP_buffer_counter = 0;

        // ADAM 4050         
        private int ADAM405x_rx_write = 0;
        private int ADAM405x_rx_counter = 0;
        private byte[] ADAM405x_rx_buffer = new byte[2048];

        private const int ADAM405x_PACKET_LENGTH = 8;
        private const int ADAM_TEMP_HUMIDITY_PACKET_LENGTH = 58;
        private byte[] ADAM405x_receive_buffer = new byte[2048];
        private int ADAM405x_buffer_counter = 0;

        private DataTable dt = new DataTable();
        private DataTable dtSamplingTest = new DataTable();
        private DataTable dtDOControl = new DataTable();

        public const string ADAM_4050 = "ADAM4050";
        public const string ADAM_4051 = "ADAM4051";
        public const string ADAM_TEMP_HUMIDITY = "TEMP_HUMIDITY";

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

        public frmMain()
        {
            InitializeComponent();
            initDataTable();
            initDataTableSamplingTest();
            initDataTableDoControl();            
        }

        private void initDataTable()
        {
            // initial datatable for datagridview
            DataColumn No = new DataColumn("No", Type.GetType("System.String"));
            DataColumn ItemName = new DataColumn("ItemName", Type.GetType("System.String"));
            DataColumn ItemCode = new DataColumn("ItemCode", Type.GetType("System.String"));
            DataColumn OneHourData = new DataColumn("OneHourData", Type.GetType("System.String"));
            DataColumn FiveMinData = new DataColumn("FiveMinData", Type.GetType("System.String"));
            DataColumn CurrentData = new DataColumn("CurrentData", Type.GetType("System.String"));
            DataColumn AutomaticStatus = new DataColumn("AutomaticStatus", Type.GetType("System.String"));
            DataColumn ManualStatus = new DataColumn("ManualStatus", Type.GetType("System.String"));
            DataColumn CommPort = new DataColumn("CommPort", Type.GetType("System.String"));

            dt.Columns.Add(No);
            dt.Columns.Add(ItemName);
            dt.Columns.Add(ItemCode);
            dt.Columns.Add(OneHourData);
            dt.Columns.Add(FiveMinData);
            dt.Columns.Add(CurrentData);
            dt.Columns.Add(AutomaticStatus);
            dt.Columns.Add(ManualStatus);
            dt.Columns.Add(CommPort);

            initValueToDataTable();
        }

        private void initValueToDataTable()
        {
            try
            {
                dt.Clear();

                for (int i = 0; i < 9; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["No"] = (i + 1).ToString();
                    string ItemNameText = "";
                    string ItemCodeText = "";
                    string CommPortText = "";
                    switch (i)
                    {
                        case 0:
                            ItemNameText = "MPS(pH)";
                            ItemCodeText = "PHYC1";
                            CommPortText = "COM7";
                            break;
                        case 1:
                            ItemNameText = "MPS(EC)";
                            ItemCodeText = "CONC2";
                            CommPortText = "COM7";
                            break;
                        case 2:
                            ItemNameText = "MPS(DO)";
                            ItemCodeText = "DOWC3";
                            CommPortText = "COM7";
                            break;
                        case 3:
                            ItemNameText = "MPS(Turbidity)";
                            ItemCodeText = "SUSC4";
                            CommPortText = "COM7";
                            break;
                        case 4:
                            ItemNameText = "MPS(ORP)";
                            ItemCodeText = "ORPC5";
                            CommPortText = "COM7";
                            break;
                        case 5:
                            ItemNameText = "MPS(Temp)";
                            ItemCodeText = "TEMC0";
                            CommPortText = "COM7";
                            break;
                        case 6:
                            ItemNameText = "TN(TN)";
                            ItemCodeText = "TON00";
                            CommPortText = "COM8";
                            break;
                        case 7:
                            ItemNameText = "TP(TP)";
                            ItemCodeText = "TOP00";
                            CommPortText = "COM9";
                            break;
                        case 8:
                            ItemNameText = "TOC(TOC)";
                            ItemCodeText = "TOC00";
                            CommPortText = "COM13";
                            break;
                        default:
                            break;
                    }
                    dr["ItemName"] = ItemNameText;
                    dr["CurrentData"] = "-";
                    dr["AutomaticStatus"] = "-";
                    dr["ItemCode"] = ItemCodeText;
                    dr["CommPort"] = CommPortText;
                    dr["ManualStatus"] = "-";
                    dr["OneHourData"] = "-";
                    dr["FiveMinData"] = "-";

                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }

                // gridData.DataSource = dt;
                dgvDetailSendingResult.DataSource = dt;

                // Sortable or not sortable
                dgvDetailSendingResult.Columns["No"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDetailSendingResult.Columns["ItemName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDetailSendingResult.Columns["CurrentData"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDetailSendingResult.Columns["AutomaticStatus"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDetailSendingResult.Columns["ItemCode"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDetailSendingResult.Columns["OneHourData"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDetailSendingResult.Columns["FiveMinData"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDetailSendingResult.Columns["ManualStatus"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDetailSendingResult.Columns["CommPort"].SortMode = DataGridViewColumnSortMode.NotSortable;

                // color of header text
                dgvDetailSendingResult.Columns["No"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvDetailSendingResult.Columns["ItemName"].HeaderCell.Style.ForeColor = Color.Teal;
                dgvDetailSendingResult.Columns["CurrentData"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvDetailSendingResult.Columns["AutomaticStatus"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvDetailSendingResult.Columns["ItemCode"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvDetailSendingResult.Columns["OneHourData"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvDetailSendingResult.Columns["FiveMinData"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvDetailSendingResult.Columns["ManualStatus"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvDetailSendingResult.Columns["CommPort"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;

                // BackColor of header text
                dgvDetailSendingResult.Columns["No"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDetailSendingResult.Columns["ItemName"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDetailSendingResult.Columns["CurrentData"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDetailSendingResult.Columns["AutomaticStatus"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDetailSendingResult.Columns["ItemCode"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDetailSendingResult.Columns["OneHourData"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDetailSendingResult.Columns["FiveMinData"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDetailSendingResult.Columns["ManualStatus"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDetailSendingResult.Columns["CommPort"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;

                // header text
                dgvDetailSendingResult.Columns["No"].HeaderText = "No.";
                dgvDetailSendingResult.Columns["ItemName"].HeaderText = "Item Name";
                dgvDetailSendingResult.Columns["CurrentData"].HeaderText = "Current Data";
                dgvDetailSendingResult.Columns["AutomaticStatus"].HeaderText = "Automatic Status";
                dgvDetailSendingResult.Columns["ItemCode"].HeaderText = "Item Code";
                dgvDetailSendingResult.Columns["OneHourData"].HeaderText = "1 Hour Data";
                dgvDetailSendingResult.Columns["FiveMinData"].HeaderText = "5 Min. Data";
                dgvDetailSendingResult.Columns["ManualStatus"].HeaderText = "Manual Status";
                dgvDetailSendingResult.Columns["CommPort"].HeaderText = "Comm. Port";

                // alignment
                // header text
                dgvDetailSendingResult.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDetailSendingResult.Columns["ItemName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvDetailSendingResult.Columns["CurrentData"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDetailSendingResult.Columns["AutomaticStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDetailSendingResult.Columns["ItemCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDetailSendingResult.Columns["OneHourData"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDetailSendingResult.Columns["FiveMinData"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDetailSendingResult.Columns["ManualStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDetailSendingResult.Columns["CommPort"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // column width
                dgvDetailSendingResult.Columns["No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDetailSendingResult.Columns["ItemName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDetailSendingResult.Columns["CurrentData"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDetailSendingResult.Columns["AutomaticStatus"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDetailSendingResult.Columns["ItemCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDetailSendingResult.Columns["OneHourData"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDetailSendingResult.Columns["FiveMinData"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDetailSendingResult.Columns["ManualStatus"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDetailSendingResult.Columns["CommPort"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvDetailSendingResult.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Refresh data result fail; " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void initDataTableSamplingTest()
        {
            // initial datatable for datagridview
            DataColumn No = new DataColumn("No", Type.GetType("System.String"));
            DataColumn ItemName = new DataColumn("ItemName", Type.GetType("System.String"));
            DataColumn CurrentData = new DataColumn("CurrentData", Type.GetType("System.String"));
            dtSamplingTest.Columns.Add(No);
            dtSamplingTest.Columns.Add(ItemName);
            dtSamplingTest.Columns.Add(CurrentData);

            initValueToDataTableSamplingTest();
        }

        private void initValueToDataTableSamplingTest()
        {
            try
            {
                dtSamplingTest.Clear();

                for (int i = 0; i < 7; i++)
                {
                    DataRow dr = dtSamplingTest.NewRow();

                    dr["No"] = (i + 1).ToString();
                    string ItemNameText = "";

                    switch (i)
                    {
                        case 0:
                            ItemNameText = "Equipment Name";
                            break;
                        case 1:
                            ItemNameText = "Response Time";
                            break;
                        case 2:
                            ItemNameText = "Refrigeration Tempetature";
                            break;
                        case 3:
                            ItemNameText = "Bottle Position";
                            break;
                        case 4:
                            ItemNameText = "Door Status";
                            break;
                        case 5:
                            ItemNameText = "Equipment Status";
                            break;
                        case 6:
                            ItemNameText = "Comm. Port";
                            break;
                        default:
                            break;
                    }
                    dr["ItemName"] = ItemNameText;
                    dr["CurrentData"] = "--";

                    dtSamplingTest.Rows.Add(dr);
                    dtSamplingTest.AcceptChanges();
                }

                // gridData.DataSource = dtSamplingTest;
                dgvSamplingTest.DataSource = dtSamplingTest;

                // Sortable or not sortable
                dgvSamplingTest.Columns["No"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvSamplingTest.Columns["ItemName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvSamplingTest.Columns["CurrentData"].SortMode = DataGridViewColumnSortMode.NotSortable;

                // color of header text
                dgvSamplingTest.Columns["No"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvSamplingTest.Columns["ItemName"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvSamplingTest.Columns["CurrentData"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;

                // BackColor of header text
                dgvSamplingTest.Columns["No"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvSamplingTest.Columns["ItemName"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvSamplingTest.Columns["CurrentData"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;

                // header text
                dgvSamplingTest.Columns["No"].HeaderText = "No.";
                dgvSamplingTest.Columns["ItemName"].HeaderText = "Item Name";
                dgvSamplingTest.Columns["CurrentData"].HeaderText = "Current Data";

                // alignment
                // header text
                dgvSamplingTest.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSamplingTest.Columns["ItemName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvSamplingTest.Columns["CurrentData"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // column width
                dgvSamplingTest.Columns["No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvSamplingTest.Columns["ItemName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvSamplingTest.Columns["CurrentData"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvSamplingTest.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Refresh data result fail; " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void initDataTableDoControl()
        {
            // initial datatable for datagridview
            DataColumn No = new DataColumn("No", Type.GetType("System.String"));
            DataColumn ItemName = new DataColumn("ItemName", Type.GetType("System.String"));
            DataColumn ItemCode = new DataColumn("ItemCode", Type.GetType("System.String"));
            DataColumn FiveMinData = new DataColumn("FiveMinData", Type.GetType("System.String"));
            DataColumn CurrentData = new DataColumn("CurrentData", Type.GetType("System.String"));

            dtDOControl.Columns.Add(No);
            dtDOControl.Columns.Add(ItemName);
            dtDOControl.Columns.Add(ItemCode);
            dtDOControl.Columns.Add(FiveMinData);
            dtDOControl.Columns.Add(CurrentData);

            initValueToDataTableDoControl();
        }

        private void initValueToDataTableDoControl()
        {
            try
            {
                dtDOControl.Clear();

                for (int i = 0; i < 13; i++)
                {
                    DataRow dr = dtDOControl.NewRow();

                    dr["No"] = (i + 1).ToString();
                    string ItemNameText = "";
                    string ItemCodeText = "";
                    switch (i)
                    {
                        case 0:
                            ItemNameText = "Power";
                            ItemCodeText = "PWRON";
                            break;
                        case 1:
                            ItemNameText = "UPS";
                            ItemCodeText = "UPSON";
                            break;
                        case 2:
                            ItemNameText = "Door";
                            ItemCodeText = "DORON";
                            break;
                        case 3:
                            ItemNameText = "Fire";
                            ItemCodeText = "FIRON";
                            break;
                        case 4:
                            ItemNameText = "Flow";
                            ItemCodeText = "FLWON";
                            break;
                        case 5:
                            ItemNameText = "Pump (L) A/M";
                            ItemCodeText = "FMLMD";
                            break;
                        case 6:
                            ItemNameText = "Pump (L) R/S";
                            ItemCodeText = "FMLON";
                            break;
                        case 7:
                            ItemNameText = "Pump (L) FLT";
                            ItemCodeText = "FMLFT";
                            break;
                        case 8:
                            ItemNameText = "Pump (R) A/M";
                            ItemCodeText = "FMRMD";
                            break;
                        case 9:
                            ItemNameText = "Pump (R) R/S";
                            ItemCodeText = "FMRON";
                            break;
                        case 10:
                            ItemNameText = "Pump (R) FLT";
                            ItemCodeText = "FMRFT";
                            break;
                        case 11:
                            ItemNameText = "Temperature";
                            ItemCodeText = "ROOMT";
                            break;
                        case 12:
                            ItemNameText = "Humidity";
                            ItemCodeText = "HUMID";
                            break;
                        default:
                            break;
                    }
                    dr["ItemName"] = ItemNameText;
                    dr["ItemCode"] = ItemCodeText;

                    dtDOControl.Rows.Add(dr);
                    dtDOControl.AcceptChanges();
                }

                // gridData.DataSource = dt;
                dgvDoControl.DataSource = dtDOControl;

                // Sortable or not sortable
                dgvDoControl.Columns["No"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDoControl.Columns["ItemName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDoControl.Columns["ItemCode"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDoControl.Columns["CurrentData"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvDoControl.Columns["FiveMinData"].SortMode = DataGridViewColumnSortMode.NotSortable;

                // color of header text
                dgvDoControl.Columns["No"].HeaderCell.Style.ForeColor = System.Drawing.Color.Teal;
                dgvDoControl.Columns["ItemName"].HeaderCell.Style.ForeColor = Color.Teal;
                dgvDoControl.Columns["ItemCode"].HeaderCell.Style.ForeColor = Color.Teal;
                dgvDoControl.Columns["CurrentData"].HeaderCell.Style.ForeColor = Color.Teal;
                dgvDoControl.Columns["FiveMinData"].HeaderCell.Style.ForeColor = Color.Teal;


                // BackColor of header text
                dgvDoControl.Columns["No"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDoControl.Columns["ItemName"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDoControl.Columns["ItemCode"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDoControl.Columns["CurrentData"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;
                dgvDoControl.Columns["FiveMinData"].HeaderCell.Style.BackColor = System.Drawing.Color.GhostWhite;

                // header text
                dgvDoControl.Columns["No"].HeaderText = "No.";
                dgvDoControl.Columns["ItemName"].HeaderText = "Item Name";
                dgvDoControl.Columns["ItemCode"].HeaderText = "Item Code";
                dgvDoControl.Columns["CurrentData"].HeaderText = "Current Data";
                dgvDoControl.Columns["FiveMinData"].HeaderText = "5 Min. Data";

                // alignment
                // header text
                dgvDoControl.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDoControl.Columns["ItemName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvDoControl.Columns["ItemCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDoControl.Columns["CurrentData"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDoControl.Columns["FiveMinData"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // column width
                dgvDoControl.Columns["No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDoControl.Columns["ItemName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDoControl.Columns["CurrentData"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDoControl.Columns["ItemCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDoControl.Columns["FiveMinData"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                dgvDoControl.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Refresh data result fail; " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void updateCell(string itemCode, double value, string status)
        {
            try
            {
                int rowIndex = 0;
                switch (itemCode.Trim())
                {
                    case CODE_MPS_PH:
                        rowIndex = 0;
                        break;
                    case CODE_MPS_EC:
                        rowIndex = 1;
                        break;
                    case CODE_MPS_DO:
                        rowIndex = 2;
                        break;
                    case CODE_MPS_TURBIDITY:
                        rowIndex = 3;
                        break;
                    case CODE_MPS_ORP:
                        rowIndex = 4;
                        break;
                    case CODE_MPS_TEMP:
                        rowIndex = 5;
                        break;
                    case CODE_TN:
                        rowIndex = 6;
                        break;
                    case CODE_TP:
                        rowIndex = 7;
                        break;
                    case CODE_TOC:
                        rowIndex = 8;
                        break;
                    default:
                        break;
                }

                dgvDetailSendingResult["CurrentData", rowIndex].Value = value.ToString("##0.000");
                dgvDetailSendingResult["AutomaticStatus", rowIndex].Value = status;
                if (status == STATUS_ERROR)
                {
                    dgvDetailSendingResult["AutomaticStatus", rowIndex].Style.BackColor = System.Drawing.Color.Red;
                }
                else if (status == STATUS_Normal)
                {
                    dgvDetailSendingResult["AutomaticStatus", rowIndex].Style.BackColor = System.Drawing.Color.Green;
                }
                else if (status == STATUS_WARNING)
                {
                    dgvDetailSendingResult["AutomaticStatus", rowIndex].Style.BackColor = System.Drawing.Color.Yellow;
                }
                else if (status == STATUS_MEASURING)
                {
                    dgvDetailSendingResult["AutomaticStatus", rowIndex].Style.BackColor = System.Drawing.Color.LightBlue;
                }
                dgvDetailSendingResult.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Refresh data result fail; " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        //private void updateCellSamplerStatus(water_sampler obj)
        //{
        //    try
        //    {
        //        dgvSamplingTest["CurrentData", 0].Value = obj.equipment_name;
        //        dgvSamplingTest["CurrentData", 1].Value = obj.response_time.ToString("dd/MM/yyyy HH:mm:ss");
        //        dgvSamplingTest["CurrentData", 2].Value = obj.refrigeration_Temperature.ToString("##0.00");
        //        dgvSamplingTest["CurrentData", 3].Value = obj.bottle_position.ToString();
        //        dgvSamplingTest["CurrentData", 4].Value = obj.door_status;
        //        dgvSamplingTest["CurrentData", 5].Value = obj.equipment_status;
        //        dgvSamplingTest["CurrentData", 6].Value = obj.comm_port;

        //        //dgvSamplingTest["AutomaticStatus", rowIndex].Style.BackColor = System.Drawing.Color.Red;

        //        dgvSamplingTest.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Refresh data result fail; " + ex.Message);
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void updateCellStationStatus(station_status obj)
        //{
        //    try
        //    {
        //        if (obj.module_Power > -1)
        //        {
        //            dgvDoControl["CurrentData", 0].Value = (obj.module_Power == 1) ? "NORMAL" : "NOT NORMAL";
        //        }
        //        if (obj.module_UPS > -1)
        //        {
        //            dgvDoControl["CurrentData", 1].Value = (obj.module_UPS == 1) ? "NORMAL" : "NOT NORMAL";
        //        }
        //        if (obj.module_Door > -1)
        //        {
        //            dgvDoControl["CurrentData", 2].Value = (obj.module_Door == 1) ? "CLOSE" : "OPEN";
        //        }
        //        if (obj.module_Fire > -1)
        //        {
        //            dgvDoControl["CurrentData", 3].Value = (obj.module_Fire == 1) ? "NORMAL" : "OFF";
        //        }
        //        if (obj.module_Flow > -1)
        //        {
        //            dgvDoControl["CurrentData", 4].Value = (obj.module_Flow == 1) ? "NORMAL" : "OFF";
        //        }
        //        if (obj.module_PumpLAM > -1)
        //        {
        //            dgvDoControl["CurrentData", 5].Value = (obj.module_PumpLAM == 1) ? "MANUAL" : "AUTO";
        //        }
        //        if (obj.module_PumpLRS > -1)
        //        {
        //            dgvDoControl["CurrentData", 6].Value = (obj.module_PumpLRS == 1) ? "STOP" : "RUN";
        //        }
        //        if (obj.module_PumpLFLT > -1)
        //        {
        //            dgvDoControl["CurrentData", 7].Value = (obj.module_PumpLFLT == 1) ? "FAULT" : "NORMAL";
        //        }
        //        if (obj.module_PumpRAM > -1)
        //        {
        //            dgvDoControl["CurrentData", 8].Value = (obj.module_PumpRAM == 1) ? "AUTO" : "RUN";
        //        }
        //        if (obj.module_PumpRRS > -1)
        //        {
        //            dgvDoControl["CurrentData", 9].Value = (obj.module_PumpRRS == 1) ? "STOP" : "RUN";
        //        }
        //        if (obj.module_PumpRFLT > -1)
        //        {
        //            dgvDoControl["CurrentData", 10].Value = (obj.module_PumpRFLT == 1) ? "FAULT" : "NORMAL";
        //        }
        //        if (obj.module_Temperature > -1)
        //        {
        //            dgvDoControl["CurrentData", 11].Value = obj.module_Temperature.ToString("##0.00");
        //        }
        //        if (obj.module_Humidity > -1)
        //        {
        //            dgvDoControl["CurrentData", 12].Value = obj.module_Humidity.ToString("##0.00");
        //        }


        //        dgvDoControl.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Refresh data result fail; " + ex.Message);
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void serialPortTOC_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!serialPortTOC.IsOpen)
                    return;
                int bytes = serialPortTOC.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortTOC.Read(buffer, 0, bytes);
                for (int i = 0; i < bytes; i++)
                {
                    TOC_rx_buffer[TOC_rx_write++] = buffer[i];
                    if (TOC_rx_write == 2048)
                        TOC_rx_write = 0;
                }
                TOC_rx_counter += bytes;
                ProcessDataTOC("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ProcessDataTOC(string text)
        {

            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataTOC);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {

                    //byte[] temp = new byte[TOC_rx_counter];
                    //for (int i = 0; i < TOC_rx_counter; i++)
                    //    temp[i] = TOC_rx_buffer[i];
                    //if (txtData.Lines.Count() > 10)
                    //    txtData.Clear();
                    string temp1 = TOCParseData(TOC_rx_buffer);
                    if (rbtnTOC.Checked)
                    {
                        txtData.Text = temp1;
                    }

                    //MessageBox.Show("000:" + txtData.Text);
                    TOC_rx_counter = 0;
                    TOC_rx_write = 0;
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {

            }

        }

        private void ProcessDataTP(string text)
        {

            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataTP);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {

                    //byte[] temp = new byte[TP_rx_counter];
                    //for (int i = 0; i < TP_rx_counter; i++)
                    //    temp[i] = TP_rx_buffer[i];
                    //if (txtData.Lines.Count() > 10)
                    //    txtData.Clear();
                    string temp1 = TPParseData(TP_rx_buffer);
                    if (rbtnTP.Checked)
                    {
                        txtData.Text = temp1;
                    }

                    //MessageBox.Show("000:" + txtData.Text);
                    TP_rx_counter = 0;
                    TP_rx_write = 0;
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {

            }

        }

        private void ProcessDataTN(string text)
        {
            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataTN);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {
                    //byte[] temp = new byte[TN_rx_counter];
                    //for (int i = 0; i < TN_rx_counter; i++)
                    //    temp[i] = TN_rx_buffer[i];
                    //if (txtData.Lines.Count() > 10)
                    //    txtData.Clear();
                    string temp1 = TNParseData(TN_rx_buffer);
                    if (rbtnTN.Checked)
                    {
                        txtData.Text = temp1;
                    }

                    //MessageBox.Show("000:" + txtData.Text);
                    TN_rx_counter = 0;
                    TN_rx_write = 0;
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {

            }
        }

        private void ProcessDataMPS(string text)
        {
            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataMPS);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {
                    //byte[] temp = new byte[MPS_rx_counter];
                    //for (int i = 0; i < MPS_rx_counter; i++)
                    //    temp[i] = MPS_rx_buffer[i];
                    //if (txtData.Lines.Count() > 10)
                    //    txtData.Clear();
                    string temp1 = MPSParseData(MPS_rx_buffer);
                    if (rbtnMPS.Checked)
                    {
                        txtData.Text = temp1;
                    }

                    //MessageBox.Show("000:" + txtData.Text);
                    MPS_rx_counter = 0;
                    MPS_rx_write = 0;
                }
            }
            catch
            {

            }

        }

        private void setText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                StringDelegate d = new StringDelegate(setText);
                this.textBox1.Invoke(d, new object[] { text });
            }
            else
            {
                textBox1.Text = text;
            }
        }
        private void setRawText(string text)
        {
            if (this.txtRawData.InvokeRequired)
            {
                StringDelegate d = new StringDelegate(setRawText);
                this.txtRawData.Invoke(d, new object[] { text });
            }
            else
            {
                txtRawData.Text = text;
            }
        }
        private void serialPortTP_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!serialPortTP.IsOpen)
                    return;
                int bytes = serialPortTP.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortTP.Read(buffer, 0, bytes);
                for (int i = 0; i < bytes; i++)
                {
                    TP_rx_buffer[TP_rx_write++] = buffer[i];
                    if (TP_rx_write == 2048)
                        TP_rx_write = 0;
                }
                TP_rx_counter += bytes;
                ProcessDataTP("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void serialPortTN_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!serialPortTN.IsOpen)
                    return;
                int bytes = serialPortTN.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortTN.Read(buffer, 0, bytes);
                for (int i = 0; i < bytes; i++)
                {
                    TN_rx_buffer[TN_rx_write++] = buffer[i];
                    if (TN_rx_write == 2048)
                        TN_rx_write = 0;
                }
                TN_rx_counter += bytes;
                ProcessDataTN("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void serialPortMPS_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!serialPortMPS.IsOpen)
                    return;
                int bytes = serialPortMPS.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortMPS.Read(buffer, 0, bytes);
                for (int i = 0; i < bytes; i++)
                {
                    MPS_rx_buffer[MPS_rx_write++] = buffer[i];
                    if (MPS_rx_write == 2048)
                        MPS_rx_write = 0;
                }
                MPS_rx_counter += bytes;
                ProcessDataMPS("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            tmrThreadingTimer = new System.Threading.Timer(new TimerCallback(tmrThreadingTimer_TimerCallback), null, System.Threading.Timeout.Infinite, 2000);

            tmrThreadingTimer.Change(0, 2000);


            tmrThreadingTimerStationStatus = new System.Threading.Timer(new TimerCallback(tmrThreadingTimerStationStatus_TimerCallback), null, System.Threading.Timeout.Infinite, 2000);

            tmrThreadingTimerStationStatus.Change(0, 2000);

            if (init())
            {

            }
            else
            {
                //MessageBox.Show(res_man.GetString("please_check_system", cul));
            }
        }

        private bool init()
        {
            try
            {
                // TOC connect
                serialPortTOC.Open();
                // TP connect
                serialPortTP.Open();
                // TN connect
                serialPortTN.Open();
                // MPS connect
                //serialPortMPS.Open();
                // SAMPLER connect
                serialPortSAMP.Open();

                serialPortADAM405x.Open();

                return true;
            }
            catch //(Exception ex)
            {
                return false;
            }

        }

        public int indexSelection = 0;
        private void tmrThreadingTimer_TimerCallback(object state)
        {
            setText(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            indexSelection = (indexSelection + 1) % 5;
            switch (indexSelection)
            {
                case 0: // TOC                    
                    requestInfor(serialPortTOC);
                    break;
                case 1: // TP
                    requestInfor(serialPortTP);
                    break;
                case 2: // TN
                    requestInfor(serialPortTN);
                    break;
                case 3: // MPS
                    requestInforMPS(serialPortMPS);
                    break;
                case 4: // SAMPLER
                    requestInforSAMPLER(serialPortSAMP);
                    break;
                default:
                    break;
            }
        }

        public int indexSelectionStation = 0;
        public static string station_status_data_type = "";
        private void tmrThreadingTimerStationStatus_TimerCallback(object state)
        {
        }

        private static void requestInfor(SerialPort com)
        {
            if (com.IsOpen)
            {
                byte[] packet = new byte[9];
                //Fill to packet
                packet[0] = 0x02;//STX

                packet[1] = 0x44;//D
                packet[2] = 0x41;//A
                packet[3] = 0x54;//T
                packet[4] = 0x41;//A
                packet[5] = 0x03;//ETX
                packet[6] = 0x31;//ETX
                packet[7] = 0x3F;//CHK
                packet[8] = 0x0D;//CR

                com.Write(packet, 0, 9);
            }
        }

        private static void requestInforMPS(SerialPort com)
        {
            if (com.IsOpen)
            {
                // $0A$04$00$00$00$0C$F1$74
                byte[] packet = new byte[9];
                //Fill to packet
                packet[0] = 0x0A;

                packet[1] = 0x04;
                packet[2] = 0x00;
                packet[3] = 0x00;
                packet[4] = 0x00;
                packet[5] = 0x0C;
                packet[6] = 0xF1;
                packet[7] = 0x74;

                com.Write(packet, 0, 8);
            }
        }

        private static void requestInforSAMPLER(SerialPort com)
        {
            if (com.IsOpen)
            {

                byte[] packet = new byte[11];
                //Fill to packet
                packet[0] = 0x02; // STX

                packet[1] = 0x53; // 'S'
                packet[2] = 0x41; // 'A'
                packet[3] = 0x4D; // 'M'
                packet[4] = 0x50; // 'P'
                packet[5] = 0x31; // 1
                packet[6] = 0x30; // 0
                packet[7] = 0x03; // ETX
                packet[8] = 0x39;
                packet[9] = 0x37;
                packet[10] = 0x0D;//
                com.Write(packet, 0, 11);

                ////packet[0] = 0x02; // STX

                //packet[0] = 0x53; // 'S'
                //packet[1] = 0x41; // 'A'
                //packet[2] = 0x4D; // 'M'
                //packet[3] = 0x50; // 'P'
                //packet[4] = 0x31; // 1
                //packet[5] = 0x30; // 0                
                //packet[6] = 0x39;
                //packet[7] = 0x37;
                //packet[8] = 0x0D;//

                //com.Write(packet, 0, 8);
            }
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
                //MessageBox.Show("0003," + ex.Message);
            }


            return ret;
        }

        private string TOCParseData(byte[] text)
        {

            //int bufferIndex = 0;
            string result = "";

            try
            {

                //if (TOC_buffer_counter > 0)
                //    text = Combine(TOC_receive_buffer, TOC_buffer_counter, text);
                if (text.Length >= PACKET_LENGTH)
                {
                    for (int i = 0; i < text.Length - PACKET_LENGTH; i++)
                    {
                        if (text[i] == 0x02 && // STX
                            text[PACKET_LENGTH + i - 1] == 0x0D && // CR
                            text[PACKET_LENGTH + i - 4] == 0x03) // ERX
                        {
                            // process data
                            string strDateTime = Encoding.UTF8.GetString(text, i + 5, 14);
                            string strNumberParameter = Encoding.UTF8.GetString(text, i + 19, 2);
                            string strParameter = Encoding.UTF8.GetString(text, i + 21, 5);
                            string strValue = Encoding.UTF8.GetString(text, i + 26, 10);
                            string strInstrumentStatus = Encoding.UTF8.GetString(text, i + 36, 2);
                            string strAdditionInfor = Encoding.UTF8.GetString(text, i + 38, 50);

                            result = string.Format("DateTime: {0}; Number Parameter: {1}; Parameter: {2}; Value: {3}; Instrument Status: {4}; Addition Info: {5}", strDateTime, strNumberParameter, strParameter, strValue, strInstrumentStatus, strAdditionInfor);

                            if (rbtnTOC.Checked)
                            {
                                byte[] subArray = new byte[PACKET_LENGTH];
                                Array.Copy(text, i, subArray, 0, PACKET_LENGTH);
                                //setRawText(Encoding.UTF8.GetString(subArray));
                            }

                            double dValue = Convert.ToDouble(strValue);
                            string status = "";
                            strAdditionInfor = strAdditionInfor.ToLower();
                            if (strAdditionInfor.Contains("error"))
                            {
                                status = STATUS_ERROR;
                            }
                            else if (strAdditionInfor.Contains("running"))
                            {
                                status = STATUS_Normal;
                            }
                            else if (strAdditionInfor.Contains("normal"))
                            {
                                status = STATUS_Normal;
                            }
                            else if (strAdditionInfor.Contains("warning"))
                            {
                                status = STATUS_WARNING;
                            }

                            updateCell(CODE_TOC, dValue, status);

                            //// read data to display
                            //byte[] temp = new byte[PACKET_LENGTH];
                            //Array.Copy(text, i, temp, 0, PACKET_LENGTH);
                            //result += ByteArrayToHexString(temp);
                            ////Remove this packet from temporary buffer
                            //bufferIndex = i + PACKET_LENGTH;
                            //i = i + PACKET_LENGTH - 1;
                        }
                    }
                }
                //Reset buffer
                TOC_buffer_counter = 0;
                Array.Clear(TOC_receive_buffer, 0, TOC_receive_buffer.Length);
                //if (bufferIndex < text.Length)
                //{
                //    TOC_buffer_counter = text.Length - bufferIndex;
                //    //Console.WriteLine("Buffer Counter: " + buffer_counter.ToString() + ", Data Length: " + text.Length);
                //    Array.Copy(text, bufferIndex, TOC_receive_buffer, 0, TOC_buffer_counter);
                //}

                //if (TOC_buffer_counter > PACKET_LENGTH * 2)
                //{
                    TOC_rx_write = 0;
                    TOC_rx_counter = 0;
                    TOC_rx_buffer = new byte[2048];

                    TOC_receive_buffer = new byte[2048];
                    TOC_buffer_counter = 0;
                //}

            }
            catch// (Exception ex)
            {
                //MessageBox.Show("0002," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }

        private string TPParseData(byte[] text)
        {

            //int bufferIndex = 0;
            string result = "";

            try
            {

                //if (TP_buffer_counter > 0)
                //    text = Combine(TP_receive_buffer, TP_buffer_counter, text);
                if (text.Length >= PACKET_LENGTH)
                {
                    for (int i = 0; i < text.Length - PACKET_LENGTH; i++)
                    {
                        if (text[i] == 0x02 && // STX
                            text[PACKET_LENGTH + i - 1] == 0x0D && // CR
                            text[PACKET_LENGTH + i - 4] == 0x03) // ERX
                        {
                            // process data
                            string strDateTime = Encoding.UTF8.GetString(text, i + 5, 14);
                            string strNumberParameter = Encoding.UTF8.GetString(text, i + 19, 2);
                            string strParameter = Encoding.UTF8.GetString(text, i + 21, 5);
                            string strValue = Encoding.UTF8.GetString(text, i + 26, 10);
                            string strInstrumentStatus = Encoding.UTF8.GetString(text, i + 36, 2);
                            string strAdditionInfor = Encoding.UTF8.GetString(text, i + 38, 50);


                            result = string.Format("DateTime: {0}; Number Parameter: {1}; Parameter: {2}; Value: {3}; Instrument Status: {4}; Addition Info: {5}", strDateTime, strNumberParameter, strParameter, strValue, strInstrumentStatus, strAdditionInfor);

                            if (rbtnTP.Checked)
                            {
                                byte[] subArray = new byte[PACKET_LENGTH];
                                Array.Copy(text, i, subArray, 0, PACKET_LENGTH);
                                //setRawText(Encoding.UTF8.GetString(subArray));
                            }

                            double dValue = Convert.ToDouble(strValue);
                            string status = "";
                            strAdditionInfor = strAdditionInfor.ToLower();
                            if (strAdditionInfor.Contains("error"))
                            {
                                status = STATUS_ERROR;
                            }
                            else if (strAdditionInfor.Contains("running"))
                            {
                                status = STATUS_Normal;
                            }
                            else if (strAdditionInfor.Contains("normal"))
                            {
                                status = STATUS_Normal;
                            }
                            else if (strAdditionInfor.Contains("warning"))
                            {
                                status = STATUS_WARNING;
                            }
                            else if (strAdditionInfor.Contains("measuring"))
                            {
                                status = STATUS_MEASURING;
                            }

                            updateCell(CODE_TP, dValue, status);

                            //// read data to display
                            //byte[] temp = new byte[PACKET_LENGTH];
                            //Array.Copy(text, i, temp, 0, PACKET_LENGTH);
                            //result += ByteArrayToHexString(temp);
                            ////Remove this packet from temporary buffer
                            //bufferIndex = i + PACKET_LENGTH;
                            //i = i + PACKET_LENGTH - 1;
                        }
                    }
                }
                //Reset buffer
                TP_buffer_counter = 0;
                Array.Clear(TP_receive_buffer, 0, TP_receive_buffer.Length);
                //if (bufferIndex < text.Length)
                //{
                //    TP_buffer_counter = text.Length - bufferIndex;
                //    //Console.WriteLine("Buffer Counter: " + buffer_counter.ToString() + ", Data Length: " + text.Length);
                //    Array.Copy(text, bufferIndex, TP_receive_buffer, 0, TP_buffer_counter);
                //}

                //if (TP_buffer_counter > PACKET_LENGTH * 2)
                //{
                    TP_rx_write = 0;
                    TP_rx_counter = 0;
                    TP_rx_buffer = new byte[2048];

                    TP_receive_buffer = new byte[2048];
                    TP_buffer_counter = 0;
                //}
            }
            catch// (Exception ex)
            {
                //MessageBox.Show("0002," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }

        private string TNParseData(byte[] text)
        {

            int bufferIndex = 0;
            string result = "";

            try
            {

                //if (TN_buffer_counter > 0)
                //    text = Combine(TN_receive_buffer, TN_buffer_counter, text);
                if (text.Length >= PACKET_LENGTH)
                {
                    for (int i = 0; i < text.Length - PACKET_LENGTH; i++)
                    {
                        if (text[i] == 0x02 && // STX
                            text[PACKET_LENGTH + i - 1] == 0x0D && // CR
                            text[PACKET_LENGTH + i - 4] == 0x03) // ERX
                        {
                            // process data
                            string strDateTime = Encoding.UTF8.GetString(text, i + 5, 14);
                            string strNumberParameter = Encoding.UTF8.GetString(text, i + 19, 2);
                            string strParameter = Encoding.UTF8.GetString(text, i + 21, 5);
                            string strValue = Encoding.UTF8.GetString(text, i + 26, 10);
                            string strInstrumentStatus = Encoding.UTF8.GetString(text, i + 36, 2);
                            string strAdditionInfor = Encoding.UTF8.GetString(text, i + 38, 50);


                            result = string.Format("DateTime: {0}; Number Parameter: {1}; Parameter: {2}; Value: {3}; Instrument Status: {4}; Addition Info: {5}", strDateTime, strNumberParameter, strParameter, strValue, strInstrumentStatus, strAdditionInfor);

                            if (rbtnTN.Checked)
                            {
                                byte[] subArray = new byte[PACKET_LENGTH];
                                Array.Copy(text, i, subArray, 0, PACKET_LENGTH);
                                //setRawText(Encoding.UTF8.GetString(subArray));
                            }

                            double dValue = Convert.ToDouble(strValue);
                            string status = "";
                            strAdditionInfor = strAdditionInfor.ToLower();
                            if (strAdditionInfor.Contains("error"))
                            {
                                status = STATUS_ERROR;
                            }
                            else if (strAdditionInfor.Contains("running"))
                            {
                                status = STATUS_Normal;
                            }
                            else if (strAdditionInfor.Contains("normal"))
                            {
                                status = STATUS_Normal;
                            }
                            else if (strAdditionInfor.Contains("warning"))
                            {
                                status = STATUS_WARNING;
                            }
                            else if (strAdditionInfor.Contains("measuring"))
                            {
                                status = STATUS_MEASURING;
                            }
                            updateCell(CODE_TN, dValue, status);

                            //// read data to display
                            //byte[] temp = new byte[PACKET_LENGTH];
                            //Array.Copy(text, i, temp, 0, PACKET_LENGTH);
                            //result += ByteArrayToHexString(temp);
                            ////Remove this packet from temporary buffer
                            bufferIndex = i + PACKET_LENGTH;
                            i = i + PACKET_LENGTH - 1;
                        }
                    }
                }
                //Reset buffer
                TN_buffer_counter = 0;
                Array.Clear(TN_receive_buffer, 0, TN_receive_buffer.Length);
                //if (bufferIndex < text.Length)
                //{
                //    TN_buffer_counter = text.Length - bufferIndex;
                //    //Console.WriteLine("Buffer Counter: " + buffer_counter.ToString() + ", Data Length: " + text.Length);
                //    Array.Copy(text, bufferIndex, TN_receive_buffer, 0, TN_buffer_counter);
                //}

                //if (TN_buffer_counter > PACKET_LENGTH * 2)
                //{
                    TN_rx_write = 0;
                    TN_rx_counter = 0;
                    TN_rx_buffer = new byte[2048];

                    TN_receive_buffer = new byte[2048];
                    TN_buffer_counter = 0;
                //}
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("0002," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }

        private string MPSParseData(byte[] text)
        {

            int bufferIndex = 0;
            string result = "";

            try
            {

                //if (MPS_buffer_counter > 0)
                //    text = Combine(MPS_receive_buffer, MPS_buffer_counter, text);
                //if (text.Length >= MPS_PACKET_LENGTH * 2)
                if (text.Length >= MPS_PACKET_LENGTH)
                {
                    for (int i = 0; i < text.Length - MPS_PACKET_LENGTH - 2; i++)
                    {
                        if (text[i] == 0x0A &&
                            text[i + 1] == 0x04 &&
                            text[i + 2] == 0x18 &&
                            text[i + MPS_PACKET_LENGTH] == 0x0A &&
                            text[i + MPS_PACKET_LENGTH + 1] == 0x04 &&
                            text[i + MPS_PACKET_LENGTH + 2] == 0x18
                            )
                        {
                            // process data
                            //MessageBox.Show(ByteArrayToHexString(SubArray(text, i + 5, 4)));
                            Single valPH = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 5, 4)));
                            Single valEC = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 9, 4)));
                            Single valDO = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 13, 4)));
                            Single valTurbidity = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 17, 4)));
                            Single valORP = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 21, 4)));
                            Single valTemp = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 25, 4)));


                            result = string.Format("PH: {0}; EC: {1}; DO: {2}; Turbidity: {3}; ORP: {4}; Temp: {5}", valPH, valEC, valDO, valTurbidity, valORP, valTemp);

                            if (rbtnMPS.Checked)
                            {
                                byte[] subArray = new byte[MPS_PACKET_LENGTH];
                                Array.Copy(text, i, subArray, 0, MPS_PACKET_LENGTH);
                                //setRawText(ByteArrayToHexString(subArray));
                            }

                            double pHValue = Convert.ToDouble(valPH);
                            double ECValue = Convert.ToDouble(valEC);
                            double DoValue = Convert.ToDouble(valDO);
                            double TurbidityValue = Convert.ToDouble(valTurbidity);
                            double ORPValue = Convert.ToDouble(valORP);
                            double TempValue = Convert.ToDouble(valTemp);
                            string status = STATUS_Normal;

                            updateCell(CODE_MPS_PH, pHValue, status);
                            updateCell(CODE_MPS_EC, ECValue, status);
                            updateCell(CODE_MPS_DO, DoValue, status);
                            updateCell(CODE_MPS_TURBIDITY, TurbidityValue, status);
                            updateCell(CODE_MPS_ORP, ORPValue, status);
                            updateCell(CODE_MPS_TEMP, TempValue, status);

                            //// read data to display
                            //byte[] temp = new byte[PACKET_LENGTH];
                            //Array.Copy(text, i, temp, 0, PACKET_LENGTH);
                            //result += ByteArrayToHexString(temp);
                            ////Remove this packet from temporary buffer
                            bufferIndex = i + MPS_PACKET_LENGTH;
                            i = i + MPS_PACKET_LENGTH - 1;
                        }
                    }
                }
                //Reset buffer
                MPS_buffer_counter = 0;
                Array.Clear(MPS_receive_buffer, 0, MPS_receive_buffer.Length);
                //if (bufferIndex < text.Length)
                //{
                //    MPS_buffer_counter = text.Length - bufferIndex;
                //    //Console.WriteLine("Buffer Counter: " + buffer_counter.ToString() + ", Data Length: " + text.Length);
                //    Array.Copy(text, bufferIndex, MPS_receive_buffer, 0, MPS_buffer_counter);
                //}

                //if (MPS_buffer_counter > MPS_PACKET_LENGTH * 2)
                //{
                    MPS_rx_write = 0;
                    MPS_rx_counter = 0;
                    MPS_rx_buffer = new byte[2048];

                    MPS_receive_buffer = new byte[2048];
                    MPS_buffer_counter = 0;
                //}
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("MPS 0003," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }

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

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //MessageBox.Show("111");
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

                if (serialPortMPS != null && serialPortMPS.IsOpen)
                {
                    serialPortMPS.Close();
                    serialPortMPS.Dispose();
                }
                if (serialPortSAMP != null && serialPortSAMP.IsOpen)
                {
                    serialPortSAMP.Close();
                    serialPortSAMP.Dispose();
                }
                if (serialPortTN != null && serialPortTN.IsOpen)
                {
                    serialPortTN.Close();
                    serialPortTN.Dispose();
                }
                if (serialPortTOC != null && serialPortTOC.IsOpen)
                {
                    serialPortTOC.Close();
                    serialPortTOC.Dispose();
                }
                if (serialPortTP != null && serialPortTP.IsOpen)
                {
                    serialPortTP.Close();
                    serialPortTP.Dispose();
                }
                if (serialPortADAM405x != null && serialPortADAM405x.IsOpen)
                {
                    serialPortADAM405x.Close();
                    serialPortADAM405x.Dispose();
                }

                //MessageBox.Show("123");
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(1);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Application.Exit();
                //throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmReport obj = new frmReport();
            obj.ShowDialog();
        }
    }
}

