using System;
using System.Collections.Generic;
using Calculation.Models;

namespace Calculation.Core
{
    public sealed class CalculationCore
    {
        private readonly CalculatorSettingsModel _calculatorSettings;
        private readonly List<string> _generatedFormulas;
        private readonly int _generateFormulaTryCount;

        private const int MaxNumbers = 3;
        private int _tryCount = 0;
        private string _calculationFormula = "Результат не найден";

        public CalculationCore(CalculatorSettingsModel settings)
        {
            _calculatorSettings = settings;
            _generatedFormulas = new List<string>();
            _generateFormulaTryCount = 50;
        }

        public CalculationResultModel Calculate()
        {
            do
            {
                _tryCount++;
            } while (DoCalculation() && _tryCount < _calculatorSettings.MaximumTryCount);

            return new CalculationResultModel
            {
                CalculationResult = _calculationFormula,
                TryCount = _tryCount
            };
        }

        private bool DoCalculation()
        {
            var generateTry = 0;
            string[] newArray;
            string formula;
            bool alreadyGenerated;
            do
            {
                newArray = GetSplittedArray();
                formula = GetFormula(newArray);
                alreadyGenerated = _generatedFormulas.Contains(formula);
                generateTry++;
            } while (!alreadyGenerated && generateTry <= _generateFormulaTryCount);

            if (alreadyGenerated) return true;
            _generatedFormulas.Add(formula);

            if (ApplyCalculation(newArray) == _calculatorSettings.ExpectedResult)
            {
                _calculationFormula = formula;
                return false;
            }
            return true;
        }

        private string[] GetSplittedArray()
        {
            var operators = GetAvailableOperators();
            var rnd = new Random();
            var array = new List<string>();
            int parsedValue;
            if(string.IsNullOrWhiteSpace(_calculatorSettings.InputString) || !int.TryParse(_calculatorSettings.InputString, out parsedValue))
                throw new Exception(string.Format("Введеное значение не является числом \"{0}\"", _calculatorSettings.InputString));

            var values = _calculatorSettings.InputString.ToCharArray();
            var startFrom = 0;
            while (startFrom < values.Length)
            {
                var maxRandomNumber = values.Length - startFrom >= MaxNumbers ? MaxNumbers : values.Length - startFrom;
                var takeCount = rnd.Next(1, maxRandomNumber);

                var value = string.Empty;
                for (var i = startFrom; i < startFrom + takeCount; i++)
                {
                    value += values[i].ToString();
                }

                startFrom += takeCount;
                array.Add(value);
                if(startFrom < values.Length) array.Add(operators[rnd.Next(operators.Count)]);
            }

            return array.ToArray();
        }

        private int ApplyCalculation(string[] values)
        {
            var result = 0;
            if (values.Length == 0) return result;
            result = GetInt(values[0]);
            
            for (var i = 1; i < values.Length; i = i + 2)
            {
                switch (values[i])
                {
                    case "+":
                        result += GetInt(values[i + 1]);
                        break;
                    case "-":
                        result -= GetInt(values[i + 1]);
                        break;
                    case "*":
                        result *= GetInt(values[i + 1]);
                        break;
                    default:
                        throw new NotImplementedException(string.Format("Оператор \"{0}\" не поддерживается", values[i]));
                }
            }

            return result;
        }

        private int GetInt(string value)
        {
            int result;
            var isInt = int.TryParse(value, out result);
            if(!isInt) throw new Exception(string.Format("Не могу привести к числу \"{0}\"", value));
            return result;
        }

        private string GetFormula(string[] values)
        {
            return string.Format("{0}={1}", string.Join("", values), _calculatorSettings.ExpectedResult);
        }

        private List<string> GetAvailableOperators()
        {
            var operators = new List<string>();
            if(_calculatorSettings.UseAdd) operators.Add("+");
            if(_calculatorSettings.UseMinus) operators.Add("-");
            if(_calculatorSettings.UseMultiply) operators.Add("*");

            if(operators.Count == 0) throw new Exception("Должен быть выбрать хоть один оператор");

            return operators;
        }
    }
}