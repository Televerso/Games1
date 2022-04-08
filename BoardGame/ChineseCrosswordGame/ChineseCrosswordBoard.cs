using System.Text;

namespace BoardGame.ChineseCrosswordGame
{

    public class ChineseCrosswordBoard
    {
        private int[][]? _linesData;
        private int[][]? _colsData;
        private int _sideX;
        private int _sideY;
        private bool[,]? _boardData;
        private bool[,]? _playingBoard;

        public ChineseCrosswordBoard()
        {
            _linesData = null;
            _colsData = null;
            _sideX = 0;
            _sideY = 0;
            _boardData = null;
            _playingBoard = null;
        }

        public ChineseCrosswordBoard(int x, int y)
        {
            _linesData = new int[x][];
            _colsData = new int[y][];
            _sideX = x;
            _sideY = y;
            _boardData = new bool[x, y];
            _playingBoard = new bool[x, y];
        }

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
        

        public int GetSideX()
        {
            return _sideX;
        }

        public int GetSideY()
        {
            return _sideY;
        }

        public bool[,] GetBoard()
        {
            if (_boardData == null) throw new NullReferenceException();
            return (bool[,]) _boardData.Clone();
        }

        public bool[,]? GetRealBoard()
        {
            return _boardData;
        }

        public int GetCellIndex(int x, int y)
        {
            if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
            if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
            return x + (y * _sideX);
        }
        
        public int GetCellXByIndex(int index)
        {
            if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
            return index % _sideX;
        }
        
        public int GetCellYByIndex(int index)
        {
            if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
            return index / _sideX;
        }

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

        public void RecreateBoard(int a)
        {
            RecreateBoard(a,a);
        }

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

        public int GetCount()
        {
            return _sideX * _sideY;
        }

        public void RandomFillBoard()
        {
            if (_boardData == null) throw new NullReferenceException();

            Random rand = new Random();
            for (int i = 0; i != _sideX * _sideY; ++i)
            {
                _boardData[i % _sideX, i / _sideX] = !Convert.ToBoolean(rand.Next(0,3));
            }
            
            CountColCells();
            CountLineCells();
        }

        public void SetCell(int index)
        {
            if (_playingBoard == null) throw new NullReferenceException();
            if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
            _playingBoard[index % _sideX, index / _sideX] = !_playingBoard[index % _sideX, index / _sideX];
        }

        public void SetCell(int x, int y)
        {
            if (_playingBoard == null) throw new NullReferenceException();
            if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
            if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
            _playingBoard[x, y] = !_playingBoard[x, y];
        }

        public int GetCell(int x, int y)
        {
            if (_playingBoard == null) throw new NullReferenceException();
            if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
            if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
            return Convert.ToInt16(_playingBoard[x, y]);
        }

        public int GetCell(int index)
        {
            if (_playingBoard == null) throw new NullReferenceException();
            if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
            return Convert.ToInt16(_playingBoard[index % _sideX, index / _sideX]);
        }

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

        public override string ToString()
        {
            if (_colsData == null) throw new NullReferenceException();
            if (_linesData == null) throw new NullReferenceException();
            if (_playingBoard == null) throw new NullReferenceException();
            
            StringBuilder builder = new StringBuilder();
            int maxNumberCols = 0;
            int maxNumberLines = 0;
            for (int i = 0; i != _sideY; ++i)
            {
                maxNumberCols = Math.Max(_colsData[i].Length,maxNumberCols);
            }
            for (int i = 0; i != _sideX; ++i)
            {
                maxNumberLines = Math.Max(_linesData[i].Length,maxNumberLines);
            }

            for (int i = 0; i != maxNumberCols; ++i)
            {
                for (int n = 0; n < maxNumberLines; ++n)
                {
                    builder.Append("  ");
                }
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
            
            for (int i = 0; i != _sideX; ++i)
            {
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