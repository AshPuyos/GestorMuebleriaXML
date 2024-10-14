using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE;
using BLL;
using System.Xml.Linq;

namespace Presentacion_IU
{
    public partial class frmMueblesVentas : Form
    {
        private BLLCliente bllCliente = new BLLCliente();
        private BLLDormitorio bllDormitorio = new BLLDormitorio();
        private BLLOficina bllOficina = new BLLOficina();
        private List<BEMueble> mueblesDisponibles;
        private List<BECliente> clientesDisponibles;

        public frmMueblesVentas()
        {
            InitializeComponent();
            // Configurar la propiedad AllowUserToAddRows en false para ambos DataGridView
            dgvMuebles.AllowUserToAddRows = false;
            dgvMueblesPorCliente.AllowUserToAddRows = false;
        }

        private void frmProductosVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarMuebles();
        }

        private void CargarClientes()
        {
            clientesDisponibles = bllCliente.ObtenerClientes();
            cbxCliente.DataSource = clientesDisponibles;
            cbxCliente.DisplayMember = "Nombre";
            cbxCliente.ValueMember = "Codigo";
        }

        private void CargarMuebles()
        {
            // Obtener listas de muebles de dormitorio y oficina
            var mueblesDormitorio = bllDormitorio.ObtenerMuebles().Where(m => ((BEDormitorio)m).JuegoCompleto).ToList();
            var mueblesOficina = bllOficina.ObtenerMuebles().Where(m => !string.IsNullOrEmpty(((BEOficina)m).Estilo)).ToList();

            // Combinar ambas listas en una sola
            mueblesDisponibles = new List<BEMueble>();
            mueblesDisponibles.AddRange(mueblesDormitorio);
            mueblesDisponibles.AddRange(mueblesOficina);

            // Limpiar las columnas existentes
            dgvMuebles.Columns.Clear();

            // Agregar columnas al DataGridView
            dgvMuebles.Columns.Add("Codigo", "Código");
            dgvMuebles.Columns.Add("Descripcion", "Descripción");
            dgvMuebles.Columns.Add("Precio", "Precio");
            dgvMuebles.Columns.Add("Descuento", "Descuento");
            dgvMuebles.Columns.Add("Cantidad", "Cantidad");
            dgvMuebles.Columns.Add("Estado", "Estado");
            dgvMuebles.Columns.Add("MaterialCodigo", "Código Material");
            dgvMuebles.Columns.Add("MaterialNombre", "Nombre Material");
            dgvMuebles.Columns.Add("MaterialColor", "Color Material");
            dgvMuebles.Columns.Add("Mueble", "Mueble");
            dgvMuebles.Columns.Add("Estilo", "Estilo");
            dgvMuebles.Columns.Add("JuegoCompleto", "Juego Completo");
            dgvMuebles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Agregar filas al DataGridView
            foreach (var mueble in mueblesDisponibles)
            {
                var tipo = mueble is BEOficina ? "Oficina" : "Dormitorio";
                var estilo = mueble is BEOficina oficina ? oficina.Estilo : string.Empty;
                var juegoCompleto = mueble is BEDormitorio dormitorio ? dormitorio.JuegoCompleto.ToString() : string.Empty;

                float descuento = tipo == "Oficina" ? bllOficina.RealizarDescuento(mueble) : bllDormitorio.RealizarDescuento(mueble);

                dgvMuebles.Rows.Add(mueble.Codigo, mueble.Descripcion, mueble.Precio, descuento, mueble.Cantidad, mueble.Estado,
                    mueble.Material.Codigo, mueble.Material.Nombre, mueble.Material.Color, tipo, estilo, juegoCompleto);
            }
        }

        private void btnVenderProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMuebles.SelectedRows.Count > 0 && cbxCliente.SelectedItem != null)
                {
                    var cliente = (BECliente)cbxCliente.SelectedItem;

                    // Verificar si el cliente ya compro un mueble
                    var ventasPorCliente = bllCliente.ObtenerVentasPorCliente(cliente.Codigo);
                    if (ventasPorCliente.Any())
                    {
                        MessageBox.Show("El cliente ya ha comprado un mueble.");
                        return;
                    }

                    var row = dgvMuebles.SelectedRows[0];
                    var codigoMueble = int.Parse(row.Cells["Codigo"].Value.ToString());
                    var descripcionMueble = row.Cells["Descripcion"].Value.ToString();
                    var mueble = mueblesDisponibles.FirstOrDefault(m => m.Codigo == codigoMueble);

                    if (mueble == null || cliente == null)
                    {
                        MessageBox.Show("Seleccione un mueble y un cliente válido.");
                        return;
                    }

                    int cantidad = 1;
                    if (mueble.Cantidad < cantidad)
                    {
                        MessageBox.Show("No hay suficiente stock disponible.");
                        return;
                    }

                    mueble.Cantidad -= cantidad;
                    if (mueble.Cantidad == 0)
                    {
                        mueble.Estado = "Adquirido";
                    }

                    bllCliente.RegistrarVenta(cliente.Codigo, mueble.Codigo, cantidad, mueble.Precio, descripcionMueble);

                    // Actualizar el stock del mueble en el XML
                    ActualizarStockMueble(mueble);

                    // Actualizar el gráfico de monto total recaudado
                    InformesChart informesChartForm = Application.OpenForms.OfType<InformesChart>().FirstOrDefault();
                    if (informesChartForm != null)
                    {
                        informesChartForm.ActualizarGraficoMontoTotalRecaudado();
                    }

                    CargarMuebles();
                    CargarVentasPorCliente(cliente.Codigo);

                    MessageBox.Show("Venta registrada exitosamente.");
                }
                else
                {
                    MessageBox.Show("Seleccione un mueble y un cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void ActualizarStockMueble(BEMueble mueble)
        {
            XDocument xmlDoc = XDocument.Load("Muebles.XML");

            var muebleElement = xmlDoc.Descendants("mueble")
                                      .FirstOrDefault(m => int.Parse(m.Attribute("codigo")?.Value ?? "0") == mueble.Codigo);

            if (muebleElement != null)
            {
                muebleElement.SetAttributeValue("cantidad", mueble.Cantidad.ToString());
                muebleElement.SetAttributeValue("estado", mueble.Estado);
                xmlDoc.Save("Muebles.XML");
            }
        }

        private void CargarVentasPorCliente(int clienteCodigo)
        {
            var ventas = bllCliente.ObtenerVentasPorCliente(clienteCodigo);

            dgvMueblesPorCliente.Rows.Clear();
            dgvMueblesPorCliente.Columns.Clear();

            dgvMueblesPorCliente.Columns.Add("Codigo", "Código");
            dgvMueblesPorCliente.Columns.Add("Descripcion", "Descripción");
            dgvMueblesPorCliente.Columns.Add("Precio", "Precio");
            dgvMueblesPorCliente.Columns.Add("Descuento", "Descuento");
            dgvMueblesPorCliente.Columns.Add("Cantidad", "Cantidad");
            dgvMueblesPorCliente.Columns.Add("Estado", "Estado");
            dgvMueblesPorCliente.Columns.Add("MaterialCodigo", "Código Material");
            dgvMueblesPorCliente.Columns.Add("MaterialNombre", "Nombre Material");
            dgvMueblesPorCliente.Columns.Add("MaterialColor", "Color Material");
            dgvMueblesPorCliente.Columns.Add("Mueble", "Mueble");
            dgvMueblesPorCliente.Columns.Add("Estilo", "Estilo");
            dgvMueblesPorCliente.Columns.Add("JuegoCompleto", "Juego Completo");
            dgvMueblesPorCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            foreach (var mueble in ventas)
            {
                dgvMueblesPorCliente.Rows.Add(mueble.Codigo, mueble.Descripcion, mueble.Precio, mueble.Descuento, mueble.Cantidad, "Adquirido",
                    mueble.Material?.Codigo, mueble.Material?.Nombre, mueble.Material?.Color, mueble is BEOficina ? "Oficina" : "Dormitorio",
                    mueble is BEOficina oficinaMueble ? oficinaMueble?.Estilo : string.Empty, mueble is BEDormitorio dormitorioMueble ? dormitorioMueble?.JuegoCompleto.ToString() : string.Empty);
            }
        }

        private void cbxCliente_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbxCliente.SelectedItem != null)
            {
                var cliente = (BECliente)cbxCliente.SelectedItem;
                CargarVentasPorCliente(cliente.Codigo);
            }
        }
    }
}
