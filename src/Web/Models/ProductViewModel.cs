namespace Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PicturePath { get; set; }
        public string PlatformLogo { get; set; }
        public int DiscountRate { get; set; }
        public string ReleaseDate { get; set; }
        public string Genres { get; set; }
    }
}
