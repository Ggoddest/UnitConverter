using System;

namespace UnitConverter
{
    public abstract class Unit
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public abstract double ToBase(double value);
        public abstract double FromBase(double baseValue);
    }

    // ===== ДЛИНА =====
    public class Meter : Unit { 
        public Meter() { Name = "Метр"; Category = "Длина"; } 
        public override double ToBase(double v) => v; 
        public override double FromBase(double b) => b; 
    }
    
    public class Centimeter : Unit { 
        private const double METERS_IN_CENTIMETER = 0.01; // 1 см = 0.01 м
        public Centimeter() { Name = "Сантиметр"; Category = "Длина"; } 
        public override double ToBase(double v) => v * METERS_IN_CENTIMETER; 
        public override double FromBase(double b) => b / METERS_IN_CENTIMETER; 
    }
    
    public class Kilometer : Unit { 
        private const double METERS_IN_KILOMETER = 1000.0; // 1 км = 1000 м
        public Kilometer() { Name = "Километр"; Category = "Длина"; } 
        public override double ToBase(double v) => v * METERS_IN_KILOMETER; 
        public override double FromBase(double b) => b / METERS_IN_KILOMETER; 
    }
    
    public class Inch : Unit { 
        private const double METERS_IN_INCH = 0.0254; // Точное значение по определению
        public Inch() { Name = "Дюйм"; Category = "Длина"; } 
        public override double ToBase(double v) => v * METERS_IN_INCH; 
        public override double FromBase(double b) => b / METERS_IN_INCH; 
    }
    
    public class Foot : Unit { 
        private const double METERS_IN_FOOT = 0.3048; // Точное значение по определению
        public Foot() { Name = "Фут"; Category = "Длина"; } 
        public override double ToBase(double v) => v * METERS_IN_FOOT; 
        public override double FromBase(double b) => b / METERS_IN_FOOT; 
    }

    // ===== МАССА ===== (без изменений)
    public class Gram : Unit { 
        public Gram() { Name = "Грамм"; Category = "Масса"; } 
        public override double ToBase(double v) => v; 
        public override double FromBase(double b) => b; 
    }
    
    public class Kilogram : Unit { 
        private const double GRAMS_IN_KILOGRAM = 1000.0;
        public Kilogram() { Name = "Килограмм"; Category = "Масса"; } 
        public override double ToBase(double v) => v * GRAMS_IN_KILOGRAM; 
        public override double FromBase(double b) => b / GRAMS_IN_KILOGRAM; 
    }
    
    public class Tonne : Unit { 
        private const double GRAMS_IN_TONNE = 1_000_000.0;
        public Tonne() { Name = "Тонна"; Category = "Масса"; } 
        public override double ToBase(double v) => v * GRAMS_IN_TONNE; 
        public override double FromBase(double b) => b / GRAMS_IN_TONNE; 
    }
    
    public class Ounce : Unit { 
        private const double GRAMS_IN_OUNCE = 28.349523125; // Точное значение
        public Ounce() { Name = "Унция"; Category = "Масса"; } 
        public override double ToBase(double v) => v * GRAMS_IN_OUNCE; 
        public override double FromBase(double b) => b / GRAMS_IN_OUNCE; 
    }
    
    public class Pound : Unit { 
        private const double GRAMS_IN_POUND = 453.59237; // Точное значение по определению
        public Pound() { Name = "Фунт"; Category = "Масса"; } 
        public override double ToBase(double v) => v * GRAMS_IN_POUND; 
        public override double FromBase(double b) => b / GRAMS_IN_POUND; 
    }

    // ===== ВРЕМЯ ===== (без изменений)
    public class Second : Unit { 
        public Second() { Name = "Секунда"; Category = "Время"; } 
        public override double ToBase(double v) => v; 
        public override double FromBase(double b) => b; 
    }
    
    public class Minute : Unit { 
        private const double SECONDS_IN_MINUTE = 60.0;
        public Minute() { Name = "Минута"; Category = "Время"; } 
        public override double ToBase(double v) => v * SECONDS_IN_MINUTE; 
        public override double FromBase(double b) => b / SECONDS_IN_MINUTE; 
    }
    
    public class Hour : Unit { 
        private const double SECONDS_IN_HOUR = 3600.0;
        public Hour() { Name = "Час"; Category = "Время"; } 
        public override double ToBase(double v) => v * SECONDS_IN_HOUR; 
        public override double FromBase(double b) => b / SECONDS_IN_HOUR; 
    }
    
    public class Day : Unit { 
        private const double SECONDS_IN_DAY = 86400.0;
        public Day() { Name = "День"; Category = "Время"; } 
        public override double ToBase(double v) => v * SECONDS_IN_DAY; 
        public override double FromBase(double b) => b / SECONDS_IN_DAY; 
    }
    
    public class Week : Unit { 
        private const double DAYS_IN_WEEK = 7.0;
        private const double SECONDS_IN_DAY = 86400.0;
        public Week() { Name = "Неделя"; Category = "Время"; } 
        public override double ToBase(double v) => v * DAYS_IN_WEEK * SECONDS_IN_DAY; 
        public override double FromBase(double b) => b / (DAYS_IN_WEEK * SECONDS_IN_DAY); 
    }

    // ===== ТЕМПЕРАТУРА ===== (без изменений)
    public class Celsius : Unit { 
        public Celsius() { Name = "Цельсий"; Category = "Температура"; } 
        public override double ToBase(double v) => v; 
        public override double FromBase(double b) => b; 
    }
    
    public class Fahrenheit : Unit { 
        public Fahrenheit() { Name = "Фаренгейт"; Category = "Температура"; } 
        public override double ToBase(double v) => (v - 32.0) * 5.0 / 9.0; 
        public override double FromBase(double b) => b * 9.0 / 5.0 + 32.0; 
    }
    
    public class Kelvin : Unit { 
        private const double ABSOLUTE_ZERO_CELSIUS = -273.15;
        public Kelvin() { Name = "Кельвин"; Category = "Температура"; } 
        public override double ToBase(double v) => v + ABSOLUTE_ZERO_CELSIUS; 
        public override double FromBase(double b) => b - ABSOLUTE_ZERO_CELSIUS; 
    }

    // ===== СКОРОСТЬ ===== (без изменений)
    public class Mps : Unit { 
        public Mps() { Name = "Метр в секунду"; Category = "Скорость"; } 
        public override double ToBase(double v) => v; 
        public override double FromBase(double b) => b; 
    }
    
    public class Kmph : Unit { 
        private const double MPS_IN_KMPH = 3.6; // Точное значение
        public Kmph() { Name = "Километр в час"; Category = "Скорость"; } 
        public override double ToBase(double v) => v / MPS_IN_KMPH; 
        public override double FromBase(double b) => b * MPS_IN_KMPH; 
    }
    
    public class Mph : Unit { 
        private const double METERS_IN_MILE = 1609.344; // Точное значение по определению
        private const double SECONDS_IN_HOUR = 3600.0;
        private const double MPS_IN_MPH = METERS_IN_MILE / SECONDS_IN_HOUR; // 0.44704
        public Mph() { Name = "Миля в час"; Category = "Скорость"; } 
        public override double ToBase(double v) => v * MPS_IN_MPH; 
        public override double FromBase(double b) => b / MPS_IN_MPH; 
    }
    
    public class Knot : Unit { 
        private const double METERS_IN_NAUTICAL_MILE = 1852.0; // Точное значение по определению
        private const double SECONDS_IN_HOUR = 3600.0;
        private const double MPS_IN_KNOT = METERS_IN_NAUTICAL_MILE / SECONDS_IN_HOUR; // 0.514444...
        public Knot() { Name = "Узел"; Category = "Скорость"; } 
        public override double ToBase(double v) => v * MPS_IN_KNOT; 
        public override double FromBase(double b) => b / MPS_IN_KNOT; 
    }
    
    public class Fps : Unit { 
        private const double METERS_IN_FOOT = 0.3048; // Точное значение по определению
        public Fps() { Name = "Фут в секунду"; Category = "Скорость"; } 
        public override double ToBase(double v) => v * METERS_IN_FOOT; 
        public override double FromBase(double b) => b / METERS_IN_FOOT; 
    }
}