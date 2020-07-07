using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardin_Entidades
{
    public class Evaluaciones
    {

        private int idEvaluacion;
        private int idAlumno;
        private int idDocente;
        private int idAula;
        private int nota_1;
        private int nota_2;
        private float notaFinal;
        private string observaciones;
       
        

        public int IdEvaluacion
        {
            get { return this.idEvaluacion; }
            set { this.idEvaluacion = value; }
        }
        public int IdAlumno
        {
            get { return this.idAlumno; }
            set { this.idAlumno = value; }
        }
        public int IdDocente
        {
            get { return this.idDocente; }
            set { this.idDocente = value; }
        }
        public int IdAula
        {
            get { return this.idAula; }
            set { this.idAula = value; }
        }
        public int Nota_1
        {
            get { return this.nota_1; }
            set { this.nota_1 = value; }
        }
        public int Nota_2
        {
            get { return this.nota_2; }
            set { this.nota_2 = value; }
        }

        public float NotaFinal
        {
            get { return this.notaFinal; }
            set { this.notaFinal = value; }
        }

        public string Observaciones
        {
            get { return this.observaciones; }
            set { this.observaciones = value; }
        }

        public Evaluaciones()
        {
            
            
        }

       
        public Evaluaciones(int idAlumno,int idDocente,int idAula,int nota1,int nota2,float notaFinal,string observaciones):this()
        {
            this.IdAlumno = idAlumno;
            this.IdDocente = idDocente;
            this.IdAula = idAula;
            this.Nota_1 = nota1;
            this.Nota_2 = nota2;
            this.NotaFinal = notaFinal;
            this.Observaciones = observaciones;

        }

        public static string AsignarObservaciones(float nota)
        {
            int aux = (int)nota;
            Random random = new Random();
            Random random2 = new Random();
            Random random3 = new Random();
            List<string>  observacionesLista = new List<string>();

            observacionesLista.Clear();
            observacionesLista.Add("Desaprobado");
            observacionesLista.Add("Aprobado");
            observacionesLista.Add("Muy Bien");
            observacionesLista.Add("Diabolico");
            observacionesLista.Add("Que agradable sujeto");

            if(aux >= 1 && aux <= 3)
            {
                return observacionesLista[random.Next(0, 0)];
            }
            else if (aux >= 4 && aux <= 7)
            {
                return observacionesLista[random2.Next(1, 3)];
            }
            else
            {
                return observacionesLista[random3.Next(4, 5)];
            }

        }

        public override string ToString()
        {
            StringBuilder datos = new StringBuilder();

            datos.AppendLine($"{this.IdAlumno}");
            datos.AppendLine($"{this.IdDocente}");
            datos.AppendLine($"{this.IdAula}");
            datos.AppendLine($"{this.Nota_1}");
            datos.AppendLine($"{this.nota_2}");
            datos.AppendLine($"{this.NotaFinal}");
            datos.AppendLine($"{this.Observaciones}");
            return datos.ToString();

        }
    }
}
