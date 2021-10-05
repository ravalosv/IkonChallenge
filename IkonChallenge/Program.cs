using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IkonChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessCalculator proceso = new ProcessCalculator();
            proceso.Start();

            Console.ReadLine();
        }
    }
}
