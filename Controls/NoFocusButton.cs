namespace YMH219_KeyEventLogger.Controls
{
    /// <summary>
    /// Klavye odağı almayan buton. Normal bir buton tıklandığında odağı üzerine çeker;
    /// bu durumda kayıt sırasında basılan Boşluk veya Enter tuşu o butonu yeniden tetikler
    /// (ör. "Kaydı Durdur" butonu kaydı durdurur). Bu sınıf, Button sınıfından kalıtım alarak
    /// yalnızca "seçilebilir olma" davranışını kapatır; tıklama ile çalışmaya devam eder.
    /// </summary>
    public class NoFocusButton : Button
    {
        public NoFocusButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
