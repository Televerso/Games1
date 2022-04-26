using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BoardGame.ChineseCrosswordGame;
using BoardGame.MinesweeperGame;
using BoardGame.SingleChessGame;
using BoardGame.SudokuGame;

namespace BoardGame
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            SingleChessBoard cb = new SingleChessBoard();

            Console.WriteLine(cb);
            
            cb.AddPiece(0,2,'Q');
            cb.AddPiece(2,1,'N');
            cb.AddPiece(4,3,'B');
            cb.AddPiece(7,2,'R');
            cb.AddPiece(7,6,'Q');
            cb.AddPiece(6,7,'B');
            Console.WriteLine(cb);
            while (!cb.CheckBoard())
            {
                string a = Console.ReadLine() ?? throw new InvalidOperationException();
                if (a.Equals("back")) cb.HistoryBackward();
                else if (a.Equals("select"))
                {
                    string b = Console.ReadLine() ?? throw new InvalidOperationException();
                    int x = 0;
                    int y = 0;
                    x = Convert.ToInt32(b.Split(' ')[0]);
                    y = Convert.ToInt32(b.Split(' ')[1]);
                    cb.SelectSquare(x, y);
                }
                else if (a.Equals("move"))
                {
                    string b = Console.ReadLine() ?? throw new InvalidOperationException();
                    int x = 0;
                    int y = 0;
                    x = Convert.ToInt32(b.Split(' ')[0]);
                    y = Convert.ToInt32(b.Split(' ')[1]);
                    cb.MovePiece(x, y);
                }
                Console.WriteLine(cb);
            }
            Console.WriteLine(cb.CheckBoard());
        }
    }
}
