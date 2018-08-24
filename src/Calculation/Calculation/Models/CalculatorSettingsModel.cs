using System.ComponentModel;
using Calculation.Core;

namespace Calculation.Models
{
    public class CalculatorSettingsModel
    {
        [InputStringValidation]
        [DisplayName("Входная строка")]
        public string InputString { get; set; }

        [DisplayName("Ожидаемый результат")]
        public int ExpectedResult { get; set; }

        [DisplayName("Использовать *")]
        public bool UseMultiply { get; set; }

        [DisplayName("Использовать +")]
        public bool UseAdd { get; set; }

        [DisplayName("Использовать -")]
        public bool UseMinus { get; set; }

        [DisplayName("Количество попыток")]
        public int MaximumTryCount { get; set; }
    }
}