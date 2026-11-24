using System;
using System.Text;

class Program
{
    const int SIZE = 5;

    static void Draw(char[,] field, int x, int y, int health, int score, int herbsLeft, string message)
    {
        Console.Clear();

        Console.WriteLine("Magic Forest. Задача собрать всю лечебную траву (*), не наступать на ядовитые грибы (#).");
        Console.WriteLine("Команды: w - вверх, s - вниз, a - влево, d - вправо,");
        Console.WriteLine("         h - лечиться (1 трава +20 здоровья), q - выход");
        Console.WriteLine();

        for (int i = 0; i < SIZE; i++)
        {
            string row = "";
            for (int j = 0; j < SIZE; j++)
            {
                if (i == y && j == x)
                {
                    row = row + "@ ";
                }
                else
                {
                    row = row + field[i, j] + " ";
                }
            }
            Console.WriteLine(row);
        }

        Console.WriteLine();
        Console.WriteLine("Здоровье: " + health + "  Трав в сумке: " + score
                          + "  Осталось на поле: " + herbsLeft);

        if (health < 30)
        {
            Console.WriteLine("ВНИМАНИЕ: здоровье очень низкое!");
        }
        else if (health < 60)
        {
            Console.WriteLine("Здоровье среднее, будь осторожен.");
        }
        else
        {
            Console.WriteLine("Ты в порядке.");
        }

        Console.WriteLine();
        Console.WriteLine(message);
    }

    static void Main()
    { 
        Console.OutputEncoding = Encoding.UTF8;

        char[,] field =
        {
            { '.', '.', '*', '.', '.' },
            { '.', '.', '#', '.', '*' },
            { '.', '#', '.', '.', '.' },
            { '*', '.', '.', '.', '#' },
            { '.', '.', '*', '.', '.' }
        };

        int x = 0, y = 0;       
        int health = 100;       
        int score = 0;          
        int herbsLeft = 4;      
        bool running = true;    
        string message = "Игра началась. Удачи!";

        while (running && health > 0 && herbsLeft > 0)
        {
            Draw(field, x, y, health, score, herbsLeft, message);

            Console.Write("Команда: ");
            string cmd = Console.ReadLine();
            if (cmd == null)
            {
                cmd = "";
            }
            cmd = cmd.Trim().ToLower();

            message = "";
            int newX = x, newY = y;
            bool move = false;

            switch (cmd)
            {
                case "w":
                    newY = y - 1;
                    move = true;
                    break;
                case "s":
                    newY = y + 1;
                    move = true;
                    break;
                case "a":
                    newX = x - 1;
                    move = true;
                    break;
                case "d":
                    newX = x + 1;
                    move = true;
                    break;
                case "h":
                    if (score > 0)
                    {
                        score = score - 1;
                        health = health + 20;
                        if (health > 100)
                        {
                            health = 100;
                        }
                        message = "Ты полечился. +20 здоровья.";
                    }
                    else
                    {
                        message = "Нет трав для лечения!";
                    }
                    break;
                case "q":
                case "exit":
                    running = false;
                    break;
                default:
                    message = "Неизвестная команда";
                    break;
            }

            if (move)
            {
                if (newX < 0)
                {
                    message = "Слева край леса!";
                }
                else if (newX >= SIZE)
                {
                    message = "Справа край леса!";
                }
                else if (newY < 0)
                {
                    message = "Сверху край леса!";
                }
                else if (newY >= SIZE)
                {
                    message = "Снизу край леса!";
                }
                else
                {
                    x = newX;
                    y = newY;

                    if (field[y, x] == '*')
                    {
                        score = score + 1;
                        herbsLeft = herbsLeft - 1;
                        field[y, x] = '.';
                        message = "Ты нашёл траву!";
                    }
                    else if (field[y, x] == '#')
                    {
                        health = health - 40;
                        if (health < 0)
                        {
                            health = 0;
                        }
                        field[y, x] = '.';
                        message = "Ядовитый гриб! -40 здоровья.";
                    }
                }
            }
        }

        Draw(field, x, y, health, score, herbsLeft, message);
        Console.WriteLine();
        if (health <= 0)
        {
            Console.WriteLine("Поражение: герой погиб.");
        }
        else if (herbsLeft == 0)
        {
            Console.WriteLine("Победа! Все травы собраны!");
        }
        else
        {
            Console.WriteLine("Выход из игры");
        }
        Console.WriteLine("Итоговый счёт: " + score);
    }
}