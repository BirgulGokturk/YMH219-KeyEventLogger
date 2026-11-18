using YMH219_KeyEventLogger.Models;
using YMH219_KeyEventLogger.Services;

namespace YMH219_KeyEventLogger;

public partial class Form1 : Form
{
    private readonly LogManager _logManager = new();

    public Form1()
    {
        InitializeComponent();

        // Çift arabellekleme ile liste titremesini önle
        typeof(ListView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.SetProperty |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic,
            null, listViewLogs, new object[] { true });

        UpdateStatusIndicator();
    }

    #region Klavye Olayları (Form Düzeyinde Yakalama)

    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (!ShouldRecordKeys()) return;

        RecordLog(new KeyLogItem(KeyEventType.KeyDown, e.KeyCode, e.Control, e.Alt, e.Shift));
    }

    private void Form1_KeyUp(object? sender, KeyEventArgs e)
    {
        if (!ShouldRecordKeys()) return;

        RecordLog(new KeyLogItem(KeyEventType.KeyUp, e.KeyCode, e.Control, e.Alt, e.Shift));
    }

    private void Form1_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!ShouldRecordKeys()) return;

        RecordLog(new KeyLogItem(e.KeyChar));
    }

    /// <summary>
    /// Tab tuşu Windows tarafından "sonraki kontrole geç" komutu olarak işlenir ve KeyDown
    /// olayı hiç oluşmaz (yalnızca KeyUp kaydediliyordu). ProcessCmdKey, tuş herhangi bir
    /// kontrole ulaşmadan önce çağrıldığı için Tab burada KeyDown olarak kaydedilir ve
    /// kayıt sırasında odağın başka kontrole kayması engellenir.
    /// (Ok tuşları liste ve kutularda normal KeyDown olayı ürettiği için burada ele alınmaz.)
    /// </summary>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if ((keyData & Keys.KeyCode) == Keys.Tab && ShouldRecordKeys())
        {
            bool ctrl = (keyData & Keys.Control) == Keys.Control;
            bool alt = (keyData & Keys.Alt) == Keys.Alt;
            bool shift = (keyData & Keys.Shift) == Keys.Shift;
            RecordLog(new KeyLogItem(KeyEventType.KeyDown, Keys.Tab, ctrl, alt, shift));
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    /// <summary>
    /// Kayıt açıksa ve kullanıcı arama/filtre alanlarına yazmıyorsa true döner.
    /// Arama kutusuna yazılan harfler hem kayda girip hem de listeyi filtreliyordu.
    /// </summary>
    private bool ShouldRecordKeys()
    {
        return _logManager.IsLogging && !txtSearch.Focused && !cmbFilterType.Focused;
    }

    private void RecordLog(KeyLogItem log)
    {
        _logManager.AddLog(log);
        AddLogToView(log);
        UpdateStatisticsUI();
    }

    #endregion

    #region Buton Olayları

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        _logManager.IsLogging = true;
        btnStart.Enabled = false;
        btnStop.Enabled = true;
        UpdateStatusIndicator();

        // Odak arama kutusunda kalırsa basılan tuşlar oraya yazılır; kayıt listesine taşınır
        ActiveControl = listViewLogs;
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        _logManager.IsLogging = false;
        btnStart.Enabled = true;
        btnStop.Enabled = false;
        UpdateStatusIndicator();
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        if (_logManager.Logs.Count == 0) return;

        var result = MessageBox.Show(
            "Ekrandaki tüm klavye kayıtları temizlenecektir. Devam etmek istiyor musunuz?",
            "Kayıtları Temizle",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            _logManager.Clear();
            listViewLogs.Items.Clear();
            UpdateStatisticsUI();
            UpdateStatusIndicator();
        }
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        if (_logManager.Logs.Count == 0)
        {
            MessageBox.Show(
                "Dışa aktarılacak herhangi bir klavye kaydı bulunmamaktadır.",
                "Uyarı",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        using var saveDialog = new SaveFileDialog
        {
            Filter = "Metin Dosyası (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*",
            FileName = $"KeyLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
            Title = "Klavye Olay Kayıtlarını Kaydet"
        };

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                _logManager.ExportToTextFile(saveDialog.FileName);
                MessageBox.Show(
                    $"Kayıtlar başarıyla kaydedildi:\n{saveDialog.FileName}",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Dosya kaydedilirken hata oluştu:\n{ex.Message}",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    #endregion

    #region Arayüz ve Görsel Güncellemeler

    private void UpdateStatusIndicator()
    {
        if (_logManager.IsLogging)
        {
            lblStatusIndicator.Text = "● KAYIT AKTİF (Dinleniyor)";
            lblStatusIndicator.BackColor = Color.FromArgb(220, 245, 220);
            lblStatusIndicator.ForeColor = Color.DarkGreen;
        }
        else
        {
            lblStatusIndicator.Text = "○ KAYIT DURDURULDU";
            lblStatusIndicator.BackColor = Color.FromArgb(240, 240, 240);
            lblStatusIndicator.ForeColor = Color.DimGray;
        }

        lblLogCountStatus.Text = $"Kayıt: {_logManager.Logs.Count} adet";
    }

    private void AddLogToView(KeyLogItem log)
    {
        // Yeni kayıt mevcut filtreye uymuyorsa listede gösterilmez (yine de kaydedilir)
        if (!LogManager.Matches(log, txtSearch.Text, SelectedEventTypeFilter)) return;

        listViewLogs.Items.Add(CreateListViewItem(log));
        listViewLogs.EnsureVisible(listViewLogs.Items.Count - 1);
    }

    /// <summary>
    /// Olay türü açılır listesinde seçili tür; "Tümü" seçiliyse null.
    /// </summary>
    private KeyEventType? SelectedEventTypeFilter =>
        cmbFilterType.SelectedIndex > 0 && Enum.TryParse(cmbFilterType.SelectedItem?.ToString(), out KeyEventType type)
            ? type
            : null;

    /// <summary>
    /// Bir kaydı ListView satırına dönüştürür; olay türüne göre hafif arka plan rengi verir.
    /// </summary>
    private static ListViewItem CreateListViewItem(KeyLogItem log)
    {
        var lvi = new ListViewItem(log.Timestamp.ToString("HH:mm:ss.fff"));
        lvi.SubItems.Add(log.EventType.ToString());
        lvi.SubItems.Add(log.KeyName);
        lvi.SubItems.Add(log.KeyCode.ToString());
        lvi.SubItems.Add(log.Modifiers);

        lvi.BackColor = log.EventType switch
        {
            KeyEventType.KeyDown => Color.FromArgb(245, 250, 255),
            KeyEventType.KeyUp => Color.FromArgb(250, 250, 250),
            _ => Color.FromArgb(240, 255, 240)
        };

        return lvi;
    }

    private void FilterChanged(object? sender, EventArgs e)
    {
        listViewLogs.BeginUpdate();
        listViewLogs.Items.Clear();

        foreach (var log in _logManager.FilterLogs(txtSearch.Text, SelectedEventTypeFilter))
        {
            listViewLogs.Items.Add(CreateListViewItem(log));
        }

        listViewLogs.EndUpdate();
    }

    private void UpdateStatisticsUI()
    {
        var stats = _logManager.GetStatistics(topKeyCount: 7);

        lblTotalEvents.Text = $"Toplam Olay: {stats.Total}";
        lblDownCount.Text = $"KeyDown: {stats.KeyDownCount}";
        lblUpCount.Text = $"KeyUp: {stats.KeyUpCount}";
        lblPressCount.Text = $"KeyPress: {stats.KeyPressCount}";
        lblLogCountStatus.Text = $"Kayıt: {stats.Total} adet";

        // En sık kullanılan tuşlar
        lstTopKeys.BeginUpdate();
        lstTopKeys.Items.Clear();
        int rank = 1;
        foreach (var usage in stats.TopKeys)
        {
            lstTopKeys.Items.Add($"{rank++}. {usage.KeyName} ({usage.Count} kez)");
        }
        lstTopKeys.EndUpdate();
    }

    #endregion
}
