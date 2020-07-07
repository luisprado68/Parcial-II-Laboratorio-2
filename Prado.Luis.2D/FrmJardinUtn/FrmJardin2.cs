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

namespace FrmJardinUtn
{
    public partial class FrmJardin2 : Form
    {
        ManejadorSqlAula sqlAula;
        ManejadorSqlEvaluacion sqlEvaluacion;

        //--------------------

        /// <summary>
        /// Retorna  la listbox de alumnos evaludaos
        /// </summary>
        public ListBox ListaEvaluados
        {
            get { return this.listAlumnoEvaluado; }
            
        }
        /// <summary>
        /// Retorna la listbox del alumnos proximo
        /// </summary>
        public ListBox ListaProximo
        {
            get { return this.listAlumnoProximo; }
           
        }
        /// <summary>
        /// Retorna el datagridview Evaluados
        /// </summary>
        public DataGridView TablaEvaluados
        {
            get { return this.dataEvaluacion; }
          
        }

        /// <summary>
        /// //Constructor que instancia sqlAula y sqlEvaluacion
        /// </summary>

        public FrmJardin2()
        {
            InitializeComponent();
            sqlAula = new ManejadorSqlAula();
            sqlEvaluacion = new ManejadorSqlEvaluacion();

          
        }
        
        private void FrmJardin2_Load(object sender, EventArgs e)
        {
            this.dataAula.ReadOnly = true;
            this.dataAula.RowHeadersVisible = false;
            //this.dataAula.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataAula.AllowUserToAddRows = false;
            this.dataEvaluacion.ReadOnly = true;
            this.dataEvaluacion.RowHeadersVisible = false;
            //this.dataAula.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataEvaluacion.AllowUserToAddRows = false;
            this.LeerBaseDataAula();
            this.LeerBaseDataEvaluacion();
        }
        /// <summary>
        /// Carga datagridview con los datos de la tabla aula
        /// </summary>
        private void LeerBaseDataAula()
        {
            this.dataAula.DataSource = sqlAula.Leer();
        }
        /// <summary>
        /// Carga datagridview con los datos de la tabla evaluacion
        /// </summary>
        private void LeerBaseDataEvaluacion()
        {
            this.dataEvaluacion.DataSource = sqlEvaluacion.Leer();
        }
        /// <summary>
        /// Maximiza el formulario y no hace visible el boton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMax.Visible = false;
            btnRestaurar.Visible = true;

        }
        /// <summary>
        /// Cierre del formualrio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Vuleve el tamaño normal del formulario
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
    }
}
