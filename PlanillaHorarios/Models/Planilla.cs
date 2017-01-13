using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanillaHorarios.Models
{
    public class Planilla
    {
        [Key]
        public int PlanillaId { get; set; }

        //[ForeignKey("Persona")]
        public int PersonaID { get; set; }

        [DisplayName("Año")]
        public int? Anio { get; set; }

        [Range(1, 12)]
        [DisplayName("Mes")]
        public int? Mes { get; set; }

        [DisplayName("Día")]
        public int Dia { get; set; }

        [DisplayName("Observación")]
        public string Observaciones { get; set; }

        [Range(800, 1900)]
        [DisplayName("Hora Entrada")]
        [DisplayFormat(DataFormatString = "{0:00:00}")]
        public int? MHoraEntrada { get; set; }

        [Range(800, 1900)]
        [DisplayName("Hora Salida")]
        [DisplayFormat(DataFormatString = "{0:00:00}")]
        public int? MHoraSalida { get; set; }

        [Range(800, 1900)]
        [DisplayName("Hora Entrada")]
        [DisplayFormat(DataFormatString = "{0:00:00}")]
        public int? THoraEntrada { get; set; }

        [Range(800, 1900)]
        [DisplayName("Hora Salida")]
        [DisplayFormat(DataFormatString = "{0:00:00}")]
        public int? THoraSalida { get; set; }

    }
}
