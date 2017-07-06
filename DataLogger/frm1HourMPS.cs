using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLogger.Entities;
using System.Resources;
using System.Globalization;
using DataLogger.Utils;
using DataLogger.Data;

namespace DataLogger
{
    public partial class frm1HourMPS : Form
    {
        LanguageService lang;

        public data_value obj_data_value { get; set; }
        public frm1HourMPS()
        {
            InitializeComponent();
        }

        public frm1HourMPS(data_value obj, LanguageService _lang)
        {
            InitializeComponent();
            obj_data_value = obj;
            lang = _lang;
            switch_language();
        }
        private void switch_language()
        {
            this.lblHeaderTitle.Text = lang.getText("form_1_hour_title");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm1HourMPS_Load(object sender, EventArgs e)
        {
            GlobalVar.moduleSettings = new module_repository().get_all();
            for (int i = 1; i <= GlobalVar.moduleSettings.Count(); i++)
            {
                foreach (var item in GlobalVar.moduleSettings)
                {
                    string currentvar = "var" + i.ToString();
                    string currentlabelname = currentvar + "Text";
                    string currentlabelunit = "Unit" + i.ToString();
                    if (item.item_name.Equals(currentvar))
                    {
                        ClearLabel(panel2, item.display_name, currentlabelname);
                        ClearLabel(panel2, item.unit, currentlabelunit);
                    }
                }
            }
            if (obj_data_value.var1 >-1)
            {
                txtCond.Text = obj_data_value.var1.ToString("##0.00");
            }
            else
            {
                txtCond.Text = "---";
            }
            if (obj_data_value.var3 > -1)
            {
                txtDO.Text = obj_data_value.var3.ToString("##0.00");
            }
            else
            {
                txtDO.Text = "---";
            }
            if (obj_data_value.var5 > -1)
            {
                txtORP.Text = obj_data_value.var5.ToString("##0.00");
            }
            else
            {
                txtORP.Text = "---";
            }
            if (obj_data_value.var2 > -1)
            {
                txtpH.Text = obj_data_value.var2.ToString("##0.00");
            }
            else
            {
                txtpH.Text = "---";
            }
            if (obj_data_value.var6 > -1)
            {
                txtTemp.Text = obj_data_value.var6.ToString("##0.00");
            }
            else
            {
                txtTemp.Text = "---";
            }
            if (obj_data_value.var4 > -1)
            {
                txtTurb.Text = obj_data_value.var4.ToString("##0.00");
            }
            else
            {
                txtTurb.Text = "---";
            }


        }
        public void ClearLabel(Control control, string text, string label)
        {
            if (control is Label)
            {
                Label lbl = (Label)control;
                if (lbl.Name.StartsWith(label))
                    lbl.Text = text;

            }
            else
                foreach (Control child in control.Controls)
                {
                    ClearLabel(child, text, label);
                }

        }
    }
}
