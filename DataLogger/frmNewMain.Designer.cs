namespace DataLogger
{
    partial class frmNewMain
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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewMain));
            this.bgwMonthlyReport = new System.ComponentModel.BackgroundWorker();
            this.panel30 = new System.Windows.Forms.Panel();
            this.Unit3 = new System.Windows.Forms.Label();
            this.Unit1 = new System.Windows.Forms.Label();
            this.panel20 = new System.Windows.Forms.Panel();
            this.lblMainMenuTitle = new System.Windows.Forms.Label();
            this.pnSoftwareInfo = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSurfaceWaterQuality = new System.Windows.Forms.Label();
            this.lblAutomaticMonitoring = new System.Windows.Forms.Label();
            this.lblThaiNguyenStation = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox52 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblWaterLevel = new System.Windows.Forms.Label();
            this.lblHeaderNationName = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.RichTextBox();
            this.picSamplerTank = new System.Windows.Forms.PictureBox();
            this.btnLanguage = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Unit2 = new System.Windows.Forms.Label();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.Unit6 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.Unit5 = new System.Windows.Forms.Label();
            this.Unit4 = new System.Windows.Forms.Label();
            this.btnMPSHistoryData = new System.Windows.Forms.Button();
            this.txtMPSCondValue = new System.Windows.Forms.TextBox();
            this.txtMPSTurbValue = new System.Windows.Forms.TextBox();
            this.picMPSStatus = new System.Windows.Forms.PictureBox();
            this.txtMPSDOValue = new System.Windows.Forms.TextBox();
            this.btnMPS5Minute = new System.Windows.Forms.Button();
            this.txtMPSTempValue = new System.Windows.Forms.TextBox();
            this.btnMPS1Hour = new System.Windows.Forms.Button();
            this.txtMPSORPValue = new System.Windows.Forms.TextBox();
            this.var1Text = new System.Windows.Forms.Label();
            this.txtMPSpHValue = new System.Windows.Forms.TextBox();
            this.var2Text = new System.Windows.Forms.Label();
            this.var6Text = new System.Windows.Forms.Label();
            this.var3Text = new System.Windows.Forms.Label();
            this.var5Text = new System.Windows.Forms.Label();
            this.var4Text = new System.Windows.Forms.Label();
            this.pnLeftSide = new System.Windows.Forms.Panel();
            this.btnMaintenance = new System.Windows.Forms.Button();
            this.btnMonthlyReport = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnAllHistory = new System.Windows.Forms.Button();
            this.panel21 = new System.Windows.Forms.Panel();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.btnLoginLogout = new System.Windows.Forms.Button();
            this.lblLoginDisplayName = new System.Windows.Forms.Label();
            this.lblHeadingTime = new System.Windows.Forms.Label();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel24 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel30.SuspendLayout();
            this.panel20.SuspendLayout();
            this.pnSoftwareInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox52)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSamplerTank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMPSStatus)).BeginInit();
            this.pnLeftSide.SuspendLayout();
            this.pnHeader.SuspendLayout();
            this.panel18.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgwMonthlyReport
            // 
            this.bgwMonthlyReport.WorkerReportsProgress = true;
            this.bgwMonthlyReport.WorkerSupportsCancellation = true;
            this.bgwMonthlyReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMonthlyReport_DoWork);
            this.bgwMonthlyReport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMonthlyReport_ProgressChanged);
            this.bgwMonthlyReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMonthlyReport_RunWorkerCompleted);
            // 
            // panel30
            // 
            this.panel30.AutoSize = true;
            this.panel30.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel30.BackColor = System.Drawing.Color.Transparent;
            this.panel30.BackgroundImage = global::DataLogger.Properties.Resources.main;
            this.panel30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel30.Controls.Add(this.Unit3);
            this.panel30.Controls.Add(this.Unit1);
            this.panel30.Controls.Add(this.panel20);
            this.panel30.Controls.Add(this.Unit2);
            this.panel30.Controls.Add(this.panel19);
            this.panel30.Controls.Add(this.panel17);
            this.panel30.Controls.Add(this.Unit6);
            this.panel30.Controls.Add(this.panel15);
            this.panel30.Controls.Add(this.Unit5);
            this.panel30.Controls.Add(this.Unit4);
            this.panel30.Controls.Add(this.btnMPSHistoryData);
            this.panel30.Controls.Add(this.txtMPSCondValue);
            this.panel30.Controls.Add(this.txtMPSTurbValue);
            this.panel30.Controls.Add(this.picMPSStatus);
            this.panel30.Controls.Add(this.txtMPSDOValue);
            this.panel30.Controls.Add(this.btnMPS5Minute);
            this.panel30.Controls.Add(this.txtMPSTempValue);
            this.panel30.Controls.Add(this.btnMPS1Hour);
            this.panel30.Controls.Add(this.txtMPSORPValue);
            this.panel30.Controls.Add(this.var1Text);
            this.panel30.Controls.Add(this.txtMPSpHValue);
            this.panel30.Controls.Add(this.var2Text);
            this.panel30.Controls.Add(this.var6Text);
            this.panel30.Controls.Add(this.var3Text);
            this.panel30.Controls.Add(this.var5Text);
            this.panel30.Controls.Add(this.var4Text);
            this.panel30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel30.Location = new System.Drawing.Point(76, 57);
            this.panel30.Margin = new System.Windows.Forms.Padding(10);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(714, 424);
            this.panel30.TabIndex = 65;
            // 
            // Unit3
            // 
            this.Unit3.AutoSize = true;
            this.Unit3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit3.ForeColor = System.Drawing.Color.Black;
            this.Unit3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Unit3.Location = new System.Drawing.Point(636, 161);
            this.Unit3.Name = "Unit3";
            this.Unit3.Size = new System.Drawing.Size(31, 19);
            this.Unit3.TabIndex = 76;
            this.Unit3.Text = "g/L";
            // 
            // Unit1
            // 
            this.Unit1.AutoSize = true;
            this.Unit1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit1.ForeColor = System.Drawing.Color.Black;
            this.Unit1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Unit1.Location = new System.Drawing.Point(292, 75);
            this.Unit1.Name = "Unit1";
            this.Unit1.Size = new System.Drawing.Size(31, 19);
            this.Unit1.TabIndex = 75;
            this.Unit1.Text = "g/L";
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.lblMainMenuTitle);
            this.panel20.Controls.Add(this.pnSoftwareInfo);
            this.panel20.Controls.Add(this.pictureBox52);
            this.panel20.Controls.Add(this.btnExit);
            this.panel20.Controls.Add(this.lblWaterLevel);
            this.panel20.Controls.Add(this.lblHeaderNationName);
            this.panel20.Controls.Add(this.txtData);
            this.panel20.Controls.Add(this.picSamplerTank);
            this.panel20.Controls.Add(this.btnLanguage);
            this.panel20.Controls.Add(this.pictureBox5);
            this.panel20.Controls.Add(this.button5);
            this.panel20.Controls.Add(this.button4);
            this.panel20.Location = new System.Drawing.Point(720, 415);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(28, 15);
            this.panel20.TabIndex = 70;
            // 
            // lblMainMenuTitle
            // 
            this.lblMainMenuTitle.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainMenuTitle.ForeColor = System.Drawing.Color.White;
            this.lblMainMenuTitle.Location = new System.Drawing.Point(-51, 33);
            this.lblMainMenuTitle.Name = "lblMainMenuTitle";
            this.lblMainMenuTitle.Size = new System.Drawing.Size(150, 22);
            this.lblMainMenuTitle.TabIndex = 3;
            this.lblMainMenuTitle.Text = "MAIN MENU";
            this.lblMainMenuTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblMainMenuTitle.Visible = false;
            // 
            // pnSoftwareInfo
            // 
            this.pnSoftwareInfo.BackColor = System.Drawing.Color.White;
            this.pnSoftwareInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnSoftwareInfo.Controls.Add(this.label6);
            this.pnSoftwareInfo.Controls.Add(this.lblSurfaceWaterQuality);
            this.pnSoftwareInfo.Controls.Add(this.lblAutomaticMonitoring);
            this.pnSoftwareInfo.Controls.Add(this.lblThaiNguyenStation);
            this.pnSoftwareInfo.Controls.Add(this.pictureBox3);
            this.pnSoftwareInfo.Controls.Add(this.pictureBox2);
            this.pnSoftwareInfo.Location = new System.Drawing.Point(-416, 23);
            this.pnSoftwareInfo.Name = "pnSoftwareInfo";
            this.pnSoftwareInfo.Size = new System.Drawing.Size(34, 13);
            this.pnSoftwareInfo.TabIndex = 4;
            this.pnSoftwareInfo.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(145, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "2015";
            // 
            // lblSurfaceWaterQuality
            // 
            this.lblSurfaceWaterQuality.AutoSize = true;
            this.lblSurfaceWaterQuality.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurfaceWaterQuality.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblSurfaceWaterQuality.Location = new System.Drawing.Point(43, 117);
            this.lblSurfaceWaterQuality.Name = "lblSurfaceWaterQuality";
            this.lblSurfaceWaterQuality.Size = new System.Drawing.Size(214, 19);
            this.lblSurfaceWaterQuality.TabIndex = 5;
            this.lblSurfaceWaterQuality.Text = "SURFACE WATER QUALITY";
            // 
            // lblAutomaticMonitoring
            // 
            this.lblAutomaticMonitoring.AutoSize = true;
            this.lblAutomaticMonitoring.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutomaticMonitoring.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblAutomaticMonitoring.Location = new System.Drawing.Point(43, 97);
            this.lblAutomaticMonitoring.Name = "lblAutomaticMonitoring";
            this.lblAutomaticMonitoring.Size = new System.Drawing.Size(220, 19);
            this.lblAutomaticMonitoring.TabIndex = 4;
            this.lblAutomaticMonitoring.Text = "AUTOMATIC MONITORING";
            // 
            // lblThaiNguyenStation
            // 
            this.lblThaiNguyenStation.AutoSize = true;
            this.lblThaiNguyenStation.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThaiNguyenStation.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblThaiNguyenStation.Location = new System.Drawing.Point(53, 66);
            this.lblThaiNguyenStation.Name = "lblThaiNguyenStation";
            this.lblThaiNguyenStation.Size = new System.Drawing.Size(126, 19);
            this.lblThaiNguyenStation.TabIndex = 3;
            this.lblThaiNguyenStation.Text = "DMM STATION";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Image = global::DataLogger.Properties.Resources.Flag_of_South_Korea_48x32;
            this.pictureBox3.Location = new System.Drawing.Point(202, 20);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(48, 32);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::DataLogger.Properties.Resources.Flag_of_Vietnam_43x32;
            this.pictureBox2.Location = new System.Drawing.Point(76, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 32);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox52
            // 
            this.pictureBox52.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox52.BackgroundImage = global::DataLogger.Properties.Resources.SamplerTank_Ruler;
            this.pictureBox52.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox52.Location = new System.Drawing.Point(138, 32);
            this.pictureBox52.Name = "pictureBox52";
            this.pictureBox52.Size = new System.Drawing.Size(10, 25);
            this.pictureBox52.TabIndex = 63;
            this.pictureBox52.TabStop = false;
            this.pictureBox52.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.BackgroundImage = global::DataLogger.Properties.Resources.Shutdown_Box_Red;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Enabled = false;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(154, 32);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 10);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblWaterLevel
            // 
            this.lblWaterLevel.AutoSize = true;
            this.lblWaterLevel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaterLevel.Location = new System.Drawing.Point(-47, 36);
            this.lblWaterLevel.Name = "lblWaterLevel";
            this.lblWaterLevel.Size = new System.Drawing.Size(67, 15);
            this.lblWaterLevel.TabIndex = 31;
            this.lblWaterLevel.Text = "Water level";
            this.lblWaterLevel.Visible = false;
            // 
            // lblHeaderNationName
            // 
            this.lblHeaderNationName.AutoSize = true;
            this.lblHeaderNationName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderNationName.ForeColor = System.Drawing.Color.White;
            this.lblHeaderNationName.Location = new System.Drawing.Point(-316, 34);
            this.lblHeaderNationName.Name = "lblHeaderNationName";
            this.lblHeaderNationName.Size = new System.Drawing.Size(84, 17);
            this.lblHeaderNationName.TabIndex = 1;
            this.lblHeaderNationName.Text = "Vietnamese";
            this.lblHeaderNationName.Visible = false;
            // 
            // txtData
            // 
            this.txtData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtData.ForeColor = System.Drawing.Color.Maroon;
            this.txtData.Location = new System.Drawing.Point(94, 41);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(48, 10);
            this.txtData.TabIndex = 62;
            this.txtData.Text = "";
            this.txtData.Visible = false;
            // 
            // picSamplerTank
            // 
            this.picSamplerTank.BackColor = System.Drawing.Color.White;
            this.picSamplerTank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSamplerTank.Image = global::DataLogger.Properties.Resources.SamplerTankerWater;
            this.picSamplerTank.Location = new System.Drawing.Point(110, 31);
            this.picSamplerTank.Name = "picSamplerTank";
            this.picSamplerTank.Size = new System.Drawing.Size(12, 26);
            this.picSamplerTank.TabIndex = 31;
            this.picSamplerTank.TabStop = false;
            this.picSamplerTank.Visible = false;
            // 
            // btnLanguage
            // 
            this.btnLanguage.BackColor = System.Drawing.Color.Transparent;
            this.btnLanguage.BackgroundImage = global::DataLogger.Properties.Resources.Flag_of_Vietnam_43x32;
            this.btnLanguage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLanguage.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnLanguage.FlatAppearance.BorderSize = 0;
            this.btnLanguage.Location = new System.Drawing.Point(-365, 29);
            this.btnLanguage.Name = "btnLanguage";
            this.btnLanguage.Size = new System.Drawing.Size(43, 16);
            this.btnLanguage.TabIndex = 50;
            this.btnLanguage.UseVisualStyleBackColor = false;
            this.btnLanguage.Visible = false;
            this.btnLanguage.Click += new System.EventHandler(this.btnLanguage_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DataLogger.Properties.Resources.WaterLevel;
            this.pictureBox5.Location = new System.Drawing.Point(-107, 34);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(64, 21);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 32;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Visible = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImage = global::DataLogger.Properties.Resources.logo;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(141)))), ((int)(((byte)(196)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(-226, 31);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(47, 22);
            this.button5.TabIndex = 67;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Visible = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = global::DataLogger.Properties.Resources.clock;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(141)))), ((int)(((byte)(196)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(-173, 26);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(37, 24);
            this.button4.TabIndex = 66;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            // 
            // Unit2
            // 
            this.Unit2.AutoSize = true;
            this.Unit2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit2.ForeColor = System.Drawing.Color.Black;
            this.Unit2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Unit2.Location = new System.Drawing.Point(636, 75);
            this.Unit2.Name = "Unit2";
            this.Unit2.Size = new System.Drawing.Size(32, 19);
            this.Unit2.TabIndex = 66;
            this.Unit2.Text = "mV";
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Transparent;
            this.panel19.Location = new System.Drawing.Point(731, 163);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(40, 10);
            this.panel19.TabIndex = 74;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.Transparent;
            this.panel17.Location = new System.Drawing.Point(701, 399);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(40, 10);
            this.panel17.TabIndex = 73;
            // 
            // Unit6
            // 
            this.Unit6.AutoSize = true;
            this.Unit6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit6.ForeColor = System.Drawing.Color.Black;
            this.Unit6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Unit6.Location = new System.Drawing.Point(292, 253);
            this.Unit6.Name = "Unit6";
            this.Unit6.Size = new System.Drawing.Size(53, 19);
            this.Unit6.TabIndex = 65;
            this.Unit6.Text = "mS/cm";
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Transparent;
            this.panel15.Location = new System.Drawing.Point(736, 30);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(40, 10);
            this.panel15.TabIndex = 72;
            // 
            // Unit5
            // 
            this.Unit5.AutoSize = true;
            this.Unit5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit5.ForeColor = System.Drawing.Color.Black;
            this.Unit5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Unit5.Location = new System.Drawing.Point(292, 164);
            this.Unit5.Name = "Unit5";
            this.Unit5.Size = new System.Drawing.Size(31, 19);
            this.Unit5.TabIndex = 64;
            this.Unit5.Text = "g/L";
            // 
            // Unit4
            // 
            this.Unit4.AutoSize = true;
            this.Unit4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Unit4.ForeColor = System.Drawing.Color.Black;
            this.Unit4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Unit4.Location = new System.Drawing.Point(636, 247);
            this.Unit4.Name = "Unit4";
            this.Unit4.Size = new System.Drawing.Size(43, 19);
            this.Unit4.TabIndex = 63;
            this.Unit4.Text = "mg/L";
            // 
            // btnMPSHistoryData
            // 
            this.btnMPSHistoryData.BackColor = System.Drawing.Color.Transparent;
            this.btnMPSHistoryData.BackgroundImage = global::DataLogger.Properties.Resources._8;
            this.btnMPSHistoryData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMPSHistoryData.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnMPSHistoryData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMPSHistoryData.Location = new System.Drawing.Point(576, 333);
            this.btnMPSHistoryData.Name = "btnMPSHistoryData";
            this.btnMPSHistoryData.Size = new System.Drawing.Size(72, 63);
            this.btnMPSHistoryData.TabIndex = 50;
            this.btnMPSHistoryData.UseVisualStyleBackColor = false;
            this.btnMPSHistoryData.Click += new System.EventHandler(this.btnMPSHistoryData_Click);
            // 
            // txtMPSCondValue
            // 
            this.txtMPSCondValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.txtMPSCondValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMPSCondValue.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPSCondValue.ForeColor = System.Drawing.Color.Maroon;
            this.txtMPSCondValue.Location = new System.Drawing.Point(194, 67);
            this.txtMPSCondValue.Name = "txtMPSCondValue";
            this.txtMPSCondValue.ReadOnly = true;
            this.txtMPSCondValue.Size = new System.Drawing.Size(64, 31);
            this.txtMPSCondValue.TabIndex = 56;
            this.txtMPSCondValue.Text = "117.242";
            this.txtMPSCondValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMPSTurbValue
            // 
            this.txtMPSTurbValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.txtMPSTurbValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMPSTurbValue.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPSTurbValue.ForeColor = System.Drawing.Color.Maroon;
            this.txtMPSTurbValue.Location = new System.Drawing.Point(545, 241);
            this.txtMPSTurbValue.Name = "txtMPSTurbValue";
            this.txtMPSTurbValue.ReadOnly = true;
            this.txtMPSTurbValue.Size = new System.Drawing.Size(64, 31);
            this.txtMPSTurbValue.TabIndex = 55;
            this.txtMPSTurbValue.Text = "9.29";
            this.txtMPSTurbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // picMPSStatus
            // 
            this.picMPSStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal;
            this.picMPSStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMPSStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMPSStatus.Location = new System.Drawing.Point(61, 333);
            this.picMPSStatus.Name = "picMPSStatus";
            this.picMPSStatus.Size = new System.Drawing.Size(70, 63);
            this.picMPSStatus.TabIndex = 59;
            this.picMPSStatus.TabStop = false;
            // 
            // txtMPSDOValue
            // 
            this.txtMPSDOValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.txtMPSDOValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMPSDOValue.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPSDOValue.ForeColor = System.Drawing.Color.Maroon;
            this.txtMPSDOValue.Location = new System.Drawing.Point(545, 154);
            this.txtMPSDOValue.Name = "txtMPSDOValue";
            this.txtMPSDOValue.ReadOnly = true;
            this.txtMPSDOValue.Size = new System.Drawing.Size(64, 31);
            this.txtMPSDOValue.TabIndex = 54;
            this.txtMPSDOValue.Text = "6.88";
            this.txtMPSDOValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnMPS5Minute
            // 
            this.btnMPS5Minute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.btnMPS5Minute.BackgroundImage = global::DataLogger.Properties.Resources._10;
            this.btnMPS5Minute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMPS5Minute.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnMPS5Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMPS5Minute.Location = new System.Drawing.Point(296, 333);
            this.btnMPS5Minute.Name = "btnMPS5Minute";
            this.btnMPS5Minute.Size = new System.Drawing.Size(70, 63);
            this.btnMPS5Minute.TabIndex = 50;
            this.btnMPS5Minute.UseVisualStyleBackColor = false;
            this.btnMPS5Minute.Click += new System.EventHandler(this.btnMPS5Minute_Click);
            // 
            // txtMPSTempValue
            // 
            this.txtMPSTempValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.txtMPSTempValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMPSTempValue.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPSTempValue.ForeColor = System.Drawing.Color.Maroon;
            this.txtMPSTempValue.Location = new System.Drawing.Point(194, 244);
            this.txtMPSTempValue.Name = "txtMPSTempValue";
            this.txtMPSTempValue.ReadOnly = true;
            this.txtMPSTempValue.Size = new System.Drawing.Size(64, 31);
            this.txtMPSTempValue.TabIndex = 53;
            this.txtMPSTempValue.Text = "27.65";
            this.txtMPSTempValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnMPS1Hour
            // 
            this.btnMPS1Hour.BackColor = System.Drawing.Color.Transparent;
            this.btnMPS1Hour.BackgroundImage = global::DataLogger.Properties.Resources._11;
            this.btnMPS1Hour.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMPS1Hour.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnMPS1Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMPS1Hour.Location = new System.Drawing.Point(433, 333);
            this.btnMPS1Hour.Name = "btnMPS1Hour";
            this.btnMPS1Hour.Size = new System.Drawing.Size(70, 63);
            this.btnMPS1Hour.TabIndex = 50;
            this.btnMPS1Hour.UseVisualStyleBackColor = false;
            this.btnMPS1Hour.Click += new System.EventHandler(this.btnMPS1Hour_Click);
            // 
            // txtMPSORPValue
            // 
            this.txtMPSORPValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.txtMPSORPValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMPSORPValue.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPSORPValue.ForeColor = System.Drawing.Color.Maroon;
            this.txtMPSORPValue.Location = new System.Drawing.Point(194, 157);
            this.txtMPSORPValue.Name = "txtMPSORPValue";
            this.txtMPSORPValue.ReadOnly = true;
            this.txtMPSORPValue.Size = new System.Drawing.Size(64, 31);
            this.txtMPSORPValue.TabIndex = 52;
            this.txtMPSORPValue.Text = "426.17";
            this.txtMPSORPValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // var1Text
            // 
            this.var1Text.AutoSize = true;
            this.var1Text.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var1Text.ForeColor = System.Drawing.Color.Black;
            this.var1Text.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.var1Text.Location = new System.Drawing.Point(66, 62);
            this.var1Text.Name = "var1Text";
            this.var1Text.Size = new System.Drawing.Size(77, 36);
            this.var1Text.TabIndex = 45;
            this.var1Text.Text = "var1";
            // 
            // txtMPSpHValue
            // 
            this.txtMPSpHValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.txtMPSpHValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMPSpHValue.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPSpHValue.ForeColor = System.Drawing.Color.Maroon;
            this.txtMPSpHValue.Location = new System.Drawing.Point(545, 68);
            this.txtMPSpHValue.Name = "txtMPSpHValue";
            this.txtMPSpHValue.Size = new System.Drawing.Size(64, 31);
            this.txtMPSpHValue.TabIndex = 51;
            this.txtMPSpHValue.Text = "7.20";
            this.txtMPSpHValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // var2Text
            // 
            this.var2Text.AutoSize = true;
            this.var2Text.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var2Text.ForeColor = System.Drawing.Color.Black;
            this.var2Text.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.var2Text.Location = new System.Drawing.Point(390, 62);
            this.var2Text.Name = "var2Text";
            this.var2Text.Size = new System.Drawing.Size(77, 36);
            this.var2Text.TabIndex = 46;
            this.var2Text.Text = "var2";
            this.var2Text.Click += new System.EventHandler(this.var2Text_Click);
            // 
            // var6Text
            // 
            this.var6Text.AutoSize = true;
            this.var6Text.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var6Text.ForeColor = System.Drawing.Color.Black;
            this.var6Text.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.var6Text.Location = new System.Drawing.Point(59, 237);
            this.var6Text.Name = "var6Text";
            this.var6Text.Size = new System.Drawing.Size(77, 36);
            this.var6Text.TabIndex = 50;
            this.var6Text.Text = "var6";
            // 
            // var3Text
            // 
            this.var3Text.AutoSize = true;
            this.var3Text.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var3Text.ForeColor = System.Drawing.Color.Black;
            this.var3Text.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.var3Text.Location = new System.Drawing.Point(390, 151);
            this.var3Text.Name = "var3Text";
            this.var3Text.Size = new System.Drawing.Size(77, 36);
            this.var3Text.TabIndex = 47;
            this.var3Text.Text = "var3";
            // 
            // var5Text
            // 
            this.var5Text.AutoSize = true;
            this.var5Text.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var5Text.ForeColor = System.Drawing.Color.Black;
            this.var5Text.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.var5Text.Location = new System.Drawing.Point(60, 152);
            this.var5Text.Name = "var5Text";
            this.var5Text.Size = new System.Drawing.Size(77, 36);
            this.var5Text.TabIndex = 49;
            this.var5Text.Text = "var5";
            // 
            // var4Text
            // 
            this.var4Text.AutoSize = true;
            this.var4Text.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.var4Text.ForeColor = System.Drawing.Color.Black;
            this.var4Text.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.var4Text.Location = new System.Drawing.Point(391, 239);
            this.var4Text.Name = "var4Text";
            this.var4Text.Size = new System.Drawing.Size(77, 36);
            this.var4Text.TabIndex = 48;
            this.var4Text.Text = "var4";
            // 
            // pnLeftSide
            // 
            this.pnLeftSide.AutoSize = true;
            this.pnLeftSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.pnLeftSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnLeftSide.Controls.Add(this.btnMaintenance);
            this.pnLeftSide.Controls.Add(this.btnMonthlyReport);
            this.pnLeftSide.Controls.Add(this.btnSetting);
            this.pnLeftSide.Controls.Add(this.btnUsers);
            this.pnLeftSide.Controls.Add(this.btnAllHistory);
            this.pnLeftSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLeftSide.Location = new System.Drawing.Point(0, 47);
            this.pnLeftSide.Margin = new System.Windows.Forms.Padding(0);
            this.pnLeftSide.Name = "pnLeftSide";
            this.tableLayoutPanel1.SetRowSpan(this.pnLeftSide, 3);
            this.pnLeftSide.Size = new System.Drawing.Size(66, 677);
            this.pnLeftSide.TabIndex = 1;
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.btnMaintenance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMaintenance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnMaintenance.FlatAppearance.BorderSize = 0;
            this.btnMaintenance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaintenance.Image = global::DataLogger.Properties.Resources.world_clock;
            this.btnMaintenance.Location = new System.Drawing.Point(-2, 230);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(70, 64);
            this.btnMaintenance.TabIndex = 50;
            this.btnMaintenance.UseVisualStyleBackColor = false;
            this.btnMaintenance.Visible = false;
            this.btnMaintenance.Click += new System.EventHandler(this.btnMaintenance_Click);
            // 
            // btnMonthlyReport
            // 
            this.btnMonthlyReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.btnMonthlyReport.BackgroundImage = global::DataLogger.Properties.Resources.MonthlyReportButton;
            this.btnMonthlyReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMonthlyReport.FlatAppearance.BorderSize = 0;
            this.btnMonthlyReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnMonthlyReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonthlyReport.Location = new System.Drawing.Point(-1, -2);
            this.btnMonthlyReport.Name = "btnMonthlyReport";
            this.btnMonthlyReport.Size = new System.Drawing.Size(70, 70);
            this.btnMonthlyReport.TabIndex = 49;
            this.btnMonthlyReport.UseVisualStyleBackColor = false;
            this.btnMonthlyReport.Click += new System.EventHandler(this.btnMonthlyReport_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetting.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnSetting.FlatAppearance.BorderSize = 0;
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnSetting.Image = global::DataLogger.Properties.Resources.applications_system_60x60;
            this.btnSetting.Location = new System.Drawing.Point(-3, 301);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(70, 64);
            this.btnSetting.TabIndex = 5;
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.btnUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUsers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnUsers.FlatAppearance.BorderSize = 0;
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Image = global::DataLogger.Properties.Resources.user;
            this.btnUsers.Location = new System.Drawing.Point(-2, 370);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(70, 64);
            this.btnUsers.TabIndex = 4;
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnAllHistory
            // 
            this.btnAllHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnAllHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAllHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnAllHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllHistory.Image = global::DataLogger.Properties.Resources.maintenance;
            this.btnAllHistory.Location = new System.Drawing.Point(-1, 450);
            this.btnAllHistory.Name = "btnAllHistory";
            this.btnAllHistory.Size = new System.Drawing.Size(70, 39);
            this.btnAllHistory.TabIndex = 3;
            this.btnAllHistory.UseVisualStyleBackColor = false;
            this.btnAllHistory.Click += new System.EventHandler(this.btnAllHistory_Click);
            // 
            // panel21
            // 
            this.panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel21.Location = new System.Drawing.Point(803, 494);
            this.panel21.Name = "panel21";
            this.tableLayoutPanel1.SetRowSpan(this.panel21, 2);
            this.panel21.Size = new System.Drawing.Size(541, 227);
            this.panel21.TabIndex = 71;
            // 
            // pnHeader
            // 
            this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.tableLayoutPanel1.SetColumnSpan(this.pnHeader, 3);
            this.pnHeader.Controls.Add(this.label9);
            this.pnHeader.Controls.Add(this.label4);
            this.pnHeader.Controls.Add(this.label3);
            this.pnHeader.Controls.Add(this.panel18);
            this.pnHeader.Controls.Add(this.lblHeadingTime);
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(1347, 47);
            this.pnHeader.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(45, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 31);
            this.label9.TabIndex = 70;
            this.label9.Text = "STATION";
            // 
            // label4
            // 
            this.label4.Image = global::DataLogger.Properties.Resources.clock;
            this.label4.Location = new System.Drawing.Point(447, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 37);
            this.label4.TabIndex = 69;
            // 
            // label3
            // 
            this.label3.Image = global::DataLogger.Properties.Resources.logo;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(4, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 37);
            this.label3.TabIndex = 68;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.panel18.Controls.Add(this.btnLoginLogout);
            this.panel18.Controls.Add(this.lblLoginDisplayName);
            this.panel18.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel18.Location = new System.Drawing.Point(656, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(173, 48);
            this.panel18.TabIndex = 65;
            // 
            // btnLoginLogout
            // 
            this.btnLoginLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.login;
            this.btnLoginLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoginLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(141)))), ((int)(((byte)(196)))));
            this.btnLoginLogout.FlatAppearance.BorderSize = 0;
            this.btnLoginLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginLogout.Location = new System.Drawing.Point(133, 5);
            this.btnLoginLogout.Name = "btnLoginLogout";
            this.btnLoginLogout.Size = new System.Drawing.Size(37, 37);
            this.btnLoginLogout.TabIndex = 64;
            this.btnLoginLogout.UseVisualStyleBackColor = false;
            this.btnLoginLogout.Click += new System.EventHandler(this.btnLoginLogout_Click);
            // 
            // lblLoginDisplayName
            // 
            this.lblLoginDisplayName.AutoSize = true;
            this.lblLoginDisplayName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginDisplayName.ForeColor = System.Drawing.Color.White;
            this.lblLoginDisplayName.Location = new System.Drawing.Point(8, 15);
            this.lblLoginDisplayName.Name = "lblLoginDisplayName";
            this.lblLoginDisplayName.Size = new System.Drawing.Size(120, 17);
            this.lblLoginDisplayName.TabIndex = 51;
            this.lblLoginDisplayName.Text = "Welcome, Guest!";
            // 
            // lblHeadingTime
            // 
            this.lblHeadingTime.AutoSize = true;
            this.lblHeadingTime.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadingTime.ForeColor = System.Drawing.Color.White;
            this.lblHeadingTime.Location = new System.Drawing.Point(490, 16);
            this.lblHeadingTime.Name = "lblHeadingTime";
            this.lblHeadingTime.Size = new System.Drawing.Size(143, 17);
            this.lblHeadingTime.TabIndex = 2;
            this.lblHeadingTime.Text = "25-09-2015 12:11:19";
            // 
            // panel23
            // 
            this.panel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel23.Location = new System.Drawing.Point(69, 494);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(728, 161);
            this.panel23.TabIndex = 70;
            // 
            // panel24
            // 
            this.panel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel24.Location = new System.Drawing.Point(803, 50);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(541, 438);
            this.panel24.TabIndex = 72;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.26636F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.73364F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 546F));
            this.tableLayoutPanel1.Controls.Add(this.panel24, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel23, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel21, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnLeftSide, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel30, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.619239F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.38076F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1347, 724);
            this.tableLayoutPanel1.TabIndex = 69;
            // 
            // frmNewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(830, 493);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmNewMain";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewMain_FormClosing);
            this.Load += new System.EventHandler(this.frmNewMain_Load);
            this.panel30.ResumeLayout(false);
            this.panel30.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.pnSoftwareInfo.ResumeLayout(false);
            this.pnSoftwareInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox52)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSamplerTank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMPSStatus)).EndInit();
            this.pnLeftSide.ResumeLayout(false);
            this.pnHeader.ResumeLayout(false);
            this.pnHeader.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.ComponentModel.BackgroundWorker bgwMonthlyReport;
        public System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Panel panel20;
        public System.Windows.Forms.Label lblMainMenuTitle;
        public System.Windows.Forms.Panel pnSoftwareInfo;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblSurfaceWaterQuality;
        public System.Windows.Forms.Label lblAutomaticMonitoring;
        public System.Windows.Forms.Label lblThaiNguyenStation;
        public System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.PictureBox pictureBox52;
        public System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Label lblWaterLevel;
        public System.Windows.Forms.Label lblHeaderNationName;
        public System.Windows.Forms.RichTextBox txtData;
        public System.Windows.Forms.PictureBox picSamplerTank;
        public System.Windows.Forms.Button btnLanguage;
        public System.Windows.Forms.PictureBox pictureBox5;
        public FlatButton listen;
        public System.Windows.Forms.Button button5;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.Label Unit2;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Panel panel17;
        public System.Windows.Forms.Label Unit6;
        private System.Windows.Forms.Panel panel15;
        public System.Windows.Forms.Label Unit5;
        public System.Windows.Forms.Label Unit4;
        public System.Windows.Forms.Button btnMPSHistoryData;
        public System.Windows.Forms.TextBox txtMPSCondValue;
        public System.Windows.Forms.TextBox txtMPSTurbValue;
        public System.Windows.Forms.PictureBox picMPSStatus;
        public System.Windows.Forms.TextBox txtMPSDOValue;
        public System.Windows.Forms.Button btnMPS5Minute;
        public System.Windows.Forms.TextBox txtMPSTempValue;
        public System.Windows.Forms.Button btnMPS1Hour;
        public System.Windows.Forms.TextBox txtMPSORPValue;
        public System.Windows.Forms.Label var1Text;
        public System.Windows.Forms.TextBox txtMPSpHValue;
        public System.Windows.Forms.Label var2Text;
        public System.Windows.Forms.Label var6Text;
        public System.Windows.Forms.Label var3Text;
        public System.Windows.Forms.Label var5Text;
        public System.Windows.Forms.Label var4Text;
        public System.Windows.Forms.Panel pnLeftSide;
        public VerticalProgressBar.VerticalProgressBar vprgMonthlyReport;
        public System.Windows.Forms.Button btnMaintenance;
        public System.Windows.Forms.Button btnMonthlyReport;
        public System.Windows.Forms.Button btnSetting;
        public System.Windows.Forms.Button btnUsers;
        public System.Windows.Forms.Button btnAllHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Panel panel23;
        public System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblHeadingTime;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel18;
        public System.Windows.Forms.Button btnLoginLogout;
        public System.Windows.Forms.Label lblLoginDisplayName;
        public System.Windows.Forms.Label Unit3;
        public System.Windows.Forms.Label Unit1;
    }
}