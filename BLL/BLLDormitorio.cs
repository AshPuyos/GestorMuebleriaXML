using System.Collections.Generic;
using BE;
using MPP;

namespace BLL
{
    public class BLLDormitorio : BLLMueble
    {
        public BLLDormitorio()
        {
            mppMueble = new MPPDormitorio();
        }

        public override float RealizarDescuento(BEMueble oBEMue)
        {
            return oBEMue.Precio * 0.75f;
        }

        public new List<BEMueble> ObtenerMuebles()
        {
            return mppMueble.LeerMuebles();
        }
    }
}
