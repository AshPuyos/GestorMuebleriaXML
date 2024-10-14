using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;

namespace Presentacion_IU
{
    public partial class InformesChart : Form
    {
        private BLLCliente bllCliente;

        public InformesChart()
        {
            InitializeComponent();
            bllCliente = new BLLCliente();
        }

        private void InformesChart_Load(object sender, EventArgs e)
        {
            CargarGraficoMuebleMasVendido();
            CargarGraficoMuebleMenosVendido();
            CargarGraficoMontoTotalRecaudado();
        }

        private void CargarGraficoMuebleMasVendido()
        {
            var mueblesMasVendidosOficina = bllCliente.ObtenerMueblesMasVendidosPorTipo("Oficina");
            var mueblesMasVendidosDormitorio = bllCliente.ObtenerMueblesMasVendidosPorTipo("Dormitorio");

            var muebleMasVendidoOficina = mueblesMasVendidosOficina.FirstOrDefault();
            var muebleMasVendidoDormitorio = mueblesMasVendidosDormitorio.FirstOrDefault();

            float montoTotalOficina = bllCliente.ObtenerMontoTotalRecaudadoPorDescripcion(muebleMasVendidoOficina.Key);
            float montoTotalDormitorio = bllCliente.ObtenerMontoTotalRecaudadoPorDescripcion(muebleMasVendidoDormitorio.Key);

            chartMuebleMasVendido.Titles.Clear();
            chartMuebleMasVendido.ChartAreas.Clear();
            chartMuebleMasVendido.Series.Clear();

            Title titulo = new Title("Muebles Más Vendidos");
            titulo.Font = new System.Drawing.Font("Tahoma", 20, System.Drawing.FontStyle.Bold);
            chartMuebleMasVendido.Titles.Add(titulo);

            ChartArea area = new ChartArea();
            area.Area3DStyle.Enable3D = true;
            area.AxisX.Title = "Mueble";
            area.AxisY.Title = "Monto Recaudado";
            area.AxisX.TitleFont = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);
            area.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);
            chartMuebleMasVendido.ChartAreas.Add(area);

            // Monto total recaudado por los muebles más vendidos
            Series serieMonto = new Series("Monto Recaudado");
            serieMonto.ChartType = SeriesChartType.Bar;
            serieMonto.Color = System.Drawing.Color.Green;
            serieMonto.Points.AddXY($"Oficina: {muebleMasVendidoOficina.Key} ({muebleMasVendidoOficina.Value})", montoTotalOficina);
            serieMonto.Points.AddXY($"Dormitorio: {muebleMasVendidoDormitorio.Key} ({muebleMasVendidoDormitorio.Value})", montoTotalDormitorio);
            serieMonto.IsValueShownAsLabel = true;
            chartMuebleMasVendido.Series.Add(serieMonto);

            Legend leyenda = new Legend();
            leyenda.Title = "Leyenda";
            chartMuebleMasVendido.Legends.Add(leyenda);
        }

        private void CargarGraficoMuebleMenosVendido()
        {
            var mueblesMenosVendidosOficina = bllCliente.ObtenerMueblesMenosVendidosPorTipo("Oficina");
            var mueblesMenosVendidosDormitorio = bllCliente.ObtenerMueblesMenosVendidosPorTipo("Dormitorio");

            var muebleMenosVendidoOficina = mueblesMenosVendidosOficina.FirstOrDefault();
            var muebleMenosVendidoDormitorio = mueblesMenosVendidosDormitorio.FirstOrDefault();

            float montoTotalOficina = bllCliente.ObtenerMontoTotalRecaudadoPorDescripcion(muebleMenosVendidoOficina.Key);
            float montoTotalDormitorio = bllCliente.ObtenerMontoTotalRecaudadoPorDescripcion(muebleMenosVendidoDormitorio.Key);

            chartMuebleMenosVendido.Titles.Clear();
            chartMuebleMenosVendido.ChartAreas.Clear();
            chartMuebleMenosVendido.Series.Clear();

            Title titulo = new Title("Muebles Menos Vendidos");
            titulo.Font = new System.Drawing.Font("Tahoma", 20, System.Drawing.FontStyle.Bold);
            chartMuebleMenosVendido.Titles.Add(titulo);

            ChartArea area = new ChartArea();
            area.Area3DStyle.Enable3D = true;
            area.AxisX.Title = "Mueble";
            area.AxisY.Title = "Monto Recaudado";
            area.AxisX.TitleFont = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);
            area.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);
            chartMuebleMenosVendido.ChartAreas.Add(area);

            // Monto total recaudado por los muebles menos vendidos
            Series serieMonto = new Series("Monto Recaudado");
            serieMonto.ChartType = SeriesChartType.Bar;
            serieMonto.Color = System.Drawing.Color.Orange;
            serieMonto.Points.AddXY($"Oficina: {muebleMenosVendidoOficina.Key} ({muebleMenosVendidoOficina.Value})", montoTotalOficina);
            serieMonto.Points.AddXY($"Dormitorio: {muebleMenosVendidoDormitorio.Key} ({muebleMenosVendidoDormitorio.Value})", montoTotalDormitorio);
            serieMonto.IsValueShownAsLabel = true;
            chartMuebleMenosVendido.Series.Add(serieMonto);

            Legend leyenda = new Legend();
            leyenda.Title = "Leyenda";
            chartMuebleMenosVendido.Legends.Add(leyenda);
        }

        private void CargarGraficoMontoTotalRecaudado()
        {
            float montoTotalDormitorio = bllCliente.ObtenerMontoTotalRecaudado("Dormitorio");
            float montoTotalOficina = bllCliente.ObtenerMontoTotalRecaudado("Oficina");

            chartMontoTotalRecaudado.Titles.Clear();
            chartMontoTotalRecaudado.ChartAreas.Clear();
            chartMontoTotalRecaudado.Series.Clear();

            Title titulo = new Title("Monto Total Recaudado");
            titulo.Font = new System.Drawing.Font("Tahoma", 20, System.Drawing.FontStyle.Bold);
            chartMontoTotalRecaudado.Titles.Add(titulo);

            ChartArea area = new ChartArea();
            area.Area3DStyle.Enable3D = true;
            area.AxisX.Title = "Tipo de Mueble";
            area.AxisY.Title = "Monto Recaudado";
            area.AxisX.TitleFont = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);
            area.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);
            chartMontoTotalRecaudado.ChartAreas.Add(area);

            Series serie = new Series("Monto Recaudado");
            serie.ChartType = SeriesChartType.Bar;
            serie.Color = System.Drawing.Color.Red;
            serie.Points.AddXY("Dormitorio", montoTotalDormitorio);
            serie.Points.AddXY("Oficina", montoTotalOficina);
            serie.IsValueShownAsLabel = true;
            chartMontoTotalRecaudado.Series.Add(serie);

            Legend leyenda = new Legend();
            leyenda.Title = "Leyenda";
            chartMontoTotalRecaudado.Legends.Add(leyenda);
        }

        public void ActualizarGraficoMontoTotalRecaudado()
        {
            CargarGraficoMontoTotalRecaudado();
        }
    }
}
