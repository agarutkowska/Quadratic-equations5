using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace paradygmaty5
{
    class Program
    {
        static void equating()
        {
            int[] tab = new int[3];

            List<Complex> results = new List<Complex>();
            Complex x1 = new Complex();
            Complex x2 = new Complex();
            Complex x3 = new Complex();
            Complex x4 = new Complex();
            results.Add(x1);
            results.Add(x2);
            results.Add(x3);
            results.Add(x4);

            double sr = 0, su = 0, rr = 0, ru = 0;
            double err, pdelta;

            Console.Write("Podaj a: ");
            tab[0] = int.Parse(Console.ReadLine());
            Console.Write("Podaj b: ");
            tab[1] = int.Parse(Console.ReadLine());
            Console.Write("Podaj c: ");
            tab[2] = int.Parse(Console.ReadLine());

            Equating equ = new Equating();
            equ.showEquation(ref tab);
            double delta = 0;

            Console.Write("Podaj err: ");
            err = double.Parse(Console.ReadLine());

            Square pier;
            if (err == 0)
                pier = new squareNormal();
            else if (err > 0.1)
                pier = new squareNewton(err);
            else
                pier = new squareHeron(err);

            if (tab[0] == 0)
            {
                if (tab[1] != 0)
                {
                    double x1r = (-tab[2]*1.0 / tab[1]) * (1.0);
                    results[0] = new Complex(x1r, 0); ;
                    equ.calculateRoots(ref tab, ref err, ref delta, ref results);
                    equ.showResults(ref tab, ref delta, ref results, ref sr, ref su, ref rr, ref ru);
                }
                if (tab[1] == 0 && tab[2] != 0)
                {
                    Console.Write("Rownanie sprzeczne. \n");
                }
                if (tab[1] == 0 && tab[2] == 0)
                {
                    Console.Write("Rownanie tozsamosciowe. \n");
                }
            }
            else
            {
                delta = equ.calculateDelta(ref tab);
                if (delta > 0)
                {
                    pdelta = pier.calculateSquare(delta);

                    double x1r = ((-tab[1] - pdelta) / (2.0 * tab[0])) * (1.0);
                    double x2r = ((-tab[1] + pdelta) / (2.0 * tab[0])) * (1.0);

                    results[0] = new Complex(x1r, 0); ;
                    results[1] = new Complex(x2r, 0); ;

                    Console.Write("x1r = {0}\nx2r = {1}\n", x1r, x2r);

                    equ.calculateRoots(ref tab, ref err, ref delta, ref results);
                    equ.showResults(ref tab, ref delta, ref results, ref sr, ref su, ref rr, ref ru);
                }
                else if (delta < 0)
                {
                    pdelta = pier.calculateSquare(Math.Abs(delta));

                    double x1r = ((-tab[1]) / (2.0 * tab[0])) * 1.0;
                    double x2r = x1r;

                    double x1u = ((-pdelta) / (2.0 * tab[0])) * 1.0;
                    double x2u = -x1u;

                    results[0] = new Complex(x1r, x1u);
                    results[1] = new Complex(x2r, x2u);

                    equ.calculateRoots(ref tab, ref err, ref delta, ref results);
                    equ.showResults(ref tab, ref delta, ref results, ref sr, ref su, ref rr, ref ru);
                }
                else if (delta == 0)
                {
                    if (tab[0] == 0 && tab[1] == 0)
                    {
                        Console.Write("Nie ma rozwiazan.\n");
                    }
                    else
                    {
                        double x1r = ((-tab[1]) / (2.0 * tab[0])) * (1.0);
                        equ.calculateRoots(ref tab, ref err, ref delta, ref results);
                        equ.showResults(ref tab, ref delta, ref results, ref sr, ref su, ref rr, ref ru);
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            equating();
            Console.ReadKey();
        }
    }
}
