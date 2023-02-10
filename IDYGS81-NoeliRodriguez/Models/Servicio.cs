using System.ComponentModel.DataAnnotations;

namespace IDYGS81_NoeliRodriguez.Models
{
    public class Servicio
    {
        [Key]
        public int PkServicio { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }
}
