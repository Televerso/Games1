namespace BoardGame.MinesweeperGame;
/// <summary>
/// Счетчик соседних мин
/// </summary>
public static class AdjacentMinesCounter
{
    /// <summary>
    /// Добавляет 1 к количеству мин на соседнем верхнем поле
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    private static void AdjacentTop(MinesweeperCell[,] board, int x, int y)
    {
        if (y == 0) return;
        board[x, y - 1].SetAdjacentMines(board[x, y - 1].GetAdjacentMines() + 1);
    }
    /// <summary>
    /// Добавляет 1 к количеству мин на соседнем верхнем левом поле
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    private static void AdjacentTopLeft(MinesweeperCell[,] board, int x, int y)
    {
        if (x == 0) return;
        if (y == 0) return;
        board[x - 1, y - 1].SetAdjacentMines(board[x - 1, y - 1].GetAdjacentMines() + 1);
    }
    /// <summary>
    /// Добавляет 1 к количеству мин на соседнем левом поле
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    private static void AdjacentLeft(MinesweeperCell[,] board, int x, int y)
    {
        if (x == 0) return;
        board[x - 1, y].SetAdjacentMines(board[x - 1, y].GetAdjacentMines() + 1);
    }
    /// <summary>
    /// Добавляет 1 к количеству мин на соседнем нижнем левом поле
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    private static void AdjacentBottomLeft(MinesweeperCell[,] board, int x, int y)
    {
        if (x == 0) return;
        if (y == board.GetLength(1) - 1) return;
        board[x - 1, y + 1].SetAdjacentMines(board[x - 1, y + 1].GetAdjacentMines() + 1);
    }
    /// <summary>
    /// Добавляет 1 к количеству мин на соседнем нижнем поле
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    private static void AdjacentBottom(MinesweeperCell[,] board, int x, int y)
    {
        if (y == board.GetLength(1) - 1) return;
        board[x, y + 1].SetAdjacentMines(board[x, y + 1].GetAdjacentMines() + 1);
    }
    /// <summary>
    /// Добавляет 1 к количеству мин на соседнем нижнем правом поле
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    private static void AdjacentBottomRight(MinesweeperCell[,] board, int x, int y)
    {
        if (x == board.GetLength(0) - 1) return;
        if (y == board.GetLength(1) - 1) return;
        board[x + 1, y + 1].SetAdjacentMines(board[x + 1, y + 1].GetAdjacentMines() + 1);
    }
    /// <summary>
    /// Добавляет 1 к количеству мин на соседнем правом поле
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    private static void AdjacentRight(MinesweeperCell[,] board, int x, int y)
    {
        if (x == board.GetLength(0) - 1) return;
        board[x + 1, y].SetAdjacentMines(board[x + 1, y].GetAdjacentMines() + 1);
    }
    /// <summary>
    /// Добавляет 1 к количеству мин на соседнем верхнем правом поле
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    private static void AdjacentTopRight(MinesweeperCell[,] board, int x, int y)
    {
        if (x == board.GetLength(0) - 1) return;
        if (y == 0) return;
        board[x + 1, y - 1].SetAdjacentMines(board[x + 1, y - 1].GetAdjacentMines() + 1);
    }
    /// <summary>
    /// Добавляет 1 к количеству мин на всех соседних полях, включая собственное
    /// </summary>
    /// <param name="board">текущая доска</param>
    /// <param name="x">координата x поля</param>
    /// <param name="y">координата y поля</param>
    public static void AdjacentMines(MinesweeperCell[,] board, int x, int y)
    {
        if (!board[x, y].GetMine()) return;
        board[x, y].SetAdjacentMines(board[x, y].GetAdjacentMines() + 1);
        AdjacentTop(board, x, y);
        AdjacentTopLeft(board, x, y);
        AdjacentLeft(board, x, y);
        AdjacentBottomLeft(board, x, y);
        AdjacentBottom(board, x, y);
        AdjacentBottomRight(board, x, y);
        AdjacentRight(board, x, y);
        AdjacentTopRight(board, x, y);
    }
}