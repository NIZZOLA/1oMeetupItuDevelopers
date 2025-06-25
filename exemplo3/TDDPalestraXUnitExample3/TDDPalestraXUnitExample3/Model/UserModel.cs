using System.ComponentModel.DataAnnotations;
using TDDPalestraXUnitExample3.Model.Enum;

namespace TDDPalestraXUnitExample3.Model;

public class UserModel
{
    [StringLength(50)]
    public string ReferenceId { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    public DateTime BirthDate { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(18)]
    public string DocumentCPF { get; set; }

    [StringLength(18)]
    public string DocumentRG { get; set; }

    public GenderEnum Gender { get; set; }

    [StringLength(15)]
    public string CellPhoneNumber { get; set; }

    [StringLength(50)]
    public string Login { get; set; }

    [StringLength(50)]
    public string Password { get; set; }

    [Required]
    public Guid TenantId { get; set; }

    #region NavigationProperties
    #endregion
}
