using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIO
{
    public class DIGITOSVERIFICADORES_BLL
    {
        DAL.MP_DIGITOSVERIFICADORES mapperDigitos = new DAL.MP_DIGITOSVERIFICADORES();
        DAL.MP_USUARIO mapperUsuario = new DAL.MP_USUARIO();
        DAL.MP_PRODUCTO mapperProducto = new DAL.MP_PRODUCTO();

        public List<string> VerificarIntegridadSistema()
        {
            List<string> erroresTotales = new List<string>();

            //USUARIOS - verificar
            List<IVerificarDigitos> listaUsuarios = mapperUsuario.Listar().Cast<IVerificarDigitos>().ToList();
            erroresTotales.AddRange(VerificarIntegridad(listaUsuarios, "USUARIO"));

            //PRODUCTOS - verificar
            List<IVerificarDigitos> listaProductos = mapperProducto.Listar().Cast<IVerificarDigitos>().ToList();
            erroresTotales.AddRange(VerificarIntegridad(listaProductos, "PRODUCTO"));

            return erroresTotales;
        }

        public List<string> VerificarIntegridad(List<IVerificarDigitos> lista, string nombreTabla)
        {
            List<string> errores = new List<string>();
            int sumaDVH = 0;

            foreach (var item in lista)
            {
                string dvhCalculado = item.CalcularDVH();

                if (item.Dvh != dvhCalculado)
                {
                    errores.Add($"[Tabla: {nombreTabla}] ID {item.Id}: DVH incorrecto. BD: {item.Dvh} | Calculado: {dvhCalculado}");
                }

                int valorNumericoFila = 0;
                int.TryParse(dvhCalculado, out valorNumericoFila);
                sumaDVH += valorNumericoFila;
            }

            int dvvBase = mapperDigitos.ObtenerDVV(nombreTabla);

            if (sumaDVH != dvvBase)
            {
                errores.Add($"[Tabla: {nombreTabla}] Error DVV: La suma vertical no coincide con la base de datos.");
            }

            return errores;
        }

        public void RecalcularDigitosSistema()
        {
            // USUARIOS - Recalcular
            List<IVerificarDigitos> listaUsuarios = mapperUsuario.Listar().Cast<IVerificarDigitos>().ToList();
            int sumaDVH = 0;

            foreach (var item in listaUsuarios)
            {
                string nuevoDvh = item.CalcularDVH();

                mapperDigitos.ActualizarDVH("USUARIO", item.Id, nuevoDvh);

                int valorNumericoFila = 0;
                int.TryParse(nuevoDvh, out valorNumericoFila);
                sumaDVH += valorNumericoFila;
            }

            mapperDigitos.ActualizarDVV("USUARIO", sumaDVH);


            //PRODUCTOS - Recalcular

            List<IVerificarDigitos> listaProductos = mapperProducto.Listar().Cast<IVerificarDigitos>().ToList();
            int sumaDVHProductos = 0;

            foreach (var item in listaProductos)
            {
                string nuevoDvh = item.CalcularDVH();
                mapperDigitos.ActualizarDVH("PRODUCTO", item.Id, nuevoDvh);

                int valorNumericoFila = 0;
                int.TryParse(nuevoDvh, out valorNumericoFila);
                sumaDVHProductos += valorNumericoFila;
            }
            mapperDigitos.ActualizarDVV("PRODUCTO", sumaDVHProductos);
        }


    }
}
