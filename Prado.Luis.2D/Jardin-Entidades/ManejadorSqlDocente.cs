using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Jardin_Entidades
{
    public class ManejadorSqlDocente
    {
        SqlConnection cl;
        SqlCommand sm;
        Docente docente;
        List<Docente> docentes;


        public ManejadorSqlDocente()
        {
            this.docentes = new List<Docente>();
            cl = new SqlConnection();
         
            sm = new SqlCommand();
                             
            cl.ConnectionString = @"Data Source= DESKTOP-Q2NN84T;Initial Catalog = JardinUtn; Integrated Security = True";
        }

        public  int Guardar( string nombre, string apellido, int dni, int edad, string sexo, string domicilio, string email)
        {
            int retorno = 0;


            this.docente = new Docente(nombre, apellido,dni,edad,sexo,domicilio,email);

            try
            {
                //limpiamos localidades.

                sm.Parameters.Clear();
                cl.Open();


                sm.Connection = cl;

                //de nuestro combo provincias buscamos el item seleccionado, con selected item lo casteamos al tipo de dato que es
                //y como ese item tiene clave y valor elegimos la clave que es el id
                sm.CommandText = "insert into Docentes (nombre, apellido,dni,edad,sexo,direccion,email) VALUES  (@nombre, @apellido,@dni,@edad,@sexo,@domicilio,@email)";

                sm.Parameters.Add(new SqlParameter("nombre", docente.Nombre));
                sm.Parameters.Add(new SqlParameter("apellido", docente.Apellido));
                sm.Parameters.Add(new SqlParameter("edad", docente.Edad));
                sm.Parameters.Add(new SqlParameter("sexo", docente.Sexo));
                sm.Parameters.Add(new SqlParameter("dni", docente.Dni));
                sm.Parameters.Add(new SqlParameter("domicilio", docente.Direccion));
                sm.Parameters.Add(new SqlParameter("email", docente.Email));



                //sive para insertar y actualizar 
                //retorna un numero de filas afectadas
                retorno = sm.ExecuteNonQuery();

             

            }
            catch (Exception ex)
            {
                Log.GuardarErrorExcepcion(ex);
            }
            finally
            {
                cl.Close();
            }


            return retorno;
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
                sm.CommandText = "select * from Docentes";
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

        public List<Docente> LeerBaseDocente()
        {
            try
            {
                docentes.Clear();//limpiamos la lista de personas
                //abre una conexion a la base de datos.
                cl.Open();

                //usamos para la consulta la conexion creada
                sm.Connection = cl;
                //texto de comando para realiza consultas a la base de datos
                sm.CommandText = "select * from Docentes";
                //ejecutamos el comando creando un objeto sqlreader con el metodo executereader
                SqlDataReader dr = sm.ExecuteReader(); //obtiene los datos

                //mientras pueda leer la proxima columna ingreso
                while (dr.Read())
                {
                    //public Docente(int idDocente,string nombre, string apellido, int dni,int edad,string sexo, string domicilio,string email) : base(idDocente,nombre, apellido, dni,edad, domicilio )
                    //se crea un keyvaluepar cada vez que se va obteniendo una fila para agregar desde la base hasta el formulario
                    docentes.Add(new Docente(int.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), int.Parse(dr[3].ToString()), dr[4].ToString(), int.Parse(dr[5].ToString()), dr[6].ToString(), dr[7].ToString()));
                }



            }
            catch (Exception ex)
            {
                Log.GuardarErrorExcepcion(ex);
            }
            finally
            {
                cl.Close();
            }
            return this.docentes;
        }

    }
}
