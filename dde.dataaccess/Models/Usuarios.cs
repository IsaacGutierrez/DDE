using System;
using System.Collections.Generic;

namespace dde.dataaccess.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            ProgramacionMultimedia = new HashSet<ProgramacionMultimedia>();
        }

        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public ICollection<ProgramacionMultimedia> ProgramacionMultimedia { get; set; }
    }
}
