using System.Collections.Generic;
using BE;
using MPP;

namespace BLL
{
    public class BLLOficina : BLLMueble
    {
        public BLLOficina()
        {
            mppMueble = new MPPOficina();
        }

        public override float RealizarDescuento(BEMueble oBEMue)
        {
            return oBEMue.Precio * 0.9f;
        }

        public new List<BEMueble> ObtenerMuebles()
        {
            return mppMueble.LeerMuebles();
        }
    }
}

