namespace Imtahan.DTOs.CardDTOs
{
    public class CardPostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public IFormFile File { get; set; }
    }
}
