using System;

namespace Mandelbrot
{
    class Mandelbrot
    {
        const string zeichenvorrat = " Programmieren!";
        // ... auch hübsch:
        //const string zeichenvorrat = "\u263b                  ";

        const int breite = 95;
        const int hoehe = 30;
        const double minX = -2;
        const double maxX = 0.8;
        const double minY = -1.5;
        const double maxY = 1.5;
        const double xSchritt = (maxX - minX) / breite;
        const double ySchritt = (maxY - minY) / hoehe;

        /// <summary>
        /// Berechnung der Folge z = z*z + c
        /// </summary>
        /// <param name="zeile">Zeile</param>
        /// <param name="spalte">Spalte</param>
        /// <returns>0, falls c Element der Mandelbrot-Menge; sonst: Anzahl der Iterationen, bis |z| > 2</returns>
        static int Mandel(int zeile, int spalte)
        {
            // Die entsprechende komplexe Zahl c:
            double x = minX + spalte * xSchritt;
            double y = minY + zeile * ySchritt;
            System.Numerics.Complex c = new System.Numerics.Complex(x, y);

            // Folge berechnen:
            System.Numerics.Complex z = c;
            for (int i = 0; i < zeichenvorrat.Length; i++)
            {
                if (z.Magnitude > 2)
                {
                    // Die Folge ist nicht beschränkt
                    // => c ist nicht Element der Mandelbrot-Menge
                    return i;
                }
                z = z * z + c;
            }
            // Die Folge ist beschränkt (|z| bleibt <= 2)
            // => c ist Element der Mandelbrot-Menge
            return 0;
        }

        static void Print(char[,] feld)   //char-Feld als Parameter
        {
            for (int i = 0; i < feld.GetLength(0); i++)
            {
                for (int j = 0; j < feld.GetLength(1); j++)
                {
                    Console.Write(feld[i, j]);    //verschachtelte for-Schleife gibt jeden Wert für [zeile, spalte] zeilenweise aus
                }
                Console.WriteLine();
            }
        }

        static char[,] Mirror(char[,] feld)
        {
            char[,] result = new char[feld.GetLength(0), feld.GetLength(1)];

            for (int i = 0; i < feld.GetLength(0); i++)
            {
                for (int j = feld.GetLength(1) - 1, x = 0; j >= 0 && x < 95; j--, x++) //
                {
                    result[i, x] = feld[i, j];
                }
            }

            return result;
        }
        static char[,] Scroll(char[,] feld)
        {
            char[,] result = new char[feld.GetLength(0), feld.GetLength(1)];
            return result;

        }
        static void Main(string[] args)
        {
            char[,] hoeheBreite = new char[hoehe, breite]; // 2-D char-Feld der Größe hoehe x breite
            for (int i = 0; i < hoehe; i++)
            {
                for (int j = 0; j < breite; j++)
                {
                    hoeheBreite[i, j] = (char)Mandel(i, j); //verschachtelte for-Schleife füllt Array
                }
            }

            var a1 = hoeheBreite;
            var a2 = Mirror(hoeheBreite);

            Print(hoeheBreite);
            Console.WriteLine();
            Print(Mirror(hoeheBreite));
            Console.ReadKey();
        }
    }
}

