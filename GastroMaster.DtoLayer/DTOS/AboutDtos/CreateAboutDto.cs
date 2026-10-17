using System;
using System.Collections.Generic;
using System.Text;

namespace GastroMaster.DtoLayer.DTOS.AboutDtos
{
    public class CreateAboutDto
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
