namespace YMH219_KeyEventLogger.Models
{
    /// <summary>
    /// Tek bir klavye olayına ait detayları saklayan model sınıfı.
    /// </summary>
    public class KeyLogItem
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public KeyEventType EventType { get; set; }
        public string KeyName { get; set; } = string.Empty;
        public int KeyCode { get; set; }
        public string Modifiers { get; set; } = "Yok";
        public char? Character { get; set; }

        public KeyLogItem() { }

        public KeyLogItem(KeyEventType eventType, Keys key, bool ctrl, bool alt, bool shift)
        {
            Timestamp = DateTime.Now;
            EventType = eventType;
            KeyName = key.ToString();
            KeyCode = (int)key;

            List<string> mods = new();
            if (ctrl) mods.Add("Ctrl");
            if (alt) mods.Add("Alt");
            if (shift) mods.Add("Shift");
            Modifiers = mods.Count > 0 ? string.Join(" + ", mods) : "Yok";
        }

        public KeyLogItem(char character)
        {
            Timestamp = DateTime.Now;
            EventType = KeyEventType.KeyPress;
            Character = character;
            KeyName = char.IsControl(character) ? $"[Ctrl+{((int)character)}]" : character.ToString();
            KeyCode = (int)character;
            Modifiers = "Yok";
        }

        /// <summary>
        /// .txt dosyasına veya metin alanına yazılacak standart format.
        /// </summary>
        public string ToFormattedString()
        {
            string timeStr = Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string charInfo = Character.HasValue && !char.IsControl(Character.Value) ? $" ('{Character.Value}')" : "";
            return $"[{timeStr}] [{EventType,-8}] Tuş: {KeyName,-12} | Kod: {KeyCode,4}{charInfo} | Değiştirici: {Modifiers}";
        }

        public override string ToString()
        {
            return ToFormattedString();
        }
    }
}
