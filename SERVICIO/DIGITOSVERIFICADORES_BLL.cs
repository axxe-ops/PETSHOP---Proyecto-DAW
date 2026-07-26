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

        public List<string> VerificarIntegridadSistema()
        {
            List<string> erroresTotales = new List<string>();

            // Traemos la lista de usuarios desde la DAL y la casteamos a la interfaz
            List<IVerificarDigitos> listaUsuarios = mapperUsuario.Listar().Cast<IVerificarDigitos>().ToList();

            // Llamamos a tu método genérico pasándole los datos de la tabla USUARIOS
            var erroresUsuarios = VerificarIntegridad(listaUsuarios, "USUARIO");
            erroresTotales.AddRange(erroresUsuarios);

            // (Si el día de mañana agregas productos, los sumarías acá abajo)

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
            DAL.MP_USUARIO mapperUsuario = new DAL.MP_USUARIO();
            List<IVerificarDigitos> listaUsuarios = mapperUsuario.Listar().Cast<IVerificarDigitos>().ToList();

            int sumaDVH = 0;

            foreach (var item in listaUsuarios)
            {
                // 1. Recalculamos el DVH de la entidad
                string nuevoDvh = item.CalcularDVH();

                // 2. Actualizamos el DVH de este registro en la base de datos (necesitarás un método en tu DAL de usuario o en el de dígitos)
                mapperDigitos.ActualizarDVH(item.Id, nuevoDvh); // O mapperUsuario.ActualizarDVH(...)

                // 3. Acumulamos para el vertical
                int valorNumericoFila = 0;
                int.TryParse(nuevoDvh, out valorNumericoFila);
                sumaDVH += valorNumericoFila;
            }

            // 4. Guardamos el nuevo DVV vertical de la tabla USUARIO en la base de datos
            mapperDigitos.ActualizarDVV("USUARIO", sumaDVH);
        }


    }
}
