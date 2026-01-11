namespace GorselProgramlamaOdev3.Models
{
    public class HaberResponse
    {
        public List<HaberItem> items { get; set; }
    }

    public class HaberItem
    {
        public string title { get; set; }
        public string pubDate { get; set; }
        public string link { get; set; }
        public string thumbnail { get; set; }
        public Enclosure enclosure { get; set; } // Görsellerin asýl bulunduðu yer

        public string Baslik => title;
        public string Tarih => pubDate;

        // XAML bu mülkü okuyacak
        public string Resim
        {
            get
            {
                // 1. Önce enclosure linkine bak (En güvenilir yer)
                if (enclosure != null && !string.IsNullOrEmpty(enclosure.link))
                    return enclosure.link;

                // 2. Yoksa thumbnail'e bak
                if (!string.IsNullOrEmpty(thumbnail))
                    return thumbnail;

                // 3. Hiçbiri yoksa varsayýlan bir görsel ver
                return "https://via.placeholder.com/400x200?text=Haber+Gorseli";
            }
        }
    }

    public class Enclosure
    {
        public string link { get; set; }
        public string type { get; set; }
    }
}