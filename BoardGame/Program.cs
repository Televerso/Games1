using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.ChineseCrosswordGame;
using BoardGame.MinesweeperGame;
using BoardGame.SudokuGame;

namespace BoardGame
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            bool[,] a =
            {
                {true, true, true, true, true, true},
                {true, false, true, false, true, false},
                {false, false, false, false, false, false},
                {true, false, true, false, true, false},
                {true, true, true, true, true, true}
            };
            ChineseCrosswordBoard cb = new ChineseCrosswordBoard(a);
            while (!cb.CheckBoard())
            {
                Console.WriteLine(cb);
                int x = Convert.ToInt32(Console.ReadLine());
                int y = Convert.ToInt32(Console.ReadLine());
                cb.SetCell(x,y);
            }
            Console.WriteLine(cb);
            Console.WriteLine(cb.CheckBoard());
            
            
        }
    }
}
