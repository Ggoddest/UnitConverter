using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitConverter
{
    public static class ConversionService
    {
        private static readonly List<Unit> AllUnits = new List<Unit>
        {
            // Длина
            new Meter(), new Centimeter(), new Kilometer(), new Inch(), new Foot(),
            // Масса
            new Gram(), new Kilogram(), new Tonne(), new Ounce(), new Pound(),
            // Время
            new Second(), new Minute(), new Hour(), new Day(), new Week(),
            // Температура
            new Celsius(), new Fahrenheit(), new Kelvin(),
            // Скорость
            new Mps(), new Kmph(), new Mph(), new Knot(), new Fps()
        };

        public static List<string> GetCategories() =>
            AllUnits.Select(u => u.Category).Distinct().OrderBy(c => c).ToList();

        public static List<Unit> GetUnitsByCategory(string category)
        {
            var units = AllUnits.Where(u => u.Category == category).ToList();

            if (category == "Температура")
            {
                // Фиксированный порядок: Цельсий, Фаренгейт, Кельвин
                var order = new[] { "Цельсий", "Фаренгейт", "Кельвин" };
                units = units.OrderBy(u => Array.IndexOf(order, u.Name)).ToList();
            }
            else
            {
                // Для всех остальных — сортировка по значению в базовой единице
                units = units.OrderBy(u => u.ToBase(1.0)).ToList();
            }

            return units;
        }

        public static double Convert(Unit from, Unit to, double value)
        {
            if (from.Category != to.Category)
                throw new ArgumentException("Нельзя конвертировать величины из разных категорий.");

            double baseValue = from.ToBase(value);
            return to.FromBase(baseValue);
        }

        public static Dictionary<string, double> ConvertAll(Unit source, double value)
        {
            var results = new Dictionary<string, double>();
            var units = GetUnitsByCategory(source.Category);
            foreach (var unit in units)
            {
                if (unit.Name != source.Name)
                {
                    try
                    {
                        double result = Convert(source, unit, value);
                        results[unit.Name] = Math.Round(result, 6);
                    }
                    catch { /* игнор */ }
                }
            }
            return results;
        }
    }
}