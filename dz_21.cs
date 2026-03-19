using System;
using System.Collections.Generic;

// 1
public class Sentence
{
    private string[] _words;

    public Sentence(string text)
    {
        _words = text.Split(' ');
    }

    public string this[int index]
    {
        get
        {
            return _words[index];
        }
        set
        {
            _words[index] = value;
        }
    }
}

// 2
public class TemperatureGrid
{
    private double[,] _grid = new double[5, 5];

    public double this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= 5 || col < 0 || col >= 5)
                throw new IndexOutOfRangeException();

            return _grid[row, col];
        }
        set
        {
            if (row < 0 || row >= 5 || col < 0 || col >= 5)
                throw new IndexOutOfRangeException();

            _grid[row, col] = value;
        }
    }
}

// 3. Телефонная книга
public class Contact
{
    public string Name;
    public string Phone;

    public Contact(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }
}

public class PhoneBook
{
    private List<Contact> contacts = new List<Contact>();

    public void Add(string name, string phone)
    {
        contacts.Add(new Contact(name, phone));
    }

    public string this[string name]
    {
        get
        {
            foreach (var contact in contacts)
            {
                if (contact.Name == name)
                    return contact.Phone;
            }

            return "Контакт не найден";
        }
    }
}

// 4
public class BitController
{
    private byte _data = 0;

    public int this[int bitIndex]
    {
        get
        {
            return (_data >> bitIndex) & 1;
        }
        set
        {
            if (value == 1)
                _data = (byte)(_data | (1 << bitIndex));
            else
                _data = (byte)(_data & ~(1 << bitIndex));
        }
    }
}

// 5
public class DataVault
{
    private string[] _items = new string[10];

    public string this[int index]
    {
        get
        {
            return _items[index];
        }
        set
        {
            if (value != null && value != "")
                _items[index] = value;
        }
    }

    public string this[string index]
    {
        get
        {
            int i = int.Parse(index);
            return _items[i];
        }
        set
        {
            int i = int.Parse(index);
            if (value != null && value != "")
                _items[i] = value;
        }
    }
}
