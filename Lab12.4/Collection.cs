using System;
using System.Collections;
using System.Collections.Generic;


public class MyCollection<T> : IEnumerable<T>, ICollection<T>, IList<T> where T : ICloneable
{
    // Внутренний класс Node для представления узлов двунаправленного списка
    private class Node
    {
        public T Data { get; set; } 
        public Node Previous { get; set; } 
        public Node Next { get; set; } 

        public Node(T data)
        {
            Data = data;
            Previous = null;
            Next = null;
        }
    }

    private Node head; 
    private Node tail; 
    private int count; 

    // Конструктор для создания пустой коллекции
    public MyCollection()
    {
        head = null;
        tail = null;
        count = 0;
    }

    // Конструктор для создания коллекции из length элементов, сформированных с помощью ДСЧ
    public MyCollection(int length) : this()
    {
        if (length < 0)
            throw new ArgumentException("Длина коллекции не может быть отрицательной.");

        Random random = new Random();
        for (int i = 0; i < length; i++)
        {
            Add((T)(object)random.Next(1, 100)); // Пример: добавляем случайные числа
        }
    }

    // Конструктор для копирования коллекции (глубокое клонирование)
    public MyCollection(MyCollection<T> c) : this()
    {
        foreach (var item in c)
        {
            Add((T)item.Clone());
        }
    }

   
    // Используем итератор yield для перебора элементов
    public IEnumerator<T> GetEnumerator()
    {
        Node current = head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public int Count => count; // Количество элементов в коллекции

    public bool IsReadOnly => false; // Коллекция не является только для чтения

    // Метод для добавления элемента в конец списка
    public void Add(T item)
    {
        Node newNode = new Node(item);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Previous = tail;
            tail = newNode;
        }

        count++;
    }

    // Метод для очистки коллекции
    public void Clear()
    {
        head = null;
        tail = null;
        count = 0;
    }

    // Метод для проверки наличия элемента в коллекции
    public bool Contains(T item)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data.Equals(item))
                return true;
            current = current.Next;
        }
        return false;
    }

    // Метод для копирования элементов коллекции в массив
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0 || arrayIndex > array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < count)
            throw new ArgumentException("Недостаточно места в массиве.");

        Node current = head;
        for (int i = 0; i < count; i++)
        {
            array[arrayIndex + i] = current.Data;
            current = current.Next;
        }
    }

    // Метод для удаления элемента из коллекции
    public bool Remove(T item)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data.Equals(item))
            {
                if (current.Previous != null)
                    current.Previous.Next = current.Next;
                else
                    head = current.Next;

                if (current.Next != null)
                    current.Next.Previous = current.Previous;
                else
                    tail = current.Previous;

                count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    
    // Индексатор для доступа к элементам по индексу
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            Node current = head;
            for (int i = 0; i < index; i++)
                current = current.Next;

            return current.Data;
        }
        set
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            Node current = head;
            for (int i = 0; i < index; i++)
                current = current.Next;

            current.Data = value;
        }
    }

    // Метод для получения индекса элемента
    public int IndexOf(T item)
    {
        Node current = head;
        int index = 0;
        while (current != null)
        {
            if (current.Data.Equals(item))
                return index;
            current = current.Next;
            index++;
        }
        return -1;
    }

    // Метод для вставки элемента по индексу
    public void Insert(int index, T item)
    {
        if (index < 0 || index > count)
            throw new IndexOutOfRangeException();

        Node newNode = new Node(item);

        if (index == 0)
        {
            newNode.Next = head;
            if (head != null)
                head.Previous = newNode;
            head = newNode;
        }
        else if (index == count)
        {
            newNode.Previous = tail;
            if (tail != null)
                tail.Next = newNode;
            tail = newNode;
        }
        else
        {
            Node current = head;
            for (int i = 0; i < index; i++)
                current = current.Next;

            newNode.Previous = current.Previous;
            newNode.Next = current;
            current.Previous.Next = newNode;
            current.Previous = newNode;
        }

        count++;
    }

    // Метод для удаления элемента по индексу
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException();

        Node current = head;
        for (int i = 0; i < index; i++)
            current = current.Next;

        if (current.Previous != null)
            current.Previous.Next = current.Next;
        else
            head = current.Next;

        if (current.Next != null)
            current.Next.Previous = current.Previous;
        else
            tail = current.Previous;

        count--;
    }
}