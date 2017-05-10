using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-----------------------------
using System.IO;

namespace NHJ.Examen2eRec
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.Title = "EXAMEN DE RECUPERACION";
            Menu();
        }

        static void Menu()
        {
           
            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            do
            {
                Console.Clear();
                Console.WriteLine("\n\t\tMENU");
                
                Console.WriteLine("A. Cuenta palabras.");
                Console.WriteLine("B. Calculas los días vividos.");
                Console.WriteLine("C. Suma de potencia de tres.");
                Console.WriteLine("D. Obtener cambio.");
                Console.WriteLine("\nEscape. Salir del programa.");
                Console.Write("\nElige una opción: ");
                tecla = Console.ReadKey();
                switch (tecla.Key)
                {
                    case ConsoleKey.A:
                        MenuCuentaPalabras();
                        break;
                    case ConsoleKey.B:
                        MenuDiasVividos();
                        break;
                    case ConsoleKey.C:
                        MenuPotencias();
                        break;
                    case ConsoleKey.D:
                        MenuCambio();
                        break;
                    default:
                        break;
                }
            } while (tecla.Key != ConsoleKey.Escape);
            if (tecla.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\nHas pulsado Escape, ¿estás seguro de que quieres salir?");
                Console.Write("[S/N]: ");
                tecla = Console.ReadKey();
                if (ConsoleKey.S != tecla.Key)
                {
                    Menu();
                }
            }
        }

        static void MenuCuentaPalabras()
        {
            string ruta = @"c:\basura\fichero.txt";
            const int TAMANIO = 100;
            string[] palabras = new string[TAMANIO];
            int nPalabras = 0;

            Console.Clear();
            if (!File.Exists(ruta))
            {
                FileStream flujoNuevo = new FileStream(ruta, FileMode.Append, FileAccess.Write);
                flujoNuevo.Close();
            }

            using(FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
	        {
                StreamReader fichero = new StreamReader(fs);
                int caracter = 0;
                while ((caracter=fichero.Peek())!= -1)
                {
                    if (Char.IsLetterOrDigit((char)caracter))
                    {
                        palabras[nPalabras] += (char)fichero.Read();
	                }
                    else
                    {
                        fichero.Read();
                        if (Char.IsLetterOrDigit((char)fichero.Peek()))
                        {
                            nPalabras++;
                        }
                    }
                    
                }
	        }

            if (palabras != null && palabras.Length > 0)
            {
                string candidata = string.Empty;
                int nVeces = 0;
                for (int i = 0; i <= nPalabras; i++)
                {
                    if (candidata.Length < palabras[i].Length)
                    {
                        candidata = palabras[i];
                        nVeces = 1;
                    }
                    else if (candidata.ToUpper().Equals(palabras[i].ToUpper()))
                    {
                        nVeces++;
                    }
                }
                Console.WriteLine("\n\tCUENTA PALABRAS\n");
                Console.WriteLine("El número total de palabras es: {0}", nPalabras+1);
                Console.WriteLine("La palabra más larga es {0} y tiene una longitud de {1} caracteres", candidata, candidata.Length);
                Console.WriteLine("{0} aparece {1} veces", candidata, nVeces); 
            }
            else
            {
                Console.WriteLine("El fichero está vacío...");
            }
            Salida();
        }

        static void MenuDiasVividos()
        {
            const int  ANIOMAX = 2015;
            const int ANIOMIN = 1900;
            bool error = false;
            int dia;
            int mes;
            int anio;
            DateTime fechaActual = DateTime.Now;
            DateTime fecha;
            TimeSpan intervaloTiempo = new TimeSpan();
            

            Console.Clear();
            try
            {
                Console.WriteLine("\n\tCALCULO DIAS VIVIDOS");
                do
                {
                    Console.WriteLine("Introduce el día del año:");
                    error = int.TryParse(Console.ReadLine(), out dia);
                } while (!error || dia < 1 || dia > 31);
                do
                {
                    Console.WriteLine("Introduce el número del mes:");
                    error = int.TryParse(Console.ReadLine(), out mes);
                } while (!error || mes < 1 || mes > 12);
                do
                {
                    Console.WriteLine("Introduce el número del año:");
                    error = int.TryParse(Console.ReadLine(), out anio);
                    if (anio <= ANIOMIN || anio >= ANIOMAX)
                    {
                        Console.WriteLine("El año tiene que ser mayor de 1990 y menor que 2015...");
                    }
                } while (!error || anio <= ANIOMIN || anio >= ANIOMAX);

                fecha = new DateTime(anio, mes, dia);
                intervaloTiempo = fechaActual.Date - fecha.Date;
                Console.WriteLine("La persona nacida el {0} ha vivido {1} días.", fecha.ToShortDateString(), intervaloTiempo.Days);
                Salida();
            }
            catch (Exception)
            {

                Console.WriteLine("Fecha intoducida no válida...");
                Salida();
            }
        }

        static void MenuPotencias()
        {

            Console.Clear();
            int[] alea = CrearArrayAlea();
            Console.WriteLine("\n\tSUMA DE POTENCIA DE TRES");
            Console.WriteLine("La suma del cubo de sus cifras da el número.");
            Console.WriteLine("Lista de los números en el array aleatorio:");
            for (int i = 0; i < alea.Length; i++)
            {
                if (EsPotenciaTres(alea[i]))
                {
                    Console.WriteLine("array[{0}]: {1}", i, alea[i]);
                }
            }
            
            Salida();
        }


        static bool EsPotenciaTres(int num)
        {
            int centena = 0;
            int decena = 0;
            int unidad = 0;
            centena = num / 100;
            decena = (num -(centena *100))/10;
            unidad = num -(centena*100)-(decena *10);
            if (num == Math.Pow(centena,3) + Math.Pow(decena,3) + Math.Pow(unidad,3))
            {
                return true;
            }
            else
            {
                return false;
            }  
        }

        static int[] CrearArrayAlea() 
        {
            const int TAMANIO = 1000;
            int[] array = new int[TAMANIO];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(100, 999);
            }
            return array;
        }

        static void MenuCambio()
        {
            double precio = 0;
            double cantidad = 0;
            double billete = -1;
            double cambio = 0;
            double nBillete = 1;
            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            bool error = false;
            Console.Clear();

                Console.WriteLine("\n\tMAQUINA DE BOLLERIA");
                Console.WriteLine("¿Cuánto tiene que pagar? (Introduce una cantidad):");
                do
                {
                    error = double.TryParse(Console.ReadLine(), out precio);
                    if (precio < 0)
                    {
                        error = false;
                        Console.WriteLine("Tiene que ser mayor de 0...");
                    }
                    if (error == false)
                    {
                        Console.WriteLine("Cantidad introducida no válida...");
                        Console.WriteLine("Introduce otra cantidad:");
                    }
                } while (!error);
                do
                {
                    Console.WriteLine("Un bollo cuesta {0} euros, puede pagar con billetes de 5, 10 o 20:", precio);
                    Console.WriteLine("A) 5 euros.");
                    Console.WriteLine("B) 10 euros.");
                    Console.WriteLine("C) 20 euros.");
                    Console.Write("Elige una opción: ");
                    tecla = Console.ReadKey();
                    switch (tecla.Key)
                    {
                        case ConsoleKey.A:
                            billete = 5;
                            break;
                        case ConsoleKey.B:
                            billete = 10;
                            break;
                        case ConsoleKey.C:
                            billete = 20;
                            break;
                    }
                } while (billete == -1);
                cantidad = precio;
                while (cantidad > billete)
                {
                    cantidad -= billete;
                    nBillete++;
                }

            cambio = billete-cantidad;
            Console.WriteLine("\n\nHas pagado con {2} billete(s) de {0} euros el cambio son {1} euros", billete, cambio, nBillete);

            int moneda2E = 0;
            int moneda1E = 0;
            int moneda50C = 0;
            int moneda20C = 0;
            int moneda10C = 0;
            bool noCambio = false;


            while (cambio > 0)
            {
                if (  cambio >= 2)
                {
                    cambio -= 2.0;
                    moneda2E++;
                }
                else if (cambio >= 1)
                {
                    cambio -= 1.0;
                    moneda1E++;
                }
                else if (cambio >= 0.5)
                {
                    cambio -= 0.5;
                    moneda50C++;
                }
                else if (cambio>= 0.2)
                {
                    cambio -= 0.2;
                    moneda20C++;
                }
                else if (cambio >= 0.1)
                {
                    cambio-= 0.1;
                    moneda10C++;
                }
                else if(cambio > 0.009)
                {
                    noCambio = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (noCambio)
            {
                Console.WriteLine("No hay cambio exacto...");
            }
            else
            {
                Console.WriteLine("{0} monedas de 2 euros", moneda2E);
                Console.WriteLine("{0} monedas de 1 euro", moneda1E);
                Console.WriteLine("{0} monedas de 0.5 euros", moneda50C);
                Console.WriteLine("{0} monedas de 0.2 euros", moneda20C);
                Console.WriteLine("{0} monedas de 0.1 euros", moneda10C);
            }
            Salida();
        }

        static void Salida()
        {
            Console.Write("\nPulse cualquier tecla para volver al menú: ");
            Console.ReadKey();
        }
    }
}
