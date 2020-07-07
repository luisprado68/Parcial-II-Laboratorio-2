using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jardin_Entidades;

namespace Menu
{
    class Program
    {
        static void Main(string[] args)
        {
            ManejadorSqlAlumnos al = new ManejadorSqlAlumnos();
            ManejadorSqlDocente sd = new ManejadorSqlDocente();
            ManejadorSqlAula aula = new ManejadorSqlAula();

            if (Alumno.GuardarBinario(al.LeerBaseA()))
            {
                Console.WriteLine("Logro guaradar");
            }
            Console.ReadLine();

            //foreach (Alumno item in Alumno.LeerBinario())
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.ReadLine();




            //foreach (var item in al.LeerBaseA())
            //{
            //    Alumno.GuardarArchivoTxt(item);
            //}
            //Console.ReadLine();

            //List <Alumno> alumnos= al.LeerBaseA();
            //Alumno.GuardarArchivoTxt(alumnos[5]);

        }

        public static void GuardarAlumnoAprobado()
        {

        }
        
    }
}
