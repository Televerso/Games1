using System.Net.NetworkInformation;
using System.Security.AccessControl;

namespace BoardGame.SingleChessGame
{
    /// <summary>
    /// Класс клетки шахматной доски
    /// </summary>
    public class SingleChessSquare
    {
        // Координата один клетки на игровом поле (вертикали)
        private readonly int _coordinate1;
        // Координата два клетки на игровом поле (горизонтали)
        private readonly int _coordinate2;
        
        // Флажок фигуры
        private bool _hasAPiece;
        // Тип фигуры
        private SingleChessPiece? _thePiece;
        // Цвет клетки
        private readonly char _color;
        
        // Выделена ли клетка
        private bool _isMarked;
        // Выбрана ли клетка
        private bool _isSelected;
        
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SingleChessSquare()
        {
            _coordinate1 = 0;
            _coordinate2 = 0;
            _hasAPiece = false;
            _thePiece = null;
            _color = ' ';
            _isMarked = false;
            _isSelected = false;
        }
        
        /// <summary>
        /// Конструктор клетки по ее координатам
        /// </summary>
        /// <param name="coord1">координата по вертикалям</param>
        /// <param name="coord2">координата по горизонтали</param>
        /// <exception cref="ArgumentException">координата меньше 0 или больше 8</exception>
        public SingleChessSquare(int coord1, int coord2)
        {
            if (coord1 is < 0 or > 8) throw new ArgumentException();
            if (coord2 is < 0 or > 8) throw new ArgumentException();

            _coordinate1 = coord1;
            _coordinate2 = coord2;
            _hasAPiece = false;
            _thePiece = null;
            if ((coord1 + coord2) % 2 == 0)
            {
                _color = 'B';
            }
            else
            {
                _color = 'W';
            }

            _isMarked = false;
            _isSelected = false;
        }
        /// <summary>
        /// Конструктор клекти
        /// </summary>
        /// <param name="coord1">координата по вертикалям</param>
        /// <param name="coord2">координата по горизонтали</param>
        /// <param name="piece">символьное обозначение фигуры, занимающей клетку</param>
        /// <exception cref="ArgumentException">координата меньше 0 или больше 8</exception>
        public SingleChessSquare(int coord1, int coord2, char piece)
        {
            if (coord1 is < 0 or > 8) throw new ArgumentException();
            if (coord2 is < 0 or > 8) throw new ArgumentException();

            _coordinate1 = coord1;
            _coordinate2 = coord2;

            _thePiece = new SingleChessPiece(piece);
            _hasAPiece = true;
            if ((coord1 + coord2) % 2 == 0)
            {
                _color = 'B';
            }
            else
            {
                _color = 'W';
            }

            _isMarked = false;
            _isSelected = false;
        }
        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="square">копируемая клетка</param>
        public SingleChessSquare(SingleChessSquare square)
        {
            _coordinate1 = square._coordinate1;
            _coordinate2 = square._coordinate2;

            if (square._hasAPiece)
            {
                _hasAPiece = true;
                _thePiece = new SingleChessPiece(square._thePiece!);
            }
            else
            {
                _hasAPiece = false;
                _thePiece = null;
            }

            _color = square._color;
            _isMarked = square._isMarked;
            _isSelected = square._isSelected;
        }
        /// <summary>
        /// Возвращает координаты текущей клетки
        /// </summary>
        /// <returns>2 символа</returns>
        /// <example>
        /// c,4
        /// </example>
        public char[] GetCoordinates()
        {
            if (_coordinate1 == 0 || _coordinate2 == 0) return new char[2] {'0', '0'};
            char[] a = new char[2]
            {
                (char) (_coordinate1 + 96),
                (char) (_coordinate2 + 48)
            };
            return a;
        }
        /// <summary>
        /// Проверяет, не занята ли текущая клетка фигурой
        /// </summary>
        /// <returns>bool</returns>
        public bool IsOccupied()
        {
            return _hasAPiece;
        }
        /// <summary>
        /// Возвращает первую координату вертикалей
        /// </summary>
        /// <returns>int</returns>
        public int GetCoord1()
        {
            return _coordinate1;
        }
        /// <summary>
        /// Возвращает вторую координату горизотналей
        /// </summary>
        /// <returns>int</returns>
        public int GetCoord2()
        {
            return _coordinate2;
        }
        /// <summary>
        /// Возвращает ссылку на текущую фигуру<br/>
        /// Может вернуть null
        /// </summary>
        /// <returns>SingleChessPiece?</returns>
        public SingleChessPiece? GetPiece()
        {
            return _thePiece;
        }
        /// <summary>
        /// Возвращает цвет данной клетки
        /// </summary>
        /// <returns>char</returns>
        public char GetColor()
        {
            return _color;
        }
        /// <summary>
        /// Устанавливает фигуру на клетку
        /// </summary>
        /// <param name="piece">символьное обозначение фигуры</param>
        public void SetPiece(char piece)
        {
            _hasAPiece = true;
            _thePiece = new SingleChessPiece(piece);
        }
        /// <summary>
        /// Устанавливает фигуру на клетку
        /// </summary>
        /// <param name="piece">сама фигура</param>
        /// TODO проверить копирование по ссылке
        public void SetPiece(SingleChessPiece piece)
        {
            _hasAPiece = true;
            _thePiece = new SingleChessPiece(piece);
        }
        /// <summary>
        /// Убирает фигуру с клетки
        /// </summary>
        public void RemovePiece()
        {
            _thePiece = null;
            _hasAPiece = false;
        }
        /// <summary>
        /// Строковое представление клетки
        /// </summary>
        /// <returns>1-символьную строку</returns>
        public override string ToString()
        {
            if (_hasAPiece) return _thePiece!.ToString();
            if (_isSelected) return "O";
            if (_isMarked) return "x";
            return " ";
        }
        /// <summary>
        /// Отмечает клетку
        /// </summary>
        public void MarkSquare()
        {
            _isMarked = true;
        }
        /// <summary>
        /// Отмечает клетку
        /// </summary>
        public void MarkSquare(bool a)
        {
            _isMarked = a;
        }
        /// <summary>
        /// Убирает отметку
        /// </summary>
        public void UnmarkSquare()
        {
            _isMarked = false;
        }
        /// <summary>
        /// Инвертирует текущее значение отметки
        /// </summary>
        public void InvMarkSquare()
        {
            _isMarked = !_isMarked;
        }
        /// <summary>
        /// Смотрит, отмечена ли клетка
        /// </summary>
        /// <returns></returns>
        public bool IsMarked()
        {
            return _isMarked;
        }
        /// <summary>
        /// Выбирает клетку
        /// </summary>
        public void SelectSquare()
        {
            _isSelected = true;
        }
        /// <summary>
        /// Выбирает клетку
        /// </summary>
        public void SelectSquare(bool a)
        {
            _isSelected = a;
        }
        /// <summary>
        /// Отменяет выбор клетки
        /// </summary>
        public void UnselectSquare()
        {
            _isSelected = false;
        }
        /// <summary>
        /// Смотрит, выбрана ли клетка
        /// </summary>
        /// <returns></returns>
        public bool IsSelected()
        {
            return _isSelected;
        }
        /// <summary>
        /// Инвертирует выбор клетки
        /// </summary>
        public void InvSelectSquare()
        {
            _isSelected = !_isSelected;
        }
    }
}