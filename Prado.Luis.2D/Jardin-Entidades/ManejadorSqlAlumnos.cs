using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Jardin_Entidades
{
    public class ManejadorSqlAlumnos
    {
        SqlConnection cl;
        SqlCommand sm;
        List<Alumno> alumnos;

        public ManejadorSqlAlumnos()
        {
            alumnos = new List<Alumno>();
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
                sm.CommandText = "select * from Alumnos";
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

        public List<Alumno> LeerBaseA()
        {
            try
            {
                alumnos.Clear();//limpiamos la lista de personas
                //abre una conexion a la base de datos.
                cl.Open();

                //usamos para la consulta la conexion creada
                sm.Connection = cl;
                //texto de comando para realiza consultas a la base de datos
                sm.CommandText = "select * from Alumnos";
                //ejecutamos el comando creando un objeto sqlreader con el metodo executereader
                SqlDataReader dr = sm.ExecuteReader(); //obtiene los datos

                //mientras pueda leer la proxima columna ingreso
                while (dr.Read())
                {
                    //int idAlumno,string nombre, string apellido, int dni,int edad,string domicilio, int responsable
                    //se crea un keyvaluepar cada vez que se va obteniendo una fila para agregar desde la base hasta el formulario
                    alumnos.Add(new Alumno(int.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), int.Parse(dr[3].ToString()), int.Parse(dr[4].ToString()),dr[5].ToString(), int.Parse(dr[6].ToString())));
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
            return this.alumnos;
        }

        public int EliminarAlumnosBase(int id)
        {
            int retorno = 0;

            try
            {
               

                sm.Parameters.Clear();
                cl.Open();

                sm.Connection = cl;

                //de nuestro combo provincias buscamos el item seleccionado, con selected item lo casteamos al tipo de dato que es
                //y como ese item tiene clave y valor elegimos la clave que es el id
                sm.CommandText = "delete from  Alumnos where idAlumnos = @id";

                sm.Parameters.Add(new SqlParameter("id", id));


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
    }
}

