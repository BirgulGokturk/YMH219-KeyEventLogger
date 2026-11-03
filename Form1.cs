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
        if (!_logManager.IsLogging) return;

        // Arama kutusundayken tuşların normal çalışmasına engel olmamak için
        var log = new KeyLogItem(KeyEventType.KeyDown, e.KeyCode, e.Control, e.Alt, e.Shift);
        _logManager.AddLog(log);

        AddLogToView(log);
        UpdateStatisticsUI();
    }

    private void Form1_KeyUp(object? sender, KeyEventArgs e)
    {
        if (!_logManager.IsLogging) return;

        var log = new KeyLogItem(KeyEventType.KeyUp, e.KeyCode, e.Control, e.Alt, e.Shift);
        _logManager.AddLog(log);

        AddLogToView(log);
        UpdateStatisticsUI();
    }

    private void Form1_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!_logManager.IsLogging) return;

        var log = new KeyLogItem(e.KeyChar);
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
        // Filtre kontrolü
        if (!MatchesCurrentFilter(log)) return;

        var lvi = new ListViewItem(log.Timestamp.ToString("HH:mm:ss.fff"));
        lvi.SubItems.Add(log.EventType.ToString());
        lvi.SubItems.Add(log.KeyName);
        lvi.SubItems.Add(log.KeyCode.ToString());
        lvi.SubItems.Add(log.Modifiers);

        // Olay türüne göre hafif arka plan tonlaması
        if (log.EventType == KeyEventType.KeyDown)
        {
            lvi.BackColor = Color.FromArgb(245, 250, 255);
        }
        else if (log.EventType == KeyEventType.KeyUp)
        {
            lvi.BackColor = Color.FromArgb(250, 250, 250);
        }
        else if (log.EventType == KeyEventType.KeyPress)
        {
            lvi.BackColor = Color.FromArgb(240, 255, 240);
        }

        listViewLogs.Items.Add(lvi);
        listViewLogs.EnsureVisible(listViewLogs.Items.Count - 1);
    }

    private bool MatchesCurrentFilter(KeyLogItem log)
    {
        // Olay türü filtresi
        if (cmbFilterType.SelectedIndex > 0)
        {
            string selected = cmbFilterType.SelectedItem?.ToString() ?? "";
            if (log.EventType.ToString() != selected) return false;
        }

        // Metin arama filtresi
        string query = txtSearch.Text.Trim();
        if (!string.IsNullOrEmpty(query))
        {
            bool matches = log.KeyName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                           log.Modifiers.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                           log.KeyCode.ToString().Contains(query);
            if (!matches) return false;
        }

        return true;
    }

    private void FilterChanged(object? sender, EventArgs e)
    {
        listViewLogs.BeginUpdate();
        listViewLogs.Items.Clear();

        foreach (var log in _logManager.Logs)
        {
            if (MatchesCurrentFilter(log))
            {
                var lvi = new ListViewItem(log.Timestamp.ToString("HH:mm:ss.fff"));
                lvi.SubItems.Add(log.EventType.ToString());
                lvi.SubItems.Add(log.KeyName);
                lvi.SubItems.Add(log.KeyCode.ToString());
                lvi.SubItems.Add(log.Modifiers);

                if (log.EventType == KeyEventType.KeyDown)
                    lvi.BackColor = Color.FromArgb(245, 250, 255);
                else if (log.EventType == KeyEventType.KeyUp)
                    lvi.BackColor = Color.FromArgb(250, 250, 250);
                else
                    lvi.BackColor = Color.FromArgb(240, 255, 240);

                listViewLogs.Items.Add(lvi);
            }
        }

        listViewLogs.EndUpdate();
    }

    private void UpdateStatisticsUI()
    {
        int total = _logManager.Logs.Count;
        int down = _logManager.Logs.Count(l => l.EventType == KeyEventType.KeyDown);
        int up = _logManager.Logs.Count(l => l.EventType == KeyEventType.KeyUp);
        int press = _logManager.Logs.Count(l => l.EventType == KeyEventType.KeyPress);

        lblTotalEvents.Text = $"Toplam Olay: {total}";
        lblDownCount.Text = $"KeyDown: {down}";
        lblUpCount.Text = $"KeyUp: {up}";
        lblPressCount.Text = $"KeyPress: {press}";
        lblLogCountStatus.Text = $"Kayıt: {total} adet";

        // En sık kullanılan tuşlar
        var top = _logManager.Logs
            .Where(l => l.EventType == KeyEventType.KeyDown)
            .GroupBy(l => l.KeyName)
            .OrderByDescending(g => g.Count())
            .Take(7);

        lstTopKeys.BeginUpdate();
        lstTopKeys.Items.Clear();
        int rank = 1;
        foreach (var item in top)
        {
            lstTopKeys.Items.Add($"{rank++}. {item.Key} ({item.Count()} kez)");
        }
        lstTopKeys.EndUpdate();
    }

    #endregion
}
