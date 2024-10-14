namespace Presentacion_IU
{
    partial class frmMueblesVentas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.gbMueblesClientes = new System.Windows.Forms.GroupBox();
            this.btnVenderMueble = new System.Windows.Forms.Button();
            this.dgvMueblesPorCliente = new System.Windows.Forms.DataGridView();
            this.cbxCliente = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMuebles = new System.Windows.Forms.GroupBox();
            this.dgvMuebles = new System.Windows.Forms.DataGridView();
            this.gbMueblesClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMueblesPorCliente)).BeginInit();
            this.gbMuebles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuebles)).BeginInit();
            this.SuspendLayout();
            // 
            // gbMueblesClientes
            // 
            this.gbMueblesClientes.Controls.Add(this.btnVenderMueble);
            this.gbMueblesClientes.Controls.Add(this.dgvMueblesPorCliente);
            this.gbMueblesClientes.Controls.Add(this.cbxCliente);
            this.gbMueblesClientes.Controls.Add(this.label1);
            this.gbMueblesClientes.Location = new System.Drawing.Point(681, 31);
            this.gbMueblesClientes.Name = "gbMueblesClientes";
            this.gbMueblesClientes.Size = new System.Drawing.Size(477, 473);
            this.gbMueblesClientes.TabIndex = 13;
            this.gbMueblesClientes.TabStop = false;
            this.gbMueblesClientes.Text = "Muebles Adquiridos por Clientes";
            // 
            // btnVenderMueble
            // 
            this.btnVenderMueble.Location = new System.Drawing.Point(195, 403);
            this.btnVenderMueble.Name = "btnVenderMueble";
            this.btnVenderMueble.Size = new System.Drawing.Size(89, 50);
            this.btnVenderMueble.TabIndex = 6;
            this.btnVenderMueble.Text = "Vender Mueble";
            this.btnVenderMueble.UseVisualStyleBackColor = true;
            this.btnVenderMueble.Click += new System.EventHandler(this.btnVenderProducto_Click);
            // 
            // dgvMueblesPorCliente
            // 
            this.dgvMueblesPorCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMueblesPorCliente.Location = new System.Drawing.Point(18, 29);
            this.dgvMueblesPorCliente.Name = "dgvMueblesPorCliente";
            this.dgvMueblesPorCliente.Size = new System.Drawing.Size(441, 305);
            this.dgvMueblesPorCliente.TabIndex = 2;
            // 
            // cbxCliente
            // 
            this.cbxCliente.FormattingEnabled = true;
            this.cbxCliente.Location = new System.Drawing.Point(219, 364);
            this.cbxCliente.Name = "cbxCliente";
            this.cbxCliente.Size = new System.Drawing.Size(86, 21);
            this.cbxCliente.TabIndex = 8;
            this.cbxCliente.SelectedIndexChanged += new System.EventHandler(this.cbxCliente_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 367);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Cliente";
            // 
            // gbMuebles
            // 
            this.gbMuebles.Controls.Add(this.dgvMuebles);
            this.gbMuebles.Location = new System.Drawing.Point(30, 73);
            this.gbMuebles.Name = "gbMuebles";
            this.gbMuebles.Size = new System.Drawing.Size(617, 343);
            this.gbMuebles.TabIndex = 12;
            this.gbMuebles.TabStop = false;
            this.gbMuebles.Text = "Muebles";
            // 
            // dgvMuebles
            // 
            this.dgvMuebles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMuebles.Location = new System.Drawing.Point(27, 29);
            this.dgvMuebles.Name = "dgvMuebles";
            this.dgvMuebles.Size = new System.Drawing.Size(553, 296);
            this.dgvMuebles.TabIndex = 1;
            // 
            // frmMueblesVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 518);
            this.Controls.Add(this.gbMueblesClientes);
            this.Controls.Add(this.gbMuebles);
            this.Name = "frmMueblesVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Ventas";
            this.Load += new System.EventHandler(this.frmProductosVentas_Load);
            this.gbMueblesClientes.ResumeLayout(false);
            this.gbMueblesClientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMueblesPorCliente)).EndInit();
            this.gbMuebles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuebles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMueblesClientes;
        private System.Windows.Forms.Button btnVenderMueble;
        private System.Windows.Forms.DataGridView dgvMueblesPorCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCliente;
        private System.Windows.Forms.GroupBox gbMuebles;
        private System.Windows.Forms.DataGridView dgvMuebles;
    }
}
