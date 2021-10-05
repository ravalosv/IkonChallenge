using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkonChallenge
{

    public class RegexStringBuilder
    {
        public static string validadorArchivo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("^(");                // Verificar desde el inicio del archivo
            sb.Append(@"[\s\n]*");       // puede comenzar con espacio o nuevas lineas
            sb.Append(@"\d+[\s\n]*");   // seguido de una linea con solo numeros
            sb.Append(@"((\((\d+[ ]*,[ ]*\d+)\)[ ]*[,]?[ ]*)+([ ]*[\n]+)*){2}");   // luego dos lineas con formato (n, n), (n, n) 
            sb.Append(")+$");   // hasta el final del archivo

            return sb.ToString();
        }

        public static string agrupadorTareas()
        {
            return @"(\((\d+),\s*(\d+)\))";
        }
    }
}
