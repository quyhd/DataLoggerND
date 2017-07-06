namespace DataLogger
{
    partial class frm5MinuteMPS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.txtCond = new System.Windows.Forms.TextBox();
            this.txtTurb = new System.Windows.Forms.TextBox();
            this.txtDO = new System.Windows.Forms.TextBox();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.txtORP = new System.Windows.Forms.TextBox();
            this.txtpH = new System.Windows.Forms.TextBox();
            this.Unit3 = new System.Windows.Forms.Label();
            this.Unit4 = new System.Windows.Forms.Label();
            this.Unit5 = new System.Windows.Forms.Label();
            this.Unit6 = new System.Windows.Forms.Label();
            this.var6Text = new System.Windows.Forms.Label();
            this.var5Text = new System.Windows.Forms.Label();
            this.var4Text = new System.Windows.Forms.Label();
            this.var3Text = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.var2Text = new System.Windows.Forms.Label();
            this.var1Text = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Unit1 = new System.Windows.Forms.Label();
            this.Unit2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.BackgroundImage = global::DataLogger.Properties.Resources.Shutdown_Box_Red;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Enabled = false;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(184, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 43);
            this.btnExit.TabIndex = 87;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.panel1.Controls.Add(this.lblHeaderTitle);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(10, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 43);
            this.panel1.TabIndex = 104;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(13, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(124, 19);
            this.lblHeaderTitle.TabIndex = 87;
            this.lblHeaderTitle.Text = "5 Minute Data";
            // 
            // txtCond
            // 
            this.txtCond.BackColor = System.Drawing.SystemColors.Window;
            this.txtCond.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCond.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCond.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtCond.Location = new System.Drawing.Point(241, 16);
            this.txtCond.Name = "txtCond";
            this.txtCond.ReadOnly = true;
            this.txtCond.Size = new System.Drawing.Size(74, 23);
            this.txtCond.TabIndex = 103;
            this.txtCond.Text = "---";
            this.txtCond.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTurb
            // 
            this.txtTurb.BackColor = System.Drawing.SystemColors.Window;
            this.txtTurb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTurb.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTurb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtTurb.Location = new System.Drawing.Point(251, 135);
            this.txtTurb.Name = "txtTurb";
            this.txtTurb.ReadOnly = true;
            this.txtTurb.Size = new System.Drawing.Size(64, 23);
            this.txtTurb.TabIndex = 102;
            this.txtTurb.Text = "---";
            this.txtTurb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDO
            // 
            this.txtDO.BackColor = System.Drawing.SystemColors.Window;
            this.txtDO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDO.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtDO.Location = new System.Drawing.Point(251, 101);
            this.txtDO.Name = "txtDO";
            this.txtDO.ReadOnly = true;
            this.txtDO.Size = new System.Drawing.Size(64, 23);
            this.txtDO.TabIndex = 101;
            this.txtDO.Text = "---";
            this.txtDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTemp
            // 
            this.txtTemp.BackColor = System.Drawing.SystemColors.Window;
            this.txtTemp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTemp.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtTemp.Location = new System.Drawing.Point(251, 216);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.ReadOnly = true;
            this.txtTemp.Size = new System.Drawing.Size(64, 23);
            this.txtTemp.TabIndex = 100;
            this.txtTemp.Text = "---";
            this.txtTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtORP
            // 
            this.txtORP.BackColor = System.Drawing.SystemColors.Window;
            this.txtORP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtORP.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtORP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtORP.Location = new System.Drawing.Point(251, 175);
            this.txtORP.Name = "txtORP";
            this.txtORP.ReadOnly = true;
            this.txtORP.Size = new System.Drawing.Size(64, 23);
            this.txtORP.TabIndex = 99;
            this.txtORP.Text = "---";
            this.txtORP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtpH
            // 
            this.txtpH.BackColor = System.Drawing.SystemColors.Window;
            this.txtpH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpH.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtpH.Location = new System.Drawing.Point(251, 62);
            this.txtpH.Name = "txtpH";
            this.txtpH.ReadOnly = true;
            this.txtpH.Size = new System.Drawing.Size(64, 23);
            this.txtpH.TabIndex = 98;
            this.txtpH.Text = "---";
            this.txtpH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Unit3
            // 
            this.Unit3.AutoSize = true;
            this.Unit3.BackColor = System.Drawing.Color.White;
            this.Unit3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.Unit3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Unit3.Location = new System.Drawing.Point(321, 101);
            this.Unit3.Name = "Unit3";
            this.Unit3.Size = new System.Drawing.Size(28, 19);
            this.Unit3.TabIndex = 96;
            this.Unit3.Text = "°C";
            // 
            // Unit4
            // 
            this.Unit4.AutoSize = true;
            this.Unit4.BackColor = System.Drawing.Color.White;
            this.Unit4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.Unit4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Unit4.Location = new System.Drawing.Point(321, 138);
            this.Unit4.Name = "Unit4";
            this.Unit4.Size = new System.Drawing.Size(57, 19);
            this.Unit4.TabIndex = 97;
            this.Unit4.Text = "mg/ L";
            // 
            // Unit5
            // 
            this.Unit5.AutoSize = true;
            this.Unit5.BackColor = System.Drawing.Color.White;
            this.Unit5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.Unit5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Unit5.Location = new System.Drawing.Point(321, 178);
            this.Unit5.Name = "Unit5";
            this.Unit5.Size = new System.Drawing.Size(44, 19);
            this.Unit5.TabIndex = 95;
            this.Unit5.Text = "NTU";
            // 
            // Unit6
            // 
            this.Unit6.AutoSize = true;
            this.Unit6.BackColor = System.Drawing.Color.White;
            this.Unit6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.Unit6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Unit6.Location = new System.Drawing.Point(321, 219);
            this.Unit6.Name = "Unit6";
            this.Unit6.Size = new System.Drawing.Size(66, 19);
            this.Unit6.TabIndex = 94;
            this.Unit6.Text = "mS/cm";
            // 
            // var6Text
            // 
            this.var6Text.AutoSize = true;
            this.var6Text.BackColor = System.Drawing.Color.White;
            this.var6Text.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var6Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.var6Text.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.var6Text.Location = new System.Drawing.Point(170, 215);
            this.var6Text.Name = "var6Text";
            this.var6Text.Size = new System.Drawing.Size(45, 19);
            this.var6Text.TabIndex = 93;
            this.var6Text.Text = "var6";
            // 
            // var5Text
            // 
            this.var5Text.AutoSize = true;
            this.var5Text.BackColor = System.Drawing.Color.White;
            this.var5Text.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var5Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.var5Text.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.var5Text.Location = new System.Drawing.Point(170, 175);
            this.var5Text.Name = "var5Text";
            this.var5Text.Size = new System.Drawing.Size(45, 19);
            this.var5Text.TabIndex = 92;
            this.var5Text.Text = "var5";
            // 
            // var4Text
            // 
            this.var4Text.AutoSize = true;
            this.var4Text.BackColor = System.Drawing.Color.White;
            this.var4Text.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var4Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.var4Text.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.var4Text.Location = new System.Drawing.Point(170, 138);
            this.var4Text.Name = "var4Text";
            this.var4Text.Size = new System.Drawing.Size(45, 19);
            this.var4Text.TabIndex = 91;
            this.var4Text.Text = "var4";
            // 
            // var3Text
            // 
            this.var3Text.AutoSize = true;
            this.var3Text.BackColor = System.Drawing.Color.White;
            this.var3Text.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var3Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.var3Text.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.var3Text.Location = new System.Drawing.Point(170, 100);
            this.var3Text.Name = "var3Text";
            this.var3Text.Size = new System.Drawing.Size(45, 19);
            this.var3Text.TabIndex = 90;
            this.var3Text.Text = "var3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(100)))), ((int)(((byte)(98)))));
            this.label1.Location = new System.Drawing.Point(28, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 35);
            this.label1.TabIndex = 88;
            this.label1.Text = "MPS";
            // 
            // var2Text
            // 
            this.var2Text.AutoSize = true;
            this.var2Text.BackColor = System.Drawing.Color.White;
            this.var2Text.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var2Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.var2Text.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.var2Text.Location = new System.Drawing.Point(170, 62);
            this.var2Text.Name = "var2Text";
            this.var2Text.Size = new System.Drawing.Size(45, 19);
            this.var2Text.TabIndex = 89;
            this.var2Text.Text = "var2";
            // 
            // var1Text
            // 
            this.var1Text.AutoSize = true;
            this.var1Text.BackColor = System.Drawing.Color.White;
            this.var1Text.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var1Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.var1Text.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.var1Text.Location = new System.Drawing.Point(170, 19);
            this.var1Text.Name = "var1Text";
            this.var1Text.Size = new System.Drawing.Size(45, 19);
            this.var1Text.TabIndex = 87;
            this.var1Text.Text = "var1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.Unit1);
            this.panel2.Controls.Add(this.Unit2);
            this.panel2.Controls.Add(this.var2Text);
            this.panel2.Controls.Add(this.var1Text);
            this.panel2.Controls.Add(this.txtCond);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtTurb);
            this.panel2.Controls.Add(this.var3Text);
            this.panel2.Controls.Add(this.txtDO);
            this.panel2.Controls.Add(this.var4Text);
            this.panel2.Controls.Add(this.txtTemp);
            this.panel2.Controls.Add(this.var5Text);
            this.panel2.Controls.Add(this.txtORP);
            this.panel2.Controls.Add(this.var6Text);
            this.panel2.Controls.Add(this.txtpH);
            this.panel2.Controls.Add(this.Unit6);
            this.panel2.Controls.Add(this.Unit3);
            this.panel2.Controls.Add(this.Unit5);
            this.panel2.Controls.Add(this.Unit4);
            this.panel2.Location = new System.Drawing.Point(21, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(392, 258);
            this.panel2.TabIndex = 105;
            // 
            // Unit1
            // 
            this.Unit1.AutoSize = true;
            this.Unit1.BackColor = System.Drawing.Color.White;
            this.Unit1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.Unit1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Unit1.Location = new System.Drawing.Point(321, 19);
            this.Unit1.Name = "Unit1";
            this.Unit1.Size = new System.Drawing.Size(57, 19);
            this.Unit1.TabIndex = 105;
            this.Unit1.Text = "mg/ L";
            // 
            // Unit2
            // 
            this.Unit2.AutoSize = true;
            this.Unit2.BackColor = System.Drawing.Color.White;
            this.Unit2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.Unit2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Unit2.Location = new System.Drawing.Point(321, 65);
            this.Unit2.Name = "Unit2";
            this.Unit2.Size = new System.Drawing.Size(57, 19);
            this.Unit2.TabIndex = 104;
            this.Unit2.Text = "mg/ L";
            // 
            // frm5MinuteMPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.BackgroundImage = global::DataLogger.Properties.Resources._5_min_Data;
            this.ClientSize = new System.Drawing.Size(431, 330);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm5MinuteMPS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.TextBox txtCond;
        private System.Windows.Forms.TextBox txtTurb;
        private System.Windows.Forms.TextBox txtDO;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.TextBox txtORP;
        private System.Windows.Forms.TextBox txtpH;
        private System.Windows.Forms.Label Unit3;
        private System.Windows.Forms.Label Unit4;
        private System.Windows.Forms.Label Unit5;
        private System.Windows.Forms.Label Unit6;
        private System.Windows.Forms.Label var6Text;
        private System.Windows.Forms.Label var5Text;
        private System.Windows.Forms.Label var4Text;
        private System.Windows.Forms.Label var3Text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label var2Text;
        private System.Windows.Forms.Label var1Text;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Unit1;
        private System.Windows.Forms.Label Unit2;
    }
}