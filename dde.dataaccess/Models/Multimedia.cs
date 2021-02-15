using System;
using System.Collections.Generic;

namespace dde.dataaccess.Models
{
    public partial class Multimedia
    {
        public Multimedia()
        {
            ProgramacionMultimedia = new HashSet<ProgramacionMultimedia>();
        }

        public int MultimediaId { get; set; }
        public string Url { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public ICollection<ProgramacionMultimedia> ProgramacionMultimedia { get; set; }
    }
}
