using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jardin_Entidades;
using System.Threading;

namespace FrmJardinUtn
{
    delegate void CalificaNota(Alumno a);
    delegate void Evaluar(object o);

    delegate void DelegadoParaAlterarLabel3(object objeto);
    delegate void DelegadoPublicaRecreo(object objeto);
    public partial class FrmJardin : Form
    {
        ManejadorSqlDocente sqlDocente;
        ManejadorSqlAlumnos sqlAlumno;
        ManejadorSqlAula sqlAula;
        ManejadorSqlEvaluacion sqlEvaluacion;
        //------------------------
        //CONSTANTES
        const int TIEMPOPROXIMO = 1000;

        int condador = 0;
        Queue<Alumno> alumnos;
        List<Alumno> alumnos2;
        List<string> alumnosEvaluadosRecreo;
        List<Docente> docentes;
        List<Aula> aulas;
        List<Thread> hilos;
        int indice;
        List<Persona> aprobados;
        List<Persona> desaprobados;
        List<Evaluaciones> evaluados;
        bool primerItem;

        //EVENTOS
        event CalificaNota EvaluarAlumno;
        event Evaluar ContinuaProximo;


        FrmJardin2 frmJardin2;

        /// <summary>
        /// Constructor de FrmJardin 
        /// </summary>
        public FrmJardin()
        {
            InitializeComponent();
            sqlDocente = new ManejadorSqlDocente();
            sqlAlumno = new ManejadorSqlAlumnos();
            sqlAula = new ManejadorSqlAula();
            sqlEvaluacion = new ManejadorSqlEvaluacion();
            //--------------
            hilos = new List<Thread>();
            this.alumnos = this.GuardoAlumnosCola();
            this.alumnos2 = sqlAlumno.LeerBaseA();
            this.alumnosEvaluadosRecreo = new List<string>();
            indice = 0;
            primerItem = true;
            this.docentes = sqlDocente.LeerBaseDocente();
            this.aulas = sqlAula.LeerBaseAula();
            this.aprobados = new List<Persona>();
            this.desaprobados = new List<Persona>();
            this.evaluados = new List<Evaluaciones>();

            //eventos       asigan los manejadores
            EvaluarAlumno += LlevarAEvaluar;
            ContinuaProximo += Proximo;


        }
        /// <summary>
        /// Obtiene los alumnos de la base de datos a una cola
        /// </summary>
        /// <returns></returns>
        private Queue<Alumno> GuardoAlumnosCola()
        {
            Queue<Alumno> alumnos = new Queue<Alumno>();


            foreach (Alumno item in sqlAlumno.LeerBaseA())
            {
                alumnos.Enqueue(item);
            }
            return alumnos;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataAlumnos.ReadOnly = true;
            this.dataAlumnos.RowHeadersVisible = false;
            this.dataAlumnos.AllowUserToAddRows = false;
            this.dataDocentes.ReadOnly = true;
            this.dataDocentes.RowHeadersVisible = false;
            this.dataDocentes.AllowUserToAddRows = false;

            if (this.docentes.Count == 0)
            {
                this.InsertarDocentesABase();
            }

            this.LeerBaseDataDocente();
            this.LeerBaseDataAlumno();

            frmJardin2 = new FrmJardin2();
            frmJardin2.Show();
            this.InciarHilos();
        }


        /// <summary>
        /// Si hay alumnos los lleve a al metodo evaluar
        /// </summary>
        /// <param name="listbox"></param>
        //metodo manejador proximo-->
        private void Proximo(object listbox)
        {
            //si tiene personas pendientes, la atiende
            if (alumnos.Count > 0)
            {
                Evaluar(alumnos.Dequeue(), (ListBox)listbox);
            }

        }
        /// <summary>
        /// Muestras los alumnos en el formulario dejardin 2 evalua los alumnos
        /// </summary>
        /// <param name="alumno"></param>
        /// <param name="txt"></param>
        void Evaluar(Alumno alumno, ListBox txt)
        {

            MostrarProximo();

            //----------------------->Llevar a evaluar
            EvaluarAlumno(alumno);


            //Lanza el evento que llama al proximo
            ContinuaProximo(txt);
        }

        /// <summary>
        /// Muestra el proximo alumno a ser evaluado en el formulario jadin2 de las lista alumnos
        /// A la mismo tiempo que va eliminando al alumno de la base datos.
        /// </summary>
        public void MostrarProximo()
        {
            indice++; //aumentamoshasta el maximo de la lista

            if (frmJardin2.ListaProximo.InvokeRequired)
            {

                frmJardin2.ListaProximo.Invoke((MethodInvoker)delegate
                {
                    if (indice < alumnos2.Count)
                    {
                        if (primerItem)
                        {
                            this.EliminarAlumnos(this.alumnos2[indice - 1]);
                            primerItem = false;
                        }
                        frmJardin2.ListaProximo.Items.Clear();

                        frmJardin2.ListaProximo.Items.Add(this.alumnos2[indice].ToString());
                        this.EliminarAlumnos(this.alumnos2[indice]);
                    }

                });
            }
            else
            {
                if (indice < alumnos2.Count)
                {
                    if (primerItem)
                    {
                        this.EliminarAlumnos(this.alumnos2[indice - 1]);
                        primerItem = false;
                    }
                    frmJardin2.ListaProximo.Items.Clear();
                    frmJardin2.ListaProximo.Items.Add(this.alumnos2[indice].ToString());
                    this.EliminarAlumnos(this.alumnos2[indice]);

                }
            }



        }
        /// <summary>
        /// Elimina el alumno de la base de datos y actualiza el datagridview
        /// </summary>
        /// <param name="alumno"></param>
        private void EliminarAlumnos(Alumno alumno)
        {

            //servira para cargar la tabla de evaluados
            if (this.dataAlumnos.InvokeRequired)
            {
                this.dataAlumnos.Invoke((MethodInvoker)delegate
                {

                    sqlAlumno.EliminarAlumnosBase(alumno.Id);
                    this.LeerBaseDataAlumno();
                });
            }
            else
            {
                sqlAlumno.EliminarAlumnosBase(alumno.Id);
                this.LeerBaseDataAlumno();
            }

        }
        /// <summary>
        /// Lleva a evaluar wl alumno para mostrar en el datagridview evaluados
        /// </summary>
        /// <param name="a"></param>
        private void LlevarAEvaluar(Alumno a)
        {

            // muestra en pantalla
            EvaluandoAlumno(a);

        }

        /// <summary>
        /// Muestra los alumnos en el list box y despues los evalua y los carga en el datagridview evaluados
        /// Guarda los alumnos aprobados y desaprobados en directorios correspondientes.
        /// El alumno es guardado en archivos xml
        /// </summary>
        /// <param name="alumno"></param>
        private void EvaluandoAlumno(Alumno alumno)
        {
            if (frmJardin2.ListaEvaluados.InvokeRequired)
            {
                frmJardin2.ListaEvaluados.Invoke((MethodInvoker)delegate
                {
                    frmJardin2.ListaEvaluados.Items.Clear();
                    frmJardin2.ListaEvaluados.Items.Add(alumno.ToString());

                });
            }
            else
            {
                frmJardin2.ListaEvaluados.Items.Clear();
                frmJardin2.ListaEvaluados.Items.Add(alumno.ToString());

            }

            Random rD = new Random();
            int nd = rD.Next(docentes[0].Id, docentes[9].Id);
            Random rN1 = new Random();
            int n1 = rN1.Next(1, 10);
            Random rN2 = new Random();
            int n2 = rN2.Next(1, 10);
            Random rA = new Random();
            int nA = rA.Next(aulas[0].IdAula, aulas[5].IdAula);
            float final = (n1 + n2) / 2;

            Evaluaciones evaluado = new Evaluaciones(alumno.Id, nd, nA, n1, n2, final, Evaluaciones.AsignarObservaciones(final));

            this.evaluados.Add(evaluado);
            Thread.Sleep(TIEMPOPROXIMO);

            this.CargarEvaluados(evaluado);

            alumno.NotaFinal = final;
            if (evaluado.NotaFinal >= 4)
            {

                this.aprobados.Add(alumno);
                Alumno.Guardar(alumno, true);
            }
            else
            {
                this.desaprobados.Add(alumno);
                Alumno.Guardar(alumno, false);
            }
            this.alumnosEvaluadosRecreo.Add(alumno.MostrarNotaFinal());
        }
        /// <summary>
        /// Carga las instancias evaluados en la datagridview evalados, muestra y la actualiza
        /// </summary>
        /// <param name="evaluado"></param>
        private void CargarEvaluados(Evaluaciones evaluado)
        {

            //servira para cargar la tabla de evaluados
            if (frmJardin2.TablaEvaluados.InvokeRequired)
            {
                frmJardin2.TablaEvaluados.Invoke((MethodInvoker)delegate
                {

                    //frmJardin2.Evaluados.Items.Add(evaluado.ToString());


                    sqlEvaluacion.Guardar(evaluado.IdAlumno, evaluado.IdDocente, evaluado.IdAula, evaluado.Nota_1, evaluado.Nota_2, evaluado.NotaFinal, evaluado.Observaciones);
                    frmJardin2.TablaEvaluados.DataSource = sqlEvaluacion.Leer();
                });
            }
            else
            {
                //frmJardin2.Evaluados.Items.Add(evaluado.ToString());
                sqlEvaluacion.Guardar(evaluado.IdAlumno, evaluado.IdDocente, evaluado.IdAula, evaluado.Nota_1, evaluado.Nota_2, evaluado.NotaFinal, evaluado.Observaciones);
                frmJardin2.TablaEvaluados.DataSource = sqlEvaluacion.Leer();
            }

        }
        /// <summary>
        /// Trae los docentes leidos en el archivo xml y los carga en la base de datos la tabla docentes
        /// </summary>
        private void InsertarDocentesABase()
        {
            if(Docente.LeerArchivoDocente() != null)
            {
                foreach (Docente item in Docente.LeerArchivoDocente())
                {
                    int resultado = sqlDocente.Guardar(item.Nombre, item.Apellido, item.Dni, item.Edad, item.Sexo, item.Direccion, item.Email);

                    if (resultado != 1)
                    {
                        MessageBox.Show("No se pudo insertar");
                    }
                    else
                    {
                        MessageBox.Show("Se ha guardado persona");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lista Docente Nula!");
            }

        }
        /// <summary>
        /// Lee los docentes de la base de datos y los inserta en el datagridview
        /// </summary>
        private void LeerBaseDataDocente()
        {
            this.dataDocentes.DataSource = sqlDocente.Leer();
        }
        /// <summary>
        /// Lee los alumnos de la base de datos y los inserta en el datagridview
        /// </summary>
        private void LeerBaseDataAlumno()
        {
            this.dataAlumnos.DataSource = sqlAlumno.Leer();
        }
        /// <summary>
        /// al cerrar el formulario Finaliza los hilos 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmJardin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Elimino los hilos si aun existen
            foreach (Thread item in hilos)
            {
                if (item.IsAlive)
                    item.Abort();
            }
        }

        /// <summary>
        /// Incia los hilos a una lista de hilos
        /// </summary>
        private void InciarHilos()
        {
            if (hilos.Count != 5)
            {
                //Agrego los hilos a la lista
                Thread t = new Thread(new ParameterizedThreadStart(Proximo));
                hilos.Add(t);
                //-------------->hilo con delegado void
                ThreadStart ts = new ThreadStart(ActualizarHora);
                hilos.Add(new Thread(ts));


            }

            //Inicio los hilos si no están andando

            if (!hilos[0].IsAlive)
            {
                hilos[0] = new Thread(new ParameterizedThreadStart(Proximo));
                hilos[0].Start(frmJardin2.ListaProximo);
            }

            if (!hilos[1].IsAlive)
            {
                ThreadStart ts = new ThreadStart(ActualizarHora);
                hilos[1] = new Thread(ts);
                hilos[1].Start();

            }

        }
        /// <summary>
        /// Muestra el objeto pasado por parametro en el label
        /// </summary>
        /// <param name="obj"></param>
        public void AlterarLabel3(object obj)
        {
            if (this.lblTiempo.InvokeRequired)
            {
                DelegadoParaAlterarLabel3 aux3 = new DelegadoParaAlterarLabel3(AlterarLabel3);

                Object[] objetos = new object[] { obj };
                //al formulario por el this invoca al segundo hilo al formulario el primero
                this.Invoke(aux3, objetos);//invoca al metodo en el hilo principal para que imprima el txt

                //invoke espera al hilo principal y depues el segundo
                //beginInvoke va al hilo principal y continua igual el segundo hilo corriendo
            }
            else
            {
                this.lblTiempo.Text = obj.ToString();

            }
        }

        /// <summary>
        /// actualiza la hora en el label desde el comienzo del programa
        /// Ademas Muestra los alumnos evaluados al tiempo del recreo
        /// </summary>
        public void ActualizarHora()
        {
            int contadorRecreo = 0;
            bool enRecreo = false;
            int minuto = 0;
            string tiempo;

            do
            {
                this.condador++;
                contadorRecreo++;


                if (this.condador == 59)
                {
                    this.condador = 0;
                    minuto++;

                }
                tiempo = "TIEMPO TRANSCURRIDO:" + minuto + ":" + this.condador;
                this.AlterarLabel3(tiempo);
                Thread.Sleep(1000);

                if (contadorRecreo == 20 && enRecreo == false)
                {
                    this.PublicarRecreo(this.alumnosEvaluadosRecreo);
                    contadorRecreo = 0;
                    enRecreo = true;

                }
                if (enRecreo == true && contadorRecreo == 5)
                {
                    this.PublicarRecreo(null);
                    contadorRecreo = 0;
                    enRecreo = false;
                }

            }
            while (this.alumnos.Count > 0);

        }
        /// <summary>
        /// Muesta el objeto pasado por parameto a listbox del formulario
        /// </summary>
        /// <param name="obj"></param>
        public void PublicarRecreo(object obj)
        {


            if (this.listRecreo.InvokeRequired)
            {
                DelegadoPublicaRecreo aux2 = new DelegadoPublicaRecreo(PublicarRecreo);

                Object[] objetos = new object[] { obj };
                //al formulario por el this invoca al segundo hilo al formulario el primero
                this.BeginInvoke(aux2, objetos);//invoca al metodo en el hilo principal para que imprima el txt
            }
            else
            {
                this.listRecreo.DataSource = obj;
            }
        }
        /// <summary>
        /// Cierra el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Restaura el tamaño normal del formualario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMax.Visible = true;
        }
        /// <summary>
        /// Minimiza el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// Maximiza el tamaño del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMax.Visible = false;
            btnRestaurar.Visible = true;
        }
    }
}
