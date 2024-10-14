using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using MPP;

namespace BLL
{
    public class BLLCliente
    {
        private MPPCliente mppCliente = new MPPCliente();
        private BLLDormitorio bllDormitorio = new BLLDormitorio();
        private BLLOficina bllOficina = new BLLOficina();

        public List<BECliente> ObtenerClientes()
        {
            return mppCliente.LeerClientes();
        }

        public void AgregarCliente(BECliente cliente)
        {
            mppCliente.AgregarCliente(cliente);
        }

        public void EliminarCliente(string id)
        {
            mppCliente.BorrarCliente(id);
        }

        public void ModificarCliente(BECliente cliente)
        {
            mppCliente.ModificarCliente(cliente);
        }

        public List<BEMueble> ObtenerMuebles()
        {
            var mueblesDormitorio = bllDormitorio.ObtenerMuebles();
            var mueblesOficina = bllOficina.ObtenerMuebles();
            return mueblesDormitorio.Concat(mueblesOficina).ToList();
        }

        public void RegistrarVenta(int clienteCodigo, int muebleCodigo, int cantidad, float precio, string descripcion)
        {
            var cliente = ObtenerClientes().FirstOrDefault(c => c.Codigo == clienteCodigo);
            var mueble = ObtenerMuebles().FirstOrDefault(m => m.Codigo == muebleCodigo);

            if (cliente != null && mueble != null)
            {
                cliente.oMueble = mueble;
                mueble.Cantidad -= cantidad;

                if (mueble.Cantidad == 0)
                {
                    mueble.Estado = "Adquirido";
                }

                mppCliente.ModificarCliente(cliente);
                mppCliente.RegistrarVenta(clienteCodigo, muebleCodigo, cantidad, precio, descripcion);

                ActualizarStockMueble(mueble);
            }
        }

        private void ActualizarStockMueble(BEMueble mueble)
        {
            MPPMueble mppMueble = mueble is BEOficina ? new MPPOficina() : (MPPMueble)new MPPDormitorio();
            mppMueble.ModificarMueble(mueble);
        }

        public List<BEMueble> ObtenerVentas()
        {
            return mppCliente.ObtenerVentas();
        }

        public List<BEMueble> ObtenerVentasPorCliente(int clienteCodigo)
        {
            return mppCliente.ObtenerVentasPorCliente(clienteCodigo);
        }

        public Dictionary<string, int> ObtenerMueblesMasVendidos()
        {
            var ventas = ObtenerVentas();
            return ventas
            .GroupBy(m => m.Descripcion)
                .OrderBy(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> ObtenerMueblesMenosVendidos()
        {
            var ventas = ObtenerVentas();
            return ventas
            .GroupBy(m => m.Descripcion)
                .OrderByDescending(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> ObtenerMueblesMasVendidosPorTipo(string tipo)
        {
            var ventas = ObtenerVentas().Where(v => (tipo == "Oficina" && v is BEOficina) || (tipo == "Dormitorio" && v is BEDormitorio));
            return ventas
                .GroupBy(v => v.Descripcion)
                .OrderByDescending(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> ObtenerMueblesMenosVendidosPorTipo(string tipo)
        {
            var ventas = ObtenerVentas().Where(v => (tipo == "Oficina" && v is BEOficina) || (tipo == "Dormitorio" && v is BEDormitorio));
            return ventas
                .GroupBy(v => v.Descripcion)
                .OrderBy(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public float ObtenerMontoTotalRecaudado(string tipoMueble)
        {
            var ventas = ObtenerVentas();
            float totalRecaudado = 0;
            if (tipoMueble == "Dormitorio")
            {
                totalRecaudado = ventas
                    .Where(m => m is BEDormitorio)
                    .Sum(m => bllDormitorio.RealizarDescuento(m) * m.Cantidad);
            }
            else if (tipoMueble == "Oficina")
            {
                totalRecaudado = ventas
                    .Where(m => m is BEOficina)
                    .Sum(m => bllOficina.RealizarDescuento(m) * m.Cantidad);
            }

            return totalRecaudado;
        }

        public float ObtenerMontoTotalRecaudadoPorDescripcion(string descripcion)
        {
            var ventas = ObtenerVentas().Where(v => v.Descripcion == descripcion);
            float totalRecaudado = 0;
            foreach (var venta in ventas)
            {
                if (venta is BEOficina)
                {
                    totalRecaudado += bllOficina.RealizarDescuento(venta) * venta.Cantidad;
                }
                else if (venta is BEDormitorio)
                {
                    totalRecaudado += bllDormitorio.RealizarDescuento(venta) * venta.Cantidad;
                }
            }

            return totalRecaudado;
        }
    }
}
