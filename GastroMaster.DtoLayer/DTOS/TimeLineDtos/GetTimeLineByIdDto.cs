

namespace GastroMaster.DtoLayer.DTOS.TimeLineDtos
{
    public class GetTimeLineByIdDto
    {
        public int TimelineId { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
