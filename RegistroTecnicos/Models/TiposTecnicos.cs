using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TipoTecnicoId { get; set; }

    [Required(ErrorMessage = " La descripcion es obligatoria.")]
    public string Descripcion { get; set; } = string.Empty;
    public ICollection<Tecnicos> Tecnicos { get; set; } = new List<Tecnicos>();
    
    public Tecnicos? Tecnico { get; set; }
}
