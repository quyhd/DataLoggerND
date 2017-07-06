using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLogger.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Resources;
using System.Reflection;
using System.Globalization;
using DataLogger.Utils;

namespace DataLogger
{
    public partial class frmHistoryMPS : Form
    {
        //ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        //CultureInfo cul;            //declare culture info
        LanguageService lang;
        private enum DataViewType
        {
            List,
            Graph
        }
        private enum DataTimeType
        {
            _5Minute,
            _60Minute
        }
        private readonly data_5minute_value_repository db5m = new data_5minute_value_repository();
        private readonly data_60minute_value_repository db60m = new data_60minute_value_repository();

        // Lưu kiểu hiển thị hiện tại: List/Graph
        private DataViewType currentViewType = DataViewType.List;

        // Lưu inteval hiện tại: 5minute/60minute
        private DataTimeType currentTimeType = DataTimeType._5Minute;


        /// <summary>
        /// contructor
        /// </summary>
        public frmHistoryMPS(LanguageService _lang)
        {
            InitializeComponent();
            //res_man = obj_res_man;
            //cul = obj_cul;
            lang = _lang;
            switch_language();
        }
        private void switch_language()
        {
            this.Text = lang.getText("form_history_title");
            this.grbSelect.Text = lang.getText("form_history_select_group_box_title");
            this.grbPreview.Text = lang.getText("form_history_preview_group_box_title");
            // 5min, 1hour            
            lang.setText(lbl5MinData, "form_history_select_group_5_min_data", btn5Minute, EAlign.Center);
            lang.setText(lbl1HourData, "form_history_select_group_1_hour_data", btn60Minute, EAlign.Center);
            // list type, graph type            
            lang.setText(lblListType, "form_history_select_group_list_type", btnListType, EAlign.Center);
            lang.setText(lblGraphType, "form_history_select_group_graph_type", btnGraphType, EAlign.Center);

            this.lblFrom.Text = lang.getText("form_history_select_group_from");
            this.lblTo.Text = lang.getText("form_history_select_group_to");
            this.btnOK.Text = lang.getText("form_history_select_group_button_ok");
            this.btnCancel.Text = lang.getText("form_history_select_group_button_cancel");

            // preview                                    
            lang.setText(lblIntevalTimeName, "form_history_preview_interval_time", lblName, EAlign.Left);
            lang.setText(lblDateFromName, "form_history_select_group_from", lblName, EAlign.Left);
            lang.setText(lblDateToName, "form_history_select_group_to", lblName, EAlign.Left);

            //lblIntevalTimeVal.Text = btn5Minute.IsActive ? "5 Minutes" : "60 Minutes";
            if (btn5Minute.IsActive)
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_5_min_data");

            }
            else
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_1_hour_data");
            }
            //lblViewTypeVal.Text = btnGraphType.IsActive ? "Graph type" : "List type";
            if (btnGraphType.IsActive)
            {
                lang.setText(lblViewType, "form_history_select_group_graph_type");
            }
            else
            {
                lang.setText(lblViewType, "form_history_select_group_list_type");
            }

        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHistoryMPS_Load(object sender, EventArgs e)
        {
            dtpDateTo.Value = DateTime.Now;
            dtpDateFrom.Value = DateTime.Now.AddDays(-1);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListType_Click(object sender, EventArgs e)
        {
            //lblViewType.Text = "List type";
            //lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            //lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            currentViewType = DataViewType.List;

            //reloadViewData(currentViewType, currentTimeType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGraphType_Click(object sender, EventArgs e)
        {
            //lblViewType.Text = "Graph type";
            //lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            //lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            currentViewType = DataViewType.Graph;

            //reloadViewData(currentViewType, currentTimeType);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5Minute_Click(object sender, EventArgs e)
        {
            //lblIntevalTimeVal.Text = "5 Minutes";
            //lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            //lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            currentTimeType = DataTimeType._5Minute;

            //reloadViewData(currentViewType, currentTimeType);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn60Minute_Click(object sender, EventArgs e)
        {
            //lblIntevalTimeVal.Text = "60 Minutes";
            //lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            //lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            currentTimeType = DataTimeType._60Minute;

            //reloadViewData(currentViewType, currentTimeType);
        }


        /// <summary>
        /// Hàm hiển thị, tùy vào viewType là List/Graph và timeType là 5Min/60Min để hiển thị cho phù hợp.
        /// </summary>
        /// <param name="_viewType"></param>
        /// <param name="_timeType"></param>
        private void reloadViewData(DataViewType _viewType, DataTimeType _timeType)
        {
            GlobalVar.moduleSettings = new module_repository().get_all();
            if (_viewType == DataViewType.List)
            {
                chtData.Visible = false;
                dgvData.Columns.Clear();
                dgvData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_mps(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_mps(dtpDateFrom.Value, dtpDateTo.Value);
                }

                // Do dt_source chứa date,hour,minute riêng nhau nên phải tạo một dt_view mới gộp các thành phần lại hiển thị cho đẹp hơn
                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("Date");
                dt_view.Columns.Add("Time");

                for (int moduleSettingsCount = 1; moduleSettingsCount <= GlobalVar.moduleSettings.Count(); moduleSettingsCount++)
                {
                    foreach (var item in GlobalVar.moduleSettings)
                    {
                        string currentvar = "var" + moduleSettingsCount.ToString();
                        if (item.item_name.Equals(currentvar))
                        {
                            dt_view.Columns.Add(item.display_name);
                            //moduleSettingsCount++;
                        }            
                    }
                }
                dt_view.Columns.Add("Status_Val");

                // dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    viewrow = dt_view.NewRow();
                    viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString())).ToString("dd/MM/yyyy");
                    viewrow["Time"] = ((int)row["stored_hour"]).ToString("00") + ":" + ((int)row["stored_minute"]).ToString("00") + ":00";
                    int j = 3;
                    for (int moduleSettingsCount = 1; moduleSettingsCount <= GlobalVar.moduleSettings.Count(); moduleSettingsCount++)
                    {
                        foreach (var item in GlobalVar.moduleSettings)
                        {
                            string currentvar = "var" + moduleSettingsCount.ToString();
                            string currentlabelname = currentvar + "Text";
                            string currentlabelunit = "Unit" + moduleSettingsCount.ToString();
                            if (item.item_name.Equals(currentvar))
                            {
                                viewrow[item.display_name] = row[currentvar];
                            }

                        }
                    }
                    viewrow["Status_Val"] = row["mps_status"];
                    dt_view.Rows.Add(viewrow);
                }
                dgvData.DataSource = dt_view;
                // thêm cột Status có màu phù hợp với status
                DataGridViewImageColumn imgColumnStatus = new DataGridViewImageColumn();
                imgColumnStatus.Name = "Status";
                dgvData.Columns.Add(imgColumnStatus);
                dgvData.Columns["Status_Val"].Visible = false; // ẩn cột status bằng số, chỉ để lại cột status có màu
                int status_val = 0;
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells["Status_Val"].Value != null)
                    {
                        Int32.TryParse(row.Cells["Status_Val"].Value.ToString(), out status_val);
                        if (status_val == 0)
                        {
                            row.Cells["Status"].Value = (System.Drawing.Image)Properties.Resources.Normal_status_x16;
                        }
                        else
                        {
                            row.Cells["Status"].Value = (System.Drawing.Image)Properties.Resources.Fault_status_x16;
                        }
                    }
                    for (int moduleSettingsCount = 1; moduleSettingsCount <= GlobalVar.moduleSettings.Count(); moduleSettingsCount++)
                    {
                        foreach (var item in GlobalVar.moduleSettings)
                        {
                            string currentvar = "var" + moduleSettingsCount.ToString();
                            string currentlabelname = currentvar + "Text";
                            string currentlabelunit = "Unit" + moduleSettingsCount.ToString();
                            if (item.item_name.Equals(currentvar))
                            {
                                if (row.Cells[item.display_name].Value != null)
                                {
                                    if (Convert.ToDouble(row.Cells[item.display_name].Value) < 0)
                                    {
                                        row.Cells[item.display_name].Value = "---";
                                    }
                                }
                            }
                        }
                    }

                }

            }
            else //if(_viewType == DataViewType.Graph)
            {
                dgvData.Visible = false;
                chtData.Series.Clear();
                chtData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_mps(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_mps(dtpDateFrom.Value, dtpDateTo.Value);
                }

                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("StoredDate");
                for (int moduleSettingsCount = 1; moduleSettingsCount <= GlobalVar.moduleSettings.Count(); moduleSettingsCount++)
                {
                    foreach (var item in GlobalVar.moduleSettings)
                    {
                        string currentvar = "var" + moduleSettingsCount.ToString();
                        string currentlabelname = currentvar + "Text";
                        string currentlabelunit = "Unit" + moduleSettingsCount.ToString();
                        if (item.item_name.Equals(currentvar))
                        {
                            dt_view.Columns.Add(item.display_name);
                        }
                    }
                }
                // chuyển dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    // kiểm tra status, chỉ lấy chững status normal
                    int _status = (int)row["mps_status"];
                    if (_status == 0)
                    {
                        viewrow = dt_view.NewRow();
                        DateTime _date = (DateTime)row["stored_date"];
                        int _hour = (int)row["stored_hour"];
                        int _minute = (int)row["stored_minute"];
                        DateTime _rdate = new DateTime(_date.Year, _date.Month, _date.Day, _hour, _minute, 0);

                        viewrow["StoredDate"] = _rdate;
                        for(int moduleSettingsCount = 1; moduleSettingsCount <= GlobalVar.moduleSettings.Count(); moduleSettingsCount++)
                        {
                            foreach (var item in GlobalVar.moduleSettings)
                            {
                                string currentvar = "var" + moduleSettingsCount.ToString();
                                string currentlabelname = currentvar + "Text";
                                string currentlabelunit = "Unit" + moduleSettingsCount.ToString();
                                if (item.item_name.Equals(currentvar))
                                {
                                    //ClearLabel(panel30, item.display_name, currentlabelname);

                                    viewrow[item.display_name] = row[currentvar];
                                }
                            }
                        }
                        dt_view.Rows.Add(viewrow);
                    }
                }

                // tạo biểu đồ mới
                for (int moduleSettingsCount = 1; moduleSettingsCount <= GlobalVar.moduleSettings.Count(); moduleSettingsCount++)
                {
                    foreach (var item in GlobalVar.moduleSettings)
                    {
                        string currentvar = "var" + moduleSettingsCount.ToString();
                        string currentlabelname = currentvar + "Text";
                        string currentlabelunit = "Unit" + moduleSettingsCount.ToString();
                        if (item.item_name.Equals(currentvar))
                        {
                            chtData.Series.Add(item.display_name);
                            chtData.Series[item.display_name].XValueMember = "StoredDate";
                            chtData.Series[item.display_name].YValueMembers = item.display_name;
                            chtData.Series[item.display_name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                            if (moduleSettingsCount == 1)
                            {
                                chtData.Series[item.display_name].Color = Color.Blue;
                            } else if (moduleSettingsCount == 2) {
                                chtData.Series[item.display_name].Color = Color.Red;
                            } else if (moduleSettingsCount == 3) {
                                chtData.Series[item.display_name].Color = Color.Purple;
                            } else if (moduleSettingsCount == 4) {
                                chtData.Series[item.display_name].Color = Color.Peru;
                            } else if (moduleSettingsCount == 5) {
                                chtData.Series[item.display_name].Color = Color.Orange;
                            } else if (moduleSettingsCount == 6) {
                                chtData.Series[item.display_name].Color = Color.Gold;
                            }
                            chtData.Series[item.display_name].BorderWidth = 3;
                        }
                    }
                }
                chtData.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chtData.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chtData.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                chtData.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.WhiteSmoke;
                chtData.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
                chtData.ChartAreas[0].AxisX.Title = "Date";
                chtData.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Lines;

                chtData.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chtData.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chtData.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
                chtData.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.WhiteSmoke;
                chtData.ChartAreas[0].AxisY.Title = "MPS";
                chtData.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;

                chtData.DataSource = dt_view;
                chtData.DataBind();

            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            // không cho phép DateFrom lớn hơn DateTo
            if (dtpDateFrom.Value > dtpDateTo.Value) dtpDateFrom.Value = dtpDateTo.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDateTo_ValueChanged(object sender, EventArgs e)
        {
            // không cho phép DateFrom lớn hơn DateTo
            if (dtpDateFrom.Value > dtpDateTo.Value) dtpDateTo.Value = dtpDateFrom.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lblIntevalTimeVal.Text = btn5Minute.IsActive ? "5 Minutes" : "60 Minutes";
            lblViewType.Text = btnGraphType.IsActive ? "Graph type" : "List type";
            //lblIntevalTimeVal.Text = btn5Minute.IsActive ? "5 Minutes" : "60 Minutes";
            if (btn5Minute.IsActive)
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_5_min_data");

            }
            else
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_1_hour_data");
            }
            //lblViewTypeVal.Text = btnGraphType.IsActive ? "Graph type" : "List type";
            if (btnGraphType.IsActive)
            {
                lang.setText(lblViewType, "form_history_select_group_graph_type");
            }
            else
            {
                lang.setText(lblViewType, "form_history_select_group_list_type");
            }
            lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            reloadViewData(currentViewType, currentTimeType);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
