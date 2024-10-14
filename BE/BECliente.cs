using System;
using System.Collections.Generic;

namespace BE
{
    public class BECliente
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }

        //Relacion 1 a 1
        public BEMueble oMueble { get; set; }

    }
}