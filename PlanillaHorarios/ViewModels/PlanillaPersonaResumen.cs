using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanillaHorarios.ViewModels
{
    public class PlanillaPersonaResumen
    {
        [Key]
        public int PlanillaId { get; set; }

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

        public DateTime Fecha
        {
            get { return new DateTime((int)Anio, (int)Mes, Dia); }
        }

        public bool EsFeriado { get; set; }

        public bool EsDiaLaboral
        {
            get
            {
                var dayOfWeek = (int)Fecha.DayOfWeek;
                return dayOfWeek != 0 && dayOfWeek != 6;
            }
        }

        private string _diaSemana;

        public string DiaSemana
        {
            get
            {
                if (string.IsNullOrEmpty(_diaSemana))
                {
                    switch ((int)Fecha.DayOfWeek)
                    {
                        case 0:
                            return "Domingo";
                        case 1:
                            return "Lunes";
                        case 2:
                            return "Martes";
                        case 3:
                            return "Miércoles";
                        case 4:
                            return "Jueves";
                        case 5:
                            return "Viernes";
                        default:
                            return "Sábado";
                    };

                }
                else
                {
                    return _diaSemana;
                }
            }
            set
            {
                _diaSemana = value;
            }
        }
    }
}
