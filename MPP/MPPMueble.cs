using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BE;

namespace MPP
{
    public abstract class MPPMueble
    {
        protected readonly string xmlFilePath = "Muebles.XML";

        public virtual List<BEMueble> LeerMuebles()
        {
            var consulta =
                from mueble in XElement.Load(xmlFilePath).Elements("mueble")
                select CrearMueble(mueble);

            return consulta.ToList();
        }

        protected abstract BEMueble CrearMueble(XElement muebleElement);

        public void AgregarMueble(BEMueble mueble)
        {
            XDocument xmlDoc = XDocument.Load(xmlFilePath);
            XElement root = xmlDoc.Element("muebles");
            root.Add(GenerarElementoMueble(mueble));
            xmlDoc.Save(xmlFilePath);
        }

        public void EliminarMueble(int codigo)
        {
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var mueble = xmlDoc.Descendants("mueble")
                               .FirstOrDefault(m => int.Parse(m.Attribute("codigo").Value) == codigo);

            if (mueble != null)
            {
                mueble.Remove();
                xmlDoc.Save(xmlFilePath);
            }
        }

        public void ModificarMueble(BEMueble mueble)
        {
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var muebleExistente = xmlDoc.Descendants("mueble")
                                        .FirstOrDefault(m => int.Parse(m.Attribute("codigo")?.Value ?? "0") == mueble.Codigo);

            if (muebleExistente != null)
            {
                muebleExistente.SetAttributeValue("cantidad", mueble.Cantidad.ToString());
                muebleExistente.SetAttributeValue("estado", mueble.Estado);
                xmlDoc.Save(xmlFilePath);
            }
        }

        protected abstract XElement GenerarElementoMueble(BEMueble mueble);
    }
}
