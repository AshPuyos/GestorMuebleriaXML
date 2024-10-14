using System.Collections.Generic;
using BE;
using MPP;

namespace BLL
{
    public abstract class BLLMueble
    {
        protected MPPMueble mppMueble;

        //Polimorfirmo
        public abstract float RealizarDescuento(BEMueble oBEMue);

        public List<BEMueble> ObtenerMuebles()
        {
            return mppMueble.LeerMuebles();
        }

        public void AgregarMueble(BEMueble mueble)
        {
            mppMueble.AgregarMueble(mueble);
        }

        public void EliminarMueble(int codigo)
        {
            mppMueble.EliminarMueble(codigo);
        }
    }
}
