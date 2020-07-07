using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardin_Entidades
{
    public static class Extension
    {
        public static string MostrarNotaFinal(this Alumno alumno)
        {
            StringBuilder datos = new StringBuilder();

            datos.AppendLine($"ID: {alumno.Id}");
            datos.AppendLine($"NOMBRE:{alumno.Nombre}");
            datos.AppendLine($"APELLIDO{alumno.Apellido}");
            datos.AppendLine($"DNI: {alumno.Edad}");
            datos.AppendLine($"EDAD: {alumno.Dni}");
            datos.AppendLine($"DIRECCION:{alumno.Direccion}");
            datos.AppendLine($"RESPONSABLE:{alumno.Responsable}");
            datos.AppendLine($"NOTA FINAL:{alumno.NotaFinal}");

            return datos.ToString();

        }
    }
}
