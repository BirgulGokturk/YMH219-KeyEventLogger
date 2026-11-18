namespace YMH219_KeyEventLogger.Models
{
    /// <summary>
    /// Kayıtlardan hesaplanan istatistik özeti. Hem arayüzdeki istatistik paneli
    /// hem de .txt raporu aynı hesaplamayı kullanır.
    /// </summary>
    public record KeyStatistics(
        int Total,
        int KeyDownCount,
        int KeyUpCount,
        int KeyPressCount,
        IReadOnlyList<KeyUsage> TopKeys);

    /// <summary>
    /// Bir tuşun kaç kez basıldığını tutar.
    /// </summary>
    public record KeyUsage(string KeyName, int Count);
}
