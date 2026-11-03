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
        /// İstatistik özet metnini üretir.
        /// </summary>
        public string GetStatisticsSummary()
        {
            if (_logs.Count == 0) return "Henüz kaydedilmiş bir klavye olayı bulunmamaktadır.";

            var sb = new StringBuilder();
            sb.AppendLine($"Toplam Olay Sayısı: {_logs.Count}");

            int downCount = _logs.Count(l => l.EventType == KeyEventType.KeyDown);
            int upCount = _logs.Count(l => l.EventType == KeyEventType.KeyUp);
            int pressCount = _logs.Count(l => l.EventType == KeyEventType.KeyPress);

            sb.AppendLine($"  - KeyDown: {downCount}");
            sb.AppendLine($"  - KeyUp: {upCount}");
            sb.AppendLine($"  - KeyPress: {pressCount}");
            sb.AppendLine();
            sb.AppendLine("En Sık Kullanılan Tuşlar (Top 5 KeyDown):");

            var topKeys = _logs
                .Where(l => l.EventType == KeyEventType.KeyDown)
                .GroupBy(l => l.KeyName)
                .OrderByDescending(g => g.Count())
                .Take(5);

            int rank = 1;
            foreach (var group in topKeys)
            {
                sb.AppendLine($"  {rank++}. {group.Key,-12} : {group.Count()} kez");
            }

            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Arama terimine ve olay tipine göre kayıtları filtreler.
        /// </summary>
        public IEnumerable<KeyLogItem> FilterLogs(string? searchTerm, KeyEventType? eventTypeFilter)
        {
            var query = _logs.AsEnumerable();

            if (eventTypeFilter.HasValue)
            {
                query = query.Where(l => l.EventType == eventTypeFilter.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.Trim().ToLowerInvariant();
                query = query.Where(l =>
                    l.KeyName.ToLowerInvariant().Contains(term) ||
                    l.Modifiers.ToLowerInvariant().Contains(term) ||
                    l.KeyCode.ToString().Contains(term));
            }

            return query;
        }
    }
}
