namespace GorselProgramlamaOdev3.Model
{
    public class ToDoItem
    {
        public string Id { get; set; }
        public string Baslik { get; set; }
        public string Detay { get; set; }
        public string HedefTarih { get; set; }
        public string HedefSaat { get; set; }
        public bool TamamlandiMi { get; set; }
    }
}