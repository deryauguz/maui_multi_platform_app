namespace GorselProgramlamaOdev3.Models
{
    public class HavaDurumuVerisi
    {
        public string Name { get; set; } // Þehir Adý (JSON'a bu kaydolacak)

        // MGM'nin 5 günlük tahmin grafiði linki
        // Bu özellik JSON'a kaydedilmez, çalýþma anýnda üretilir.
        public string MgmResimUrl => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";

        // Ýsterseniz aþaðýdaki alanlarý da tutabilirsiniz (API kullanacaksanýz)
        public double Temp { get; set; }
        public string Description { get; set; }
    }
}