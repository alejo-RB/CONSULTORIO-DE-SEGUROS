using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CONSULTORIO_DE_SEGUROS.Models;

public partial class Seguros
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Nombre del Seguro")]
    public string NombreSeguro { get; set; } = null!;

    [Display(Name = "Código del Seguro")]
    public string CodigoSeguro { get; set; } = null!;

    [Display(Name = "Suma Asegurada")]
    public decimal SumaAsegurada { get; set; }

    public decimal Prima { get; set; }

    public virtual ICollection<AseguradosSeguros> AseguradosSeguros { get; set; } = new List<AseguradosSeguros>();
}
