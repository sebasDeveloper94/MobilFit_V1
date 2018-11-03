using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Models
{
    class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string email { get; set; }
        public string contraseña { get; set; }
        public string nombre_usuario { get; set; }
        public DateTime fechaRegistro { get; set; }
        public decimal peso { get; set; }
        public decimal altura { get; set; }
        public int id_tipoCuerpo { get; set; }
        public int id_nivel { get; set; }
        public Usuario()
        {

        }
    }
}
