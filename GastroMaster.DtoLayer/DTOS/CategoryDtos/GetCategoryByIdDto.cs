using System;
using System.Collections.Generic;
using System.Text;

namespace GastroMaster.DtoLayer.DTOS.CategoryDtos
{
    public class GetCategoryByIdDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool CategoryStatus { get; set; }
    }
}
