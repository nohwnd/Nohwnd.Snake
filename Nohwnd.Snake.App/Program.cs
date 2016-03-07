using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Console;

namespace Nohwnd.Snake.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CursorVisible = false;
            var size = 50;
            WindowWidth = WindowHeight = size + 10;

            foreach (var c in Enumerable.Range(0, size))
            {
                SetCursorPosition(0, c);
                Write("*");
                SetCursorPosition(c, 0);
                Write("*");
                SetCursorPosition(size - c, size);
                Write("*");
                SetCursorPosition(size, size - c);
                Write("*");
                Thread.Sleep(10);
            }

            SetCursorPosition(20, 25);
            Write("Snake!");
            Thread.Sleep(1000);
            SetCursorPosition(20, 25);
            Write("      ");

            var delay = 300;
            var arr = new byte[size, size];
            var x = 0;
            var y = 0;
            var lastKey = new ConsoleKeyInfo('o', ConsoleKey.DownArrow, false, false, false);

            var length = 10;
            var history = new Queue<Tuple<int, int>>();
            var random = new Random();
            arr[5, 5] = 2;
            SetCursorPosition(6, 6);
            Write("m");


            while (true)
            {
                SetCursorPosition(x + 1, y + 1);
                Write('o');


                if (x < 0 || 50 < x || y < 0 || 50 < y || arr[x, y] == 1)
                {
                    SetCursorPosition(20, 25);
                    Write("you dideded!");
                    ReadLine();
                    break;
                }

                var vypisMnaminku = false;
                if (arr[x, y] == 2)
                {
                    length++;

                    vypisMnaminku = true;

                    delay = (int) (delay*0.9m);
                }

                arr[x, y] = 1;
                if (history.Count >= length)
                {
                    var tail = history.Dequeue();

                    arr[tail.Item1, tail.Item2] = 0;
                    SetCursorPosition(tail.Item1 + 1, tail.Item2 + 1);
                    Write(" ");
                }

                if (vypisMnaminku)
                {
                    int rx;
                    int ry;
                    do
                    {
                        rx = random.Next(0, size);
                        ry = random.Next(0, size);
                    } while (arr[rx, ry] == 1);

                    arr[rx, ry] = 2;
                    SetCursorPosition(rx + 1, ry + 1);
                    Write("m");
                }

                history.Enqueue(new Tuple<int, int>(x, y));

                if (KeyAvailable)
                {
                    lastKey = ReadKey(true);
                    while (KeyAvailable)
                    {
                        ReadKey(true);
                    }
                }

                if (lastKey.Key == ConsoleKey.DownArrow)
                {
                    y++;
                }
                if (lastKey.Key == ConsoleKey.UpArrow)
                {
                    y--;
                }
                if (lastKey.Key == ConsoleKey.LeftArrow)
                {
                    x--;
                }
                if (lastKey.Key == ConsoleKey.RightArrow)
                {
                    x++;
                }

                Thread.Sleep(delay);
            }
        }
    }
}