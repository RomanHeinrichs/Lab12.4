using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

class Program
{
    static void Main()
    {
        var collection = new MyCollection<AirVehicle>(); 

        while (true)
        {
            
            Console.WriteLine("1. Создать пустую коллекцию");
            Console.WriteLine("2. Создать коллекцию из N случайных элементов");
            Console.WriteLine("3. Клонировать коллекцию");
            Console.WriteLine("4. Добавить элемент");
            Console.WriteLine("5. Удалить элемент");
            Console.WriteLine("6. Вывести коллекцию");
            Console.WriteLine("7. Очистить коллекцию");
            Console.WriteLine("8. Выход");

            Console.Write("Выберите действие: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Неверный ввод. Попробуйте снова.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    collection = new MyCollection<AirVehicle>();
                    Console.WriteLine("Создана пустая коллекция.");
                    break;

                case 2:
                    Console.Write("Введите количество элементов: ");
                    if (int.TryParse(Console.ReadLine(), out int length))
                    {
                        collection = new MyCollection<AirVehicle>(length);
                        Console.WriteLine($"Создана коллекция из {length} элементов.");
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод.");
                    }
                    break;

                case 3:
                    var clonedCollection = new MyCollection<AirVehicle>(collection);
                    Console.WriteLine("Коллекция склонирована.");
                    collection = clonedCollection;
                    break;

                case 4:
                    var vehicle = new AirVehicle();
                    vehicle.Init();
                    collection.Add(vehicle);
                    Console.WriteLine("Элемент добавлен.");
                    break;

                case 5:
                    Console.Write("Введите ID элемента для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out int idToRemove))
                    {
                        var vehicleToRemove = FindVehicleById(collection, idToRemove);
                        if (vehicleToRemove != null && collection.Remove(vehicleToRemove))
                        {
                            Console.WriteLine("Элемент удален.");
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод.");
                    }
                    break;

                case 6:
                    Console.WriteLine("Содержимое коллекции:");
                    foreach (var item in collection)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 7:
                    collection.Clear();
                    Console.WriteLine("Коллекция очищена.");
                    break;

                case 8:
                    return;

                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    // Вспомогательный метод для поиска элемента по ID
    static AirVehicle FindVehicleById(MyCollection<AirVehicle> collection, int id)
    {
        foreach (var vehicle in collection)
        {
            if (vehicle.Id == id)
                return vehicle;
        }
        return null;
    }
}