using System.Text;

namespace BoardGame.ChineseCrosswordGame
{
    /// <summary>
    /// Доска для японского кроссворда
    /// </summary>
    public class ChineseCrosswordBoard
    {
        // Подсказки о строках кроссворда
        private int[][]? _linesData;
        // Подсказки о сполбцах кроссворда
        private int[][]? _colsData;
        // Длина стороны X
        private int _sideX;
        // Длина стороны Y
        private int _sideY;
        // Данные решенного кроссворда
        private bool[,]? _boardData;
        // Данные активной доски, с ней взаимодействует пользователь
        private bool[,]? _playingBoard;
        
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ChineseCrosswordBoard()
        {
            _linesData = null;
            _colsData = null;
            _sideX = 0;
            _sideY = 0;
            _boardData = null;
            _playingBoard = null;
        }
        /// <summary>
        /// Конструктор, создающий доску разменом X на Y
        /// </summary>
        /// <param name="x">длина стороны x</param>
        /// <param name="y">длина стороны y</param>
        public ChineseCrosswordBoard(int x, int y)
        {
            _linesData = new int[x][];
            _colsData = new int[y][];
            _sideX = x;
            _sideY = y;
            _boardData = new bool[x, y];
            _playingBoard = new bool[x, y];
        }
        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="board">Доска, которую необходимо скопировать</param>
        /// <exception cref="ArgumentNullException">Если <code>board == null</code></exception>
        /// <exception cref="NullReferenceException">Если одно из полей передаваемой доски не инициализировано</exception>
        public ChineseCrosswordBoard(ChineseCrosswordBoard board)
        {
            if (board == null) throw new ArgumentNullException();
            if (board._linesData == null) throw new NullReferenceException();
            if (board._colsData == null) throw new NullReferenceException();
            if (board._boardData == null) throw new NullReferenceException();
            if (board._playingBoard == null) throw new NullReferenceException();
            
            _linesData = (int[][]?) board._linesData.Clone();
            _colsData = (int[][]?) board._colsData.Clone();
            _sideX = board._sideX;
            _sideY = board._sideY;
            _boardData = (bool[,]?) board._boardData.Clone();
            _playingBoard = (bool[,]?) board._playingBoard.Clone();
        }
        /// <summary>
        /// Конструктор, создающий доску по передаваемому ей массиву
        /// </summary>
        /// <param name="data">Разметка будущей доски</param>
        /// <exception cref="ArgumentNullException">Если передано значение null</exception>
        public ChineseCrosswordBoard(bool[,] data)
        {
            if (data == null) throw new ArgumentNullException();
            
            _sideX = data.GetLength(0);
            _sideY = data.GetLength(1);
            _boardData = (bool[,]?) data.Clone();
            _playingBoard = new bool[_sideX, _sideY];
            
            CountLineCells();
            CountColCells();
        }
        /// <summary>
        /// Считает подсказку по строкам
        /// </summary>
        private void CountLineCells()
        {
            _linesData = new int[_sideX][];
            for (int i = 0; i != _sideX; ++i)
            {
                
                LinkedList<int> tmpList = new LinkedList<int>();
                int ptr = 0;
                bool flag = false;
                int j = 0;
                while (j < _sideY)
                {
                    if (!flag && _boardData![i, j])
                    {
                        ptr = j;
                        flag = true;
                    }

                    if (flag && !_boardData![i, j])
                    {
                        tmpList.AddLast(j - ptr);
                        flag = false;
                    }
                    ++j;
                }
                if (flag)
                {
                    tmpList.AddLast(j - ptr);
                }
                
                _linesData[i] = tmpList.ToArray();
            }
        }
        /// <summary>
        /// Считает подсказку по столбцам
        /// </summary>
        private void CountColCells()
        {
            _colsData = new int[_sideY][];
            for (int i = 0; i != _sideY; ++i)
            {
                
                LinkedList<int> tmpList = new LinkedList<int>();
                int ptr = 0;
                bool flag = false;
                int j = 0;
                while (j < _sideX)
                {
                    if (!flag && _boardData![j, i])
                    {
                        ptr = j;
                        flag = true;
                    }

                    if (flag && !_boardData![j, i])
                    {
                        tmpList.AddLast(j - ptr);
                        flag = false;
                    }
                    ++j;
                }
                if (flag)
                {
                    tmpList.AddLast(j - ptr);
                }
                
                _colsData[i] = tmpList.ToArray();
            }
        }
        
        /// <summary>
        /// Возвращает длину стороны X
        /// </summary>
        /// <returns>sideX</returns>
        public int GetSideX()
        {
            return _sideX;
        }
        /// <summary>
        /// Возвращает длину стороны Y
        /// </summary>
        /// <returns>sideY</returns>
        public int GetSideY()
        {
            return _sideY;
        }
        /// <summary>
        /// Возвращает разметку решения кроссворда
        /// </summary>
        /// <returns>boadrData</returns>
        /// <exception cref="NullReferenceException">Если разметка не задана</exception>
        public bool[,] GetBoard()
        {
            if (_boardData == null) throw new NullReferenceException();
            return (bool[,]) _boardData.Clone();
        }
        /// <summary>
        /// Возвращает ссылку на текущее решение кроссворда
        /// </summary>
        /// <returns></returns>
        public bool[,]? GetRealBoard()
        {
            return _boardData;
        }
        /// <summary>
        /// Возвращает индекс поля
        /// </summary>
        /// <param name="x">координата x</param>
        /// <param name="y">координата y</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Если переданные аргументы не входят в диапозон значений</exception>
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
        /// Создает новую доску указанного размера
        /// </summary>
        /// <param name="x">длина стороны x</param>
        /// <param name="y">длина стороны y</param>
        /// <exception cref="ArgumentOutOfRangeException">если x или y меньше 1</exception>
        public void RecreateBoard(int x, int y)
        {
            if (x < 1) throw new ArgumentOutOfRangeException();
            if (y < 1) throw new ArgumentOutOfRangeException();
            
            _sideX = x;
            _sideY = y;
            _boardData = new bool[x, y];
            _playingBoard = new bool[x, y];
            CountColCells();
            CountLineCells();
        }
        /// <summary>
        /// Создает новую квадратную доску указанного размера
        /// </summary>
        /// <param name="a">длина стороны доски</param>
        /// <exception cref="ArgumentOutOfRangeException">если a меньше 1</exception>
        public void RecreateBoard(int a)
        {
            RecreateBoard(a,a);
        }
        /// <summary>
        /// Создает новую доску с переданным ей решением
        /// </summary>
        /// <param name="data">разметка решения кроссворда</param>
        /// <exception cref="ArgumentNullException">если передано значение null</exception>
        public void SetBoard(bool[,] data)
        {
            if (data == null) throw new ArgumentNullException();
            
            _boardData = (bool[,]) data.Clone();
            _sideX = data.GetLength(0);
            _sideY = data.GetLength(1);
            _playingBoard = new bool[_sideX, _sideY];
            CountColCells();
            CountLineCells();
        }
        /// <summary>
        /// Возвращает количество полей доски
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return _sideX * _sideY;
        }
        /// <summary>
        /// Создает новую доску со случайным решенеием кроссворда и очищает игровую доску
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public void RandomFillBoard()
        {
            if (_boardData == null) throw new NullReferenceException();

            Random rand = new Random();
            for (int i = 0; i != _sideX * _sideY; ++i)
            {
                _boardData[i % _sideX, i / _sideX] = !Convert.ToBoolean(rand.Next(0,3));
            }

            _playingBoard = new bool[_sideX, _sideY];
            CountColCells();
            CountLineCells();
        }
        /// <summary>
        /// Отмечает/очищает поле игровой доски по переданному индексу
        /// </summary>
        /// <param name="index">передаваемый индекс</param>
        /// <exception cref="NullReferenceException">если игровая доска не инициализирована</exception>
        /// <exception cref="ArgumentOutOfRangeException">если передан некорректный индекс</exception>
        public void SetCell(int index)
        {
            if (_playingBoard == null) throw new NullReferenceException();
            if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
            _playingBoard[index % _sideX, index / _sideX] = !_playingBoard[index % _sideX, index / _sideX];
        }
        /// <summary>
        /// Отмечает/очищает поле игровой доски по переданным координатам
        /// </summary>
        /// <param name="x">координата x</param>
        /// <param name="y">координата y</param>
        /// <exception cref="NullReferenceException">если игровая доска не инициализирована</exception>
        /// <exception cref="ArgumentOutOfRangeException">если переданы некорректные координаты</exception>
        public void SetCell(int x, int y)
        {
            if (_playingBoard == null) throw new NullReferenceException();
            if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
            if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
            _playingBoard[x, y] = !_playingBoard[x, y];
        }
        /// <summary>
        /// Возвращает значение клетки игровой доски по переданным координатам
        /// </summary>
        /// <param name="x">координата x</param>
        /// <param name="y">координата y</param>
        /// <returns>значение true или false, в зависимости от того, отмечена ли клетка</returns>
        /// <exception cref="NullReferenceException">если игровая доска не инициализирована</exception>
        /// <exception cref="ArgumentOutOfRangeException">если переданы некорректные координаты</exception>
        public int GetCell(int x, int y)
        {
            if (_playingBoard == null) throw new NullReferenceException();
            if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
            if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
            return Convert.ToInt16(_playingBoard[x, y]);
        }
        /// <summary>
        /// Возвращает значение клетки игровой доски по переданному индексу
        /// </summary>
        /// <param name="index">передаваемый индекс</param>
        /// <returns>значение true или false, в зависимости от того, отмечена ли клетка</returns>
        /// <exception cref="NullReferenceException">если игровая доска не инициализирована</exception>
        /// <exception cref="ArgumentOutOfRangeException">если переданы некорректный индекс</exception>
        public int GetCell(int index)
        {
            if (_playingBoard == null) throw new NullReferenceException();
            if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
            return Convert.ToInt16(_playingBoard[index % _sideX, index / _sideX]);
        }
        /// <summary>
        /// Проверяет доску на завершенность
        /// </summary>
        /// <returns>true, если доска завершена<br/>
        ///          false в противном случае</returns>
        /// <exception cref="NullReferenceException">Если доска не инициализирована</exception>
        public bool CheckBoard()
        {
            if (_boardData == null) throw new NullReferenceException();
            if (_playingBoard == null) throw new NullReferenceException();

            for (int i = 0; i != _sideX; ++i)
            {
                for (int j = 0; j != _sideY; ++j)
                {
                    if (_playingBoard[i, j] != _boardData[i, j]) return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Преобразует доску к строке
        /// </summary>
        /// <returns>Игровое поле с подсказками</returns>
        /// <exception cref="NullReferenceException">Если доска либо подсказки не инициализированы</exception>
        public override string ToString()
        {
            if (_colsData == null) throw new NullReferenceException();
            if (_linesData == null) throw new NullReferenceException();
            if (_playingBoard == null) throw new NullReferenceException();
            
            StringBuilder builder = new StringBuilder();
            int maxNumberCols = 0;
            int maxNumberLines = 0;
            // Определяем максимальные смещения
            for (int i = 0; i != _sideY; ++i)
            {
                maxNumberCols = Math.Max(_colsData[i].Length,maxNumberCols);
            }
            for (int i = 0; i != _sideX; ++i)
            {
                maxNumberLines = Math.Max(_linesData[i].Length,maxNumberLines);
            }
            
            // Строим заголовок
            for (int i = 0; i != maxNumberCols; ++i)
            {
                // Смещение 
                for (int n = 0; n < maxNumberLines; ++n)
                {
                    builder.Append("  ");
                }
                // Поссказки по вертикалям
                for (int j = 0; j < _sideY; ++j)
                {
                    if (_colsData[j].Length > i)
                    {
                        builder.Append('/');
                        builder.Append(_colsData[j][i]);
                    }
                    else
                    {
                        builder.Append("/ ");
                    }
                }

                builder.Append('/');
                builder.AppendLine();
            }
            
            // Основная часть
            for (int i = 0; i != _sideX; ++i)
            {
                //Подсказки по горизонталям
                for (int j = 0; j < maxNumberLines; ++j)
                {
                    if (_linesData[i].Length > j)
                    {
                        builder.Append(_linesData[i][j]);
                        if (_linesData[i].Length > j + 1)
                        {
                            builder.Append('-');
                        }
                        else
                        {
                            builder.Append(' ');
                        }
                    }
                    else
                    {
                        builder.Append("  ");
                    }
                }
                // Сами данные
                for (int n = 0; n != _sideY; ++n)
                {
                    builder.Append('|');
                    if (_playingBoard[i, n])
                    {
                        builder.Append(char.ConvertFromUtf32(9632));
                    }
                    else
                    {
                        builder.Append(' ');
                    }
                }

                builder.Append('|');
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}