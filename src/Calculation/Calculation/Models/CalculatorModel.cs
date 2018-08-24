using Calculation.Extentions;

namespace Calculation.Models
{
    public class CalculatorModel
    {
        public CalculatorSettingsModel CalculatorSettings { get; set; }
        public CalculationResultModel CalculationResult { get; set; }

        public string ModelException { get; set; }

        public CalculatorModel()
        {
            CalculatorSettings = new CalculatorSettingsModel();
            this.ApplyDefaults();
        }
    }
}