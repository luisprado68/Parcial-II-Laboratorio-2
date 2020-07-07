using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Jardin_Entidades
{
    public class ManejadorSqlEvaluacion
    {
        SqlConnection cl;
        SqlCommand sm;
        Evaluaciones evaluacion;

        public ManejadorSqlEvaluacion()
        {
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
                sm.CommandText = "select * from Evaluaciones";
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

        public int Guardar(int idAlumno, int idDocente, int idAula, int nota_1, int nota_2, float notaFinal, string observaciones)
        {
            int retorno = 0;


            this.evaluacion = new Evaluaciones( idAlumno, idDocente, idAula, nota_1, nota_2, notaFinal, observaciones);

            try
            {
                //limpiamos localidades.

                sm.Parameters.Clear();
                cl.Open();


                sm.Connection = cl;

                //de nuestro combo provincias buscamos el item seleccionado, con selected item lo casteamos al tipo de dato que es
                //y como ese item tiene clave y valor elegimos la clave que es el id
                sm.CommandText = "insert into Evaluaciones (idAlumno, idDocente, idAula, nota_1, nota_2, notaFinal, observaciones) VALUES  (@idAlumno, @idDocente, @idAula, @nota1, @nota2, @notaFinal, @observaciones)";

                sm.Parameters.Add(new SqlParameter("idAlumno", evaluacion.IdAlumno));
                sm.Parameters.Add(new SqlParameter("idDocente", evaluacion.IdDocente));
                sm.Parameters.Add(new SqlParameter("idAula", evaluacion.IdAula));
                sm.Parameters.Add(new SqlParameter("nota1", evaluacion.Nota_1));
                sm.Parameters.Add(new SqlParameter("nota2", evaluacion.Nota_2));
                sm.Parameters.Add(new SqlParameter("notaFinal", evaluacion.NotaFinal));
                sm.Parameters.Add(new SqlParameter("observaciones", evaluacion.Observaciones));



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
