

namespace GastroMaster.DtoLayer.DTOS.NotificationDtos
{
    public class CreateNotificationDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Icon { get; set; }
        public bool Status { get; set; }
    }
}
