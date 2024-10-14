using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE;
using BLL;

namespace Presentacion_IU
{
    public partial class frmMuebles : Form
    {
        private BLLDormitorio bllDormitorio = new BLLDormitorio();
        private BLLOficina bllOficina = new BLLOficina();

        public frmMuebles()
        {
            InitializeComponent();
            dgvProductos.AllowUserToAddRows = false;
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            // Cargar valores en los ComboBox
            cmbEstado.Items.AddRange(new string[] { "Disponible", "Adquirido" });
            cmbEstilo.Items.AddRange(new string[] { "Moderno", "Rústico", "Colonial" });
            cmbEstado.SelectedIndex = 0;
            cmbEstilo.SelectedIndex = 0;
            cbxMueble.SelectedIndex = 0;

            CargarGrilla();
        }

        private void CargarGrilla()
        {
            // Limpiar las filas existentes
            dgvProductos.Rows.Clear();

            // Obtener listas de muebles de dormitorio y oficina
            var mueblesDormitorio = bllDormitorio.ObtenerMuebles().Where(m => ((BEDormitorio)m).JuegoCompleto).ToList();
            var mueblesOficina = bllOficina.ObtenerMuebles().Where(m => !string.IsNullOrEmpty(((BEOficina)m).Estilo)).ToList();

            // Combinar ambas listas en una sola
            var muebles = new List<BEMueble>();
            muebles.AddRange(mueblesDormitorio);
            muebles.AddRange(mueblesOficina);

            // Limpiar las columnas existentes
            dgvProductos.Columns.Clear();

            // Agregar columnas al DataGridView
            dgvProductos.Columns.Add("Codigo", "Código");
            dgvProductos.Columns.Add("Descripcion", "Descripción");
            dgvProductos.Columns.Add("Precio", "Precio");
            dgvProductos.Columns.Add("Descuento", "Descuento"); 
            dgvProductos.Columns.Add("Cantidad", "Cantidad");
            dgvProductos.Columns.Add("Estado", "Estado");
            dgvProductos.Columns.Add("MaterialCodigo", "Código Material");
            dgvProductos.Columns.Add("MaterialNombre", "Nombre Material");
            dgvProductos.Columns.Add("MaterialColor", "Color Material");
            dgvProductos.Columns.Add("Mueble", "Mueble");
            dgvProductos.Columns.Add("Estilo", "Estilo");
            dgvProductos.Columns.Add("JuegoCompleto", "Juego Completo");
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Configurar el DisplayIndex de la columna Descuento
            dgvProductos.Columns["Descuento"].DisplayIndex = dgvProductos.Columns["Precio"].DisplayIndex + 1;

            // Agregar filas al DataGridView
            foreach (var mueble in muebles)
            {
                var tipo = mueble is BEOficina ? "Oficina" : "Dormitorio";
                var estilo = mueble is BEOficina oficina ? oficina.Estilo : string.Empty;
                var juegoCompleto = mueble is BEDormitorio dormitorio ? dormitorio.JuegoCompleto.ToString() : string.Empty;

                float descuento = tipo == "Oficina" ? bllOficina.RealizarDescuento(mueble) : bllDormitorio.RealizarDescuento(mueble);

                dgvProductos.Rows.Add(mueble.Codigo, mueble.Descripcion, mueble.Precio, descuento, mueble.Cantidad, mueble.Estado,
                    mueble.Material.Codigo, mueble.Material.Nombre, mueble.Material.Color, tipo, estilo, juegoCompleto);
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                var tipo = cbxMueble.SelectedItem.ToString();
                BEMueble nuevoMueble;

                if (tipo == "Oficina")
                {
                    nuevoMueble = new BEOficina
                    {
                        Estilo = cmbEstilo.SelectedItem.ToString()
                    };
                }
                else
                {
                    nuevoMueble = new BEDormitorio
                    {
                        JuegoCompleto = chckJuegoCompleto.Checked
                    };
                }

                nuevoMueble.Codigo = int.Parse(txtCodigo.Text);
                nuevoMueble.Descripcion = txtDescripcion.Text;
                nuevoMueble.Precio = float.Parse(txtPrecio.Text);
                nuevoMueble.Cantidad = int.Parse(txtCantidad.Text);
                nuevoMueble.Estado = cmbEstado.SelectedItem.ToString();
                nuevoMueble.Material = new BEMaterial
                {
                    Codigo = int.Parse(txtCodigoMaterial.Text),
                    Nombre = txtNombreMaterial.Text,
                    Color = txtColor.Text
                };

                if (tipo == "Oficina")
                {
                    bllOficina.AgregarMueble(nuevoMueble);
                }
                else
                {
                    bllDormitorio.AgregarMueble(nuevoMueble);
                }

                CargarGrilla();
                MessageBox.Show("Mueble agregado exitosamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.SelectedRows.Count > 0)
                {
                    int codigo = int.Parse(txtCodigo.Text);
                    var tipo = cbxMueble.SelectedItem.ToString();

                    if (tipo == "Oficina")
                    {
                        bllOficina.EliminarMueble(codigo);
                    }
                    else
                    {
                        bllDormitorio.EliminarMueble(codigo);
                    }

                    CargarGrilla();
                    MessageBox.Show("Mueble eliminado exitosamente.");
                }
                else
                {
                    MessageBox.Show("Seleccione un mueble para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            cmbEstado.SelectedIndex = -1;
            txtCodigoMaterial.Text = string.Empty;
            txtNombreMaterial.Text = string.Empty;
            txtColor.Text = string.Empty;
            cmbEstilo.SelectedIndex = -1;
            chckJuegoCompleto.Checked = false;
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProductos.Rows[e.RowIndex];

                txtCodigo.Text = row.Cells["Codigo"].Value?.ToString() ?? string.Empty;
                txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? string.Empty;
                txtPrecio.Text = row.Cells["Precio"].Value?.ToString() ?? string.Empty;
                txtCantidad.Text = row.Cells["Cantidad"].Value?.ToString() ?? string.Empty;
                cmbEstado.SelectedItem = row.Cells["Estado"].Value?.ToString() ?? string.Empty;
                txtCodigoMaterial.Text = row.Cells["MaterialCodigo"].Value?.ToString() ?? string.Empty;
                txtNombreMaterial.Text = row.Cells["MaterialNombre"].Value?.ToString() ?? string.Empty;
                txtColor.Text = row.Cells["MaterialColor"].Value?.ToString() ?? string.Empty;
                var tipo = row.Cells["Mueble"].Value?.ToString() ?? string.Empty;
                cbxMueble.SelectedItem = tipo;
                cmbEstilo.SelectedItem = tipo == "Oficina" ? row.Cells["Estilo"].Value?.ToString() : string.Empty;
                chckJuegoCompleto.Checked = tipo == "Dormitorio" && bool.Parse(row.Cells["JuegoCompleto"].Value?.ToString() ?? "false");
            }
        }

        private void cbxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMueble.SelectedItem.ToString() == "Oficina")
            {
                cmbEstilo.Enabled = true;
                chckJuegoCompleto.Enabled = false;
                chckJuegoCompleto.Checked = false;
            }
            else
            {
                cmbEstilo.Enabled = false;
                cmbEstilo.SelectedIndex = -1;
                chckJuegoCompleto.Enabled = true;
            }
        }

        private void chckJuegoCompleto_CheckedChanged(object sender, EventArgs e)
        {
            bool isDormitorio = cbxMueble.SelectedItem.ToString() == "Dormitorio";
            chckJuegoCompleto.Visible = true; 
            chckJuegoCompleto.Checked = isDormitorio;
            
        }
    }
}