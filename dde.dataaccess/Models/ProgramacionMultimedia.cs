using System;
using System.Collections.Generic;

namespace dde.dataaccess.Models
{
    public partial class ProgramacionMultimedia
    {
        public ProgramacionMultimedia()
        {
            CapacitacionMultimedia = new HashSet<CapacitacionMultimedia>();
        }

        public int ProgramacionMultimediaId { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }
        public DateTime? FechaInicioProgramacion { get; set; }
        public DateTime? FechaTerminoProgramacion { get; set; }
        public int? MultimediaId { get; set; }
        public int? UsuarioCreacionId { get; set; }

        public Multimedia Multimedia { get; set; }
        public Usuarios UsuarioCreacion { get; set; }
        public ICollection<CapacitacionMultimedia> CapacitacionMultimedia { get; set; }
    }
}
