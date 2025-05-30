using System;

// Производный класс Helicopter, наследуется от AirVehicle
public class Helicopter : AirVehicle
{
    public int BladeCount { get; set; } // Количество лопастей винта

    // Конструктор по умолчанию
    public Helicopter() : base()
    {
        BladeCount = 0;
    }

    // Переопределение метода Init для ввода данных с клавиатуры
    public override void Init()
    {
        base.Init(); // Вызываем метод базового класса для общих полей
        Console.Write("Введите количество лопастей винта: ");
        BladeCount = ReadIntFromConsole("Количество лопастей винта", 1, int.MaxValue);
    }

    
    public override string ToString()
    {
        return $"{base.ToString()}, Лопасти: {BladeCount}";
    }
}