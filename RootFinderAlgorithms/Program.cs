using System;

namespace RootFinderAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> funcA = x => 2 * Math.Pow(x, 3) - 11.7 * Math.Pow(x, 2) + 17.7 * x - 5;
            Func<double, double> derFuncA = x => 6 * Math.Pow(x, 2) - 23.4 * x + 17.7;
            var MathFunctionA = new MathFunction(funcA, derFuncA, "2x^3 – 11.7x^2 + 17.7x – 5");

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Equation: " + MathFunctionA.description);
            Console.ResetColor();

            Console.WriteLine("\nRoot: {0}\n", BisectionMethod(MathFunctionA, 0, 0.5, 0.01));
            Console.WriteLine("\nRoot: {0}\n", NewtonRaphsonMethod(MathFunctionA, 0.5, 0.01));
            Console.WriteLine("\nRoot: {0}\n", SecantMethod(MathFunctionA, 0, 0.5, 0.01));
        }
        static double BisectionMethod(MathFunction func, double startingA, double startingB, double maxError)
        {
            double a, b, c, error;
            a = startingA;
            b = startingB;
            c = 0;
            error = 0;

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Bisection Method\n");

            Console.WriteLine("a\tb\tc\tf(a)\tf(b)\tf(c)\terror");
            Console.ResetColor();

            do
            {
                double newC = (a + b) / 2;
                error = Math.Abs(a - (a + b) / 2) / Math.Abs((a + b) / 2);

                double funcA = func.Evaluate(a);
                double funcB = func.Evaluate(b);
                double funcC = func.Evaluate(newC);

                //guard
                if (funcA < 0 && funcB < 0 || funcA > 0 && funcB > 0)
                    return -1; //error

                Console.WriteLine("{0:0.0000}\t{1:0.0000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t{5:0.0000}\t{6:0.0000}", a, b, c, funcA, funcB, funcC, error);

                c = newC;
                if (funcA < 0 && funcC < 0)
                    a = newC;
                else if (funcB < 0 && funcC < 0)
                    b = newC;
                else if (funcA > 0 && funcC > 0)
                    a = newC;
                else if (funcB > 0 && funcC > 0)
                    b = newC;
                else
                    return -1; //error
            } while (error > maxError);

            return c;
        }
        static double NewtonRaphsonMethod(MathFunction func, double startingX, double maxError)
        {
            double x, error;
            x = startingX;
            error = 0;

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Newton Raphson Method Method\n");
            Console.WriteLine("x\tf(x)\tf(x+)\terror");
            Console.ResetColor();

            do
            {
                double function = func.Evaluate(x);
                double functionDerrived = func.EvaluateDerivative(x);

                error = Math.Abs(function / functionDerrived);

                Console.WriteLine("{0:0.00}\t{1:0.00}\t{2:0.00}\t{3:0.00}", x, function, functionDerrived, error);

                x = x - (function / functionDerrived);
            } while (error > maxError);

            return x;
        }
        static double SecantMethod(MathFunction func, double startingX0, double startingX1, double maxError)
        {
            double x0 = startingX0;
            double x1 = startingX1;
            double error = 0;

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Secant Method\n");
            Console.WriteLine("x_n-1\tx_n\tx_n+1\tf(x_n-1)\tf(x_n)\tf(x_n+1)\terror");
            Console.ResetColor();

            do
            {
                double xPlusOne = x1 - ((x1 - x0) / (func.Evaluate(x1) - func.Evaluate(x0))) * func.Evaluate(x1);
                error = Math.Abs((xPlusOne - x1) / xPlusOne);

                Console.WriteLine("{0:0.0000}\t{1:0.0000}\t{2:0.0000}\t{3:0.0000}\t\t{4:0.0000}\t{5:0.0000}\t\t{6:0.0000}", x0, x1, xPlusOne, func.Evaluate(x0), func.Evaluate(x1), func.Evaluate(xPlusOne), error);
                x0 = x1;
                x1 = xPlusOne;
            } while (error > maxError);

            return (x0 + x1)/2;
        }
    }
}
