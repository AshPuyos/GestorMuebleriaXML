using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE;

namespace MPP
{
    public class MPPCliente
    {
        private readonly string xmlFilePath = "Clientes.XML";
        private readonly string xmlFilePathVentas = "Ventas.XML";

        public List<BECliente> LeerClientes()
        {
            var consulta =
                from cliente in XElement.Load(xmlFilePath).Elements("cliente")
                select new BECliente
                {
                    Codigo = Convert.ToInt32(cliente.Attribute("codigo").Value),
                    Nombre = cliente.Element("nombre").Value,
                    Apellido = cliente.Element("apellido").Value,
                    Dni = Convert.ToInt32(cliente.Element("dni").Value),
                    oMueble = cliente.Element("mueble") != null ? CrearMuebleDesdeElemento(cliente.Element("mueble")) : null
                };

            return consulta.ToList();
        }

        public void AgregarCliente(BECliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
            }

            XDocument xmlDoc = XDocument.Load(xmlFilePath);
            XElement root = xmlDoc.Element("clientes");
            if (root == null)
            {
                throw new InvalidOperationException("El documento XML no tiene un elemento raíz 'clientes'.");
            }

            XElement clienteElement = new XElement("cliente",
                new XAttribute("codigo", cliente.Codigo.ToString()),
                new XElement("nombre", cliente.Nombre),
                new XElement("apellido", cliente.Apellido),
                new XElement("dni", cliente.Dni.ToString()));

            if (cliente.oMueble != null)
            {
                clienteElement.Add(new XElement("mueble",
                    new XAttribute("codigo", cliente.oMueble.Codigo),
                    new XElement("descripcion", cliente.oMueble.Descripcion),
                    new XAttribute("precio", cliente.oMueble.Precio),
                    new XAttribute("cantidad", cliente.oMueble.Cantidad),
                    new XElement("estado", cliente.oMueble.Estado),
                    new XElement("material",
                        new XAttribute("codigo", cliente.oMueble.Material.Codigo),
                        new XElement("nombre", cliente.oMueble.Material.Nombre),
                        new XElement("color", cliente.oMueble.Material.Color)
                    ),
                    cliente.oMueble is BEOficina oficina ? new XElement("estilo", oficina.Estilo) : null,
                    cliente.oMueble is BEDormitorio dormitorio ? new XElement("juegocompleto", dormitorio.JuegoCompleto) : null,
                    new XElement("tipo", cliente.oMueble is BEOficina ? "Oficina" : "Dormitorio")
                ));
            }

            root.Add(clienteElement);

            xmlDoc.Save(xmlFilePath);
        }

        public void BorrarCliente(string id)
        {
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var cliente = xmlDoc.Descendants("cliente")
                                .FirstOrDefault(c => c.Attribute("codigo").Value == id);

            if (cliente != null)
            {
                cliente.Remove();
                xmlDoc.Save(xmlFilePath);
            }
        }

        public void ModificarCliente(BECliente cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));

            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var clienteExistente = xmlDoc.Descendants("cliente")
                                         .FirstOrDefault(c => c.Attribute("codigo").Value == cliente.Codigo.ToString());

            if (clienteExistente != null)
            {
                clienteExistente.Element("nombre").Value = cliente.Nombre;
                clienteExistente.Element("apellido").Value = cliente.Apellido;
                clienteExistente.Element("dni").Value = cliente.Dni.ToString();

                clienteExistente.Element("mueble")?.Remove();

                if (cliente.oMueble != null)
                {
                    var muebleElement = new XElement("mueble",
                        new XAttribute("codigo", cliente.oMueble.Codigo),
                        new XElement("descripcion", cliente.oMueble.Descripcion),
                        new XAttribute("precio", cliente.oMueble.Precio),
                        new XAttribute("cantidad", cliente.oMueble.Cantidad),
                        new XElement("estado", cliente.oMueble.Estado),
                        new XElement("material",
                            new XAttribute("codigo", cliente.oMueble.Material.Codigo),
                            new XElement("nombre", cliente.oMueble.Material.Nombre),
                            new XElement("color", cliente.oMueble.Material.Color)
                        ),
                        cliente.oMueble is BEOficina oficina ? new XElement("estilo", oficina.Estilo) : null,
                        cliente.oMueble is BEDormitorio dormitorio ? new XElement("juegocompleto", dormitorio.JuegoCompleto) : null,
                        new XElement("tipo", cliente.oMueble is BEOficina ? "Oficina" : "Dormitorio")
                    );

                    clienteExistente.Add(muebleElement);
                }

                xmlDoc.Save(xmlFilePath);
            }
        }

        public void RegistrarVenta(int clienteCodigo, int muebleCodigo, int cantidad, float precio, string descripcion)
        {
            XDocument docVentas;
            if (File.Exists(xmlFilePathVentas))
            {
                docVentas = XDocument.Load(xmlFilePathVentas);
            }
            else
            {
                docVentas = new XDocument(new XElement("ventas"));
            }

            var ventas = docVentas.Element("ventas");

            var nuevaVenta = new XElement("venta",
                new XAttribute("codigo", Guid.NewGuid().ToString()),
                new XAttribute("clienteCodigo", clienteCodigo),
                new XAttribute("muebleCodigo", muebleCodigo),
                new XAttribute("cantidad", cantidad),
                new XAttribute("precio", precio),
                new XElement("descripcion", descripcion)
            );

            ventas.Add(nuevaVenta);
            docVentas.Save(xmlFilePathVentas);
        }

        public List<BEMueble> ObtenerVentasPorCliente(int clienteCodigo)
        {
            if (!File.Exists(xmlFilePathVentas))
            {
                return new List<BEMueble>();
            }

            XDocument docVentas = XDocument.Load(xmlFilePathVentas);

            var ventas = from venta in docVentas.Descendants("venta")
                         where int.Parse(venta.Attribute("clienteCodigo")?.Value ?? "0") == clienteCodigo
                         select CrearMuebleConDescuento(venta);

            return ventas.ToList();
        }

        public List<BEMueble> ObtenerVentas()
        {
            if (!File.Exists(xmlFilePathVentas))
            {
                return new List<BEMueble>();
            }

            XDocument docVentas = XDocument.Load(xmlFilePathVentas);

            var ventas = from venta in docVentas.Descendants("venta")
                         select CrearMuebleConDescuento(venta);

            return ventas.ToList();
        }

        private BEMueble CrearMuebleConDescuento(XElement ventaElement)
        {
            int muebleCodigo = int.Parse(ventaElement.Attribute("muebleCodigo")?.Value ?? "0");
            var mueble = ObtenerMueblePorCodigo(muebleCodigo);

            if (mueble != null)
            {
                mueble.Cantidad = int.Parse(ventaElement.Attribute("cantidad")?.Value ?? "0");
                mueble.Precio = float.Parse(ventaElement.Attribute("precio")?.Value ?? "0");
                mueble.Descuento = RealizarDescuento(mueble);
            }

            return mueble;
        }

        private BEMueble ObtenerMueblePorCodigo(int codigo)
        {
            var muebles = XElement.Load("Muebles.XML").Elements("mueble");
            var muebleElement = muebles.FirstOrDefault(m => int.Parse(m.Attribute("codigo")?.Value ?? "0") == codigo);

            if (muebleElement != null)
            {
                var tipo = muebleElement.Element("tipo")?.Value ?? string.Empty;
                if (tipo == "Oficina")
                {
                    return new BEOficina
                    {
                        Codigo = codigo,
                        Descripcion = muebleElement.Element("descripcion")?.Value,
                        Precio = float.Parse(muebleElement.Attribute("precio")?.Value ?? "0"),
                        Cantidad = int.Parse(muebleElement.Attribute("cantidad")?.Value ?? "0"),
                        Estado = muebleElement.Element("estado")?.Value,
                        Material = new BEMaterial
                        {
                            Codigo = int.Parse(muebleElement.Element("material")?.Element("codigo")?.Value ?? "0"),
                            Nombre = muebleElement.Element("material")?.Element("nombre")?.Value,
                            Color = muebleElement.Element("material")?.Element("color")?.Value
                        },
                        Estilo = muebleElement.Element("estilo")?.Value
                    };
                }
                else if (tipo == "Dormitorio")
                {
                    return new BEDormitorio
                    {
                        Codigo = codigo,
                        Descripcion = muebleElement.Element("descripcion")?.Value,
                        Precio = float.Parse(muebleElement.Attribute("precio")?.Value ?? "0"),
                        Cantidad = int.Parse(muebleElement.Attribute("cantidad")?.Value ?? "0"),
                        Estado = muebleElement.Element("estado")?.Value,
                        Material = new BEMaterial
                        {
                            Codigo = int.Parse(muebleElement.Element("material")?.Element("codigo")?.Value ?? "0"),
                            Nombre = muebleElement.Element("material")?.Element("nombre")?.Value,
                            Color = muebleElement.Element("material")?.Element("color")?.Value
                        },
                        JuegoCompleto = bool.Parse(muebleElement.Element("juegocompleto")?.Value ?? "false")
                    };
                }
            }

            return null;
        }

        private float RealizarDescuento(BEMueble mueble)
        {
            if (mueble is BEOficina)
            {
                return mueble.Precio * 0.9f;
            }
            else if (mueble is BEDormitorio)
            {
                return mueble.Precio * 0.75f;
            }

            return mueble.Precio;
        }

        private BEMueble CrearMuebleDesdeElemento(XElement muebleElement)
        {
            var tipo = muebleElement.Element("tipo")?.Value ?? string.Empty;
            if (tipo == "Oficina")
            {
                return new BEOficina
                {
                    Codigo = int.Parse(muebleElement.Attribute("codigo")?.Value ?? "0"),
                    Descripcion = muebleElement.Element("descripcion")?.Value,
                    Precio = float.Parse(muebleElement.Attribute("precio")?.Value ?? "0"),
                    Cantidad = int.Parse(muebleElement.Attribute("cantidad")?.Value ?? "0"),
                    Estado = muebleElement.Element("estado")?.Value,
                    Material = new BEMaterial
                    {
                        Codigo = int.Parse(muebleElement.Element("material")?.Attribute("codigo")?.Value ?? "0"),
                        Nombre = muebleElement.Element("material")?.Element("nombre")?.Value,
                        Color = muebleElement.Element("material")?.Element("color")?.Value
                    },
                    Estilo = muebleElement.Element("estilo")?.Value
                };
            }
            else if (tipo == "Dormitorio")
            {
                return new BEDormitorio
                {
                    Codigo = int.Parse(muebleElement.Attribute("codigo")?.Value ?? "0"),
                    Descripcion = muebleElement.Element("descripcion")?.Value,
                    Precio = float.Parse(muebleElement.Attribute("precio")?.Value ?? "0"),
                    Cantidad = int.Parse(muebleElement.Attribute("cantidad")?.Value ?? "0"),
                    Estado = muebleElement.Element("estado")?.Value,
                    Material = new BEMaterial
                    {
                        Codigo = int.Parse(muebleElement.Element("material")?.Attribute("codigo")?.Value ?? "0"),
                        Nombre = muebleElement.Element("material")?.Element("nombre")?.Value,
                        Color = muebleElement.Element("material")?.Element("color")?.Value
                    },
                    JuegoCompleto = bool.Parse(muebleElement.Element("juegocompleto")?.Value ?? "false")
                };
            }

            return null;
        }
    }
}
