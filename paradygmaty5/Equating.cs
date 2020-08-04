using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text;

namespace paradygmaty5
{
    class Equating
    {
        public void showEquation(ref int[] tab)
        {
            if (tab[0] == 0 && tab[1] == 0 && tab[2] == 0)
            {
                Console.Write("0=0 \n");
            }
            else if (tab[1] < 0 || tab[2] < 0)
            {
                Console.Write("{0}x^4 ", tab[0]);

                if (tab[1] < 0)
                {
                    Console.Write("+({0})x^2 ", tab[1]);
                }
                else
                {
                    Console.Write("+ {0}x^2 ", tab[1]);
                }

                if (tab[2] < 0)
                {
                    Console.Write("+({0}) = 0 \n\n", tab[2]);
                }
                else
                {
                    Console.Write("+ {0} = 0 \n\n", tab[2]);
                }
            }
            else
            {
                Console.Write("{0}x^4 + {1}x^2 + {2} = 0 \n\n", tab[0], tab[1], tab[2]);
            }
        }

        public double calculateDelta(ref int[] tab)
        {
            double delta = Math.Pow(tab[1], 2) - (4 * tab[0] * tab[2]);
            Console.Write("delta = {0}\n", delta);
            return delta;
        }

        public void calculateRoots(ref int[] tab, ref double err, ref double delta, ref List<Complex> results)
        {
            Square pier;
            if (err == 0)
                pier = new squareNormal();
            else if (err > 0.1)
                pier = new squareNewton(err);
            else
                pier = new squareHeron(err);

            if (tab[0] == 0 || delta == 0)
            {
                if (results[0].Real >= 0)
                {
                    double x1r = pier.calculateSquare(results[0].Real);
                    double x2r = -(x1r);
                    results[0] = new Complex(x1r, 0);
                    results[1] = new Complex(x2r, 0);

                }
                else
                {
                    double x1u = pier.calculateSquare(Math.Abs(results[0].Real));
                    double x2u = -(x1u);

                    results[0] = new Complex(0, x1u);
                    results[1] = new Complex(0, x2u);
                }

            }
            else
            {
                if (delta > 0)
                {
                    if (results[0].Real >= 0)
                    {
                        double x1r = pier.calculateSquare(results[0].Real);
                        double x3r = -(x1r);
                        results[0] = new Complex(x1r, 0);
                        results[2] = new Complex(x3r, 0);
                    }
                    else
                    {
                        double x1u = pier.calculateSquare(Math.Abs(results[0].Real));
                        double x3u = -(x1u);
                        results[0] = new Complex(0, x1u);
                        results[2] = new Complex(0, x3u);
                    }

                    if (results[1].Real >= 0)
                    {
                        double x2r = pier.calculateSquare(results[1].Real);
                        double x4r = -(x2r);
                        results[1] = new Complex(x2r, 0);
                        results[3] = new Complex(x4r, 0);
                    }
                    else
                    {
                        double x2u = pier.calculateSquare(Math.Abs(results[1].Real));
                        double x4u = -(x2u);
                        results[1] = new Complex(0, x2u);
                        results[3] = new Complex(0, x4u);
                    }
                }
                else if (delta < 0)
                {
                    double tmp = results[0].Real;
                    double x1r = pier.calculateSquare((pier.calculateSquare(Math.Pow(results[0].Real, 2) + Math.Pow(results[0].Imaginary, 2)) + results[0].Real) / 2);
                    double x1u = -pier.calculateSquare((pier.calculateSquare(Math.Pow(tmp, 2) + Math.Pow(results[0].Imaginary, 2)) - tmp) / 2);
                    double x3r = -(x1r);
                    double x3u = -(x1u);

                    double tmp2 = results[1].Real;
                    double x2r = pier.calculateSquare((pier.calculateSquare(Math.Pow(results[1].Real, 2) + Math.Pow(results[1].Imaginary, 2)) + results[1].Real) / 2);
                    double x2u = pier.calculateSquare((pier.calculateSquare(Math.Pow(tmp2, 2) + Math.Pow(results[1].Imaginary, 2)) - tmp2) / 2);
                    double x4r = -(x2r);
                    double x4u = -(x2u);

                    results[0] = new Complex(x1r, x1u);
                    results[1] = new Complex(x2r, x2u);
                    results[2] = new Complex(x3r, x3u);
                    results[3] = new Complex(x4r, x4u);

                }
            }
        }

        public void showResults(ref int[] tab, ref double delta, ref List<Complex> results, ref double sr, ref double su, ref double rr, ref double ru)
        {
            if (tab[0] == 0 || delta == 0)
            {
                if (results[0].Real >= 0)
                {
                    Console.Write("\n1: {0}  przeciwna: {1}  odwrotna: {2}\n", results[0].Real, -results[0].Real, 1 / results[0].Real);
                    Console.Write("\n2: {0}  przeciwna: {1}  odwrotna: {2}\n", results[1].Real, -results[1].Real, 1 / results[1].Real);
                }
                else
                {
                    Console.Write("\n1: {0} i    przeciwna: {1} i  odwrotna: {2} i\n", results[0].Imaginary, -results[0].Imaginary, 1 / results[0].Imaginary);
                    Console.Write("\n2: {0} i    przeciwna: {1} i  odwrotna: {2} i\n", results[1].Imaginary, -results[1].Imaginary, 1 / results[1].Imaginary);
                }
                addRoots(ref results, ref sr, ref su);
                subRoots(ref results, ref rr, ref ru);
            }
            else
            {
                if (delta > 0)
                {
                    if (results[0].Imaginary == 0)
                    {
                        Console.Write("\n1: {0}  przeciwna: {1}  odwrotna: {2}\n", results[0].Real, -results[0].Real, 1 / results[0].Real);
                        Console.Write("\n2: {0}  przeciwna: {1}  odwrotna: {2}\n", results[2].Real, -results[2].Real, 1 / results[2].Real);
                    }
                    else
                    {
                        Console.Write("\n1: {0} i    przeciwna: {1} i  odwrotna: {2} i\n", results[0].Imaginary, -results[0].Imaginary, 1 / results[0].Imaginary);
                        Console.Write("\n2: {0} i    przeciwna: {1} i  odwrotna: {2} i\n", results[2].Imaginary, -results[2].Imaginary, 1 / results[2].Imaginary);
                    }

                    if (results[1].Imaginary == 0)
                    {
                        Console.Write("\n3: {0}  przeciwna: {1}  odwrotna: {2}\n", results[1].Real, -results[1].Real, 1 / results[1].Real);
                        Console.Write("\n4: {0}  przeciwna: {1}  odwrotna: {2}\n", results[3].Real, -results[3].Real, 1 / results[3].Real);
                    }
                    else
                    {
                        Console.Write("\n3: {0} i    przeciwna: {1} i  odwrotna: {2} i\n", results[1].Imaginary, -results[1].Imaginary, 1 / results[1].Imaginary);
                        Console.Write("\n4: {0} i    przeciwna: {1} i  odwrotna: {2} i\n", results[3].Imaginary, -results[3].Imaginary, 1 / results[3].Imaginary);
                    }

                    addRoots(ref results, ref sr, ref su);
                    subRoots(ref results, ref rr, ref ru);
                }
                else if (delta < 0)
                {
                    Console.Write("1: {0}", results[0].Real);
                    if (results[0].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i   ", results[0].Imaginary);
                    }
                    else
                    {
                        Console.Write("+ {0} * i    ", results[0].Imaginary);
                    }

                    Console.Write("przeciwna: {0}", -results[0].Real);
                    if (-results[0].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i   ", -results[0].Imaginary);
                    }
                    else
                    {
                        Console.Write("+ {0} * i    ", -results[0].Imaginary);
                    }

                    Console.Write("odwrotna: {0}", 1/(results[0].Real));
                    if (results[0].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i\n", 1/(results[0].Imaginary));
                    }
                    else
                    {
                        Console.Write("+ {0} * i\n", 1/(results[0].Imaginary));
                    }

                    Console.Write("2: {0}", results[1].Real);
                    if (results[1].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i   ", results[1].Imaginary);
                    }
                    else
                    {
                        Console.Write("+ {0} * i    ", results[1].Imaginary);
                    }

                    Console.Write("przeciwna: {0}", -results[1].Real);
                    if (-results[1].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i   ", -results[1].Imaginary);
                    }
                    else
                    {
                        Console.Write("+ {0} * i    ", -results[1].Imaginary);
                    }

                    Console.Write("odwrotna: {0}", 1 / (results[1].Real));
                    if (results[1].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i\n", 1 / (results[1].Imaginary));
                    }
                    else
                    {
                        Console.Write("+ {0} * i\n", 1 / (results[1].Imaginary));
                    }

                    Console.Write("3: {0}", results[2].Real);
                    if (results[2].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i   ", results[2].Imaginary);
                    }
                    else
                    {
                        Console.Write("+ {0} * i    ", results[2].Imaginary);
                    }

                    Console.Write("przeciwna: {0}", -results[2].Real);
                    if (-results[2].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i   ", -results[2].Imaginary);
                    }
                    else
                    {
                        Console.Write("+ {0} * i    ", -results[2].Imaginary);
                    }

                    Console.Write("odwrotna: {0}", 1 / (results[2].Real));
                    if (results[2].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i\n", 1 / (results[2].Imaginary));
                    }
                    else
                    {
                        Console.Write("+ {0} * i\n", 1 / (results[2].Imaginary));
                    }

                    Console.Write("4: {0}", results[3].Real);
                    if (results[3].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i   ", results[3].Imaginary);
                    }
                    else
                    {
                        Console.Write("+ {0} * i    ", results[3].Imaginary);
                    }

                    Console.Write("przeciwna: {0}", -results[3].Real);
                    if (-results[3].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i   ", -results[3].Imaginary);
                    }
                    else
                    {
                        Console.Write("+ {0} * i    ", -results[3].Imaginary);
                    }

                    Console.Write("odwrotna: {0}", 1 / (results[3].Real));
                    if (results[3].Imaginary < 0)
                    {
                        Console.Write("+({0}) * i\n", 1 / (results[3].Imaginary));
                    }
                    else
                    {
                        Console.Write("+ {0} * i\n", 1 / (results[3].Imaginary));
                    }
                    addRoots(ref results, ref sr, ref su);
                    subRoots(ref results, ref rr, ref ru);
                }
            }
        }

        private void addRoots(ref List<Complex> results, ref double sr, ref double su)
        {
            Complex sum = results[0] + results[1] + results[2] + results[3];
            sr = sum.Real;
            Console.Write("\n\nsr: {0} \n", sr);

            if (results[0].Imaginary != 0 || results[1].Imaginary != 0 || results[2].Imaginary != 0 || results[3].Imaginary != 0)
            {
                su = sum.Imaginary;
                Console.Write("su: {0} \n", su);
            }
        }

        private void subRoots(ref List<Complex> results, ref double rr, ref double ru)
        {
            Complex sub = results[0] - results[1] - results[2] - results[3];
            rr = sub.Real;
            Console.Write("rr: {0} \n", rr);

            if (results[0].Imaginary != 0 || results[1].Imaginary != 0 || results[2].Imaginary != 0 || results[3].Imaginary != 0)
            {
                ru = sub.Imaginary;
                Console.Write("ru: {0} \n", ru);
            }
        }

    }
}
