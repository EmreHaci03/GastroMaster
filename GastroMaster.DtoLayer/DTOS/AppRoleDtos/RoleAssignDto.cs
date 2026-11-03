

namespace GastroMaster.DtoLayer.DTOS.AppRoleDtos
{
    public class RoleAssignDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleCheckDto> roleChecks { get; set; }
    }
}
