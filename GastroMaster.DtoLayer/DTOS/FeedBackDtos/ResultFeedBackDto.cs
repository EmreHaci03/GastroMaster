
namespace GastroMaster.DtoLayer.DTOS.FeedBackDtos
{
    public class ResultFeedBackDto
    {
        public int FeedbackId { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
