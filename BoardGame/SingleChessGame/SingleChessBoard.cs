using System.Text;

namespace BoardGame.SingleChessGame
{
    /// <summary>
    /// Класс шахматной доски
    /// </summary>
    public class SingleChessBoard
    {
        // Данные доски
        private SingleChessSquare[,] _boardData;

        // координаты выбранной клетки
        private int[]? _selectedSquare;

        // Испория позиций, котрые были на доске
        private Stack<CoordinatePiece[]> _history;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SingleChessBoard()
        {
            _boardData = new SingleChessSquare[8, 8];
            for (int i = 0; i != 8; ++i)
            {
                for (int j = 0; j != 8; ++j)
                {
                    _boardData[i, j] = new SingleChessSquare(j + 1, 8 - i);
                }
            }

            _selectedSquare = null;
            _history = new Stack<CoordinatePiece[]>();
        }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="board">копируемая доска</param>
        public SingleChessBoard(SingleChessBoard board)
        {
            if (board == null) throw new ArgumentNullException();
            _boardData = (SingleChessSquare[,]) board._boardData.Clone();
            if (board._selectedSquare == null)
            {
                _selectedSquare = null;
            }
            else
            {
                _selectedSquare = (int[]?) board._selectedSquare.Clone();
            }

            _history = new Stack<CoordinatePiece[]>(board._history);
        }

        /// <summary>
        /// Добавляет фигуру на заданную клетку
        /// </summary>
        /// <param name="coord1">индекс один клетки</param>
        /// <param name="coord2">индекс два клетки</param>
        /// <param name="piece">символьное обозначение фигуры</param>
        public void AddPiece(int coord1, int coord2, char piece)
        {
            if (coord1 < 0 || coord2 < 0) throw new ArgumentOutOfRangeException();
            if (coord1 > 7 || coord2 > 7) throw new ArgumentOutOfRangeException();
            _boardData[coord1, coord2].SetPiece(piece);
        }

        /// <summary>
        /// Возвращает индекс поля с координатами (x,y)
        /// </summary>
        /// <param name="x">координата x</param>
        /// <param name="y">координата y</param>
        /// <returns>index</returns>
        public int GetCellIndex(int x, int y)
        {
            if (x < 0 || y < 0) throw new ArgumentOutOfRangeException();
            if (x > 7 || y > 7) throw new ArgumentOutOfRangeException();
            return x + y * 8;
        }

        /// <summary>
        /// Возвращает координату X поля с индексом index
        /// </summary>
        /// <param name="index">индекс поля</param>
        /// <returns>координата x</returns>
        public int GetCellXByIndex(int index)
        {
            if (index < 0 || index > 63) throw new ArgumentOutOfRangeException();
            return index % 8;
        }

        /// <summary>
        /// Возвращает координату Y поля с индексом index
        /// </summary>
        /// <param name="index">индекс поля</param>
        /// <returns>координата y</returns>
        public int GetCellYByIndex(int index)
        {
            if (index < 0 || index > 63) throw new ArgumentOutOfRangeException();
            return index / 8;
        }

        /// <summary>
        /// Пересоздает доску (просто очищает ее)
        /// </summary>
        public void RecreateBoard()
        {
            foreach (var i in _boardData)
            {
                i.RemovePiece();
            }
        }

        /// <summary>
        /// Пересоздает доску (просто очищает ее)
        /// </summary>
        public void RecreateBoard(int a)
        {
            RecreateBoard();
        }

        /// <summary>
        /// Пересоздает доску (просто очищает ее)
        /// </summary>
        public void RecreateBoard(int x, int y)
        {
            RecreateBoard();
        }

        /// <summary>
        /// Возвращает количество клеток
        /// </summary>
        /// <returns>64</returns>
        public int GetCount()
        {
            return 64;
        }

        /// <summary>
        /// Возвращает длину стороны X доски
        /// </summary>
        /// <returns>8</returns>
        public int GetXSide()
        {
            return 8;
        }

        /// <summary>
        /// Возвращает длину стороны X доски
        /// </summary>
        /// <returns>8</returns>
        public int GetYSide()
        {
            return 8;
        }

        /// <summary>
        /// Возвращает данные доски в виде двумерного массива
        /// </summary>
        /// <returns></returns>
        public int[,]? GetBoard()
        {
            int[,] array = new int[8, 8];
            for (int i = 0; i != 8; ++i)
            {
                for (int j = 0; j != 8; ++j)
                {
                    if (!_boardData[i, j].IsOccupied())
                    {
                        array[i, j] = 0;
                    }
                    else
                    {
                        switch (_boardData[i, j].GetPiece()!.GetPieceChar())
                        {
                            case 'K':
                                array[i, j] = 1;
                                break;
                            case 'Q':
                                array[i, j] = 2;
                                break;
                            case 'R':
                                array[i, j] = 3;
                                break;
                            case 'B':
                                array[i, j] = 4;
                                break;
                            case 'N':
                                array[i, j] = 5;
                                break;
                            default:
                                array[i, j] = 0;
                                break;
                        }
                    }
                }
            }

            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void RandomFillBoard()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetCell(int index, int val)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="val"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetCell(int x, int y, int val)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetCell(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetCell(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает клетку по указанным индексам
        /// </summary>
        /// <param name="x">индекс x</param>
        /// <param name="y">индекс y</param>
        /// <returns>SingleChessSquare</returns>
        public SingleChessSquare GetSquare(int x, int y)
        {
            if (x < 0 || y < 0) throw new ArgumentOutOfRangeException();
            if (x > 7 || y > 7) throw new ArgumentOutOfRangeException();
            return _boardData[x, y];
        }

        /// <summary>
        /// Возвращает клетку по указанному индексу
        /// </summary>
        /// <param name="index">индекс клетки</param>
        /// <returns>SingleChessSquare</returns>
        public SingleChessSquare GetSquare(int index)
        {
            if (index < 0 || index > 63) throw new ArgumentOutOfRangeException();
            return _boardData[index % 8, index / 8];
        }

        /// <summary>
        /// проверяет доску на завершенность
        /// </summary>
        /// <returns>true, если на доске одна фигура<br/>
        ///          false в любом другом случае</returns>
        public bool CheckBoard()
        {
            bool flag = false;
            for (int i = 0; i != 8; ++i)
            {
                for (int j = 0; j != 8; ++j)
                {
                    if (_boardData[i, j].IsOccupied())
                    {
                        if (flag) return false;
                        flag = true;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Отмечает все клетки значением a, на которые может пойти король из (x,y)
        /// </summary>
        /// <param name="x">индекс x</param>
        /// <param name="y">индекс y</param>
        /// <param name="a">значение a</param>
        private void MarkKingMoves(int x, int y, bool a)
        {
            if (x != 0 && y != 0) _boardData[x - 1, y - 1].MarkSquare(a);
            if (x != 0) _boardData[x - 1, y].MarkSquare(a);
            if (y != 0) _boardData[x, y - 1].MarkSquare(a);
            if (x != 8 && y != 8) _boardData[x + 1, y + 1].MarkSquare(a);
            if (x != 8) _boardData[x + 1, y].MarkSquare(a);
            if (y != 8) _boardData[x, y + 1].MarkSquare(a);
            if (x != 0 && y != 8) _boardData[x - 1, y + 1].MarkSquare(a);
            if (x != 8 && y != 0) _boardData[x + 1, y - 1].MarkSquare(a);
        }

        /// <summary>
        /// Отмечает все клетки значением a, на которые может пойти слон из (x,y)
        /// </summary>
        /// <param name="x">индекс x</param>
        /// <param name="y">индекс y</param>
        /// <param name="a">значение a</param>
        private void MarkBishopMoves(int x, int y, bool a)
        {
            int i = x;
            int j = y;
            while (i != 0 && j != 0)
            {
                _boardData[--i, --j].MarkSquare(a);
                if (_boardData[i, j].IsOccupied()) break;
            }

            i = x;
            j = y;
            while (i != 7 && j != 7)
            {
                _boardData[++i, ++j].MarkSquare(a);
                if (_boardData[i, j].IsOccupied()) break;
            }

            i = x;
            j = y;
            while (i != 0 && j != 7)
            {
                _boardData[--i, ++j].MarkSquare(a);
                if (_boardData[i, j].IsOccupied()) break;
            }

            i = x;
            j = y;
            while (i != 7 && j != 0)
            {
                _boardData[++i, --j].MarkSquare(a);
                if (_boardData[i, j].IsOccupied()) break;
            }
        }

        /// <summary>
        /// Отмечает все клетки значением a, на которые может пойти ладья из (x,y)
        /// </summary>
        /// <param name="x">индекс x</param>
        /// <param name="y">индекс y</param>
        /// <param name="a">значение a</param>
        private void MarkRookMoves(int x, int y, bool a)
        {
            int i = x;
            int j = y;
            while (i != 0)
            {
                _boardData[--i, y].MarkSquare(a);
                if (_boardData[i, y].IsOccupied()) break;
            }

            while (j != 0)
            {
                _boardData[x, --j].MarkSquare(a);
                if (_boardData[x, j].IsOccupied()) break;
            }

            i = x;
            j = y;
            while (i != 7)
            {
                _boardData[++i, y].MarkSquare(a);
                if (_boardData[i, y].IsOccupied()) break;
            }

            while (j != 7)
            {
                _boardData[x, ++j].MarkSquare(a);
                if (_boardData[x, j].IsOccupied()) break;
            }
        }

        /// <summary>
        /// Отмечает все клетки значением a, на которые может пойти конь из (x,y)
        /// </summary>
        /// <param name="x">индекс x</param>
        /// <param name="y">индекс y</param>
        /// <param name="a">значение a</param>
        private void MarkKnightMoves(int x, int y, bool a)
        {
            if (x - 2 >= 0 && y - 1 >= 0) _boardData[x - 2, y - 1].MarkSquare(a);
            if (x - 1 >= 0 && y - 2 >= 0) _boardData[x - 1, y - 2].MarkSquare(a);
            if (x + 2 < 7 && y + 1 < 7) _boardData[x + 2, y + 1].MarkSquare(a);
            if (x + 1 < 7 && y + 2 < 7) _boardData[x + 1, y + 2].MarkSquare(a);
            if (x - 2 >= 0 && y + 1 < 7) _boardData[x - 2, y + 1].MarkSquare(a);
            if (x + 1 < 7 && y - 2 >= 0) _boardData[x + 1, y - 2].MarkSquare(a);
            if (x + 2 < 7 && y - 1 >= 0) _boardData[x + 2, y - 1].MarkSquare(a);
            if (x - 1 >= 0 && y + 2 < 7) _boardData[x - 1, y + 2].MarkSquare(a);
        }

        /// <summary>
        /// Выбирает заданное поле
        /// </summary>
        /// <param name="x">индекс x</param>
        /// <param name="y">индекс y</param>
        public void SelectSquare(int x, int y)
        {
            if (x < 0 || y < 0) throw new ArgumentOutOfRangeException();
            if (x > 7 || y > 7) throw new ArgumentOutOfRangeException();

            // Если уже выбрано какое-то поле, то стрирает эту отметку
            if (_selectedSquare != null)
            {
                UnselectSquare(_selectedSquare[0], _selectedSquare[1]);
            }

            _selectedSquare = new int[2] {x, y};

            // Если на заданном поле нет фигуры
            if (!_boardData[x, y].IsOccupied())
            {
                _boardData[x, y].SelectSquare();
                return;
            }

            // Если у выбранной фигуры не осталось ходов
            if (_boardData[x, y].GetPiece()!.GetRemainingMoves() == 0)
            {
                _boardData[x, y].SelectSquare();
                return;
            }

            // Определяем тип выбранной фигуры, отмечаем соответствующие поля
            switch (_boardData[x, y].GetPiece()!.GetPieceChar())
            {
                case 'K':
                    _boardData[x, y].SelectSquare();
                    MarkKingMoves(x, y, true);
                    break;

                case 'Q':
                    _boardData[x, y].SelectSquare();
                    MarkBishopMoves(x, y, true);
                    MarkRookMoves(x, y, true);
                    break;

                case 'R':
                    _boardData[x, y].SelectSquare();
                    MarkRookMoves(x, y, true);
                    break;

                case 'B':
                    _boardData[x, y].SelectSquare();
                    MarkBishopMoves(x, y, true);
                    break;

                case 'N':
                    _boardData[x, y].SelectSquare();
                    MarkKnightMoves(x, y, true);
                    break;
            }
        }

        /// <summary>
        /// Стираем отместу с данного поля
        /// </summary>
        /// <param name="x">индекс x</param>
        /// <param name="y">индекс y</param>
        private void UnselectSquare(int x, int y)
        {
            if (x < 0 || y < 0) throw new ArgumentOutOfRangeException();
            if (x > 7 || y > 7) throw new ArgumentOutOfRangeException();

            // Если на данном поле нет фигуры
            if (!_boardData[x, y].IsOccupied())
            {
                _boardData[x, y].UnselectSquare();
                return;
            }

            // Определяем тип фигуры
            switch (_boardData[x, y].GetPiece()!.GetPieceChar())
            {
                case 'K':
                    _boardData[x, y].UnselectSquare();
                    MarkKingMoves(x, y, false);
                    break;

                case 'Q':
                    _boardData[x, y].UnselectSquare();
                    MarkBishopMoves(x, y, false);
                    MarkRookMoves(x, y, false);
                    break;

                case 'R':
                    _boardData[x, y].UnselectSquare();
                    MarkRookMoves(x, y, false);
                    break;

                case 'B':
                    _boardData[x, y].UnselectSquare();
                    MarkBishopMoves(x, y, false);
                    break;

                case 'N':
                    _boardData[x, y].UnselectSquare();
                    MarkKnightMoves(x, y, false);
                    break;
            }
        }

        /// <summary>
        /// Совершает ход фигурой
        /// </summary>
        /// <param name="x">индекс x поля, на которое нужно пойти</param>
        /// <param name="y">индекс y поля, на которое нужно пойти</param>
        public void MovePiece(int x, int y)
        {
            if (x < 0 || y < 0) throw new ArgumentOutOfRangeException();
            if (x > 7 || y > 7) throw new ArgumentOutOfRangeException();

            // Если никакое поле не выбрано
            if (_selectedSquare == null) return;
            // Если на выбранном поле нет фигуры
            if (!_boardData[_selectedSquare[0], _selectedSquare[1]].IsOccupied()) return;
            // Если поле, на которое нужно пойти, не является возможным ходом для данной фигуры
            if (!_boardData[x, y].IsMarked()) return;
            // Если мы хотим взять короля
            if (_boardData[x, y].GetPiece()!.GetPieceChar() == 'K') return;

            // Записываем данные доски в историю
            StoreBoard();
            // Убирает уже ненужные отметки возможных ходов
            UnselectSquare(_selectedSquare[0], _selectedSquare[1]);
            // Ходим фигурой на заданной поле
            _boardData[x, y].SetPiece(_boardData[_selectedSquare[0], _selectedSquare[1]].GetPiece()!.Move());
            _boardData[_selectedSquare[0], _selectedSquare[1]].RemovePiece();

            _selectedSquare = null;
        }

        /// <summary>
        /// Сохраняет текущее состояние доски в историю
        /// </summary>
        private void StoreBoard()
        {
            // Считаем количество фигур на доске
            int n = 0;
            foreach (var i in _boardData)
            {
                if (i.IsOccupied()) ++n;
            }

            // Создаем массив и записываем в него фигуры с их координатами
            CoordinatePiece[] currData = new CoordinatePiece[n];
            int k = 0;
            for (int i = 0; i != 8; ++i)
            {
                for (int j = 0; j != 8; ++j)
                {
                    if (_boardData[i, j].IsOccupied())
                    {
                        currData[k++] = new CoordinatePiece(i, j, _boardData[i, j].GetPiece()!);
                    }
                }
            }

            // Добавляем массив в историю
            _history.Push(currData);
        }

        /// <summary>
        /// Достаем последнюю сохраненную доску из истории
        /// </summary>
        private void ReceiveBoard()
        {
            // Пробуем достать доску: если не вышло, возвращаемся
            if (!_history.TryPop(out CoordinatePiece[]? currData)) return;

            // Очищаем доску
            RecreateBoard();

            // Считываем данные и заполняем доску фигурами
            foreach (var i in currData)
            {
                _boardData[i.GetX(), i.GetY()].SetPiece(i.GetPiece());
            }
        }

        /// <summary>
        /// Устанавливаем состояние доски на последее сохраненное в историю
        /// </summary>
        public void HistoryBackward()
        {
            ReceiveBoard();
        }

        /// <summary>
        /// Метод ToString
        /// </summary>
        /// <returns>строковое представление доски</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i != 8; ++i)
            {
                for (int j = 0; j != 8; ++j)
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
}