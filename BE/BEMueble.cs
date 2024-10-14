using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEMueble
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public float Descuento { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        public BEMaterial Material { get; set; }


        public override string ToString()
        {
            return $"{Codigo} {Descripcion} {Precio} {Descuento} {Cantidad} {Estado} {Material}";
        }
    }
}