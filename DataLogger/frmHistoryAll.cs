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
using DataLogger.Utils;

using System.Resources;
using System.Reflection;
using System.Globalization;

namespace DataLogger
{
    public partial class frmHistoryAll : Form
    {
        //ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        //CultureInfo cul;            //declare culture info
        LanguageService lang;
        private enum DataViewType
        {
            List,
            Graph
        }

        private enum DataViewParameterType
        {
            Analyzer,
            MPS,
            SamplerSystem,
            Station,
            Custom
        }

        private enum DataTimeType
        {
            _5Minute,
            _60Minute
        }

        private readonly data_5minute_value_repository db5m = new data_5minute_value_repository();
        private readonly data_60minute_value_repository db60m = new data_60minute_value_repository();

        private bool isCheckAuto = false;

        // Lưu kiểu hiển thị hiện tại: List/Graph
        private DataViewType currentViewType = DataViewType.List;

        // Lưu kiểu loại tham số hiện tại: Analyzer/MPS/SamplerSystem/Station
        private DataViewParameterType currentViewParameterType = DataViewParameterType.Analyzer;

        // Lưu inteval hiện tại: 5minute/60minute
        private DataTimeType currentTimeType = DataTimeType._5Minute;


        /// <summary>
        /// contructor
        /// </summary>
        public frmHistoryAll(LanguageService _lang)
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
            this.lblGroupSelect.Text = lang.getText("form_history_select_group_box_title");
            this.lblGroupPreview.Text = lang.getText("form_history_preview_group_box_title");
            // 5min, 1hour            
            lang.setText(lbl5MinData, "form_history_select_group_5_min_data", btn5Minute, EAlign.Center);            
            lang.setText(lbl1HourData, "form_history_select_group_1_hour_data", btn60Minute, EAlign.Center);
            // list type, graph type            
            lang.setText(lblListType, "form_history_select_group_list_type", btnListType, EAlign.Center);
            lang.setText(lblGraphType, "form_history_select_group_graph_type", btnGraphType, EAlign.Center);
       
            lang.setText(grbSelectParameter, "form_history_select_custom");
            this.lblFrom.Text = lang.getText("form_history_select_group_from");
            this.lblTo.Text = lang.getText("form_history_select_group_to");
            this.btnOK.Text = lang.getText("form_history_select_group_button_ok");
            this.btnCancel.Text = lang.getText("form_history_select_group_button_cancel");
            this.chkSelectAll.Text = lang.getText("form_history_select_group_select_all");

            // preview
            lang.setText(lblSamplerSystem, "form_history_select_sampler_system");
            lang.setText(lblAnalyzer, "form_history_select_analyzer", lblSamplerSystem, EAlign.Left);
            lang.setText(lblMPS, "form_history_select_mps", lblSamplerSystem, EAlign.Left);            
            lang.setText(lblStation, "form_history_select_station", lblSamplerSystem, EAlign.Left);
            lang.setText(lblIntevalTimeName, "form_history_preview_interval_time", lblSamplerSystem, EAlign.Left);
            lang.setText(lblViewTypeLabel, "form_history_preview_view_type", lblSamplerSystem, EAlign.Left);
            lang.setText(lblDateFromName, "form_history_select_group_from", lblSamplerSystem, EAlign.Left);
            lang.setText(lblDateToName, "form_history_select_group_to", lblSamplerSystem, EAlign.Left);

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
                lang.setText(lblViewTypeVal, "form_history_select_group_graph_type");
            }
            else
            {
                lang.setText(lblViewTypeVal, "form_history_select_group_list_type");
            }

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHistoryAll_Load(object sender, EventArgs e)
        {
            dtpDateTo.Value = DateTime.Now;
            dtpDateFrom.Value = DateTime.Now.AddDays(-1);
            checkedListBoxParameters.Items.Clear();
            checkedListBoxParameters.Items.AddRange(DataLoggerParam.PARAMETER_LIST.Select(p => p.NameDisplay).ToArray());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListType_Click(object sender, EventArgs e)
        {
            currentViewType = DataViewType.List;

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGraphType_Click(object sender, EventArgs e)
        {
            currentViewType = DataViewType.Graph;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5Minute_Click(object sender, EventArgs e)
        {
            currentTimeType = DataTimeType._5Minute;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn60Minute_Click(object sender, EventArgs e)
        {
            currentTimeType = DataTimeType._60Minute;
        }


        /// <summary>
        /// Hàm hiển thị, tùy vào viewType là List/Graph và timeType là 5Min/60Min để hiển thị cho phù hợp.
        /// </summary>
        /// <param name="_viewType"></param>
        /// <param name="_timeType"></param>
        private void reloadViewData(DataViewType _viewType, DataTimeType _timeType, DataViewParameterType _paraType)
        {
            switch (_paraType)
            {
                case DataViewParameterType.Analyzer:
                    reloadViewDataAnalyzer(_viewType, _timeType);
                    break;
                case DataViewParameterType.MPS:
                    reloadViewDataMPS(_viewType, _timeType);
                    break;
                case DataViewParameterType.SamplerSystem:
                    reloadViewDataAutoSampler(_viewType, _timeType);
                    break;
                case DataViewParameterType.Station:
                    reloadViewDataStation(_viewType, _timeType);
                    break;
                case DataViewParameterType.Custom:
                    reloadViewDataCustom(_viewType, _timeType);
                    break;
                default:
                    break;
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
            //lblIntevalTimeVal.Text = btn5Minute.IsActive ? "5 Minutes" : "60 Minutes";
            if (btn5Minute.IsActive)
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_5_min_data");
            }
            else
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_1_hour_data");
            }
            if (btnGraphType.IsActive)
            {
                lang.setText(lblViewTypeVal, "form_history_select_group_graph_type");
            }
            else
            {
                lang.setText(lblViewTypeVal, "form_history_select_group_list_type");
            }
            lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            reloadViewData(currentViewType, currentTimeType, currentViewParameterType);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAnalyzer_Click(object sender, EventArgs e)
        {
            grbSelectParameter.Visible = false;
            
            currentViewParameterType = DataViewParameterType.Analyzer;

        }

        private void btnMPS_Click(object sender, EventArgs e)
        {
            grbSelectParameter.Visible = false;
            
            currentViewParameterType = DataViewParameterType.MPS;
        }

        private void btnSamplerSystem_Click(object sender, EventArgs e)
        {
            grbSelectParameter.Visible = false;

            currentViewParameterType = DataViewParameterType.SamplerSystem;
        }

        private void btnStation_Click(object sender, EventArgs e)
        {
            grbSelectParameter.Visible = false;
            currentViewParameterType = DataViewParameterType.Station;
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            grbSelectParameter.Visible = true;

            currentViewParameterType = DataViewParameterType.Custom;
            lang.setText(grbSelectParameter, "form_history_select_custom");
        }

        /// <summary>
        /// Hàm hiển thị, tùy vào viewType là List/Graph và timeType là 5Min/60Min để hiển thị cho phù hợp.
        /// </summary>
        /// <param name="_viewType"></param>
        /// <param name="_timeType"></param>
        private void reloadViewDataAutoSampler(DataViewType _viewType, DataTimeType _timeType)
        {
            if (_viewType == DataViewType.List)
            {
                chtData.Visible = false;
                dgvData.Columns.Clear();
                dgvData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_sampler(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_sampler(dtpDateFrom.Value, dtpDateTo.Value);
                }

                // Do dt_source chứa date,hour,minute riêng nhau nên phải tạo một dt_view mới gộp các thành phần lại hiển thị cho đẹp hơn
                DataTable dt_view = new DataTable();              
                dt_view.Columns.Add("Date");
                dt_view.Columns.Add("Time");
                dt_view.Columns.Add("Refrigeration_Temperature");
                dt_view.Columns.Add("Bottle_Position");
                dt_view.Columns.Add("Door_Status");
                dt_view.Columns.Add("Equipment_Status");

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

                    viewrow["Refrigeration_Temperature"] = row["refrigeration_temperature"];
                    viewrow["Bottle_Position"] = row["bottle_position"];
                    viewrow["Door_Status"] = row["door_status"];
                    viewrow["Equipment_Status"] = row["equipment_status"];

                    viewrow["Status_Val"] = row["equipment_status"];

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
                    if (row.Cells["Refrigeration_Temperature"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["Refrigeration_Temperature"].Value) < 0)
                        {
                            row.Cells["Refrigeration_Temperature"].Value = "---";
                        }
                    }
                    if (row.Cells["Bottle_Position"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["Bottle_Position"].Value) < 0)
                        {
                            row.Cells["Bottle_Position"].Value = "---";
                        }
                    }
                    if (row.Cells["Door_Status"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["Door_Status"].Value) < 0)
                        {
                            row.Cells["Door_Status"].Value = "---";
                        }
                    }
                    if (row.Cells["Equipment_Status"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["Equipment_Status"].Value) < 0)
                        {
                            row.Cells["Equipment_Status"].Value = "---";
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
                    dt_source = db5m.get_all_sampler(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_sampler(dtpDateFrom.Value, dtpDateTo.Value);
                }

                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("StoredDate");
                dt_view.Columns.Add("Refrigeration_Temperature");
                dt_view.Columns.Add("Bottle_Position");
                dt_view.Columns.Add("Door_Status");
                dt_view.Columns.Add("Equipment_Status");
                dt_view.Columns.Add("Status_Val");
                // chuyển dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    // kiểm tra status, chỉ lấy chững status normal
                    int _status = (int)row["equipment_status"];
                    if (_status == 0)
                    {
                        viewrow = dt_view.NewRow();
                        DateTime _date = (DateTime)row["stored_date"];
                        int _hour = (int)row["stored_hour"];
                        int _minute = (int)row["stored_minute"];
                        DateTime _rdate = new DateTime(_date.Year, _date.Month, _date.Day, _hour, _minute, 0);

                        viewrow["StoredDate"] = _rdate;
                        viewrow["Refrigeration_Temperature"] = row["refrigeration_temperature"];
                        viewrow["Bottle_Position"] = row["bottle_position"];
                        viewrow["Door_Status"] = row["door_status"];
                        viewrow["Equipment_Status"] = row["equipment_status"];

                        viewrow["Status_Val"] = row["equipment_status"];

                        dt_view.Rows.Add(viewrow);
                    }
                }

                // tạo biểu đồ mới
                chtData.Series.Add("Refrigeration_Temperature");
                chtData.Series["Refrigeration_Temperature"].XValueMember = "StoredDate";
                chtData.Series["Refrigeration_Temperature"].YValueMembers = "Refrigeration_Temperature";
                chtData.Series["Refrigeration_Temperature"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Refrigeration_Temperature"].Color = Color.Blue;
                chtData.Series["Refrigeration_Temperature"].BorderWidth = 3;

                chtData.Series.Add("Bottle_Position");
                chtData.Series["Bottle_Position"].XValueMember = "StoredDate";
                chtData.Series["Bottle_Position"].YValueMembers = "Bottle_Position";
                chtData.Series["Bottle_Position"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Bottle_Position"].Color = Color.Red;
                chtData.Series["Bottle_Position"].BorderWidth = 3;

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
                chtData.ChartAreas[0].AxisY.Title = "Auto Sampler (mg/L)";
                chtData.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;

                chtData.DataSource = dt_view;
                chtData.DataBind();

            }
        }

        /// <suary>
        /// Hàm hiển thị, tùy vào viewType là List/Graph và timeType là 5Min/60Min để hiển thị cho phù hợp.
        /// </summary>
        /// <param name="_viewType"></param>
        /// <param name="_timeType"></param>
        private void reloadViewDataMPS(DataViewType _viewType, DataTimeType _timeType)
        {
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
                dt_view.Columns.Add("MPS_pH");
                dt_view.Columns.Add("MPS_EC");
                dt_view.Columns.Add("MPS_DO");
                dt_view.Columns.Add("MPS_Turbidity");
                dt_view.Columns.Add("MPS_ORP");
                dt_view.Columns.Add("MPS_Temp");

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

                    viewrow["MPS_pH"] = row["mps_ph"];
                    viewrow["MPS_EC"] = row["mps_ec"];
                    viewrow["MPS_DO"] = row["mps_do"];
                    viewrow["MPS_Turbidity"] = row["mps_turbidity"];
                    viewrow["MPS_ORP"] = row["mps_orp"];
                    viewrow["MPS_Temp"] = row["mps_temp"];

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
                    if (row.Cells["MPS_pH"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["MPS_pH"].Value) < 0)
                        {
                            row.Cells["MPS_pH"].Value = "---";
                        }
                    }
                    if (row.Cells["MPS_EC"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["MPS_EC"].Value) < 0)
                        {
                            row.Cells["MPS_EC"].Value = "---";
                        }
                    }
                    if (row.Cells["MPS_DO"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["MPS_DO"].Value) < 0)
                        {
                            row.Cells["MPS_DO"].Value = "---";
                        }
                    }
                    if (row.Cells["MPS_Turbidity"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["MPS_Turbidity"].Value) < 0)
                        {
                            row.Cells["MPS_Turbidity"].Value = "---";
                        }
                    }
                    if (row.Cells["MPS_ORP"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["MPS_ORP"].Value) < 0)
                        {
                            row.Cells["MPS_ORP"].Value = "---";
                        }
                    }
                    if (row.Cells["MPS_Temp"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["MPS_Temp"].Value) < 0)
                        {
                            row.Cells["MPS_Temp"].Value = "---";
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
                dt_view.Columns.Add("MPS_pH");
                dt_view.Columns.Add("MPS_EC");
                dt_view.Columns.Add("MPS_DO");
                dt_view.Columns.Add("MPS_Turbidity");
                dt_view.Columns.Add("MPS_ORP");
                dt_view.Columns.Add("MPS_Temp");

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
                        viewrow["MPS_pH"] = row["mps_ph"];
                        viewrow["MPS_EC"] = row["mps_ec"];
                        viewrow["MPS_DO"] = row["mps_do"];
                        viewrow["MPS_Turbidity"] = row["mps_turbidity"];
                        viewrow["MPS_ORP"] = row["mps_orp"];
                        viewrow["MPS_Temp"] = row["mps_temp"];

                        dt_view.Rows.Add(viewrow);
                    }
                }

                // tạo biểu đồ mới
                chtData.Series.Add("MPS_pH");
                chtData.Series["MPS_pH"].XValueMember = "StoredDate";
                chtData.Series["MPS_pH"].YValueMembers = "MPS_pH";
                chtData.Series["MPS_pH"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["MPS_pH"].Color = Color.Blue;
                chtData.Series["MPS_pH"].BorderWidth = 3;

                chtData.Series.Add("MPS_EC");
                chtData.Series["MPS_EC"].XValueMember = "StoredDate";
                chtData.Series["MPS_EC"].YValueMembers = "MPS_EC";
                chtData.Series["MPS_EC"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["MPS_EC"].Color = Color.Red;
                chtData.Series["MPS_EC"].BorderWidth = 3;

                chtData.Series.Add("MPS_DO");
                chtData.Series["MPS_DO"].XValueMember = "StoredDate";
                chtData.Series["MPS_DO"].YValueMembers = "MPS_DO";
                chtData.Series["MPS_DO"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["MPS_DO"].Color = Color.Green;
                chtData.Series["MPS_DO"].BorderWidth = 3;

                chtData.Series.Add("MPS_ORP");
                chtData.Series["MPS_ORP"].XValueMember = "StoredDate";
                chtData.Series["MPS_ORP"].YValueMembers = "MPS_ORP";
                chtData.Series["MPS_ORP"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["MPS_ORP"].Color = Color.Yellow;
                chtData.Series["MPS_ORP"].BorderWidth = 3;

                chtData.Series.Add("MPS_Turbidity");
                chtData.Series["MPS_Turbidity"].XValueMember = "StoredDate";
                chtData.Series["MPS_Turbidity"].YValueMembers = "MPS_Turbidity";
                chtData.Series["MPS_Turbidity"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["MPS_Turbidity"].Color = Color.Cyan;
                chtData.Series["MPS_Turbidity"].BorderWidth = 3;

                chtData.Series.Add("MPS_Temp");
                chtData.Series["MPS_Temp"].XValueMember = "StoredDate";
                chtData.Series["MPS_Temp"].YValueMembers = "MPS_Temp";
                chtData.Series["MPS_Temp"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["MPS_Temp"].Color = Color.DarkViolet;
                chtData.Series["MPS_Temp"].BorderWidth = 3;

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
                chtData.ChartAreas[0].AxisY.Title = "MPS (mg/L)";
                chtData.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;

                chtData.DataSource = dt_view;
                chtData.DataBind();

            }
        }

        /// <summary>
        /// Hàm hiển thị, tùy vào viewType là List/Graph và timeType là 5Min/60Min để hiển thị cho phù hợp.
        /// </summary>
        /// <param name="_viewType"></param>
        /// <param name="_timeType"></param>
        private void reloadViewDataAnalyzer(DataViewType _viewType, DataTimeType _timeType)
        {
            if (_viewType == DataViewType.List)
            {
                chtData.Visible = false;
                dgvData.Columns.Clear();
                dgvData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_analyzer(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_analyzer(dtpDateFrom.Value, dtpDateTo.Value);
                }

                // Do dt_source chứa date,hour,minute riêng nhau nên phải tạo một dt_view mới gộp các thành phần lại hiển thị cho đẹp hơn
                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("Date");
                dt_view.Columns.Add("Time");
                dt_view.Columns.Add("TN");
                dt_view.Columns.Add("TN_Status");
                dt_view.Columns.Add("TN_Status_Img", System.Type.GetType("System.Byte[]"));
                dt_view.Columns.Add("TP");
                dt_view.Columns.Add("TP_Status");
                dt_view.Columns.Add("TP_Status_Img", System.Type.GetType("System.Byte[]"));
                dt_view.Columns.Add("TOC");
                dt_view.Columns.Add("TOC_Status");
                dt_view.Columns.Add("TOC_Status_Img", System.Type.GetType("System.Byte[]"));

                //dt_view.Columns.Add("Status_Val");

                // dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                //int status_val = 0;
                int tn_status_val = 0;
                int tp_status_val = 0;
                int toc_status_val = 0;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    viewrow = dt_view.NewRow();
                    //viewrow["Date"] = row["stored_date"].ToString().Substring(0, 10);
                    //viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString().Substring(0, 10))).ToString("dd/MM/yyyy");
                    //viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString().Substring(0, 10))).ToString("dd/MM/yyyy");
                    viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString())).ToString("dd/MM/yyyy");
                    //viewrow["Date"] = (Convert.ToDateTime((row["stored_date"].ToString().Split(' '))[0])).ToString("dd/MM/yyyy");
                    viewrow["Time"] = ((int)row["stored_hour"]).ToString("00") + ":" + ((int)row["stored_minute"]).ToString("00") + ":00";

                    viewrow["TN"] = row["TN"];
                    viewrow["TP"] = row["TP"];
                    viewrow["TOC"] = row["TOC"];

                    viewrow["TN_Status"] = row["TN_Status"];
                    viewrow["TP_Status"] = row["TP_Status"];
                    viewrow["TOC_Status"] = row["TOC_Status"];

                    //viewrow["Status_Val"] = row["TN_status"];
                    var imageConverter = new ImageConverter();
                    if (viewrow["TN_Status"] != null)
                    {
                        Int32.TryParse(viewrow["TN_Status"].ToString(), out tn_status_val);

                        if (tn_status_val == 0)
                        {
                            viewrow["TN_Status_Img"] = imageConverter.ConvertTo(Properties.Resources.Normal_status_x16, System.Type.GetType("System.Byte[]"));
                        }
                        else
                        {
                            viewrow["TN_Status_Img"] = imageConverter.ConvertTo((System.Drawing.Image)Properties.Resources.Fault_status_x16, System.Type.GetType("System.Byte[]"));
                        }
                    }
                    if (viewrow["TP_Status"] != null)
                    {
                        Int32.TryParse(viewrow["TP_Status"].ToString(), out tp_status_val);

                        if (tp_status_val == 0)
                        {
                            viewrow["TP_Status_Img"] = imageConverter.ConvertTo(Properties.Resources.Normal_status_x16, System.Type.GetType("System.Byte[]"));
                        }
                        else
                        {
                            viewrow["TP_Status_Img"] = imageConverter.ConvertTo((System.Drawing.Image)Properties.Resources.Fault_status_x16, System.Type.GetType("System.Byte[]"));
                        }
                    }
                    if (viewrow["TOC_Status"] != null)
                    {
                        Int32.TryParse(viewrow["TOC_Status"].ToString(), out toc_status_val);

                        if (toc_status_val == 0)
                        {
                            viewrow["TOC_Status_Img"] = imageConverter.ConvertTo(Properties.Resources.Normal_status_x16, System.Type.GetType("System.Byte[]"));
                        }
                        else
                        {
                            viewrow["TOC_Status_Img"] = imageConverter.ConvertTo((System.Drawing.Image)Properties.Resources.Fault_status_x16, System.Type.GetType("System.Byte[]"));
                        }
                    }
                    if (viewrow["TN"] != null)
                    {
                        if (Convert.ToDouble(viewrow["TN"]) < 0)
                        {
                            viewrow["TN"] = "---";
                        }
                    }
                    if (viewrow["TOC"] != null)
                    {
                        if (Convert.ToDouble(viewrow["TOC"]) < 0)
                        {
                            viewrow["TOC"] = "---";
                        }
                    }
                    if (viewrow["TP"] != null)
                    {
                        if (Convert.ToDouble(viewrow["TP"]) < 0)
                        {
                            viewrow["TP"] = "---";
                        }
                    }


                    dt_view.Rows.Add(viewrow);
                }
                dgvData.DataSource = dt_view;

                dgvData.Columns["TN_Status"].Visible = false;
                dgvData.Columns["TP_Status"].Visible = false;
                dgvData.Columns["TOC_Status"].Visible = false;
            }
            else //if(_viewType == DataViewType.Graph)
            {
                dgvData.Visible = false;
                chtData.Series.Clear();
                chtData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_analyzer(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_analyzer(dtpDateFrom.Value, dtpDateTo.Value);
                }

                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("StoredDate");
                dt_view.Columns.Add("TN");
                dt_view.Columns.Add("TN_Status");
                dt_view.Columns.Add("TP");
                dt_view.Columns.Add("TP_Status");
                dt_view.Columns.Add("TOC");
                dt_view.Columns.Add("TOC_Status");
                if (dt_source == null)
                {
                    return;
                }
                // chuyển dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                foreach (DataRow row in dt_source.Rows)
                {
                    // kiểm tra status, chỉ lấy chững status normal
                    int _status = (int)row["TN_status"];
                    if (_status == 0)
                    {
                        viewrow = dt_view.NewRow();
                        DateTime _date = (DateTime)row["stored_date"];
                        int _hour = (int)row["stored_hour"];
                        int _minute = (int)row["stored_minute"];
                        DateTime _rdate = new DateTime(_date.Year, _date.Month, _date.Day, _hour, _minute, 0);

                        viewrow["StoredDate"] = _rdate;
                        viewrow["TN"] = row["TN"];
                        viewrow["TP"] = row["TP"];
                        viewrow["TOC"] = row["TOC"];

                        viewrow["TN_Status"] = row["TN_Status"];
                        viewrow["TP_Status"] = row["TP_Status"];
                        viewrow["TOC_Status"] = row["TOC_Status"];

                        dt_view.Rows.Add(viewrow);
                    }
                }

                // tạo biểu đồ mới
                chtData.Series.Add("TN");
                chtData.Series["TN"].XValueMember = "StoredDate";
                chtData.Series["TN"].YValueMembers = "TN";
                chtData.Series["TN"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["TN"].Color = Color.Blue;

                chtData.Series.Add("TOC");
                chtData.Series["TOC"].XValueMember = "StoredDate";
                chtData.Series["TOC"].YValueMembers = "TOC";
                chtData.Series["TOC"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["TOC"].Color = Color.Maroon;

                chtData.Series.Add("TP");
                chtData.Series["TP"].XValueMember = "StoredDate";
                chtData.Series["TP"].YValueMembers = "TP";
                chtData.Series["TP"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["TP"].Color = Color.DarkGreen;

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
                chtData.ChartAreas[0].AxisY.Title = "TN (mg/L)";
                chtData.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;

                chtData.DataSource = dt_view;
                chtData.DataBind();

            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="_viewType"></param>
        /// <param name="_timeType"></param>
        private void reloadViewDataStation(DataViewType _viewType, DataTimeType _timeType)
        {
            if (_viewType == DataViewType.List)
            {
                chtData.Visible = false;
                dgvData.Columns.Clear();
                dgvData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_station(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_station(dtpDateFrom.Value, dtpDateTo.Value);
                }

                // Do dt_source chứa date,hour,minute riêng nhau nên phải tạo một dt_view mới gộp các thành phần lại hiển thị cho đẹp hơn
                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("Date");
                dt_view.Columns.Add("Time");

                dt_view.Columns.Add("Power");
                dt_view.Columns.Add("UPS");
                dt_view.Columns.Add("Door");
                dt_view.Columns.Add("Fire");
                dt_view.Columns.Add("Flow");
                dt_view.Columns.Add("PumpLAM");
                dt_view.Columns.Add("PumpLRS");
                dt_view.Columns.Add("PumpLFLT");
                dt_view.Columns.Add("PumpRAM");
                dt_view.Columns.Add("PumpRRS");
                dt_view.Columns.Add("PumpRFLT");
                dt_view.Columns.Add("Temperature");
                dt_view.Columns.Add("Humidity");

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
                    //viewrow["Date"] = row["stored_date"].ToString().Substring(0, 10);
                    //viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString())).ToString("dd/MM/yyyy");
                    //viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString().Substring(0, 10))).ToString("dd/MM/yyyy");
                    viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString())).ToString("dd/MM/yyyy");
                    //viewrow["Date"] = (Convert.ToDateTime((row["stored_date"].ToString().Split(' '))[0])).ToString("dd/MM/yyyy");
                    viewrow["Time"] = ((int)row["stored_hour"]).ToString("00") + ":" + ((int)row["stored_minute"]).ToString("00") + ":00";

                    viewrow["Power"] = row["module_power"];
                    viewrow["UPS"] = row["module_ups"];
                    viewrow["Door"] = row["module_door"];
                    viewrow["Fire"] = row["module_fire"];
                    viewrow["Flow"] = row["module_flow"];
                    viewrow["PumpLAM"] = row["module_pumplam"];
                    viewrow["PumpLRS"] = row["module_pumplrs"];
                    viewrow["PumpLFLT"] = row["module_pumplflt"];
                    viewrow["PumpRAM"] = row["module_pumpram"];
                    viewrow["PumpRRS"] = row["module_pumprrs"];
                    viewrow["PumpRFLT"] = row["module_pumprflt"];
                    viewrow["Temperature"] = row["module_temperature"];
                    viewrow["Humidity"] = row["module_humidity"];

                    //viewrow["Status_Val"] = row["station_status"];
                    viewrow["Status_Val"] = row["module_humidity"];

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

                    double tempValue = Convert.ToDouble(row.Cells["Power"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["Power"].Value = "ON";
                            //this.picStationStatusMainPower.BackgroundImage = global::DataLogger.Properties.Resources.on_icon_68x68;
                        }
                        else
                        {
                            row.Cells["Power"].Value = "OFF";
                            //this.picStationStatusMainPower.BackgroundImage = global::DataLogger.Properties.Resources.off_icon_68x68;
                        }
                    }
                    else
                    {
                        row.Cells["Power"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["UPS"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["UPS"].Value = "ON";
                            //this.picStationStatusUPS.BackgroundImage = global::DataLogger.Properties.Resources.on_icon_68x68;
                        }
                        else
                        {
                            row.Cells["UPS"].Value = "OFF";
                            //this.picStationStatusUPS.BackgroundImage = global::DataLogger.Properties.Resources.off_icon_68x68;
                        }
                    }
                    else
                    {
                        row.Cells["UPS"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["Door"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["Door"].Value = "CLOSE";
                            //this.picStationStatusMainDoor.BackgroundImage = global::DataLogger.Properties.Resources.door_close_68x68;
                        }
                        else
                        {
                            row.Cells["Door"].Value = "OPEN";
                            //this.picStationStatusMainDoor.BackgroundImage = global::DataLogger.Properties.Resources.door_open_68x68;
                        }
                    }
                    else
                    {
                        row.Cells["Door"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["Fire"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["Fire"].Value = "NORMAL";
                            //this.picStationStatusFireDetector.BackgroundImage = global::DataLogger.Properties.Resources.house_normal_68x68;
                        }
                        else
                        {
                            row.Cells["Fire"].Value = "FIRE";
                            //this.picStationStatusFireDetector.BackgroundImage = global::DataLogger.Properties.Resources.house_fire_68x68;
                        }
                    }
                    else
                    {
                        row.Cells["Fire"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["Flow"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["Flow"].Value = "OPEN";
                            //this.picSamplerTank.Image = global::DataLogger.Properties.Resources.SamplerTankerWater;

                        }
                        else
                        {
                            row.Cells["Flow"].Value = "CLOSE";
                            //this.picSamplerTank.Image = null;
                            //this.picDrainValue.BackgroundImage = global::DataLogger.Properties.Resources.Valve_Close;
                        }
                    }
                    else
                    {
                        row.Cells["Flow"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["PumpLAM"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["PumpLAM"].Value = "MANUAL";
                            //this.picPump1_RunningType.Image = global::DataLogger.Properties.Resources.Manual_56x56;
                        }
                        else
                        {
                            row.Cells["PumpLAM"].Value = "AUTO";
                            //this.picPump1_RunningType.Image = global::DataLogger.Properties.Resources.Auto_56x56;
                        }
                    }
                    else
                    {
                        row.Cells["PumpLAM"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["PumpLRS"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["PumpLRS"].Value = "STOP";
                            //this.picPumpingSystemLRS.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                            //this.lblPumpLRS.Text = "Stop";
                        }
                        else
                        {
                            row.Cells["PumpLRS"].Value = "RUN";
                            //this.picPumpingSystemLRS.Image = global::DataLogger.Properties.Resources.Run_42x42;
                            //this.lblPumpLRS.Text = "Run";
                        }
                    }
                    else
                    {
                        row.Cells["PumpLRS"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["PumpLFLT"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["PumpLFLT"].Value = "FAULT";
                            //this.picPumpingSystemLFLT.Image = global::DataLogger.Properties.Resources.Fault_42x42;
                            //this.lblPumpLFLT.Text = "Fault";
                        }
                        else
                        {
                            row.Cells["PumpLFLT"].Value = "NORMAL";
                            //this.picPumpingSystemLFLT.Image = global::DataLogger.Properties.Resources.Run_42x42;
                            //this.lblPumpLFLT.Text = "Normal";
                        }
                    }
                    else
                    {
                        row.Cells["PumpLFLT"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["PumpRAM"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["PumpRAM"].Value = "AUTO";
                            //this.picPump2_RunningType.Image = global::DataLogger.Properties.Resources.Auto_56x56;
                        }
                        else
                        {
                            row.Cells["PumpRAM"].Value = "MANUAL";
                            //this.picPump2_RunningType.Image = global::DataLogger.Properties.Resources.Manual_56x56;
                        }
                    }
                    else
                    {
                        row.Cells["PumpRAM"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["PumpRRS"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["PumpRRS"].Value = "STOP";
                            //this.picPumpingSystemRRS.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                            //this.lblPumpRRS.Text = "Stop";
                        }
                        else
                        {
                            row.Cells["PumpRRS"].Value = "RUN";
                            //this.picPumpingSystemRRS.Image = global::DataLogger.Properties.Resources.Run_42x42;
                            //this.lblPumpRRS.Text = "Run";
                        }
                    }
                    else
                    {
                        row.Cells["PumpRRS"].Value = "---";
                    }

                    tempValue = Convert.ToDouble(row.Cells["PumpRFLT"].Value);
                    if (tempValue > -1)
                    {
                        if (tempValue == 1)
                        {
                            row.Cells["PumpRFLT"].Value = "FAULT";
                            //this.picPumpingSystemRFLT.Image = global::DataLogger.Properties.Resources.Fault_42x42;
                            //this.lblPumpRFLT.Text = "Fault";
                        }
                        else
                        {
                            row.Cells["PumpRFLT"].Value = "NORMAL";
                            //this.picPumpingSystemRFLT.Image = global::DataLogger.Properties.Resources.Run_42x42;
                            //this.lblPumpRFLT.Text = "Normal";
                        }
                    }
                    else
                    {
                        row.Cells["PumpRFLT"].Value = "---";
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
                    dt_source = db5m.get_all_station(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_station(dtpDateFrom.Value, dtpDateTo.Value);
                }

                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("StoredDate");
                dt_view.Columns.Add("Power");
                dt_view.Columns.Add("UPS");
                dt_view.Columns.Add("Door");
                dt_view.Columns.Add("Fire");
                dt_view.Columns.Add("Flow");
                dt_view.Columns.Add("PumpLAM");
                dt_view.Columns.Add("PumpLRS");
                dt_view.Columns.Add("PumpLFLT");
                dt_view.Columns.Add("PumpRAM");
                dt_view.Columns.Add("PumpRRS");
                dt_view.Columns.Add("PumpRFLT");
                dt_view.Columns.Add("Temperature");
                dt_view.Columns.Add("Humidity");

                // chuyển dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    // kiểm tra status, chỉ lấy chững status normal
                    //int _status = (int)row["mps_status"];
                    int _status = (int)row["module_power"];
                    //if (_status == 0)
                    if (_status >= 0)
                    {
                        viewrow = dt_view.NewRow();
                        DateTime _date = (DateTime)row["stored_date"];
                        int _hour = (int)row["stored_hour"];
                        int _minute = (int)row["stored_minute"];
                        DateTime _rdate = new DateTime(_date.Year, _date.Month, _date.Day, _hour, _minute, 0);

                        viewrow["StoredDate"] = _rdate;
                        viewrow["Power"] = row["module_power"];
                        viewrow["UPS"] = row["module_ups"];
                        viewrow["Door"] = row["module_door"];
                        viewrow["Fire"] = row["module_fire"];
                        viewrow["Flow"] = row["module_flow"];
                        viewrow["PumpLAM"] = row["module_pumplam"];
                        viewrow["PumpLRS"] = row["module_pumplrs"];
                        viewrow["PumpLFLT"] = row["module_pumplflt"];
                        viewrow["PumpRAM"] = row["module_pumpram"];
                        viewrow["PumpRRS"] = row["module_pumprrs"];
                        viewrow["PumpRFLT"] = row["module_pumprflt"];
                        viewrow["Temperature"] = row["module_temperature"];
                        viewrow["Humidity"] = row["module_humidity"];

                        dt_view.Rows.Add(viewrow);
                    }
                }

                // tạo biểu đồ mới
                chtData.Series.Add("Power");
                chtData.Series["Power"].XValueMember = "StoredDate";
                chtData.Series["Power"].YValueMembers = "Power";
                chtData.Series["Power"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Power"].Color = Color.Blue;
                chtData.Series["Power"].BorderWidth = 3;

                chtData.Series.Add("UPS");
                chtData.Series["UPS"].XValueMember = "StoredDate";
                chtData.Series["UPS"].YValueMembers = "UPS";
                chtData.Series["UPS"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["UPS"].Color = Color.Red;
                chtData.Series["UPS"].BorderWidth = 3;

                chtData.Series.Add("Door");
                chtData.Series["Door"].XValueMember = "StoredDate";
                chtData.Series["Door"].YValueMembers = "Door";
                chtData.Series["Door"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Door"].Color = Color.Green;
                chtData.Series["Door"].BorderWidth = 3;

                chtData.Series.Add("Fire");
                chtData.Series["Fire"].XValueMember = "StoredDate";
                chtData.Series["Fire"].YValueMembers = "Fire";
                chtData.Series["Fire"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Fire"].Color = Color.Yellow;
                chtData.Series["Fire"].BorderWidth = 3;

                chtData.Series.Add("Flow");
                chtData.Series["Flow"].XValueMember = "StoredDate";
                chtData.Series["Flow"].YValueMembers = "Flow";
                chtData.Series["Flow"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Flow"].Color = Color.Cyan;
                chtData.Series["Flow"].BorderWidth = 3;

                chtData.Series.Add("PumpLAM");
                chtData.Series["PumpLAM"].XValueMember = "StoredDate";
                chtData.Series["PumpLAM"].YValueMembers = "PumpLAM";
                chtData.Series["PumpLAM"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["PumpLAM"].Color = Color.DarkViolet;
                chtData.Series["PumpLAM"].BorderWidth = 3;

                chtData.Series.Add("PumpLRS");
                chtData.Series["PumpLRS"].XValueMember = "StoredDate";
                chtData.Series["PumpLRS"].YValueMembers = "PumpLRS";
                chtData.Series["PumpLRS"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["PumpLRS"].Color = Color.BlueViolet;
                chtData.Series["PumpLRS"].BorderWidth = 3;

                chtData.Series.Add("PumpLFLT");
                chtData.Series["PumpLFLT"].XValueMember = "StoredDate";
                chtData.Series["PumpLFLT"].YValueMembers = "PumpLFLT";
                chtData.Series["PumpLFLT"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["PumpLFLT"].Color = Color.ForestGreen;
                chtData.Series["PumpLFLT"].BorderWidth = 3;

                chtData.Series.Add("PumpRAM");
                chtData.Series["PumpRAM"].XValueMember = "StoredDate";
                chtData.Series["PumpRAM"].YValueMembers = "PumpRAM";
                chtData.Series["PumpRAM"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["PumpRAM"].Color = Color.DeepPink;
                chtData.Series["PumpRAM"].BorderWidth = 3;

                chtData.Series.Add("PumpRRS");
                chtData.Series["PumpRRS"].XValueMember = "StoredDate";
                chtData.Series["PumpRRS"].YValueMembers = "PumpRRS";
                chtData.Series["PumpRRS"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["PumpRRS"].Color = Color.Goldenrod;
                chtData.Series["PumpRRS"].BorderWidth = 3;

                chtData.Series.Add("PumpRFLT");
                chtData.Series["PumpRFLT"].XValueMember = "StoredDate";
                chtData.Series["PumpRFLT"].YValueMembers = "PumpRFLT";
                chtData.Series["PumpRFLT"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["PumpRFLT"].Color = Color.Purple;
                chtData.Series["PumpRFLT"].BorderWidth = 3;

                chtData.Series.Add("Temperature");
                chtData.Series["Temperature"].XValueMember = "StoredDate";
                chtData.Series["Temperature"].YValueMembers = "Temperature";
                chtData.Series["Temperature"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Temperature"].Color = Color.PowderBlue;
                chtData.Series["Temperature"].BorderWidth = 3;

                chtData.Series.Add("Humidity");
                chtData.Series["Humidity"].XValueMember = "StoredDate";
                chtData.Series["Humidity"].YValueMembers = "Humidity";
                chtData.Series["Humidity"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Humidity"].Color = Color.PaleGreen;
                chtData.Series["Humidity"].BorderWidth = 3;

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
                chtData.ChartAreas[0].AxisY.Title = "Station";
                chtData.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;

                chtData.DataSource = dt_view;
                chtData.DataBind();

            }
        }




        private void reloadViewDataCustom(DataViewType _viewType, DataTimeType _timeType)
        {
            DataLoggerParam.PARAMETER_LIST.ForEach(p => p.Selected = false);

            foreach (Object item in checkedListBoxParameters.CheckedItems)
            {
                string _key = item.ToString();
                ParamInfo _pinfo = DataLoggerParam.PARAMETER_LIST.FirstOrDefault(p => p.NameDisplay == _key);
                if (_pinfo != null)
                {
                    _pinfo.Selected = true;
                }
            }

            IEnumerable<string> _statusNameList = DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected && p.HasStatus).Select(p => p.StatusNameDB).ToList();
            IEnumerable<string> _paramNameList = DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected).Select(p => p.NameDB).ToList();
            List<string> _paramListForQuery = _paramNameList.Concat(_statusNameList).ToList();


            if (_viewType == DataViewType.List)
            {
                chtData.Visible = false;
                dgvData.Columns.Clear();
                dgvData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_custom(dtpDateFrom.Value, dtpDateTo.Value, _paramListForQuery);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_custom(dtpDateFrom.Value, dtpDateTo.Value, _paramListForQuery);
                }

                // Do dt_source chứa date,hour,minute riêng nhau nên phải tạo một dt_view mới gộp các thành phần lại hiển thị cho đẹp hơn
                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("Date");
                dt_view.Columns.Add("Time");

                foreach (ParamInfo item in DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected))
                {
                    dt_view.Columns.Add(item.NameDisplay);
                    if (item.HasStatus)
                        dt_view.Columns.Add(item.StatusNameVisible);
                }

                // dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    viewrow = dt_view.NewRow();
                    //viewrow["Date"] = row["stored_date"].ToString().Substring(0, 10);
                    viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString())).ToString("dd/MM/yyyy");
                    viewrow["Time"] = ((int)row["stored_hour"]).ToString("00") + ":" + ((int)row["stored_minute"]).ToString("00") + ":00";

                    foreach (ParamInfo item in DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected))
                    {
                        viewrow[item.NameDisplay] = row[item.NameDB];
                        if (item.HasStatus)
                            viewrow[item.StatusNameVisible] = row[item.StatusNameDB];

                    }

                    dt_view.Rows.Add(viewrow);
                }
                dgvData.DataSource = dt_view;

                // thêm cột Status có màu phù hợp với status
                foreach (ParamInfo item in DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected))
                {
                    if (item.HasStatus)
                    {
                        int cindex = dgvData.Columns[item.StatusNameVisible].Index;

                        DataGridViewImageColumn imgColumnStatus = new DataGridViewImageColumn();
                        imgColumnStatus.Name = item.StatusNameDisplay;
                        dgvData.Columns.Insert(cindex, imgColumnStatus);
                        dgvData.Columns[item.StatusNameVisible].Visible = false; // ẩn cột status bằng số, chỉ để lại cột status có màu
                    }
                }

                // chuẩn hóa dữ liệu hiển thị: chọn màu status, lọc giá trị âm
                int status_val = 0;
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    foreach (ParamInfo item in DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected))
                    {
                        // chọn màu status
                        if (item.HasStatus)
                        {
                            if (row.Cells[item.StatusNameVisible].Value != null)
                            {
                                Int32.TryParse(row.Cells[item.StatusNameVisible].Value.ToString(), out status_val);

                                if (status_val == 0)
                                {
                                    row.Cells[item.StatusNameDisplay].Value = (System.Drawing.Image)Properties.Resources.Normal_status_x16;
                                }
                                else
                                {
                                    row.Cells[item.StatusNameDisplay].Value = (System.Drawing.Image)Properties.Resources.Fault_status_x16;
                                }
                            }
                        }

                        // lọc giá trị âm
                        if (Convert.ToDouble(row.Cells[item.NameDisplay].Value) < 0)
                        {
                            row.Cells[item.NameDisplay].Value = "---";
                        }
                        else
                        {
                            double tempValue = Convert.ToDouble(row.Cells[item.NameDisplay].Value);
                            string displayValueTemp = Convert.ToString(row.Cells[item.NameDisplay].Value);
                            switch (item.NameDB)
                            {
                                case "module_power":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "ON";
                                    }
                                    else
                                    {
                                        displayValueTemp = "OFF";
                                    }
                                    break;
                                case "module_ups":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "ON";
                                    }
                                    else
                                    {
                                        displayValueTemp = "OFF";
                                    }
                                    break;
                                case "module_door":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "CLOSE";
                                    }
                                    else
                                    {
                                        displayValueTemp = "OPEN";
                                    }
                                    break;
                                case "module_fire":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "NORMAL";
                                    }
                                    else
                                    {
                                        displayValueTemp = "FIRE";
                                    }
                                    break;
                                case "module_flow":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "OPEN";
                                    }
                                    else
                                    {
                                        displayValueTemp = "CLOSE";
                                    }
                                    break;
                                case "module_pumplam":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "MANUAL";
                                    }
                                    else
                                    {
                                        displayValueTemp = "AUTO";
                                    }
                                    break;
                                case "module_pumplrs":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "STOP";
                                    }
                                    else
                                    {
                                        displayValueTemp = "RUN";
                                    }
                                    break;
                                case "module_pumplflt":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "FAULT";
                                    }
                                    else
                                    {
                                        displayValueTemp = "NORMAL";
                                    }
                                    break;
                                case "module_pumpram":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "AUTO";
                                    }
                                    else
                                    {
                                        displayValueTemp = "MANUAL";
                                    }
                                    break;
                                case "module_pumprrs":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "STOP";
                                    }
                                    else
                                    {
                                        displayValueTemp = "RUN";
                                    }
                                    break;
                                case "module_pumprflt":
                                    if (tempValue == 1)
                                    {
                                        displayValueTemp = "FAULT";
                                    }
                                    else
                                    {
                                        displayValueTemp = "NORMAL";
                                    }
                                    break;
                                default:
                                    break;
                            }
                            row.Cells[item.NameDisplay].Value = displayValueTemp;
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
                    dt_source = db5m.get_all_custom(dtpDateFrom.Value, dtpDateTo.Value, _paramListForQuery);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_custom(dtpDateFrom.Value, dtpDateTo.Value, _paramListForQuery);
                }

                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("StoredDate");
                foreach (ParamInfo item in DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected))
                {
                    dt_view.Columns.Add(item.NameDisplay);
                }

                // chuyển dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    bool allowAdd = true;
                    foreach (ParamInfo item in DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected))
                    {
                        if (item.HasStatus)
                        {
                            // kiểm tra status, chỉ lấy chững status normal
                            int _status = (int)row[item.StatusNameDB];
                            if (_status == 0)
                            {
                                viewrow = dt_view.NewRow();
                                DateTime _date = (DateTime)row["stored_date"];
                                int _hour = (int)row["stored_hour"];
                                int _minute = (int)row["stored_minute"];
                                DateTime _rdate = new DateTime(_date.Year, _date.Month, _date.Day, _hour, _minute, 0);

                                viewrow["StoredDate"] = _rdate;
                                viewrow[item.NameDisplay] = row[item.NameDB];
                            }
                            else
                            {
                                allowAdd = false;
                                break;
                            }
                        }
                    }
                    if (allowAdd)
                    {
                        dt_view.Rows.Add(viewrow);
                    }
                }

                // tạo biểu đồ mới
                foreach (ParamInfo item in DataLoggerParam.PARAMETER_LIST.Where(p => p.Selected))
                {
                    chtData.Series.Add(item.NameDisplay);
                    chtData.Series[item.NameDisplay].XValueMember = "StoredDate";
                    chtData.Series[item.NameDisplay].YValueMembers = item.NameDisplay;
                    chtData.Series[item.NameDisplay].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    chtData.Series[item.NameDisplay].Color = item.GraphColor;// Color.Blue;
                    chtData.Series[item.NameDisplay].BorderWidth = 3;
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
                chtData.ChartAreas[0].AxisY.Title = "Data";
                chtData.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;

                chtData.DataSource = dt_view;
                chtData.DataBind();

            }
        }



        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (isCheckAuto) return;

            isCheckAuto = true;
            bool state = chkSelectAll.Checked;
            for (int i = 0; i < checkedListBoxParameters.Items.Count; i++)
                checkedListBoxParameters.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));
            isCheckAuto = false;
        }

        private void checkedListBoxParameters_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isCheckAuto) return;

            isCheckAuto = true;
            // Get the current number checked.
            int num_checked = checkedListBoxParameters.CheckedItems.Count;

            // See if the item is being checked or unchecked.
            if ((e.CurrentValue != CheckState.Checked) && (e.NewValue == CheckState.Checked)) num_checked++;
            if ((e.CurrentValue == CheckState.Checked) && (e.NewValue != CheckState.Checked)) num_checked--;

            if (num_checked == 0)
            {
                chkSelectAll.CheckState = CheckState.Unchecked;
            }
            else if (num_checked == checkedListBoxParameters.Items.Count)
            {
                chkSelectAll.CheckState = CheckState.Checked;
            }
            else
            {
                chkSelectAll.CheckState = CheckState.Indeterminate;
            }
            isCheckAuto = false;
        }

    }
}
