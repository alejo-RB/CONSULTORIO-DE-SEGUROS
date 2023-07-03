using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULTORIO_DE_SEGUROS.Models
{
    public partial class AseguradosSeguros
    {
        [Key]
        public int Id { get; set; }

        public int AseguradoId { get; set; }

        public int SeguroId { get; set; }

        [BindNever]
        public virtual Asegurados Asegurado { get; set; } = null!;

        [BindNever]
        public virtual Seguros Seguro { get; set; } = null!;
    }
}
