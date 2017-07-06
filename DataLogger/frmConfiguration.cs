using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Resources;
using System.Reflection;
using System.Globalization;
using DataLogger.Utils;
using System.IO.Ports;

using DataLogger.Entities;
using DataLogger.Data;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using WinformProtocol;

namespace DataLogger
{
    public partial class frmConfiguration : Form
    {
        LanguageService lang;
        // module configuration list
        public string[] MODULE_CONFIG_LIST = { "var1", "var2", "var3", "var4", "var5", "var6" };
        public string[] MODULE_ID_LIST = { "1", "2", "3" };
        DataTable dt = new DataTable();
        module_repository _modules = new module_repository();
        frmNewMain newMain;
        public static Form1 protocol;
        public frmConfiguration()
        {
            InitializeComponent();
        }

        public frmConfiguration(LanguageService _lang)
        {
            InitializeComponent();
            lang = _lang;
            switch_language();
        }
        private void switch_language()
        {
            this.Text = lang.getText("configuration");

            lang.setText(lblStationName, "station_name");
            lang.setText(lblStationID, "station_id");
            lang.setText(lblSocketPort, "socket_port");

            lang.setText(btnSave, "button_save");

        }
        private void frmConfiguration_Load(object sender, EventArgs e)
        {


            refreshDataForControl();
        }
        private bool isOpen(int port)
        {

            int isAvailable = 0;
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
            foreach (IPEndPoint tcpi in tcpConnInfoArray)
            {
                if (tcpi.Port == port)
                {
                    isAvailable = 1;
                    break;
                }
            }
            if (isAvailable == 1)
            {
                return true;
            }
            else return false;
        }

        private void refreshDataForControl()
        {
            // get all comport name from computer.
            string[] availableComportList;
            availableComportList = SerialPort.GetPortNames();
            if (availableComportList.Length <= 0)
            {
                //MessageBox.Show(lang.getText("available_port_null"));
                //this.Close();
                //return;
            }
            // check station info setting
            station existedStationsSetting = new station_repository().get_info();
            if (existedStationsSetting == null)
            {
                existedStationsSetting = new station();
                if (new station_repository().add(ref existedStationsSetting) > 0)
                {
                    // insert ok to database
                }
                else
                {
                    MessageBox.Show(lang.getText("system_error"));
                    return;
                }
            }
            else
            {
                // set data to control from existed station setting
                txtStationName.Text = existedStationsSetting.station_name;
                txtStationID.Text = existedStationsSetting.station_id;
                txtSocketPort.Text = existedStationsSetting.socket_port.ToString();
                if (isOpen(existedStationsSetting.socket_port))
                {
                    this.btnSOCKET.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                }
                else
                {
                    this.btnSOCKET.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                }
                var sortComportList = availableComportList.OrderBy(port => Convert.ToInt32(port.Replace("COM", string.Empty)));
                cbModulePH.Items.Clear();
                cbModuleOrp.Items.Clear();
                cbModuleTemp.Items.Clear();
                cbModuleDO.Items.Clear();
                cbModuleTurb.Items.Clear();
                cbModuleCond.Items.Clear();


            }
            foreach (string itemModuleID in MODULE_ID_LIST)
            {
                cbModulePH.Items.Add(itemModuleID);
                cbModuleOrp.Items.Add(itemModuleID);
                cbModuleTemp.Items.Add(itemModuleID);
                cbModuleDO.Items.Add(itemModuleID);
                cbModuleTurb.Items.Add(itemModuleID);
                cbModuleCond.Items.Add(itemModuleID);
            }

            IEnumerable<module> moduleConfigurationList = checkAndInsertModuleConfiguration();
            foreach (module itemModuleSetting in moduleConfigurationList)
            {
                displayModuleSetting(itemModuleSetting);
            }

        }

        private void displayModuleSetting(module itemModuleSetting)
        {
            string channel_no = itemModuleSetting.channel_number.ToString("0#");
            int module_id = itemModuleSetting.module_id - 1;
            string on = itemModuleSetting.on_value;
            string off = itemModuleSetting.off_value;
            int input_min = itemModuleSetting.input_min;
            int input_max = itemModuleSetting.input_max;
            int output_min = itemModuleSetting.output_min;
            int output_max = itemModuleSetting.output_max;
            double offset = itemModuleSetting.off_set;
            string display_name = itemModuleSetting.display_name;
            string unit = itemModuleSetting.unit;
            string sensorId = itemModuleSetting.sensorId;

            switch (itemModuleSetting.item_name.ToLower())
            {

                case "var1":
                    txtpHChannel.Text = channel_no;
                    cbModulePH.SelectedIndex = module_id;
                    txtpHInputMin.Text = input_min.ToString();
                    txtpHInputMax.Text = input_max.ToString();
                    txtpHOutputMin.Text = output_min.ToString();
                    txtpHOutputMax.Text = output_max.ToString();
                    txtpHOffset.Text = offset.ToString();
                    txtName1.Text = display_name;
                    txtUnit1.Text = unit;
                    txtpHSensorId.Text = sensorId;
                    break;
                case "var2":
                    txtOrpChannel.Text = channel_no;
                    cbModuleOrp.SelectedIndex = module_id;
                    txtOrpInputMin.Text = input_min.ToString();
                    txtOrpInputMax.Text = input_max.ToString();
                    txtOrpOutputMin.Text = output_min.ToString();
                    txtOrpOutputMax.Text = output_max.ToString();
                    txtOrpOffset.Text = offset.ToString();
                    txtName2.Text = display_name;
                    txtUnit2.Text = unit;
                    txtOrpSensorId.Text = sensorId;
                    break;
                case "var3":
                    txtTempChannel.Text = channel_no;
                    cbModuleTemp.SelectedIndex = module_id;
                    txtTempInputMin.Text = input_min.ToString();
                    txtTempInputMax.Text = input_max.ToString();
                    txtTempOutputMin.Text = output_min.ToString();
                    txtTempOutputMax.Text = output_max.ToString();
                    txtTempOffset.Text = offset.ToString();
                    txtName3.Text = display_name;
                    txtUnit3.Text = unit;
                    txtTempSensorId.Text = sensorId;
                    break;
                case "var4":
                    txtDOChannel.Text = channel_no;
                    cbModuleDO.SelectedIndex = module_id;
                    txtDOInputMin.Text = input_min.ToString();
                    txtDOInputMax.Text = input_max.ToString();
                    txtDOOutputMin.Text = output_min.ToString();
                    txtDOOutputMax.Text = output_max.ToString();
                    txtDOOffset.Text = offset.ToString();
                    txtName4.Text = display_name;
                    txtUnit4.Text = unit;
                    txtDOSensorId.Text = sensorId;
                    break;
                case "var5":
                    txtTurbChannel.Text = channel_no;
                    cbModuleTurb.SelectedIndex = module_id;
                    txtTurbInputMin.Text = input_min.ToString();
                    txtTurbInputMax.Text = input_max.ToString();
                    txtTurbOutputMin.Text = output_min.ToString();
                    txtTurbOutputMax.Text = output_max.ToString();
                    txtTurbOffset.Text = offset.ToString();
                    txtName5.Text = display_name;
                    txtUnit5.Text = unit;
                    txtTurbSensorId.Text = sensorId;
                    break;
                case "var6":
                    txtCondChannel.Text = channel_no;
                    cbModuleCond.SelectedIndex = module_id;
                    txtCondInputMin.Text = input_min.ToString();
                    txtCondInputMax.Text = input_max.ToString();
                    txtCondOutputMin.Text = output_min.ToString();
                    txtCondOutputMax.Text = output_max.ToString();
                    txtCondOffset.Text = offset.ToString();
                    txtName6.Text = display_name;
                    txtUnit6.Text = unit;
                    txtCondSensorId.Text = sensorId;
                    break;
                default:
                    break;
            }
        }

        private IEnumerable<module> checkAndInsertModuleConfiguration()
        {
            using (module_repository _modules = new module_repository())
            {
                // get all;
                IEnumerable<module> moduleConfigList = _modules.get_all();

                if (moduleConfigList.Count() == MODULE_CONFIG_LIST.Count())
                {
                    return moduleConfigList;
                }
                else
                {
                    foreach (string itemModuleName in MODULE_CONFIG_LIST)
                    {
                        module objExistedModuleByName = _modules.get_info_by_name(itemModuleName);
                        if (objExistedModuleByName != null)
                        {
                            continue;
                        }
                        else
                        {
                            objExistedModuleByName = new module();
                            objExistedModuleByName.item_name = itemModuleName;
                            if (_modules.add(ref objExistedModuleByName) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                                MessageBox.Show(lang.getText("system_error"));
                                this.Close();
                                return null;
                            }
                        }
                    }
                    moduleConfigList = _modules.get_all();
                    return moduleConfigList;
                }
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            // saving to db
            station objStationSetting = new station_repository().get_info();

            if (isOpen(objStationSetting.socket_port) && (!txtSocketPort.Text.Equals(objStationSetting.socket_port.ToString())))
            {
                //close socket
                try
                {
                    frmNewMain.tcpListener.Stop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ! Cant close this socket.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            try
            {
                objStationSetting.station_name = txtStationName.Text;
                objStationSetting.station_id = txtStationID.Text;
                objStationSetting.socket_port = Convert.ToInt32(txtSocketPort.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cant SAVE !");
                this.Close();
                return;
            }
            try
            {
                if (new station_repository().update(ref objStationSetting) > 0)
                {
                    // ok
                }
                else
                {
                    // fail
                }

                foreach (string itemModule in MODULE_CONFIG_LIST)
                {
                    switch (itemModule.ToLower())
                    {
                        //PH ORP TEMP DO TURB COND
                        case "var1":
                            module objpH = _modules.get_info_by_name(itemModule);
                            objpH.display_name = txtName1.Text;
                            objpH.unit = txtUnit1.Text;
                            objpH.module_id = cbModulePH.SelectedIndex + 1;
                            objpH.channel_number = Convert.ToInt32(txtpHChannel.Text);
                            objpH.input_min = Convert.ToInt32(txtpHInputMin.Text);
                            objpH.input_max = Convert.ToInt32(txtpHInputMax.Text);
                            objpH.output_min = Convert.ToInt32(txtpHOutputMin.Text);
                            objpH.output_max = Convert.ToInt32(txtpHOutputMax.Text);
                            objpH.off_set = Convert.ToInt32(txtpHOffset.Text);
                            objpH.sensorId = txtpHSensorId.Text;
                            if (_modules.update(ref objpH) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                            }
                            break;
                        case "var2":
                            module objOrp = _modules.get_info_by_name(itemModule);
                            objOrp.display_name = txtName2.Text;
                            objOrp.unit = txtUnit2.Text;
                            objOrp.module_id = cbModuleOrp.SelectedIndex + 1;
                            objOrp.channel_number = Convert.ToInt32(txtOrpChannel.Text);
                            objOrp.input_min = Convert.ToInt32(txtOrpInputMin.Text);
                            objOrp.input_max = Convert.ToInt32(txtOrpInputMax.Text);
                            objOrp.output_min = Convert.ToInt32(txtOrpOutputMin.Text);
                            objOrp.output_max = Convert.ToInt32(txtOrpOutputMax.Text);
                            objOrp.off_set = Convert.ToInt32(txtOrpOffset.Text);
                            objOrp.sensorId = txtOrpSensorId.Text;
                            if (_modules.update(ref objOrp) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                            }
                            break;
                        case "var3":
                            module objTemp = _modules.get_info_by_name(itemModule);
                            objTemp.display_name = txtName3.Text;
                            objTemp.module_id = cbModuleTemp.SelectedIndex + 1;
                            objTemp.unit = txtUnit3.Text;
                            objTemp.channel_number = Convert.ToInt32(txtTempChannel.Text);
                            objTemp.input_min = Convert.ToInt32(txtTempInputMin.Text);
                            objTemp.input_max = Convert.ToInt32(txtTempInputMax.Text);
                            objTemp.output_min = Convert.ToInt32(txtTempOutputMin.Text);
                            objTemp.output_max = Convert.ToInt32(txtTempOutputMax.Text);
                            objTemp.off_set = Convert.ToInt32(txtTempOffset.Text);
                            objTemp.sensorId = txtTempSensorId.Text;
                            if (_modules.update(ref objTemp) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                            }
                            break;
                        case "var4":
                            module objDO = _modules.get_info_by_name(itemModule);
                            objDO.display_name = txtName4.Text;
                            objDO.unit = txtUnit4.Text;
                            objDO.module_id = cbModuleDO.SelectedIndex + 1;
                            objDO.channel_number = Convert.ToInt32(txtDOChannel.Text);
                            objDO.input_min = Convert.ToInt32(txtDOInputMin.Text);
                            objDO.input_max = Convert.ToInt32(txtDOInputMax.Text);
                            objDO.output_min = Convert.ToInt32(txtDOOutputMin.Text);
                            objDO.output_max = Convert.ToInt32(txtDOOutputMax.Text);
                            objDO.off_set = Convert.ToInt32(txtDOOffset.Text);
                            objDO.sensorId = txtDOSensorId.Text;
                            if (_modules.update(ref objDO) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                            }
                            break;
                        case "var5":
                            module objTurb = _modules.get_info_by_name(itemModule);
                            objTurb.display_name = txtName5.Text;
                            objTurb.unit = txtUnit5.Text;
                            objTurb.module_id = cbModuleTurb.SelectedIndex + 1;
                            objTurb.channel_number = Convert.ToInt32(txtTurbChannel.Text);
                            objTurb.input_min = Convert.ToInt32(txtTurbInputMin.Text);
                            objTurb.input_max = Convert.ToInt32(txtTurbInputMax.Text);
                            objTurb.output_min = Convert.ToInt32(txtTurbOutputMin.Text);
                            objTurb.output_max = Convert.ToInt32(txtTurbOutputMax.Text);
                            objTurb.off_set = Convert.ToInt32(txtTurbOffset.Text);
                            objTurb.sensorId = txtTurbSensorId.Text;
                            if (_modules.update(ref objTurb) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                            }
                            break;
                        case "var6":
                            module objCond = _modules.get_info_by_name(itemModule);
                            objCond.display_name = txtName6.Text;
                            objCond.unit = txtUnit6.Text;
                            objCond.module_id = cbModuleCond.SelectedIndex + 1;
                            objCond.channel_number = Convert.ToInt32(txtCondChannel.Text);
                            objCond.input_min = Convert.ToInt32(txtCondInputMin.Text);
                            objCond.input_max = Convert.ToInt32(txtCondInputMax.Text);
                            objCond.output_min = Convert.ToInt32(txtCondOutputMin.Text);
                            objCond.output_max = Convert.ToInt32(txtCondOutputMax.Text);
                            objCond.off_set = Convert.ToInt32(txtCondOffset.Text);
                            objCond.sensorId = txtCondSensorId.Text;
                            if (_modules.update(ref objCond) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                            }
                            break;
                        default:
                            break;
                    }
                }
                MessageBox.Show("Success", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshDataForControl();
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("127.0.0.1");
        }
        private void btnSOCKET_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Save change ?", "Important Question", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                station existedStationsSetting = new station_repository().get_info();
                if (existedStationsSetting == null)
                {

                }
                else
                {
                    if (isOpen(existedStationsSetting.socket_port))
                    {
                        this.btnSOCKET.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                        //close socket
                        try
                        {
                            if (Application.OpenForms.OfType<Form1>().Count() == 1)
                                frmNewMain.tcpListener.Stop();
                            {
                                Application.OpenForms.OfType<Form1>().First().Close();

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error ! Cant close this socket.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        this.btnSOCKET.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                        //open socket
                        Int32 port = existedStationsSetting.socket_port;
                        IPAddress localAddr = IPAddress.Parse(frmConfiguration.GetLocalIPAddress());
                        try
                        {
                            if (Application.OpenForms.OfType<Form1>().Count() == 1)
                            {
                                Application.OpenForms.OfType<Form1>().First().Close();
                            }

                            protocol = new Form1(newMain);
                            protocol.Show();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error ! Cant open this socket.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
