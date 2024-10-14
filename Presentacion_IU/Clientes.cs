using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BE;
using BLL;

namespace Presentacion_IU
{
    public partial class frmClientes : Form
    {
        private BLLCliente bllCliente = new BLLCliente();

        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            this.dgvClientes.DataSource = null;
            this.dgvClientes.DataSource = bllCliente.ObtenerClientes();
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            
            if (dgvClientes.Columns["oMueble"] != null)
            {
                dgvClientes.Columns["oMueble"].Visible = false;
            }
        }

        private bool ValidarCampos()
        {
            string dniPattern = @"^\d{8}$"; // 8 dígitos para DNI
            string nombreApellidoPattern = @"^[a-zA-Z\s]+$"; // Solo letras y espacios

            if (!Regex.IsMatch(txtDNI.Text, dniPattern))
            {
                MessageBox.Show("Ingrese un DNI válido (8 dígitos).");
                return false;
            }

            if (!Regex.IsMatch(txtNombre.Text, nombreApellidoPattern))
            {
                MessageBox.Show("Ingrese un nombre válido (solo letras y espacios).");
                return false;
            }

            if (!Regex.IsMatch(txtApellido.Text, nombreApellidoPattern))
            {
                MessageBox.Show("Ingrese un apellido válido (solo letras y espacios).");
                return false;
            }

            return true;
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }

                // Generar un nuevo código automáticamente
                int nuevoCodigo = GenerarNuevoCodigo();

                // Crear el nuevo cliente con los valores validados y el código generado
                BECliente nuevoCliente = new BECliente
                {
                    Codigo = nuevoCodigo,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Dni = int.Parse(txtDNI.Text)
                };

                // Agregar el cliente y recargar la grilla
                bllCliente.AgregarCliente(nuevoCliente);
                CargarGrilla();
                MessageBox.Show("Cliente agregado exitosamente.");

                // Limpiar los campos después de agregar el cliente
                btnLimpiar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        // Método para generar un nuevo código único
        private int GenerarNuevoCodigo()
        {
            var clientes = bllCliente.ObtenerClientes();
            if (clientes == null || clientes.Count == 0)
                return 1; 

            
            int maxCodigo = clientes.Max(c => c.Codigo);
            return maxCodigo + 1;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.SelectedRows.Count > 0)
                {
                    if (!ValidarCampos())
                    {
                        return;
                    }

                    // Crear el cliente modificado con los valores validados
                    BECliente clienteModificado = new BECliente
                    {
                        Codigo = int.Parse(txtCodigo.Text),
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Dni = int.Parse(txtDNI.Text)
                    };

                    // Modificar el cliente y recargar la grilla
                    bllCliente.ModificarCliente(clienteModificado);
                    CargarGrilla();
                    MessageBox.Show("Cliente modificado exitosamente.");

                    // Limpiar los campos después de modificar el cliente
                    btnLimpiar_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Seleccione un cliente para modificar.");
                }
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
                if (dgvClientes.SelectedRows.Count > 0)
                {
                    string id = txtCodigo.Text;
                    bllCliente.EliminarCliente(id);
                    CargarGrilla();
                    MessageBox.Show("Cliente eliminado exitosamente.");
                }
                else
                {
                    MessageBox.Show("Seleccione un cliente para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDNI.Text = string.Empty;
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvClientes.Rows.Count)
            {
                DataGridViewRow row = dgvClientes.Rows[e.RowIndex];

                txtCodigo.Text = row.Cells["Codigo"].Value?.ToString() ?? string.Empty;
                txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? string.Empty;
                txtApellido.Text = row.Cells["Apellido"].Value?.ToString() ?? string.Empty;
                txtDNI.Text = row.Cells["Dni"].Value?.ToString() ?? string.Empty;
            }
        }
    }
}
