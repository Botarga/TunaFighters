using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TunaFightersShellClient
{
    class Program
    {
        public const string USER_NAME = "botarga";

        private static void PrintMenu(string key)
        {
            switch(key)
            {
                case "main":
                    Console.WriteLine("1. Jugar contra CPU.");
                    Console.WriteLine("2. Jugar online.");
                    Console.WriteLine("3. Salir");
                    break;
            }
            Console.Write("Elige una opcion: ");
        }

        public static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                PrintMenu("main");
                var opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        break;

                    case "2":
                        OnlineMatch om = new OnlineMatch();
                        om.Start();
                        break;
                    case "3":
                        exit = true;
                        break;
                }
            }
        }

    }
}
