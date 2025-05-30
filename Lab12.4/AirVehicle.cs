using System;

// Базовый класс AirVehicle, реализует интерфейсы IComparable<AirVehicle> и ICloneable
public class AirVehicle : IComparable<AirVehicle>, ICloneable
{
    
    public string Model { get; set; } 
    public int YearOfManufacture { get; set; } 
    public string EngineType { get; set; } 
    public int CrewCount { get; set; } 
    public int Id { get; set; } 

    // Конструктор по умолчанию
    public AirVehicle()
    {
        Model = "Неизвестная модель";
        YearOfManufacture = 1900;
        EngineType = "Неизвестный тип";
        CrewCount = 1;
        Id = 0;
    }

    // Метод для ввода данных с клавиатуры
    public virtual void Init()
    {
        Console.Write("Введите модель: ");
        Model = Console.ReadLine();

        Console.Write("Введите год производства: ");
        YearOfManufacture = ReadIntFromConsole("Год производства", 1900, DateTime.Now.Year);

        Console.Write("Введите тип двигателя: ");
        EngineType = Console.ReadLine();

        Console.Write("Введите количество членов экипажа: ");
        CrewCount = ReadIntFromConsole("Количество членов экипажа", 1, int.MaxValue);

        Console.Write("Введите уникальный ID: ");
        Id = ReadIntFromConsole("ID", 0, int.MaxValue);
    }

    // Метод для чтения целого числа с клавиатуры
    protected int ReadIntFromConsole(string fieldName, int minValue, int maxValue)
    {
        while (true)
        {
            Console.Write($"{fieldName}: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (value >= minValue && value <= maxValue)
                    return value;
                Console.WriteLine($"Ошибка: Значение должно быть в диапазоне от {minValue} до {maxValue}. Попробуйте снова.");
            }
            else
            {
                Console.WriteLine($"Ошибка: Неверный ввод для поля '{fieldName}'. Попробуйте снова.");
            }
        }
    }

    // Переопределение метода ToString для вывода информации об объекте
    public override string ToString()
    {
        return $"[ID: {Id}] Модель: {Model}, Год: {YearOfManufacture}, Двигатель: {EngineType}, Экипаж: {CrewCount}";
    }

    
    // Сравнивает объекты по их уникальному ID
    public int CompareTo(AirVehicle other)
    {
        if (other == null) return 1;
        return this.Id.CompareTo(other.Id);
    }

    // Реализация интерфейса ICloneable для глубокого клонирования
    public object Clone()
    {
        return new AirVehicle
        {
            Model = this.Model,
            YearOfManufacture = this.YearOfManufacture,
            EngineType = this.EngineType,
            CrewCount = this.CrewCount,
            Id = this.Id
        };
    }
}