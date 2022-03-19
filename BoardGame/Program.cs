using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.MinesweeperGame;
using BoardGame.SudokuGame;

namespace BoardGame
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            IBoard mb = new MinesweeperBoard(4,5);
            mb.RandomFillBoard();

            Console.WriteLine(mb);
            
            while (mb.CheckBoard() == false)
            {
                Console.WriteLine(mb);
                int d = Convert.ToInt32(Console.ReadLine());
                int x = Convert.ToInt32(Console.ReadLine());
                int y = Convert.ToInt32(Console.ReadLine());
                mb.SetCell(x, y, d);
            }
            Console.WriteLine(mb.CheckBoard());
        }
    }
}
