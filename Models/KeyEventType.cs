namespace YMH219_KeyEventLogger.Models
{
    /// <summary>
    /// Yakalanan klavye olayının türünü belirten numaralandırma.
    /// </summary>
    public enum KeyEventType
    {
        KeyDown,   // Tuşa basılma anı
        KeyUp,     // Tuşun serbest bırakılma anı
        KeyPress   // Karakter üretildiği an
    }
}
