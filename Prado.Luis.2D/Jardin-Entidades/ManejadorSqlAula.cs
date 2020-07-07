using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Jardin_Entidades
{
    public class ManejadorSqlAula
    {
        SqlConnection cl;
        SqlCommand sm;
        List<Aula> aulas;
        public ManejadorSqlAula()
        {
            aulas = new List<Aula>();
            cl = new SqlConnection();

            sm = new SqlCommand();

            cl.ConnectionString = @"Data Source= DESKTOP-Q2NN84T;Initial Catalog = JardinUtn; Integrated Security = True";
        }

        public DataTable Leer()
        {
            DataTable dt = new DataTable();
            try
            {

                //abre una conexion a la base de datos.
                cl.Open();

                //usamos para la consulta la conexion creada
                sm.Connection = cl;
                //texto de comando para realiza consultas a la base de datos
                sm.CommandText = "select * from Aulas";
                //ejecutamos el comando creando un objeto sqlreader con el metodo executereader
                SqlDataReader dr = sm.ExecuteReader(); //obtiene los datos

                dt.Load(dr);
                //tiene los datos en tabla del reader

                //mientras pueda leer la proxima columna ingreso


            }
            catch (Exception ex)
            {
                Log.GuardarErrorExcepcion(ex);
            }
            finally
            {
                cl.Close();
            }
            return dt;
        }

        public List<Aula> LeerBaseAula()
        {
            try
            {
                aulas.Clear();//limpiamos la lista de personas
                //abre una conexion a la base de datos.
                cl.Open();

                //usamos para la consulta la conexion creada
                sm.Connection = cl;
                //texto de comando para realiza consultas a la base de datos
                sm.CommandText = "select * from Aulas";
                //ejecutamos el comando creando un objeto sqlreader con el metodo executereader
                SqlDataReader dr = sm.ExecuteReader(); //obtiene los datos

                //mientras pueda leer la proxima columna ingreso
                while (dr.Read())
                {
                    //public Docente(int idDocente,string nombre, string apellido, int dni,int edad,string sexo, string domicilio,string email) : base(idDocente,nombre, apellido, dni,edad, domicilio )
                    //se crea un keyvaluepar cada vez que se va obteniendo una fila para agregar desde la base hasta el formulario
                    aulas.Add(new Aula(int.Parse(dr[0].ToString()), dr[1].ToString()));
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cl.Close();
            }
            return this.aulas;
        }

    }
}
