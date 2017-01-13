using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace PlanillaHorarios.Models
{
    public class Feriado
    {
        [Key]
        public int FeriadoID { get; set; }

        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Ingrese la conmemoración")]
        [DisplayName("Descripción")]
        public String Descripcion { get; set; }

    }
}
