using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class MAPPER<T>
    {
        public DAL.ACCESO acceso = new DAL.ACCESO();
        public abstract void Insertar(T obj);

        public abstract void Modificar(T obj);

        public abstract void Eliminar(T obj);

        public abstract List<T> Listar();


    }
}
