namespace BoardGame.SingleChessGame
{
    /// <summary>
    /// Класс шахматной фигуры
    /// </summary>
    public class SingleChessPiece
    {
        /// <summary>
        /// Возможные типы фигур
        /// </summary>
        internal enum Piece
        {
            King = 0,
            Queen,
            Rook,
            Bishop,
            Knight,
            None
        };
        // Тип фигуры
        private readonly Piece _pieceType;
        // Цвет фигуры
        private char _color;
        // Количество оставшихся ходов у фигуры
        private int _movesLeft;
        
        /// <summary>
        /// Статический метод, конвертирующий тип фигуры в ее символьное обозначение
        /// </summary>
        /// <param name="piece">тип фигуры</param>
        /// <returns><code>char</code></returns>
        internal static char ConvertPiece(Piece piece)
        {
            switch (piece)
            {
                case Piece.King:
                    return 'K';
                case Piece.Queen:
                    return 'Q';
                case Piece.Rook:
                    return 'R';
                case Piece.Bishop:
                    return 'B';
                case Piece.Knight:
                    return 'N';
                default:
                    return '0';
            }
        }
        /// <summary>
        /// Статический метод, конвертирующий символьное обозначение фигуры в ее тип
        /// </summary>
        /// <param name="piece">символьное обозначение фигуры</param>
        /// <returns>Значение типа <code>Piece</code></returns>
        internal static Piece ConvertPiece(char piece)
        {
            switch (piece)
            {
                case 'K':
                    return Piece.King;
                case 'Q':
                    return Piece.Queen;
                case 'R':
                    return Piece.Rook;
                case 'B':
                    return Piece.Bishop;
                case 'N':
                    return Piece.Knight;
                default:
                    return Piece.None;
            }
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SingleChessPiece()
        {
            _pieceType = Piece.None;
            _color = 'B';
            _movesLeft = 0;
        }
        /// <summary>
        /// Конструктор фигуры
        /// </summary>
        /// <param name="piece">символьное обозначение фигуры</param>
        public SingleChessPiece(char piece)
        {
            _pieceType = ConvertPiece(piece);
            _color = 'W';
            _movesLeft = 2;
        }
        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="piece">передаваемая фигура</param>
        public SingleChessPiece(SingleChessPiece piece)
        {
            _pieceType = piece._pieceType;
            _color = piece._color;
            _movesLeft = piece._movesLeft;
        }
        /// <summary>
        /// Совершает операции, связанные с ходом фигуры
        /// </summary>
        /// <returns>модифицированное значение this</returns>
        public SingleChessPiece Move()
        {
            if (_movesLeft == 0) return this;
            if (--_movesLeft == 0)
            {
                _color = 'B';
            }

            return this;
        }
        /// <summary>
        /// Возвращает цвет фигуры
        /// </summary>
        /// <returns><code>char color</code></returns>
        public char GetColor()
        {
            return _color;
        }
        /// <summary>
        /// Возвращает количество оставшихся ходов фигуры
        /// </summary>
        /// <returns>movesLeft</returns>
        public int GetRemainingMoves()
        {
            return _movesLeft;
        }
        /// <summary>
        /// Возвращает символьное обозначение фигуры
        /// </summary>
        /// <returns>char</returns>
        public char GetPieceChar()
        {
            return ConvertPiece(_pieceType);
        }
        /// <summary>
        /// Метод ToString
        /// </summary>
        /// <returns>строковое представление фигуры</returns>
        public override string ToString()
        {
            return ConvertPiece(_pieceType).ToString();
        }
    }
}