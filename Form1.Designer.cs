namespace YMH219_KeyEventLogger;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Panel topPanel;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.Label lblStatusIndicator;

    private System.Windows.Forms.Panel filterPanel;
    private System.Windows.Forms.Label lblSearch;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.Label lblFilterType;
    private System.Windows.Forms.ComboBox cmbFilterType;

    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.ListView listViewLogs;
    private System.Windows.Forms.ColumnHeader colTime;
    private System.Windows.Forms.ColumnHeader colType;
    private System.Windows.Forms.ColumnHeader colKey;
    private System.Windows.Forms.ColumnHeader colCode;
    private System.Windows.Forms.ColumnHeader colMods;

    private System.Windows.Forms.GroupBox grpStats;
    private System.Windows.Forms.Label lblTotalEvents;
    private System.Windows.Forms.Label lblDownCount;
    private System.Windows.Forms.Label lblUpCount;
    private System.Windows.Forms.Label lblPressCount;
    private System.Windows.Forms.Label lblTopKeysTitle;
    private System.Windows.Forms.ListBox lstTopKeys;

    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel lblFooterNotice;
    private System.Windows.Forms.ToolStripStatusLabel lblLogCountStatus;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        topPanel = new System.Windows.Forms.Panel();
        btnStart = new System.Windows.Forms.Button();
        btnStop = new System.Windows.Forms.Button();
        btnClear = new System.Windows.Forms.Button();
        btnExport = new System.Windows.Forms.Button();
        lblStatusIndicator = new System.Windows.Forms.Label();

        filterPanel = new System.Windows.Forms.Panel();
        lblSearch = new System.Windows.Forms.Label();
        txtSearch = new System.Windows.Forms.TextBox();
        lblFilterType = new System.Windows.Forms.Label();
        cmbFilterType = new System.Windows.Forms.ComboBox();

        splitContainer = new System.Windows.Forms.SplitContainer();
        listViewLogs = new System.Windows.Forms.ListView();
        colTime = new System.Windows.Forms.ColumnHeader();
        colType = new System.Windows.Forms.ColumnHeader();
        colKey = new System.Windows.Forms.ColumnHeader();
        colCode = new System.Windows.Forms.ColumnHeader();
        colMods = new System.Windows.Forms.ColumnHeader();

        grpStats = new System.Windows.Forms.GroupBox();
        lblTotalEvents = new System.Windows.Forms.Label();
        lblDownCount = new System.Windows.Forms.Label();
        lblUpCount = new System.Windows.Forms.Label();
        lblPressCount = new System.Windows.Forms.Label();
        lblTopKeysTitle = new System.Windows.Forms.Label();
        lstTopKeys = new System.Windows.Forms.ListBox();

        statusStrip = new System.Windows.Forms.StatusStrip();
        lblFooterNotice = new System.Windows.Forms.ToolStripStatusLabel();
        lblLogCountStatus = new System.Windows.Forms.ToolStripStatusLabel();

        topPanel.SuspendLayout();
        filterPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
        splitContainer.Panel1.SuspendLayout();
        splitContainer.Panel2.SuspendLayout();
        splitContainer.SuspendLayout();
        grpStats.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();

        // 
        // topPanel
        // 
        topPanel.BackColor = Color.FromArgb(245, 247, 250);
        topPanel.BorderStyle = BorderStyle.FixedSingle;
        topPanel.Controls.Add(btnStart);
        topPanel.Controls.Add(btnStop);
        topPanel.Controls.Add(btnClear);
        topPanel.Controls.Add(btnExport);
        topPanel.Controls.Add(lblStatusIndicator);
        topPanel.Dock = DockStyle.Top;
        topPanel.Location = new Point(0, 0);
        topPanel.Name = "topPanel";
        topPanel.Size = new Size(884, 55);
        topPanel.TabIndex = 0;

        // 
        // btnStart
        // 
        btnStart.BackColor = Color.FromArgb(46, 139, 87);
        btnStart.FlatStyle = FlatStyle.Flat;
        btnStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnStart.ForeColor = Color.White;
        btnStart.Location = new Point(12, 10);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(125, 34);
        btnStart.TabIndex = 0;
        btnStart.Text = "▶ Kaydı Başlat";
        btnStart.UseVisualStyleBackColor = false;
        btnStart.Click += BtnStart_Click;

        // 
        // btnStop
        // 
        btnStop.BackColor = Color.FromArgb(205, 92, 92);
        btnStop.Enabled = false;
        btnStop.FlatStyle = FlatStyle.Flat;
        btnStop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnStop.ForeColor = Color.White;
        btnStop.Location = new Point(143, 10);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(125, 34);
        btnStop.TabIndex = 1;
        btnStop.Text = "⏹ Kaydı Durdur";
        btnStop.UseVisualStyleBackColor = false;
        btnStop.Click += BtnStop_Click;

        // 
        // btnClear
        // 
        btnClear.BackColor = Color.FromArgb(240, 240, 240);
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 9F);
        btnClear.ForeColor = Color.Black;
        btnClear.Location = new Point(274, 10);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(110, 34);
        btnClear.TabIndex = 2;
        btnClear.Text = "🗑 Temizle";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += BtnClear_Click;

        // 
        // btnExport
        // 
        btnExport.BackColor = Color.FromArgb(70, 130, 180);
        btnExport.FlatStyle = FlatStyle.Flat;
        btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnExport.ForeColor = Color.White;
        btnExport.Location = new Point(390, 10);
        btnExport.Name = "btnExport";
        btnExport.Size = new Size(140, 34);
        btnExport.TabIndex = 3;
        btnExport.Text = "💾 .txt Olarak Kaydet";
        btnExport.UseVisualStyleBackColor = false;
        btnExport.Click += BtnExport_Click;

        // 
        // lblStatusIndicator
        // 
        lblStatusIndicator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblStatusIndicator.BackColor = Color.FromArgb(230, 230, 230);
        lblStatusIndicator.BorderStyle = BorderStyle.FixedSingle;
        lblStatusIndicator.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblStatusIndicator.ForeColor = Color.DimGray;
        lblStatusIndicator.Location = new Point(620, 11);
        lblStatusIndicator.Name = "lblStatusIndicator";
        lblStatusIndicator.Size = new Size(250, 32);
        lblStatusIndicator.TabIndex = 4;
        lblStatusIndicator.Text = "○ KAYIT DURDURULDU";
        lblStatusIndicator.TextAlign = ContentAlignment.MiddleCenter;

        // 
        // filterPanel
        // 
        filterPanel.BackColor = Color.White;
        filterPanel.Controls.Add(lblSearch);
        filterPanel.Controls.Add(txtSearch);
        filterPanel.Controls.Add(lblFilterType);
        filterPanel.Controls.Add(cmbFilterType);
        filterPanel.Dock = DockStyle.Top;
        filterPanel.Location = new Point(0, 55);
        filterPanel.Name = "filterPanel";
        filterPanel.Size = new Size(884, 38);
        filterPanel.TabIndex = 1;

        // 
        // lblSearch
        // 
        lblSearch.AutoSize = true;
        lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSearch.Location = new Point(14, 11);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(65, 15);
        lblSearch.TabIndex = 0;
        lblSearch.Text = "Filtre / Ara:";

        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(85, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Tuş adı veya değiştirici ara...";
        txtSearch.Size = new Size(220, 23);
        txtSearch.TabIndex = 1;
        txtSearch.TextChanged += FilterChanged;

        // 
        // lblFilterType
        // 
        lblFilterType.AutoSize = true;
        lblFilterType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblFilterType.Location = new Point(325, 11);
        lblFilterType.Name = "lblFilterType";
        lblFilterType.Size = new Size(61, 15);
        lblFilterType.TabIndex = 2;
        lblFilterType.Text = "Olay Türü:";

        // 
        // cmbFilterType
        // 
        cmbFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFilterType.FormattingEnabled = true;
        cmbFilterType.Items.AddRange(new object[] { "Tümü", "KeyDown", "KeyUp", "KeyPress" });
        cmbFilterType.Location = new Point(392, 8);
        cmbFilterType.Name = "cmbFilterType";
        cmbFilterType.Size = new Size(125, 23);
        cmbFilterType.TabIndex = 3;
        cmbFilterType.SelectedIndex = 0;
        cmbFilterType.SelectedIndexChanged += FilterChanged;

        // 
        // splitContainer
        // 
        splitContainer.Dock = DockStyle.Fill;
        splitContainer.FixedPanel = FixedPanel.Panel2;
        splitContainer.Location = new Point(0, 93);
        splitContainer.Name = "splitContainer";
        // 
        // splitContainer.Panel1
        // 
        splitContainer.Panel1.Controls.Add(listViewLogs);
        // 
        // splitContainer.Panel2
        // 
        splitContainer.Panel2.Controls.Add(grpStats);
        splitContainer.Panel2.Padding = new Padding(6);
        splitContainer.Size = new Size(884, 439);
        splitContainer.SplitterDistance = 620;
        splitContainer.TabIndex = 2;

        // 
        // listViewLogs
        // 
        listViewLogs.Columns.AddRange(new ColumnHeader[] {
            colTime,
            colType,
            colKey,
            colCode,
            colMods
        });
        listViewLogs.Dock = DockStyle.Fill;
        listViewLogs.FullRowSelect = true;
        listViewLogs.GridLines = true;
        listViewLogs.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        listViewLogs.Location = new Point(0, 0);
        listViewLogs.Name = "listViewLogs";
        listViewLogs.Size = new Size(620, 439);
        listViewLogs.TabIndex = 0;
        listViewLogs.UseCompatibleStateImageBehavior = false;
        listViewLogs.View = View.Details;

        // 
        // colTime
        // 
        colTime.Text = "Zaman Damgası";
        colTime.Width = 135;

        // 
        // colType
        // 
        colType.Text = "Olay Türü";
        colType.Width = 90;

        // 
        // colKey
        // 
        colKey.Text = "Tuş Adı";
        colKey.Width = 140;

        // 
        // colCode
        // 
        colCode.Text = "Tuş Kodu";
        colCode.Width = 85;

        // 
        // colMods
        // 
        colMods.Text = "Değiştiriciler (Ctrl/Alt/Shift)";
        colMods.Width = 150;

        // 
        // grpStats
        // 
        grpStats.Controls.Add(lblTotalEvents);
        grpStats.Controls.Add(lblDownCount);
        grpStats.Controls.Add(lblUpCount);
        grpStats.Controls.Add(lblPressCount);
        grpStats.Controls.Add(lblTopKeysTitle);
        grpStats.Controls.Add(lstTopKeys);
        grpStats.Dock = DockStyle.Fill;
        grpStats.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grpStats.Location = new Point(6, 6);
        grpStats.Name = "grpStats";
        grpStats.Size = new Size(248, 427);
        grpStats.TabIndex = 0;
        grpStats.TabStop = false;
        grpStats.Text = "📊 İstatistikler";

        // 
        // lblTotalEvents
        // 
        lblTotalEvents.AutoSize = true;
        lblTotalEvents.Font = new Font("Segoe UI", 9F);
        lblTotalEvents.Location = new Point(12, 28);
        lblTotalEvents.Name = "lblTotalEvents";
        lblTotalEvents.Size = new Size(111, 15);
        lblTotalEvents.TabIndex = 0;
        lblTotalEvents.Text = "Toplam Olay: 0";

        // 
        // lblDownCount
        // 
        lblDownCount.AutoSize = true;
        lblDownCount.Font = new Font("Segoe UI", 9F);
        lblDownCount.Location = new Point(12, 50);
        lblDownCount.Name = "lblDownCount";
        lblDownCount.Size = new Size(77, 15);
        lblDownCount.TabIndex = 1;
        lblDownCount.Text = "KeyDown: 0";

        // 
        // lblUpCount
        // 
        lblUpCount.AutoSize = true;
        lblUpCount.Font = new Font("Segoe UI", 9F);
        lblUpCount.Location = new Point(12, 72);
        lblUpCount.Name = "lblUpCount";
        lblUpCount.Size = new Size(61, 15);
        lblUpCount.TabIndex = 2;
        lblUpCount.Text = "KeyUp: 0";

        // 
        // lblPressCount
        // 
        lblPressCount.AutoSize = true;
        lblPressCount.Font = new Font("Segoe UI", 9F);
        lblPressCount.Location = new Point(12, 94);
        lblPressCount.Name = "lblPressCount";
        lblPressCount.Size = new Size(72, 15);
        lblPressCount.TabIndex = 3;
        lblPressCount.Text = "KeyPress: 0";

        // 
        // lblTopKeysTitle
        // 
        lblTopKeysTitle.AutoSize = true;
        lblTopKeysTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTopKeysTitle.Location = new Point(12, 126);
        lblTopKeysTitle.Name = "lblTopKeysTitle";
        lblTopKeysTitle.Size = new Size(128, 15);
        lblTopKeysTitle.TabIndex = 4;
        lblTopKeysTitle.Text = "En Sık Basılan Tuşlar:";

        // 
        // lstTopKeys
        // 
        lstTopKeys.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lstTopKeys.Font = new Font("Segoe UI", 9F);
        lstTopKeys.FormattingEnabled = true;
        lstTopKeys.ItemHeight = 15;
        lstTopKeys.Location = new Point(12, 148);
        lstTopKeys.Name = "lstTopKeys";
        lstTopKeys.Size = new Size(224, 259);
        lstTopKeys.TabIndex = 5;

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] {
            lblFooterNotice,
            lblLogCountStatus
        });
        statusStrip.Location = new Point(0, 532);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(884, 22);
        statusStrip.TabIndex = 3;

        // 
        // lblFooterNotice
        // 
        lblFooterNotice.ForeColor = Color.DarkSlateGray;
        lblFooterNotice.Name = "lblFooterNotice";
        lblFooterNotice.Size = new Size(642, 17);
        lblFooterNotice.Spring = true;
        lblFooterNotice.Text = "Kapsam Sınırı: Yalnızca bu pencere odaktayken tuşlar yakalanır. Eğitsel amaçlıdır.";
        lblFooterNotice.TextAlign = ContentAlignment.MiddleLeft;

        // 
        // lblLogCountStatus
        // 
        lblLogCountStatus.Name = "lblLogCountStatus";
        lblLogCountStatus.Size = new Size(77, 17);
        lblLogCountStatus.Text = "Kayıt: 0 adet";

        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(884, 554);
        Controls.Add(splitContainer);
        Controls.Add(filterPanel);
        Controls.Add(topPanel);
        Controls.Add(statusStrip);
        KeyPreview = true;
        MinimumSize = new Size(720, 480);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "YMH219 - Key Event Logger (Eğitsel Klavye Olay Takipçisi)";
        KeyDown += Form1_KeyDown;
        KeyUp += Form1_KeyUp;
        KeyPress += Form1_KeyPress;
        topPanel.ResumeLayout(false);
        filterPanel.ResumeLayout(false);
        filterPanel.PerformLayout();
        splitContainer.Panel1.ResumeLayout(false);
        splitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
        splitContainer.ResumeLayout(false);
        grpStats.ResumeLayout(false);
        grpStats.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
