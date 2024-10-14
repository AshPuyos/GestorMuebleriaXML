using System;
using System.Xml.Linq;
using BE;

namespace MPP
{
    public class MPPOficina : MPPMueble
    {
        protected override BEMueble CrearMueble(XElement muebleElement)
        {
            if (muebleElement == null)
                throw new ArgumentNullException(nameof(muebleElement));

            var materialElement = muebleElement.Element("material");

            var oficina = new BEOficina
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
                Estilo = muebleElement.Element("estilo")?.Value ?? string.Empty
            };

            return oficina;
        }

        protected override XElement GenerarElementoMueble(BEMueble mueble)
        {
            var oficina = (BEOficina)mueble;

            var elementoMueble = new XElement("mueble",
                new XAttribute("codigo", oficina.Codigo),
                new XElement("descripcion", oficina.Descripcion),
                new XAttribute("precio", oficina.Precio),
                new XAttribute("cantidad", oficina.Cantidad),
                new XElement("estado", oficina.Estado),
                new XElement("material",
                    new XAttribute("codigo", oficina.Material.Codigo),
                    new XElement("nombre", oficina.Material.Nombre),
                    new XElement("color", oficina.Material.Color)
                ),
                new XElement("estilo", oficina.Estilo),
                new XElement("tipo", "Oficina")
            );

            return elementoMueble;
        }
    }
}
