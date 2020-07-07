using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardin_Entidades
{
    public interface IArchivo<T>
    {
        bool GuardarArchivo(string archivo, T datos);
        bool LeerArchivo(string archivo, out T datos);
    }
}
