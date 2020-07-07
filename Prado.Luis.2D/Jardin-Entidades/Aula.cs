using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardin_Entidades
{
    public class Aula
    {
        private int idAula;
        private string salita;
        public int IdAula
        {
            get { return this.idAula; }
            set { this.idAula = value; }
        }
        public string Salita
        {
            get { return this.salita; }
            set { this.salita = value; }
        }
        
        public Aula()
        {

        }
        public Aula(int id,string color)
        {
            this.IdAula = id;
            this.Salita = color;
        }
        public override string ToString()
        {
            StringBuilder datos = new StringBuilder();

            datos.AppendLine($"id Aula:{this.IdAula}");
            datos.AppendLine($"Color Sala:{this.Salita}");
           

            return datos.ToString();
        }


    }
}
