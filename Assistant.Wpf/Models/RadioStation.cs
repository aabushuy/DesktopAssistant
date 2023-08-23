namespace Assistant.Wpf.Models
{
    public class RadioStation
    {
        private static int _ids;

        public int Id { get;}

        public string Name { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public RadioStation()
        {
            Id = ++_ids;
        }
    }
}
