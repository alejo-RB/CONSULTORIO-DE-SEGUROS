using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULTORIO_DE_SEGUROS.Models;

public partial class Asegurados
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Cédula")]
    public string Cedula { get; set; } = null!;

    [Display(Name = "Nombre del Cliente")]
    public string NombreCliente { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int Edad { get; set; }

    public virtual ICollection<AseguradosSeguros> AseguradosSeguros { get; set; } = new List<AseguradosSeguros>();
}
