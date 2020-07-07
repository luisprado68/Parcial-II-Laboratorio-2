using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jardin_Entidades;
using System.Collections.Generic;

namespace Test_Unitarios
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PruebaSerializarBinario()
        {
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AlumnosTest.bin";
            //Arrange
            List<Alumno> alumnos = new List<Alumno>();

            alumnos.Add(new Alumno(1, "Luis", "Prado", 24, 39207770, "Juncal 2925", 5));
            alumnos.Add(new Alumno(2, "Luis", "Prado", 24, 39207770, "Juncal 2925", 5));
            alumnos.Add(new Alumno(3, "Luis", "Prado", 24, 39207770, "Juncal 2925", 5));
            alumnos.Add(new Alumno(4, "Luis", "Prado", 24, 39207770, "Juncal 2925", 5));
            alumnos.Add(new Alumno(5, "Luis", "Prado", 24, 39207770, "Juncal 2925", 5));

            IArchivo<List<Alumno>> archivo = new ArchivoBinario<List<Alumno>>();
            bool guardo = false;

            //Act

            guardo = archivo.GuardarArchivo(ruta, alumnos);

            //Assert
            Assert.IsTrue(guardo);
        }

        [TestMethod]
        public void PruebaDeserializarBinario()
        {
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AlumnosTest.bin";
            //Arrange
            IArchivo<List<Alumno>> archivo = new ArchivoBinario<List<Alumno>>();
            bool leyo = false;

            //Act
            leyo = archivo.LeerArchivo(ruta, out List<Alumno> alumnos);

            //Assert
            Assert.IsTrue(leyo);
        }
    }
}

