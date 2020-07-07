using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Jardin_Entidades
{
    public class ArchivoBinario<T> : IArchivo<T>
    {
        

        /// <summary>
        /// Implementación del método de la interface IArchivo
        /// que serializa datos en formato binario.
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool GuardarArchivo(string archivo, T datos)
        {
            FileStream fs;
            BinaryFormatter serBN;

            bool pudoSerializar = false;

            if (!(archivo is null))
            {
                try
                {
                    using (fs = new FileStream(archivo, FileMode.OpenOrCreate))
                    {
                        serBN = new BinaryFormatter();
                        serBN.Serialize(fs, datos);
                        pudoSerializar = true;
                    }
                        
                    
                }
                catch(ArchivoException ex)
                {
                    throw new ArchivoException("Error al guardar archivo .dat", ex);
                }
                
                
            }

            return pudoSerializar;
        }

        /// <summary>
        /// Implementación del método de la interface IArchivo
        /// que deserializa datos en formato binario.
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool LeerArchivo(string archivo, out T datos)
        {
            FileStream fs;//obj que leera en binario.
            BinaryFormatter deserBN;//obj que deserializará.
            bool pudoLeer = false;
            datos = default;

            if (!(archivo is null))
            {
                try
                {
                    using (fs = new FileStream(archivo, FileMode.Open)) //, FileAccess.Read, FileShare.ReadWrite);
                    {
                        deserBN = new BinaryFormatter();
                        datos = (T)deserBN.Deserialize(fs);
                    }
                }
                catch(ArchivoException ex)
                {
                    throw new ArchivoException("Error al leer archivo .dat", ex);
                }
               
                pudoLeer = true;
            }

            return pudoLeer;
        }
        
    }
}
