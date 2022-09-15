namespace RCMAppApi.Models
{
    public class BookingDTO
    {
        public BookingDTO()
        {

        }
        public int? Id { get; set; }
        public string? User { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
    }
}
