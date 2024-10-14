namespace Presentacion_IU
{
    partial class InformesChart
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMuebleMasVendido;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMuebleMenosVendido;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMontoTotalRecaudado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chartMuebleMasVendido = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMuebleMenosVendido = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMontoTotalRecaudado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartMuebleMasVendido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMuebleMenosVendido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMontoTotalRecaudado)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMuebleMasVendido
            // 
            this.chartMuebleMasVendido.Location = new System.Drawing.Point(12, 12);
            this.chartMuebleMasVendido.Name = "chartMuebleMasVendido";
            this.chartMuebleMasVendido.Size = new System.Drawing.Size(637, 300);
            this.chartMuebleMasVendido.TabIndex = 0;
            this.chartMuebleMasVendido.Text = "chartMuebleMasVendido";
            // 
            // chartMuebleMenosVendido
            // 
            this.chartMuebleMenosVendido.Location = new System.Drawing.Point(655, 12);
            this.chartMuebleMenosVendido.Name = "chartMuebleMenosVendido";
            this.chartMuebleMenosVendido.Size = new System.Drawing.Size(654, 300);
            this.chartMuebleMenosVendido.TabIndex = 1;
            this.chartMuebleMenosVendido.Text = "chartMuebleMenosVendido";
            // 
            // chartMontoTotalRecaudado
            // 
            this.chartMontoTotalRecaudado.Location = new System.Drawing.Point(212, 318);
            this.chartMontoTotalRecaudado.Name = "chartMontoTotalRecaudado";
            this.chartMontoTotalRecaudado.Size = new System.Drawing.Size(924, 318);
            this.chartMontoTotalRecaudado.TabIndex = 2;
            this.chartMontoTotalRecaudado.Text = "chartMontoTotalRecaudado";
            // 
            // InformesChart
            // 
            this.ClientSize = new System.Drawing.Size(1321, 658);
            this.Controls.Add(this.chartMontoTotalRecaudado);
            this.Controls.Add(this.chartMuebleMenosVendido);
            this.Controls.Add(this.chartMuebleMasVendido);
            this.Name = "InformesChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informes con Charts";
            this.Load += new System.EventHandler(this.InformesChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartMuebleMasVendido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMuebleMenosVendido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMontoTotalRecaudado)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
