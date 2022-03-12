using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardGame.SudokuGame;

namespace BoardGame
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            int[,] arr3 = 
            {
                {1,0,3},
                {3,0,2},
                {0,3,1}
            };
            int[,] arr9 =
            {
                {0,7,8,1,9,3,5,6,2},
                {2,0,1,8,6,5,4,7,3},
                {6,3,0,4,7,2,8,9,1},
                {8,1,7,0,2,4,9,3,5},
                {9,2,4,5,0,7,1,8,6},
                {5,6,3,9,1,0,7,2,4},
                {3,4,6,7,8,1,0,5,9},
                {7,5,9,2,4,6,3,0,8},
                {1,8,2,3,5,9,6,4,0}
            }; // 4,9,5,6,3,8,2,1,7
            
            IBoard s;
            Console.WriteLine("Choose level");
            int lvl = Convert.ToInt32(Console.ReadLine());
            switch (lvl)
            {
                case 3:
                    s = new SudokuBoard(arr3);
                    break;
                case 9:
                    s = new SudokuBoard(arr9);
                    break;
                default:
                    Console.WriteLine("Error: " + lvl + " unknown integer");
                    return;
            }
            
            Console.WriteLine(s);
            //s.RandomFillBoard();

            while (!s.CheckBoard())
            {
                int y = Convert.ToInt32(Console.ReadLine());
                int x = Convert.ToInt32(Console.ReadLine());
                int a = Convert.ToInt32(Console.ReadLine());
                s.SetCell(x, y, a);
                Console.WriteLine(s);
            }
            Console.WriteLine(s.CheckBoard());
            
        }
    }
}
