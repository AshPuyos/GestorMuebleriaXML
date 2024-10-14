using System;
using System.Xml.Linq;
using BE;

namespace MPP
{
    public class MPPDormitorio : MPPMueble
    {
        protected override BEMueble CrearMueble(XElement muebleElement)
        {
            if (muebleElement == null)
                throw new ArgumentNullException(nameof(muebleElement));

            var materialElement = muebleElement.Element("material");

            var dormitorio = new BEDormitorio
            {
                Codigo = int.Parse(muebleElement.Attribute("codigo")?.Value ?? "0"),
                Descripcion = muebleElement.Element("descripcion")?.Value ?? string.Empty,
                Precio = float.Parse(muebleElement.Attribute("precio")?.Value ?? "0"),
                Cantidad = int.Parse(muebleElement.Attribute("cantidad")?.Value ?? "0"),
                Estado = muebleElement.Element("estado")?.Value ?? string.Empty,
                Material = new BEMaterial
                {
                    Codigo = int.Parse(materialElement?.Attribute("codigo")?.Value ?? "0"),
                    Nombre = materialElement?.Element("nombre")?.Value ?? string.Empty,
                    Color = materialElement?.Element("color")?.Value ?? string.Empty
                },
                JuegoCompleto = bool.Parse(muebleElement.Element("juegocompleto")?.Value ?? "false")
            };

            return dormitorio;
        }

        protected override XElement GenerarElementoMueble(BEMueble mueble)
        {
            var dormitorio = (BEDormitorio)mueble;

            var elementoMueble = new XElement("mueble",
                new XAttribute("codigo", dormitorio.Codigo),
                new XElement("descripcion", dormitorio.Descripcion),
                new XAttribute("precio", dormitorio.Precio),
                new XAttribute("cantidad", dormitorio.Cantidad),
                new XElement("estado", dormitorio.Estado),
                new XElement("material",
                    new XAttribute("codigo", dormitorio.Material?.Codigo ?? 0),
                    new XElement("nombre", dormitorio.Material?.Nombre ?? string.Empty),
                    new XElement("color", dormitorio.Material?.Color ?? string.Empty)
                ),
                new XElement("juegocompleto", dormitorio.JuegoCompleto),
                new XElement("tipo", "Dormitorio")
            );

            return elementoMueble;
        }
    }
}
