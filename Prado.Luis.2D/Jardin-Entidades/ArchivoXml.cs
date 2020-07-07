using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Jardin_Entidades
{
    public class ArchivoXml<T> : IArchivo<T>
    {
        /// <summary>
        /// Guardara un archivo xml con los datos  del parametro datos 
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool GuardarArchivo(string archivo, T datos)
        {
         
            if (!string.IsNullOrEmpty(archivo) && datos != null)
            {
                try
                {

                    using (XmlTextWriter escribir = new XmlTextWriter(archivo, Encoding.UTF8))
                    {
                        XmlSerializer serializar = new XmlSerializer(typeof(T));

                        serializar.Serialize(escribir, datos);
                    }
                    return true;

                }
                catch (Exception ex)
                {
                    throw new ArchivoException("Error al guardar archivo xml",ex);
                }
            }

            return false;

        }

        /// <summary>
        /// Leera el archivo pasado por el parametro archivo 
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns>True en caso de poder leerlo y el objeto T</returns>
        public bool LeerArchivo(string archivo, out T datos)
        {
            datos = default;

            if (!string.IsNullOrEmpty(archivo))
            {
                try
                {
                    using (XmlTextReader leer = new XmlTextReader(archivo))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(T));

                        datos = (T)ser.Deserialize(leer);
                       
                        return true;
                    }


                }
                catch (Exception ex)
                {
                    throw new ArchivoException("Error al leer archivo de xml",ex);
                }
            }

            return false;

        }
    }
}
