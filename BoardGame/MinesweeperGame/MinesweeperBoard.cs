using System.Security.Cryptography;
using System.Text;

namespace BoardGame.MinesweeperGame;

/// <summary>
/// Доска для игры в сапер
/// </summary>
/// <remarks>
/// _sideX<br/>
/// _sideY<br/>
/// _boardData
/// </remarks>
public class MinesweeperBoard : IBoard
{
    // Длина стороны x
    private int _sideX;
    // Длина стороны Y
    private int _sideY;
    // Данные доски
    private MinesweeperCell[,]? _boardData;
    
    /// <summary>
    /// Создает новую доску с пустыми значениями размером X*Y
    /// </summary>
    /// <param name="x">длина стороны x</param>
    /// <param name="y">длина стороны Y</param>
    private void CreateBoard(int x, int y)
    {
        _boardData = new MinesweeperCell[x, y];
        _sideX = x;
        _sideY = y;
        for (int i = 0; i != _sideX * _sideY; ++i)
        {
            _boardData[i % _sideX, i / _sideX] = new MinesweeperCell();
        }
    }
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public MinesweeperBoard()
    {
        _sideX = 0;
        _sideY = 0;
        _boardData = null;
    }
    /// <summary>
    /// Создает доску размером X*Y
    /// </summary>
    /// <param name="x">длина стороны x</param>
    /// <param name="y">длина стороны y</param>
    /// <exception cref="ArgumentOutOfRangeException">если x или y меньше 1</exception>
    public MinesweeperBoard(int x, int y)
    {
        if (x <= 0 || y <= 0) throw new ArgumentOutOfRangeException();
        CreateBoard(x, y);
    }
    /// <summary>
    /// Конструктор копирования
    /// </summary>
    /// <param name="mb">копируемая доска</param>
    /// <exception cref="ArgumentNullException">если копируемая доска не инициализирована</exception>
    public MinesweeperBoard(MinesweeperBoard mb)
    {
        if (mb._boardData == null) throw new ArgumentNullException();
        _sideX = mb._sideX;
        _sideY = mb._sideY;
        _boardData = (MinesweeperCell[,]) mb._boardData.Clone();
    }
    /// <summary>
    /// Возвращает индекс поля по его координатам
    /// </summary>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    /// <returns>index</returns>
    /// <exception cref="ArgumentOutOfRangeException">если переданы некорректные аргументы</exception>
    public int GetCellIndex(int x, int y)
    {
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        return x + (y * _sideX);
    }
    /// <summary>
    /// Возвращает координату X клетки по его индексу
    /// </summary>
    /// <param name="index">индекс поля</param>
    /// <returns>X</returns>
    /// <exception cref="ArgumentOutOfRangeException">если передан неверный индекс</exception>
    public int GetCellXByIndex(int index)
    {
        if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
        return index % _sideX;
    }
    /// <summary>
    /// Возвращает координату Y клетки по его индексу
    /// </summary>
    /// <param name="index">индекс поля</param>
    /// <returns>Y</returns>
    /// <exception cref="ArgumentOutOfRangeException">если передан неверный индекс</exception>
    public int GetCellYByIndex(int index)
    {
        if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
        return index / _sideX;
    }
    /// <summary>
    /// Создает новую квадратную доску указанного размера
    /// </summary>
    /// <param name="a">длина стороны доски</param>
    /// <exception cref="ArgumentOutOfRangeException">если a меньше 1</exception>
    public void RecreateBoard(int a)
    {
        if (a < 1) throw new ArgumentOutOfRangeException();
        CreateBoard(a, a);
    }
    /// <summary>
    /// Создает новую доску указанного размера
    /// </summary>
    /// <param name="x">длина стороны x</param>
    /// <param name="y">длина стороны y</param>
    /// <exception cref="ArgumentOutOfRangeException">если x или y меньше 1</exception>
    public void RecreateBoard(int x, int y)
    {
        if (x < 1) throw new ArgumentOutOfRangeException();
        if (y < 1) throw new ArgumentOutOfRangeException();
        CreateBoard(x, y);
    }
    /// <summary>
    /// Возвращает количество клеток доски
    /// </summary>
    /// <returns>количество клеток доски</returns>
    public int GetCount()
    {
        return _sideX * _sideY;
    }
    /// <summary>
    /// Возвращает сторону X
    /// </summary>
    /// <returns>длина стороны X</returns>
    public int GetXSide()
    {
        return _sideX;
    }
    /// <summary>
    /// Возвращает сторону Y
    /// </summary>
    /// <returns>длина стороны Y</returns>
    public int GetYSide()
    {
        return _sideY;
    }
    /// <summary>
    /// Возвращает текущую доску
    /// </summary>
    /// <returns>ссылка на доску</returns>
    public MinesweeperCell[,]? GetRealBoard()
    {
        return _boardData;
    }
    /// <summary>
    /// Возвращает текущую доску
    /// </summary>
    /// <returns>данные доски</returns>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    public int[,] GetBoard()
    {
        if (_boardData == null) throw new NullReferenceException();
        
        int[,] intBoard = new int[_sideX, _sideY];
        for (int i = 0; i != _sideX; ++i)
        {
            // Спагетти-код вперед!!!!
            for (int j = 0; j != _sideY; ++j)
            {
                if (_boardData[i, j].GetFlag() || _boardData[i, j].GetMine())
                {
                    intBoard[i, j] = 3;
                    continue;
                }
                if (_boardData[i, j].GetFlag())
                {
                    intBoard[i, j] = 2;
                    continue;
                }
                if (_boardData[i, j].GetMine())
                {
                    intBoard[i, j] = 1;
                    continue;
                }
                intBoard[i, j] = _boardData[i, j].GetAdjacentMines();
            }
        }
        return intBoard;
    }
    /// <summary>
    /// Заполняет доску минами
    /// </summary>
    public void RandomFillBoard()
    {
        int diff = 85;
        RandomFillBoard(diff);
    }
    /// <summary>
    /// Заполняет доску минами
    /// </summary>
    /// <param name="difficulty">плотность мин</param>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentException">если передан неверный аргумент</exception>
    public void RandomFillBoard(int difficulty)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (difficulty < 0 || difficulty > 99) throw new ArgumentException();
        for (int i = 0; i != _sideX * _sideY; ++i)
        {
            _boardData[i % _sideX, i / _sideX].SetMine(!Convert.ToBoolean(Random.Shared.Next(0, 100-difficulty)));
        }
        CountAdjacentMines();
    }
    /// <summary>
    /// 0 - играет на данное поле<br/>
    /// 1 - устанавливает флаг на данное поле
    /// 2 - убирает флаг с данного поля
    /// </summary>
    /// <param name="index">индекс поля</param>
    /// <param name="val">передаваемое значение</param>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если передан неверный индекс</exception>
    /// <exception cref="ArgumentException">если передано неверное значение</exception>
    public void SetCell(int index, int val)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();

        switch (val)
        {
            case 0:
                SeekForMines(index % _sideX, index / _sideX);
                break;
            case 1:
                _boardData[index%_sideX,index/_sideX].SetFlag(true);
                break;
            case 2:
                _boardData[index%_sideX,index/_sideX].SetFlag(false);
                break;
            default:
                throw new ArgumentException();
        }
    }
    /// <summary>
    /// 0 - играет на данное поле<br/>
    /// 1 - устанавливает флаг на данное поле
    /// 2 - убирает флаг с данного поля
    /// </summary>
    /// <param name="x">координата X поля</param>
    /// <param name="y">координата Y поля</param>
    /// <param name="val">передаваемое значение</param>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если переданы невеные координаты</exception>
    /// <exception cref="ArgumentException">если передано неверное значение</exception>
    public void SetCell(int x, int y, int val)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();

        switch (val)
        {
            case 0:
                SeekForMines(x, y);
                break;
            case 1:
                _boardData[x, y].SetFlag(true);
                break;
            case 2:
                _boardData[x, y].SetFlag(false);
                break;
            default:
                throw new ArgumentException();
        }
    }
    /// <summary>
    /// Возвращает данные поля
    /// </summary>
    /// <param name="x">крродината x поля</param>
    /// <param name="y">координата y поля</param>
    /// <returns>значение данных поля</returns>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если переданы неверные координаты</exception>
    public int GetCell(int x, int y)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        
        if (_boardData[x, y].GetFlag() || _boardData[x, y].GetMine()) return 3;
        if (_boardData[x, y].GetFlag()) return 2;
        if (_boardData[x, y].GetMine()) return 1;
        return 0;
    }
    /// <summary>
    /// Возвращает данные поля
    /// </summary>
    /// <param name="index">индекс поля</param>
    /// <returns>значение данных поля</returns>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если передан неверный индекс</exception>
    public int GetCell(int index)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();

        int x = index % _sideX;
        int y = index / _sideY;
        
        if (_boardData[x, y].GetFlag() || _boardData[x, y].GetMine()) return 3;
        if (_boardData[x, y].GetFlag()) return 2;
        if (_boardData[x, y].GetMine()) return 1;
        return 0;
    }
    /// <summary>
    /// Проверяет доску на завершенность
    /// </summary>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    public bool CheckBoard()
    {
        if (_boardData == null) throw new NullReferenceException();
        for (int i = 0; i != _sideX * _sideY; ++i)
        {
            int x = i % _sideX;
            int y = i / _sideX;
            if (_boardData[x, y].GetFlag() != _boardData[x, y].GetMine()) return false;
        }

        return true;
    }
    /// <summary>
    /// Безопасно устанавливает/убирает мину
    /// </summary>
    /// <param name="x">координата x поля</param>
    /// <param name="y">координата y поля</param>
    /// <param name="val">мина</param>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если переданы неверные координаты</exception>
    public void SetMine(int x, int y, bool val)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        _boardData[x, y].SetMine(val);
        CountAdjacentMines();
    }
    /// <summary>
    /// Устанавливает/убирает флаг
    /// </summary>
    /// <param name="x">координата x поля</param>
    /// <param name="y">координата y поля</param>
    /// <param name="val">флаг</param>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если переданы неверные координаты</exception>
    public void SetFlag(int x, int y, bool val)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        _boardData[x, y].SetFlag(val);
    }
    /// <summary>
    /// Возвращает значение мины данного поля
    /// </summary>
    /// <param name="x">координата x поля</param>
    /// <param name="y">координата y поля</param>
    /// <returns>мину</returns>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если переданы неверные координаты</exception>
    public bool GetMine(int x, int y)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        return _boardData[x, y].GetMine();
    }
    /// <summary>
    /// Возвращает флаг данного поля
    /// </summary>
    /// <param name="x">координата x поля</param>
    /// <param name="y">координата y поля</param>
    /// <returns>флаг</returns>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если переданы неверные координаты</exception>
    public bool GetFlag(int x, int y)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        return _boardData[x, y].GetFlag();
    }
    /// <summary>
    /// Пересчитывает значения соседних мин для всех полей
    /// </summary>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    public void CountAdjacentMines()
    {
        if (_boardData == null) throw new NullReferenceException();
        // Обнуляем
        foreach (MinesweeperCell i in _boardData)
        {
            i.SetAdjacentMines(0);
        }
        // Считаем
        for (int i = 0; i != _sideX * _sideY; ++i)
        {
            // Обнаруживаем мину
            if (_boardData[i % _sideX, i / _sideX].GetMine())
            {
                // Добавляем 1 ко всем соседним полям
                AdjacentMinesCounter.AdjacentMines(_boardData, i % _sideX, i / _sideX);
            }
        }
    }
    /// <summary>
    /// Целиком открывает доску
    /// </summary>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    public void OpenBoard()
    {
        if (_boardData == null) throw new NullReferenceException();
        foreach (MinesweeperCell i in _boardData)
        {
            i.Hide(false);
        }
    }
    /// <summary>
    /// Целиком скрывает доску
    /// </summary>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    public void HideBoard()
    {
        if (_boardData == null) throw new NullReferenceException();
        foreach (MinesweeperCell i in _boardData)
        {
            i.Hide(true);
        }
    }
    /// <summary>
    /// Открывает поля методом рекурсивного поиска
    /// </summary>
    /// <param name="x">координата x началиного поля</param>
    /// <param name="y">координата y начального поля</param>
    private void RecursiveMinesSeeker(int x, int y)
    {
        if (_boardData![x, y].IsHidden() == false) return;
        // Открывает данное поле
        _boardData![x, y].Hide(false);
        // Открывает соседние поля, возле текущего поля нет мин
        if (_boardData![x, y].GetAdjacentMines() == 0)
        {
            if (x != 0 && y != 0) RecursiveMinesSeeker(x-1,y-1);
            if (x != 0) RecursiveMinesSeeker(x - 1, y);
            if (y != 0) RecursiveMinesSeeker(x, y - 1);
            if (x != _sideX - 1 && y != _sideY - 1) RecursiveMinesSeeker(x + 1, y + 1);
            if (x != _sideX - 1) RecursiveMinesSeeker(x + 1, y);
            if (y != _sideY - 1) RecursiveMinesSeeker(x, y + 1);
            if (x != 0 && y != _sideY - 1) RecursiveMinesSeeker(x - 1, y + 1);
            if (x != _sideX - 1 && y != 0) RecursiveMinesSeeker(x + 1, y - 1);
        }
    }
    /// <summary>
    /// Раскрывает поля относительно переданного 
    /// </summary>
    /// <param name="x">координата x</param>
    /// <param name="y">координата y</param>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    /// <exception cref="ArgumentOutOfRangeException">если переданы некорректные координаты</exception>
    public void SeekForMines(int x, int y)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        RecursiveMinesSeeker(x, y);
    }
    /// <summary>
    /// Метод ToString
    /// </summary>
    /// <example>
    /// <code>
    /// | |1|*|1|<br/>
    /// | |1|1|1|<br/>
    /// |1|1| | |<br/>
    /// |P|1| | |
    /// </code>
    /// </example>
    /// <returns>String</returns>
    /// <exception cref="NullReferenceException">если доска не инициализирована</exception>
    public override string ToString()
    {
        if (_boardData == null) throw new NullReferenceException();
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i != _sideX; ++i)
        {
            for (int j = 0; j != _sideY; ++j)
            {
                builder.Append('|');
                builder.Append(_boardData[i, j]);
            }

            builder.Append('|');
            builder.AppendLine();
        }

        return builder.ToString();
    }
}