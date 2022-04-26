using System.Net.NetworkInformation;
using System.Security.AccessControl;

namespace BoardGame.SingleChessGame
{
    /// <summary>
    /// Класс клетки шахматной доски
    /// </summary>
    public class SingleChessSquare
    {
        
        private readonly int _coordinate1;
        private readonly int _coordinate2;

        private bool _hasAPiece;
        private SingleChessPiece? _thePiece;
        private readonly char _color;

        private bool _isMarked;
        private bool _isSelected;

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

        public bool IsOccupied()
        {
            return _hasAPiece;
        }

        public int GetCoord1()
        {
            return _coordinate1;
        }

        public int GetCoord2()
        {
            return _coordinate2;
        }

        public SingleChessPiece? GetPiece()
        {
            return _thePiece;
        }

        public char GetColor()
        {
            return _color;
        }

        public void SetPiece(char piece)
        {
            _hasAPiece = true;
            _thePiece = new SingleChessPiece(piece);
        }

        public void SetPiece(SingleChessPiece piece)
        {
            _hasAPiece = true;
            _thePiece = new SingleChessPiece(piece);
        }

        public void RemovePiece()
        {
            _thePiece = null;
            _hasAPiece = false;
        }

        public override string ToString()
        {
            if (_hasAPiece) return _thePiece!.ToString();
            if (_isSelected) return "O";
            if (_isMarked) return "X";
            return " ";
        }

        public void MarkSquare()
        {
            _isMarked = true;
        }

        public void UnmarkSquare()
        {
            _isMarked = false;
        }

        public void InvMarkSquare()
        {
            _isMarked = !_isMarked;
        }

        public bool IsMarked()
        {
            return _isMarked;
        }

        public void SelectSquare()
        {
            _isSelected = true;
        }

        public void UnselectSquare()
        {
            _isSelected = false;
        }

        public bool IsSelected()
        {
            return _isSelected;
        }

        public void InvSelectSquare()
        {
            _isSelected = !_isSelected;
        }
    }
}