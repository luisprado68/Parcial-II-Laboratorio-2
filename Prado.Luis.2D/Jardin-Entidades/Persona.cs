using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardin_Entidades
{
    [Serializable]
    public abstract class Persona
    {
        private int id;
        private string nombre;
        private string apellido;
        private int dni;
        private int edad;
        private string direccion;

        public Persona()
        {

        }
        protected Persona( string nombre, string apellido, int dni):this()
        {
           
            this.Nombre = nombre;
            this.Apellido = apellido;

            this.Dni = dni; // lo asgnamos a la propiedad para que haga la validacion de dni.
        }
        protected Persona(string nombre, string apellido, int edad, int dni, string direccion) : this( nombre, apellido, dni)
        {
            this.Edad = edad;
            this.direccion = direccion;
        }
        protected Persona(int id,string nombre, string apellido,int edad, int dni, string direccion) : this(nombre, apellido,edad, dni,direccion)
        {
            this.id = id;
            
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int Edad
        {
            get { return this.edad; }
            set { this.edad = value; }
        }

        public string Apellido
        {
            get { return this.apellido; }
            set { this.apellido = value; }
        }

        public string Direccion
        {
            get { return this.direccion; }
            set { this.direccion = value; }
        }
        public int Dni
        {
            get { return this.dni; }

            set
            {
                
                this.dni = value;
            }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public override string ToString()
        {
            StringBuilder datos = new StringBuilder();

            datos.AppendLine($"{this.Id}");
            datos.AppendLine($"{this.Nombre}");
            datos.AppendLine($"{this.Apellido}");
            datos.AppendLine($"{this.Dni}");
            datos.AppendLine($"{this.Edad}");
            datos.AppendLine($"{this.Direccion}");
            return datos.ToString();

        }


    }
}
