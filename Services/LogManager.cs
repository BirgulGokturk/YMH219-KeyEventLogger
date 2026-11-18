using System.Text;
using YMH219_KeyEventLogger.Models;

namespace YMH219_KeyEventLogger.Services
{
    /// <summary>
    /// Klavye olay kayıtlarını, istatistikleri ve dosya dışa aktarma işlemlerini yöneten servis.
    /// </summary>
    public class LogManager
    {
        private readonly List<KeyLogItem> _logs = new();

        /// <summary>
        /// Kaydın aktif olup olmadığını belirtir.
        /// </summary>
        public bool IsLogging { get; set; } = false;

        /// <summary>
        /// Tüm kayıtların salt okunur listesi.
        /// </summary>
        public IReadOnlyList<KeyLogItem> Logs => _logs.AsReadOnly();

        /// <summary>
        /// Yeni bir olay kaydı ekler.
        /// </summary>
        public void AddLog(KeyLogItem item)
        {
            if (!IsLogging) return;
            _logs.Add(item);
        }

        /// <summary>
        /// Tüm kayıtları temizler.
        /// </summary>
        public void Clear()
        {
            _logs.Clear();
        }

        /// <summary>
        /// Kayıtları yerel bir metin (.txt) dosyasına kaydeder.
        /// </summary>
        public void ExportToTextFile(string filePath)
        {
            try
            {
                using var writer = new StreamWriter(filePath, false, Encoding.UTF8);

                writer.WriteLine("================================================================================");
                writer.WriteLine("                      YMH219 - KEY EVENT LOGGER RAPORU                         ");
                writer.WriteLine("================================================================================");
                writer.WriteLine($"Rapor Tarihi   : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"Toplam Kayıt   : {_logs.Count}");
                writer.WriteLine($"Kayıt Kapsamı  : Yalnızca uygulama penceresi odaktayken kaydedilen eğitsel veriler.");
                writer.WriteLine("================================================================================");
                writer.WriteLine();

                foreach (var log in _logs)
                {
                    writer.WriteLine(log.ToFormattedString());
                }

                writer.WriteLine();
                writer.WriteLine("--------------------------------- İSTATİSTİKLER --------------------------------");
                writer.WriteLine(GetStatisticsSummary());
                writer.WriteLine("================================================================================");
            }
            catch (Exception ex)
            {
                throw new IOException($"Dosya yazılırken bir hata oluştu: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Olay türü sayılarını ve en sık basılan tuşları (KeyDown) hesaplar.
        /// </summary>
        public KeyStatistics GetStatistics(int topKeyCount = 7)
        {
            var topKeys = _logs
                .Where(l => l.EventType == KeyEventType.KeyDown)
                .GroupBy(l => l.KeyName)
                .OrderByDescending(g => g.Count())
                .Take(topKeyCount)
                .Select(g => new KeyUsage(g.Key, g.Count()))
                .ToList();

            return new KeyStatistics(
                Total: _logs.Count,
                KeyDownCount: _logs.Count(l => l.EventType == KeyEventType.KeyDown),
                KeyUpCount: _logs.Count(l => l.EventType == KeyEventType.KeyUp),
                KeyPressCount: _logs.Count(l => l.EventType == KeyEventType.KeyPress),
                TopKeys: topKeys);
        }

        /// <summary>
        /// İstatistik özet metnini üretir (.txt raporu için).
        /// </summary>
        public string GetStatisticsSummary()
        {
            if (_logs.Count == 0) return "Henüz kaydedilmiş bir klavye olayı bulunmamaktadır.";

            var stats = GetStatistics(topKeyCount: 5);
            var sb = new StringBuilder();
            sb.AppendLine($"Toplam Olay Sayısı: {stats.Total}");
            sb.AppendLine($"  - KeyDown: {stats.KeyDownCount}");
            sb.AppendLine($"  - KeyUp: {stats.KeyUpCount}");
            sb.AppendLine($"  - KeyPress: {stats.KeyPressCount}");
            sb.AppendLine();
            sb.AppendLine("En Sık Kullanılan Tuşlar (Top 5 KeyDown):");

            int rank = 1;
            foreach (var usage in stats.TopKeys)
            {
                sb.AppendLine($"  {rank++}. {usage.KeyName,-12} : {usage.Count} kez");
            }

            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Bir kaydın arama terimine ve olay türüne uyup uymadığını kontrol eder.
        /// Hem listeye yeni kayıt eklenirken hem de filtre değiştiğinde aynı kural kullanılır.
        /// </summary>
        public static bool Matches(KeyLogItem log, string? searchTerm, KeyEventType? eventTypeFilter)
        {
            if (eventTypeFilter.HasValue && log.EventType != eventTypeFilter.Value)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return true;
            }

            string term = searchTerm.Trim();
            return log.KeyName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                   log.Modifiers.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                   log.KeyCode.ToString().Contains(term);
        }

        /// <summary>
        /// Arama terimine ve olay tipine göre kayıtları filtreler.
        /// </summary>
        public IEnumerable<KeyLogItem> FilterLogs(string? searchTerm, KeyEventType? eventTypeFilter)
        {
            return _logs.Where(l => Matches(l, searchTerm, eventTypeFilter));
        }
    }
}
