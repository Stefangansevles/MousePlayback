using System.Windows.Forms;

namespace MousePlayback
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabHome = new System.Windows.Forms.TabPage();
            this.btnExport = new MaterialSkin.Controls.MaterialButton();
            this.btnImport = new MaterialSkin.Controls.MaterialButton();
            this.tbLog = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.materialProgressBar1 = new MaterialSkin.Controls.MaterialProgressBar();
            this.lblStatus = new MaterialSkin.Controls.MaterialLabel();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.numRandomSleep = new System.Windows.Forms.NumericUpDown();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.cbRandomizeInput = new MaterialSkin.Controls.MaterialCheckbox();
            this.cbStartStopRecording = new System.Windows.Forms.ComboBox();
            this.cbPlaybackRecording = new System.Windows.Forms.ComboBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.numRepeatTimes = new System.Windows.Forms.NumericUpDown();
            this.lblTimes = new MaterialSkin.Controls.MaterialLabel();
            this.lblRepeat = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.cbRepeatForever = new MaterialSkin.Controls.MaterialCheckbox();
            this.menuIconList = new System.Windows.Forms.ImageList(this.components);
            this.tmrProgress = new System.Windows.Forms.Timer(this.components);
            this.tmrFadeIn = new System.Windows.Forms.Timer(this.components);
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.btnRecord = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnStop = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnPlayback = new Bunifu.Framework.UI.BunifuTileButton();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            this.numPixels = new System.Windows.Forms.NumericUpDown();
            this.materialLabel9 = new MaterialSkin.Controls.MaterialLabel();
            this.materialTabControl1.SuspendLayout();
            this.tabHome.SuspendLayout();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRandomSleep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeatTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixels)).BeginInit();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabHome);
            this.materialTabControl1.Controls.Add(this.tabSettings);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.ImageList = this.menuIconList;
            this.materialTabControl1.Location = new System.Drawing.Point(57, 75);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(339, 327);
            this.materialTabControl1.TabIndex = 3;
            // 
            // tabHome
            // 
            this.tabHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.tabHome.Controls.Add(this.btnExport);
            this.tabHome.Controls.Add(this.btnImport);
            this.tabHome.Controls.Add(this.tbLog);
            this.tabHome.Controls.Add(this.materialProgressBar1);
            this.tabHome.Controls.Add(this.lblStatus);
            this.tabHome.Controls.Add(this.pbStatus);
            this.tabHome.Controls.Add(this.btnRecord);
            this.tabHome.Controls.Add(this.btnStop);
            this.tabHome.Controls.Add(this.btnPlayback);
            this.tabHome.ForeColor = System.Drawing.Color.Transparent;
            this.tabHome.ImageKey = "homeWhite.png";
            this.tabHome.Location = new System.Drawing.Point(4, 31);
            this.tabHome.Name = "tabHome";
            this.tabHome.Padding = new System.Windows.Forms.Padding(3);
            this.tabHome.Size = new System.Drawing.Size(331, 292);
            this.tabHome.TabIndex = 0;
            this.tabHome.Text = "Home";
            // 
            // btnExport
            // 
            this.btnExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExport.Depth = 0;
            this.btnExport.DrawShadows = true;
            this.btnExport.HighEmphasis = true;
            this.btnExport.Icon = null;
            this.btnExport.Location = new System.Drawing.Point(17, 251);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnExport.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(77, 36);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "Export";
            this.btnExport.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnExport.UseAccentColor = false;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImport.Depth = 0;
            this.btnImport.DrawShadows = true;
            this.btnImport.HighEmphasis = true;
            this.btnImport.Icon = null;
            this.btnImport.Location = new System.Drawing.Point(101, 251);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnImport.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(76, 36);
            this.btnImport.TabIndex = 9;
            this.btnImport.Text = "Import";
            this.btnImport.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnImport.UseAccentColor = false;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // tbLog
            // 
            this.tbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.tbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLog.Depth = 0;
            this.tbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbLog.Hint = "";
            this.tbLog.Location = new System.Drawing.Point(15, 122);
            this.tbLog.MouseState = MaterialSkin.MouseState.HOVER;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.tbLog.Size = new System.Drawing.Size(300, 120);
            this.tbLog.TabIndex = 8;
            this.tbLog.Text = "";
            this.tbLog.TextChanged += new System.EventHandler(this.materialMultiLineTextBox1_TextChanged);
            // 
            // materialProgressBar1
            // 
            this.materialProgressBar1.Depth = 0;
            this.materialProgressBar1.Location = new System.Drawing.Point(17, 104);
            this.materialProgressBar1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialProgressBar1.Name = "materialProgressBar1";
            this.materialProgressBar1.Size = new System.Drawing.Size(300, 5);
            this.materialProgressBar1.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.lblStatus.Depth = 0;
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblStatus.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblStatus.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(231, 257);
            this.lblStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 19);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Inactive";
            // 
            // tabSettings
            // 
            this.tabSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.tabSettings.Controls.Add(this.materialLabel9);
            this.tabSettings.Controls.Add(this.numPixels);
            this.tabSettings.Controls.Add(this.materialLabel8);
            this.tabSettings.Controls.Add(this.materialLabel7);
            this.tabSettings.Controls.Add(this.numRandomSleep);
            this.tabSettings.Controls.Add(this.materialLabel6);
            this.tabSettings.Controls.Add(this.materialLabel1);
            this.tabSettings.Controls.Add(this.cbRandomizeInput);
            this.tabSettings.Controls.Add(this.cbStartStopRecording);
            this.tabSettings.Controls.Add(this.cbPlaybackRecording);
            this.tabSettings.Controls.Add(this.materialLabel5);
            this.tabSettings.Controls.Add(this.materialLabel4);
            this.tabSettings.Controls.Add(this.materialLabel3);
            this.tabSettings.Controls.Add(this.numRepeatTimes);
            this.tabSettings.Controls.Add(this.lblTimes);
            this.tabSettings.Controls.Add(this.lblRepeat);
            this.tabSettings.Controls.Add(this.materialLabel2);
            this.tabSettings.Controls.Add(this.cbRepeatForever);
            this.tabSettings.ForeColor = System.Drawing.Color.Transparent;
            this.tabSettings.ImageKey = "round_build_white_24dp.png";
            this.tabSettings.Location = new System.Drawing.Point(4, 31);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(331, 292);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.materialLabel7.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel7.ForeColor = System.Drawing.Color.DimGray;
            this.materialLabel7.Location = new System.Drawing.Point(172, 258);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(166, 19);
            this.materialLabel7.TabIndex = 15;
            this.materialLabel7.Text = "ms after each playback";
            // 
            // numRandomSleep
            // 
            this.numRandomSleep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.numRandomSleep.Location = new System.Drawing.Point(120, 257);
            this.numRandomSleep.Maximum = new decimal(new int[] {
            2147483646,
            0,
            0,
            0});
            this.numRandomSleep.Name = "numRandomSleep";
            this.numRandomSleep.Size = new System.Drawing.Size(45, 20);
            this.numRandomSleep.TabIndex = 14;
            this.numRandomSleep.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numRandomSleep.ValueChanged += new System.EventHandler(this.numRandomSleep_ValueChanged);
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel6.ForeColor = System.Drawing.Color.DimGray;
            this.materialLabel6.Location = new System.Drawing.Point(27, 258);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(107, 19);
            this.materialLabel6.TabIndex = 13;
            this.materialLabel6.Text = "Sleep for max. ";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(15, 199);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(61, 19);
            this.materialLabel1.TabIndex = 12;
            this.materialLabel1.Text = "Random";
            // 
            // cbRandomizeInput
            // 
            this.cbRandomizeInput.AutoSize = true;
            this.cbRandomizeInput.Depth = 0;
            this.cbRandomizeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRandomizeInput.Location = new System.Drawing.Point(23, 219);
            this.cbRandomizeInput.Margin = new System.Windows.Forms.Padding(0);
            this.cbRandomizeInput.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbRandomizeInput.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbRandomizeInput.Name = "cbRandomizeInput";
            this.cbRandomizeInput.Ripple = true;
            this.cbRandomizeInput.Size = new System.Drawing.Size(155, 37);
            this.cbRandomizeInput.TabIndex = 11;
            this.cbRandomizeInput.Text = "Randomize input";
            this.cbRandomizeInput.UseVisualStyleBackColor = true;
            this.cbRandomizeInput.CheckedChanged += new System.EventHandler(this.cbRandomizeInput_CheckedChanged);
            // 
            // cbStartStopRecording
            // 
            this.cbStartStopRecording.FormattingEnabled = true;
            this.cbStartStopRecording.Items.AddRange(new object[] {
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12"});
            this.cbStartStopRecording.Location = new System.Drawing.Point(172, 141);
            this.cbStartStopRecording.Name = "cbStartStopRecording";
            this.cbStartStopRecording.Size = new System.Drawing.Size(63, 21);
            this.cbStartStopRecording.TabIndex = 10;
            this.cbStartStopRecording.Text = "F1";
            this.cbStartStopRecording.SelectedIndexChanged += new System.EventHandler(this.cbStartStopRecording_SelectedIndexChanged);
            // 
            // cbPlaybackRecording
            // 
            this.cbPlaybackRecording.FormattingEnabled = true;
            this.cbPlaybackRecording.Items.AddRange(new object[] {
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12"});
            this.cbPlaybackRecording.Location = new System.Drawing.Point(172, 169);
            this.cbPlaybackRecording.Name = "cbPlaybackRecording";
            this.cbPlaybackRecording.Size = new System.Drawing.Size(63, 21);
            this.cbPlaybackRecording.TabIndex = 9;
            this.cbPlaybackRecording.Text = "F2";
            this.cbPlaybackRecording.SelectedIndexChanged += new System.EventHandler(this.cbPlaybackRecording_SelectedIndexChanged);
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.ForeColor = System.Drawing.Color.DimGray;
            this.materialLabel5.Location = new System.Drawing.Point(31, 171);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(144, 19);
            this.materialLabel5.TabIndex = 8;
            this.materialLabel5.Text = "Play back recording:";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.ForeColor = System.Drawing.Color.DimGray;
            this.materialLabel4.Location = new System.Drawing.Point(31, 143);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(147, 19);
            this.materialLabel4.TabIndex = 7;
            this.materialLabel4.Text = "Start/stop recording:";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(15, 113);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(58, 19);
            this.materialLabel3.TabIndex = 6;
            this.materialLabel3.Text = "Hotkeys";
            // 
            // numRepeatTimes
            // 
            this.numRepeatTimes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.numRepeatTimes.Location = new System.Drawing.Point(95, 42);
            this.numRepeatTimes.Maximum = new decimal(new int[] {
            2147483646,
            0,
            0,
            0});
            this.numRepeatTimes.Name = "numRepeatTimes";
            this.numRepeatTimes.Size = new System.Drawing.Size(45, 20);
            this.numRepeatTimes.TabIndex = 5;
            this.numRepeatTimes.Value = new decimal(new int[] {
            124312412,
            0,
            0,
            0});
            this.numRepeatTimes.ValueChanged += new System.EventHandler(this.numRepeatTimes_ValueChanged);
            // 
            // lblTimes
            // 
            this.lblTimes.AutoSize = true;
            this.lblTimes.Depth = 0;
            this.lblTimes.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTimes.Location = new System.Drawing.Point(147, 41);
            this.lblTimes.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(40, 19);
            this.lblTimes.TabIndex = 3;
            this.lblTimes.Text = "times";
            // 
            // lblRepeat
            // 
            this.lblRepeat.AutoSize = true;
            this.lblRepeat.Depth = 0;
            this.lblRepeat.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblRepeat.Location = new System.Drawing.Point(31, 41);
            this.lblRepeat.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblRepeat.Name = "lblRepeat";
            this.lblRepeat.Size = new System.Drawing.Size(58, 19);
            this.lblRepeat.TabIndex = 2;
            this.lblRepeat.Text = "Repeat: ";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(15, 12);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(66, 19);
            this.materialLabel2.TabIndex = 0;
            this.materialLabel2.Text = "Playback";
            // 
            // cbRepeatForever
            // 
            this.cbRepeatForever.AutoSize = true;
            this.cbRepeatForever.Depth = 0;
            this.cbRepeatForever.Font = new System.Drawing.Font("Microsoft Sans Serif", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRepeatForever.Location = new System.Drawing.Point(24, 65);
            this.cbRepeatForever.Margin = new System.Windows.Forms.Padding(0);
            this.cbRepeatForever.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbRepeatForever.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbRepeatForever.Name = "cbRepeatForever";
            this.cbRepeatForever.Ripple = true;
            this.cbRepeatForever.Size = new System.Drawing.Size(137, 37);
            this.cbRepeatForever.TabIndex = 5;
            this.cbRepeatForever.Text = "Repeat forever";
            this.cbRepeatForever.UseVisualStyleBackColor = true;
            this.cbRepeatForever.CheckedChanged += new System.EventHandler(this.cbRepeatForever_CheckedChanged);
            // 
            // menuIconList
            // 
            this.menuIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("menuIconList.ImageStream")));
            this.menuIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.menuIconList.Images.SetKeyName(0, "homeWhite.png");
            this.menuIconList.Images.SetKeyName(1, "round_build_white_24dp.png");
            // 
            // tmrProgress
            // 
            this.tmrProgress.Interval = 1;
            this.tmrProgress.Tick += new System.EventHandler(this.tmrProgress_Tick);
            // 
            // tmrFadeIn
            // 
            this.tmrFadeIn.Interval = 10;
            this.tmrFadeIn.Tick += new System.EventHandler(this.tmrFadeIn_Tick);
            // 
            // pbStatus
            // 
            this.pbStatus.BackgroundImage = global::MousePlayback.Properties.Resources.blueZzz;
            this.pbStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbStatus.Location = new System.Drawing.Point(193, 254);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(25, 25);
            this.pbStatus.TabIndex = 3;
            this.pbStatus.TabStop = false;
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRecord.color = System.Drawing.Color.RoyalBlue;
            this.btnRecord.colorActive = System.Drawing.Color.CornflowerBlue;
            this.btnRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecord.Font = new System.Drawing.Font("Century Gothic", 11.75F);
            this.btnRecord.ForeColor = System.Drawing.Color.White;
            this.btnRecord.Image = global::MousePlayback.Properties.Resources.record31;
            this.btnRecord.ImagePosition = 18;
            this.btnRecord.ImageZoom = 40;
            this.btnRecord.LabelPosition = 36;
            this.btnRecord.LabelText = "Record";
            this.btnRecord.Location = new System.Drawing.Point(17, 8);
            this.btnRecord.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(92, 88);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnStop.color = System.Drawing.Color.RoyalBlue;
            this.btnStop.colorActive = System.Drawing.Color.CornflowerBlue;
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.Font = new System.Drawing.Font("Century Gothic", 11.75F);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Image = global::MousePlayback.Properties.Resources.stopwhite;
            this.btnStop.ImagePosition = 18;
            this.btnStop.ImageZoom = 35;
            this.btnStop.LabelPosition = 36;
            this.btnStop.LabelText = "Stop";
            this.btnStop.Location = new System.Drawing.Point(121, 8);
            this.btnStop.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(92, 88);
            this.btnStop.TabIndex = 1;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlayback
            // 
            this.btnPlayback.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnPlayback.color = System.Drawing.Color.RoyalBlue;
            this.btnPlayback.colorActive = System.Drawing.Color.CornflowerBlue;
            this.btnPlayback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayback.Font = new System.Drawing.Font("Century Gothic", 11.75F);
            this.btnPlayback.ForeColor = System.Drawing.Color.White;
            this.btnPlayback.Image = global::MousePlayback.Properties.Resources.playWhite;
            this.btnPlayback.ImagePosition = 18;
            this.btnPlayback.ImageZoom = 40;
            this.btnPlayback.LabelPosition = 36;
            this.btnPlayback.LabelText = "Playback";
            this.btnPlayback.Location = new System.Drawing.Point(225, 8);
            this.btnPlayback.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnPlayback.Name = "btnPlayback";
            this.btnPlayback.Size = new System.Drawing.Size(92, 88);
            this.btnPlayback.TabIndex = 2;
            this.btnPlayback.Click += new System.EventHandler(this.btnPlayback_Click);
            // 
            // materialLabel8
            // 
            this.materialLabel8.AutoSize = true;
            this.materialLabel8.Depth = 0;
            this.materialLabel8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.materialLabel8.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel8.ForeColor = System.Drawing.Color.DimGray;
            this.materialLabel8.Location = new System.Drawing.Point(27, 286);
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            this.materialLabel8.Size = new System.Drawing.Size(240, 19);
            this.materialLabel8.TabIndex = 16;
            this.materialLabel8.Text = "Randomize cursor between 1 and ";
            // 
            // numPixels
            // 
            this.numPixels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.numPixels.Location = new System.Drawing.Point(244, 286);
            this.numPixels.Name = "numPixels";
            this.numPixels.Size = new System.Drawing.Size(45, 20);
            this.numPixels.TabIndex = 17;
            this.numPixels.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPixels.ValueChanged += new System.EventHandler(this.numPixels_ValueChanged);
            // 
            // materialLabel9
            // 
            this.materialLabel9.AutoSize = true;
            this.materialLabel9.Depth = 0;
            this.materialLabel9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.materialLabel9.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel9.ForeColor = System.Drawing.Color.DimGray;
            this.materialLabel9.Location = new System.Drawing.Point(295, 286);
            this.materialLabel9.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel9.Name = "materialLabel9";
            this.materialLabel9.Size = new System.Drawing.Size(42, 19);
            this.materialLabel9.TabIndex = 18;
            this.materialLabel9.Text = "pixels";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(392, 405);
            this.Controls.Add(this.materialTabControl1);
            this.DrawerShowIconsWhenHidden = true;
            this.DrawerTabControl = this.materialTabControl1;
            this.DrawerWidth = 150;
            this.ForeColor = System.Drawing.Color.Red;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(398, 405);
            this.Name = "Form1";
            this.Text = "MousePlayback";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tabHome.ResumeLayout(false);
            this.tabHome.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRandomSleep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRepeatTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuTileButton btnRecord;
        private Bunifu.Framework.UI.BunifuTileButton btnStop;
        private Bunifu.Framework.UI.BunifuTileButton btnPlayback;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private TabPage tabHome;
        private TabPage tabSettings;
        private PictureBox pbStatus;
        private MaterialSkin.Controls.MaterialLabel lblStatus;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialCheckbox cbRepeatForever;
        private ImageList menuIconList;
        private MaterialSkin.Controls.MaterialLabel lblTimes;
        private MaterialSkin.Controls.MaterialLabel lblRepeat;
        private NumericUpDown numRepeatTimes;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialProgressBar materialProgressBar1;
        private Timer tmrProgress;
        private ComboBox cbStartStopRecording;
        private ComboBox cbPlaybackRecording;
        private MaterialSkin.Controls.MaterialCheckbox cbRandomizeInput;
        private MaterialSkin.Controls.MaterialMultiLineTextBox tbLog;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private NumericUpDown numRandomSleep;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialButton btnImport;
        private MaterialSkin.Controls.MaterialButton btnExport;
        private Timer tmrFadeIn;
        private NumericUpDown numPixels;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
        private MaterialSkin.Controls.MaterialLabel materialLabel9;
    }
}

