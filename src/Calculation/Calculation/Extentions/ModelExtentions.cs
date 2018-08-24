using Calculation.Core;
using Calculation.Models;

namespace Calculation.Extentions
{
    public static class ModelExtentions
    {
        public static void ApplyDefaults(this CalculatorModel model)
        {
            model.CalculatorSettings.InputString = "123456789";
            model.CalculatorSettings.ExpectedResult = 100;
            model.CalculatorSettings.MaximumTryCount = 200;
            model.CalculatorSettings.UseAdd = true;
            model.CalculatorSettings.UseMinus = true;
        }

        public static void Calculate(this CalculatorModel model)
        {
            var calc = new CalculationCore(model.CalculatorSettings);
            model.CalculationResult = calc.Calculate();
        }
    }
}