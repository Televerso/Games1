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
            //for (int i = 0; i < 10; i++)
            //{
            //    MinesweeperBoard mb = new MinesweeperBoard(10,10);
            //    mb.RandomFillBoard(100);
            //    mb.OpenBoard();
            //    Console.WriteLine(mb);
            //}
            int s = 0;
            for (int i = 0; i < 100; i++)
            {
                SudokuBoardGenerator sudokuBoardGenerator = new SudokuBoardGenerator(16);
                sudokuBoardGenerator.GenerateStable();
                SudokuBoard sudokuBoard = new SudokuBoard(sudokuBoardGenerator.ToFullSudokuBoard());
                Console.WriteLine(sudokuBoard);
                Console.WriteLine(sudokuBoard.CheckBoard());
                if (sudokuBoard.CheckBoard()) ++s;
            }
            Console.WriteLine(s);
        }
    }
}
