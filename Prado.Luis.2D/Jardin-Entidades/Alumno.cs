using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Jardin_Entidades
{   [Serializable]
    public class Alumno : Persona
    {
        //private Ecolor colorSala;


        private int responsable;
        private float notaFinal;



        public int Responsable
        {
            get { return this.responsable; }
            set { this.responsable = value; }
        }
        public float NotaFinal
        {
            get { return this.notaFinal; }
            set { this.notaFinal = value; }
        }

        public Alumno() : base()
        {

        }
        public Alumno(int idAlumno, string nombre, string apellido, int edad, int dni, string domicilio, int responsable) : base(idAlumno, nombre, apellido, edad, dni, domicilio)
        {

            this.responsable = responsable;
        }





        public override string ToString()
        {
            StringBuilder datos = new StringBuilder();


            datos.Append(base.ToString());
            datos.AppendLine($"{this.Responsable}");

            return datos.ToString();

        }
        

        public static bool Guardar(Alumno alumno, bool aprobado)
        {
            bool seGuardo = false;
            try
            {
                IArchivo<Alumno> archivo = new ArchivoXml<Alumno>();

                if (aprobado)
                {
                    string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Aprobados";

                    if (!Directory.Exists(ruta))
                    {

                        Directory.CreateDirectory(ruta);
                        throw new ArchivoException("Se creo directorio Aprobados por que no existe");
                    }
                    if (archivo.GuardarArchivo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Aprobados\" + alumno.Nombre + "_" + alumno.Apellido + "_" + DateTime.Now.ToString("dd MM yyyy") + ".xml", alumno))
                    {
                        seGuardo = true;
                    }
                    else
                    {
                        throw new ArchivoException("Error al Guardar el Archivo");

                    }
                }
                else
                {
                    string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Desaprobados";

                    if (!Directory.Exists(ruta))
                    {

                        Directory.CreateDirectory(ruta);
                        throw new ArchivoException("Se creo directorio Desaprobados por que no existe");
                    }
                    if (archivo.GuardarArchivo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Desaprobados\" + alumno.Nombre + "_" + alumno.Apellido + "_" + DateTime.Now.ToString("dd MM yyyy") + ".xml", alumno))
                    {
                        seGuardo = true;
                    }
                    else
                    {
                        throw new ArchivoException("Error al Guardar el Archivo");

                    }
                }
            }
            catch (ArchivoException ex)
            {
                Log.GuardarErrorExcepcion(ex);
            }


            return seGuardo;

        }

        public static bool GuardarBinario(List<Alumno> alumno)
        {
            IArchivo<List<Alumno>> archivo = new ArchivoBinario<List<Alumno>>();
            bool pudoGuardarBin = false;
            bool existe = true;
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Binario";

            try
            {
                if (!Directory.Exists(ruta))
                {
                    existe = false;
                    Directory.CreateDirectory(ruta);

                }

                if (archivo.GuardarArchivo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Binario\Alumnos.bin", alumno))
                {
                    pudoGuardarBin = true;
                }
                else
                {
                    throw new ArchivoException("Error al Guardar el Archivo");

                }
                if (!existe)
                {
                    throw new ArchivoException("Se creo directorio Binario por que no existe");
                }

            }
            catch (ArchivoException ex)
            {
                Log.GuardarErrorExcepcion(ex);
            }

            return pudoGuardarBin;
        }

        public static List<Alumno> LeerBinario()
        {
            IArchivo<List<Alumno>> archivo = new ArchivoBinario<List<Alumno>>();

            List<Alumno> alumnos = null;
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Binario\alumnos.bin";



            try
            {
                if (archivo.LeerArchivo(ruta, out alumnos))
                {

                }
                else
                {
                    throw new ArchivoException("Error al Guardar el Archivo");

                }
            }
            catch (ArchivoException ex)
            {
                Log.GuardarErrorExcepcion(ex);
            }

            return alumnos;
        }

        //public static bool GuardarArchivoTxt(Alumno alumno,bool aprobado)
        //{
        //    Texto texto = new Texto();

        //    if (aprobado)
        //    {
        //        string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Aprobados";
        //        if (!Directory.Exists(ruta))
        //        {

        //            Directory.CreateDirectory(ruta);
        //            throw new ArchivoExcepcion("Se creo directorio Aprobados por que no existe");
        //        }

        //        if (texto.GuardarArchivo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Aprobados\" + alumno.Nombre + "_" + alumno.Apellido + "_" + DateTime.Now.ToString("dd MM yyyy") + ".txt", alumno.MostrarNotaFinal()))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            throw new ArchivoExcepcion("Error a Guardar el Archivo");

        //        }
        //    }
        //    else
        //    {
        //        string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Desaprobados";

        //        if (!Directory.Exists(ruta))
        //        {
        //             Directory.CreateDirectory(ruta);
        //            throw new ArchivoExcepcion("Se creo directorio Aprobados por que no existe");
        //        }
        //        if (texto.GuardarArchivo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Desaprobados\" + alumno.Nombre + "_" + alumno.Apellido + "_" + DateTime.Now.ToString("dd MM yyyy") + ".txt", alumno.MostrarNotaFinal()))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            throw new ArchivoExcepcion("Error al guardar archivo txt");

        //        }
        //    }




        //}



    }
}
