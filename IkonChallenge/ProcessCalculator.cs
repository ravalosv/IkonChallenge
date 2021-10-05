using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IkonChallenge
{
    public class ProcessCalculator
    {
        public void Start()
        {
            List<string> procesosOptimos = new List<string>(); 

            FileManager file = new FileManager();
            (string mensaje, List<string> lineas) = file.ObtenerArchivoEntrada();

            if (string.IsNullOrEmpty(mensaje))
            {
                // Se van tomando lineas en grupos de 3
                for (int i = 0; i <= lineas.Count() - 1; i += 3)
                {
                    procesosOptimos.Add(
                        procesar(int.Parse(lineas[i]), lineas[i + 1], lineas[i + 2])
                        );
                }

                mensaje = file.EscribirArchivoSalida(procesosOptimos);
            }

            if (string.IsNullOrEmpty(mensaje))
                mensaje = "Processes executed successfully.";

            Console.WriteLine(mensaje);
        }

        // convierte una linea en formato (n,n), (n,n) a un dictionary 
        private Dictionary<int, int> obtenerListaTareas(string taskLine)
        {
            Dictionary<int, int> tasks = new Dictionary<int, int>();

            foreach (Match match in Regex.Matches(taskLine, RegexStringBuilder.agrupadorTareas(), RegexOptions.IgnoreCase))
            {
                tasks.Add(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
            }

            return tasks;
        }

        // Dada la capacidad máxima, encuentra la combinación de capacidades (foreground y background tasks) que mas se aproxime
        private string obtenerCombinacionDeTareasOptimas(int capacidadMaxima, Dictionary<int, int> foregroundTasks, Dictionary<int, int> backgroundTasks)
        {

            var crossTasks = foregroundTasks.Join(backgroundTasks, x => true, y => true, (f, b) => new { f, b })  // realizar cross join de las dos entradas
                        .Where(p => p.f.Value + p.b.Value <= capacidadMaxima);       // donde la suma de ambas capacidades no exceda la capacidad Maxima

            // Verificar si no existe algun posible resultado
            if (crossTasks.Count() == 0)
            {
                return "(,)";  // Si no existe ningun posible resultado
            }

            var capacidadMaximaAlcanzada = crossTasks.Max(p => p.f.Value + p.b.Value);       // De los procesos posibles obtener la capacidad maxima alcanzada 

            var candidatos = crossTasks.Where(p => p.f.Value + p.b.Value == capacidadMaximaAlcanzada);   // Obtener solo los procesos que alcancen la capacidad maxima posible

            var resultado = (from x in candidatos
                             select $"({x.f.Key}, {x.b.Key})").ToList();        // Generar cadena resultate en formato (foregroundID, backgrounID)

            return String.Join(", ", resultado);    // Juntar todos los resultados en una linea
        }

        private string procesar(int capacidadMaxima, string foregroundTasksLine, string backgroundTasksLine)
        {

            Dictionary<int, int> foregroundTasks = obtenerListaTareas(foregroundTasksLine);
            Dictionary<int, int> backgroundTasks = obtenerListaTareas(backgroundTasksLine);

            return obtenerCombinacionDeTareasOptimas(capacidadMaxima, foregroundTasks, backgroundTasks);

        }
    }
}
