using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace paradygmaty5
{
    public abstract class Square
    {
        public abstract double calculateSquare(double number);
    }

    public class squareNewton: Square 
    {
        private double err;

        public squareNewton(double err)
        {
            this.err = err;
        }

        public override double calculateSquare(double number)
        {
            double a = 1;
            double b = number;

            for (;;)
            {
                if (Math.Abs(a - b) < err) { break; }
                b = (a + b) / 2;
                a = number / b;
            }
            return a;
        }
    }

    public class squareHeron : Square {
        private double err;
        public squareHeron(double err)
        {
            this.err = err;
        }
        public override double calculateSquare(double number)
        {
            double a = number / 2;
            double b = 1;

            for (; ; )
            {
                if (Math.Abs(a - b) < err) { break; }
                b = (a + (number / a)) / 2;
                a = number / b;
            }
            return a;
        }
    }

    class squareNormal : Square {
	    public override double calculateSquare(double number) {
            return Math.Sqrt(number);
        }
    }
}
