using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Jardin_Entidades
{
    public class Log
    {
        public static void GuardarErrorExcepcion(Exception ex)
        {
            
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Errores";
            if (!Directory.Exists(ruta))
            {
                
                Directory.CreateDirectory(ruta);
                
            }


            using (StreamWriter sw = File.AppendText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Errores\Log.txt"))
            {
                sw.WriteLine("=============Log de Errores ===========");
                sw.WriteLine("===========Inico============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========Fin============= " + DateTime.Now);

            }
        }
    }
}
