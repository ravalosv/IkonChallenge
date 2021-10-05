using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace IkonChallenge
{
    public class FileManager
    {

        string inputFileName = "challenge.in";
        string outputFileName = "challenge.out";

        // Abre el archivo de entrada, lee sus lineas, las valida y regresa un listado con las lineas leidas
        public (string mensaje, List<string> lineas) ObtenerArchivoEntrada()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string inputFullFileName = System.IO.Path.Combine(appPath, inputFileName);

            try
            {
                if (!File.Exists(inputFullFileName))
                    return ($"Error: No se encuentra el archivo {inputFileName}", null);

                using (var inputFile = File.OpenText(inputFileName))
                {
                    var contenido = inputFile.ReadToEnd();
                    //

                    (bool valido, string mensaje) = validarArchivoEntrada(contenido);

                    if (!valido)
                        return ($"Error: {mensaje}", null);
                    else
                    {
                        // regresar solo lineas no vacias 
                        var lineas = contenido.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None).Where(p => p.Trim() != "");
                        return ("", lineas.ToList());
                    }
                }
            } catch(Exception ex)
            {
                return ($"Error: {ex.Message}", null);
            }

        }

        public string EscribirArchivoSalida(List<string> lineas)
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string outputFullFileName = System.IO.Path.Combine(appPath, outputFileName);

            try
            {
                using (var file = File.CreateText(outputFullFileName))
                {
                    file.Write(String.Join("\n", lineas));
                }

                return "";
            }
            catch (Exception ex)
            {
                return ($"Error al escribir archivo de salida: {ex.Message}");
            }
        }

        private (bool valido, string mensaje) validarArchivoEntrada(string contenido)
        {
            contenido = contenido.Replace("\r", "");    // quitar los retorno de carro

            Regex rgx = new Regex(RegexStringBuilder.validadorArchivo()); ;

            var isMatch = rgx.IsMatch(contenido);

            if (!isMatch)
                return (false, "El contenido del archivo de entrada es inválido");

            return (true, "");
            
        }

    }
}
