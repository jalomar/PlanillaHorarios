using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanillaHorarios.Models
{
    public class Persona
    {
        [Key]
        public int PersonaID { get; set; }

        [Required(ErrorMessage = "Ingrese un Nombre")]
        [DisplayName("Nombre")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un Apellido")]
        [DisplayName("Apellido")]
        public String Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese un CUIL")]
        [DisplayName("CUIL")]
        public String Cuil { get; set; }

        [Required(ErrorMessage = "Ingrese un correo electrónico")]
        [DisplayName("Mail")]
        public String Mail { get; set; }

        public virtual ICollection<Planilla> Planillas { get; set; }

        //        [DisplayName("Deshabilitar")]
        //        public Boolean Deshabil { get; set; }

    }
}
