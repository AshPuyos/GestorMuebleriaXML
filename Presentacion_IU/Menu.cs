using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;

namespace Presentacion_IU
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void generosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes ofc = new frmClientes();
            ofc.MdiParent = this;
            ofc.Show();
        }

        private void registrarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMuebles ofm = new frmMuebles();
            ofm.MdiParent = this;
            ofm.Show();
        }

        private void registrarVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMueblesVentas ofmv = new frmMueblesVentas();
            ofmv.MdiParent = this;
            ofmv.Show();
        }

        private void informesConChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformesChart ofc = new InformesChart();
            ofc.MdiParent = this;
            ofc.Show();
        }
    }
}
