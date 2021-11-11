using System;
namespace RootFinderAlgorithms
{
    public class MathFunction
    {
        private Func<double, double> func;
        private Func<double, double> derivative;
        private string displayValue = "N/A";
        public MathFunction(Func<double, double> func, Func<double, double> derivative, string displayValue)
        {
            this.func = func;
            this.derivative = derivative;
            this.displayValue = displayValue;            
        }
        public double Evaluate(double x)
        {
            return func(x);
        }
        public double EvaluateDerivative(double x)
        {
            return derivative(x);
        }
        public string description
        {
            get
            {
                return "f(x) = " + displayValue;
            }
        }
    }
}
