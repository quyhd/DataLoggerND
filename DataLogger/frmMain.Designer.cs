namespace DataLogger
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.serialPortTN = new System.IO.Ports.SerialPort(this.components);
            this.serialPortTP = new System.IO.Ports.SerialPort(this.components);
            this.serialPortTOC = new System.IO.Ports.SerialPort(this.components);
            this.serialPortMPS = new System.IO.Ports.SerialPort(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.RichTextBox();
            this.txtRawData = new System.Windows.Forms.RichTextBox();
            this.rbtnTOC = new System.Windows.Forms.RadioButton();
            this.rbtnTN = new System.Windows.Forms.RadioButton();
            this.rbtnTP = new System.Windows.Forms.RadioButton();
            this.rbtnMPS = new System.Windows.Forms.RadioButton();
            this.dgvDetailSendingResult = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvSamplingTest = new System.Windows.Forms.DataGridView();
            this.dgvDoControl = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbtnStationStatus = new System.Windows.Forms.RadioButton();
            this.rbtnSAMP = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sETTINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPortSAMP = new System.IO.Ports.SerialPort(this.components);
            this.serialPortADAM405x = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailSendingResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplingTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPortTN
            // 
            this.serialPortTN.PortName = "COM8";
            this.serialPortTN.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortTN_DataReceived);
            // 
            // serialPortTP
            // 
            this.serialPortTP.PortName = "COM9";
            this.serialPortTP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortTP_DataReceived);
            // 
            // serialPortTOC
            // 
            this.serialPortTOC.PortName = "COM13";
            this.serialPortTOC.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortTOC_DataReceived);
            // 
            // serialPortMPS
            // 
            this.serialPortMPS.PortName = "COM7";
            this.serialPortMPS.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortMPS_DataReceived);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(831, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(147, 19);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "09/09/2015 15:10:12";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(12, 560);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(430, 38);
            this.txtData.TabIndex = 2;
            this.txtData.Text = "";
            // 
            // txtRawData
            // 
            this.txtRawData.Location = new System.Drawing.Point(12, 622);
            this.txtRawData.Name = "txtRawData";
            this.txtRawData.Size = new System.Drawing.Size(430, 38);
            this.txtRawData.TabIndex = 5;
            this.txtRawData.Text = "";
            // 
            // rbtnTOC
            // 
            this.rbtnTOC.AutoSize = true;
            this.rbtnTOC.Checked = true;
            this.rbtnTOC.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnTOC.ForeColor = System.Drawing.Color.White;
            this.rbtnTOC.Location = new System.Drawing.Point(7, 4);
            this.rbtnTOC.Name = "rbtnTOC";
            this.rbtnTOC.Size = new System.Drawing.Size(52, 19);
            this.rbtnTOC.TabIndex = 7;
            this.rbtnTOC.TabStop = true;
            this.rbtnTOC.Text = "TOC";
            this.rbtnTOC.UseVisualStyleBackColor = true;
            // 
            // rbtnTN
            // 
            this.rbtnTN.AutoSize = true;
            this.rbtnTN.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnTN.ForeColor = System.Drawing.Color.White;
            this.rbtnTN.Location = new System.Drawing.Point(60, 4);
            this.rbtnTN.Name = "rbtnTN";
            this.rbtnTN.Size = new System.Drawing.Size(42, 19);
            this.rbtnTN.TabIndex = 8;
            this.rbtnTN.Text = "TN";
            this.rbtnTN.UseVisualStyleBackColor = true;
            // 
            // rbtnTP
            // 
            this.rbtnTP.AutoSize = true;
            this.rbtnTP.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnTP.ForeColor = System.Drawing.Color.White;
            this.rbtnTP.Location = new System.Drawing.Point(107, 4);
            this.rbtnTP.Name = "rbtnTP";
            this.rbtnTP.Size = new System.Drawing.Size(41, 19);
            this.rbtnTP.TabIndex = 9;
            this.rbtnTP.Text = "TP";
            this.rbtnTP.UseVisualStyleBackColor = true;
            // 
            // rbtnMPS
            // 
            this.rbtnMPS.AutoSize = true;
            this.rbtnMPS.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnMPS.ForeColor = System.Drawing.Color.White;
            this.rbtnMPS.Location = new System.Drawing.Point(153, 4);
            this.rbtnMPS.Name = "rbtnMPS";
            this.rbtnMPS.Size = new System.Drawing.Size(52, 19);
            this.rbtnMPS.TabIndex = 10;
            this.rbtnMPS.Text = "MPS";
            this.rbtnMPS.UseVisualStyleBackColor = true;
            // 
            // dgvDetailSendingResult
            // 
            this.dgvDetailSendingResult.AllowUserToAddRows = false;
            this.dgvDetailSendingResult.AllowUserToDeleteRows = false;
            this.dgvDetailSendingResult.AllowUserToResizeColumns = false;
            this.dgvDetailSendingResult.AllowUserToResizeRows = false;
            this.dgvDetailSendingResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetailSendingResult.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetailSendingResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetailSendingResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetailSendingResult.EnableHeadersVisualStyles = false;
            this.dgvDetailSendingResult.Location = new System.Drawing.Point(12, 78);
            this.dgvDetailSendingResult.Name = "dgvDetailSendingResult";
            this.dgvDetailSendingResult.ReadOnly = true;
            this.dgvDetailSendingResult.RowHeadersVisible = false;
            this.dgvDetailSendingResult.RowHeadersWidth = 20;
            this.dgvDetailSendingResult.Size = new System.Drawing.Size(966, 240);
            this.dgvDetailSendingResult.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 19);
            this.label4.TabIndex = 21;
            this.label4.Text = "Measued Data";
            // 
            // dgvSamplingTest
            // 
            this.dgvSamplingTest.AllowUserToAddRows = false;
            this.dgvSamplingTest.AllowUserToDeleteRows = false;
            this.dgvSamplingTest.AllowUserToResizeColumns = false;
            this.dgvSamplingTest.AllowUserToResizeRows = false;
            this.dgvSamplingTest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSamplingTest.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSamplingTest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSamplingTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSamplingTest.EnableHeadersVisualStyles = false;
            this.dgvSamplingTest.Location = new System.Drawing.Point(12, 344);
            this.dgvSamplingTest.Name = "dgvSamplingTest";
            this.dgvSamplingTest.ReadOnly = true;
            this.dgvSamplingTest.RowHeadersVisible = false;
            this.dgvSamplingTest.RowHeadersWidth = 20;
            this.dgvSamplingTest.Size = new System.Drawing.Size(430, 186);
            this.dgvSamplingTest.TabIndex = 20;
            // 
            // dgvDoControl
            // 
            this.dgvDoControl.AllowUserToAddRows = false;
            this.dgvDoControl.AllowUserToDeleteRows = false;
            this.dgvDoControl.AllowUserToResizeColumns = false;
            this.dgvDoControl.AllowUserToResizeRows = false;
            this.dgvDoControl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoControl.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDoControl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDoControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoControl.EnableHeadersVisualStyles = false;
            this.dgvDoControl.Location = new System.Drawing.Point(449, 344);
            this.dgvDoControl.Name = "dgvDoControl";
            this.dgvDoControl.ReadOnly = true;
            this.dgvDoControl.RowHeadersVisible = false;
            this.dgvDoControl.RowHeadersWidth = 20;
            this.dgvDoControl.Size = new System.Drawing.Size(529, 317);
            this.dgvDoControl.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(329, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 19);
            this.label6.TabIndex = 21;
            this.label6.Text = "Sampling Test";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(946, 559);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(12, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 24);
            this.panel1.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(12, 320);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 24);
            this.panel2.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "Water Sampler Status";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(449, 320);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(529, 24);
            this.panel3.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Station Status";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(440, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 19);
            this.label9.TabIndex = 21;
            this.label9.Text = "D/O Control";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 666);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(990, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(12, 599);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(430, 24);
            this.panel4.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(8, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 19);
            this.label7.TabIndex = 21;
            this.label7.Text = "Raw Data";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.panel5.Controls.Add(this.rbtnStationStatus);
            this.panel5.Controls.Add(this.rbtnSAMP);
            this.panel5.Controls.Add(this.rbtnMPS);
            this.panel5.Controls.Add(this.rbtnTOC);
            this.panel5.Controls.Add(this.rbtnTN);
            this.panel5.Controls.Add(this.rbtnTP);
            this.panel5.Location = new System.Drawing.Point(12, 537);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(430, 24);
            this.panel5.TabIndex = 26;
            // 
            // rbtnStationStatus
            // 
            this.rbtnStationStatus.AutoSize = true;
            this.rbtnStationStatus.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnStationStatus.ForeColor = System.Drawing.Color.White;
            this.rbtnStationStatus.Location = new System.Drawing.Point(300, 4);
            this.rbtnStationStatus.Name = "rbtnStationStatus";
            this.rbtnStationStatus.Size = new System.Drawing.Size(72, 19);
            this.rbtnStationStatus.TabIndex = 12;
            this.rbtnStationStatus.Text = "STATUS";
            this.rbtnStationStatus.UseVisualStyleBackColor = true;
            // 
            // rbtnSAMP
            // 
            this.rbtnSAMP.AutoSize = true;
            this.rbtnSAMP.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSAMP.ForeColor = System.Drawing.Color.White;
            this.rbtnSAMP.Location = new System.Drawing.Point(209, 4);
            this.rbtnSAMP.Name = "rbtnSAMP";
            this.rbtnSAMP.Size = new System.Drawing.Size(85, 19);
            this.rbtnSAMP.TabIndex = 11;
            this.rbtnSAMP.Text = "SAMPLER";
            this.rbtnSAMP.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.sETTINGToolStripMenuItem,
            this.rEPORTToolStripMenuItem,
            this.hELPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(990, 24);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // sETTINGToolStripMenuItem
            // 
            this.sETTINGToolStripMenuItem.Name = "sETTINGToolStripMenuItem";
            this.sETTINGToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.sETTINGToolStripMenuItem.Text = "SETTING";
            // 
            // rEPORTToolStripMenuItem
            // 
            this.rEPORTToolStripMenuItem.Name = "rEPORTToolStripMenuItem";
            this.rEPORTToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.rEPORTToolStripMenuItem.Text = "REPORT";
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.hELPToolStripMenuItem.Text = "HELP";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DataLogger.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(990, 688);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvDoControl);
            this.Controls.Add(this.dgvSamplingTest);
            this.Controls.Add(this.dgvDetailSendingResult);
            this.Controls.Add(this.txtRawData);
            this.Controls.Add(this.txtData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailSendingResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplingTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPortTN;
        private System.IO.Ports.SerialPort serialPortTP;
        private System.IO.Ports.SerialPort serialPortTOC;
        private System.IO.Ports.SerialPort serialPortMPS;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox txtData;
        private System.Windows.Forms.RichTextBox txtRawData;
        private System.Windows.Forms.RadioButton rbtnTOC;
        private System.Windows.Forms.RadioButton rbtnTN;
        private System.Windows.Forms.RadioButton rbtnTP;
        private System.Windows.Forms.RadioButton rbtnMPS;
        private System.Windows.Forms.DataGridView dgvDetailSendingResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvSamplingTest;
        private System.Windows.Forms.DataGridView dgvDoControl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sETTINGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEPORTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.IO.Ports.SerialPort serialPortSAMP;
        private System.Windows.Forms.RadioButton rbtnSAMP;
        private System.IO.Ports.SerialPort serialPortADAM405x;
        private System.Windows.Forms.RadioButton rbtnStationStatus;
    }
}

