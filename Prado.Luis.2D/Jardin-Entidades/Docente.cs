using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardin_Entidades
{
    public class Docente : Persona
    {

        private string sexo;
        private string email;

        public Docente():base()
        {

        }
        public Docente(string nombre, string apellido, int dni, int edad, string sexo, string domicilio, string email) : base(nombre, apellido, dni, edad, domicilio)
        {
            this.Sexo = sexo;
            this.Email = email;
        }
        public Docente(int idDocente,string nombre, string apellido, int edad, string sexo,int dni, string domicilio,string email) : base(idDocente,nombre, apellido, dni,edad, domicilio )
        {
            this.Sexo = sexo;
            this.Email = email;
        }
        
       
        public string Sexo
        {
            get { return this.sexo; }
            set { this.sexo = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
      

        public override string ToString()
        {
            StringBuilder datos = new StringBuilder();


            datos.Append(base.ToString());
            datos.AppendLine($"{this.Sexo}");
            datos.AppendLine($"{this.Email}");


            return datos.ToString();
        }

        public static List<Docente> LeerArchivoDocente()
        {
            ArchivoXml<List<Docente>> archivo = new ArchivoXml<List<Docente>>();
            List<Docente> nueva= null;

            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SegundoParcialUtn\JardinUtn\Docentes\Docentes.xml";
            try
            {
                if (archivo.LeerArchivo(ruta, out  nueva))
                {
                    
                }
                else
                {
                    throw new Exception("Error al Leer");

                }
            }
            catch(Exception ex)
            {
                Log.GuardarErrorExcepcion(ex);
            }
            finally
            {
                
            }
            return nueva;
        }
    }
}
