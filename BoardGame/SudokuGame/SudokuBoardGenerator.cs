using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;

namespace BoardGame.SudokuGame
{

    public class SudokuBoardGenerator
    {
        private static readonly int[] BinaryNr = 
        {
            0x1,
            0x2,
            0x4,
            0x8,
            0x10,
            0x20,
            0x40,
            0x80,
            0x100,
            0x200,
            0x400,
            0x800,
            0x1000,
            0x2000,
            0x4000,
            0x8000,
            0x10000,
            0x20000,
            0x40000,
            0x80000,
            0x100000,
            0x200000,
            0x400000,
            0x800000,
            0x1000000,
            0x2000000,
            0x4000000,
            0x8000000,
            0x10000000,
            0x20000000,
            0x40000000
        };

        private readonly Random _rand;
        private readonly int[,] _boardData;
        private readonly bool[,] _checkerBoard;
        private readonly int[,] _counterBoard;
        private readonly int _size;
        private readonly bool _flag;

        private readonly int[] _possValues;
        public SudokuBoardGenerator(int size)
        {
            if (size > 31) throw new ArgumentOutOfRangeException();

            _rand = new Random();
            _size = size;
            _boardData = new int[_size, _size];
            _checkerBoard = new bool[_size, _size];
            _counterBoard = new int[_size, _size];
            _possValues = new int[_size];

            int s = 0;
            for (int i = 0; i != _size; ++i)
            {
                s |= BinaryNr[i];
            }
            
            for (int i = 0; i != _size; ++i)
            {
                for (int j = 0; j != _size; ++j)
                {
                    _checkerBoard[i, j] = false;
                    _boardData[i, j] = s;
                    _counterBoard[i, j] = _size+1;
                }
            }
            _flag = Math.Ceiling(Math.Sqrt(_size)) - Math.Sqrt(_size) < 0.0000001;
        }

        public int GetSize()
        {
            return _size;
        }

        public int[,] GetBoard()
        {
            return (int[,]) _boardData.Clone();
        }

        private int NrOfPossibleValues(int x, int y)
        {
            int a = _boardData[x, y];
            int s = 0;
            for (int i = 0; i != _size; ++i)
            {
                if ((a & 1) == 1) ++s;
                a >>= 1;
            }
            return s;
        }

        private int GetValueFromCurrent(int x, int y)
        {
            int length = NrOfPossibleValues(x,y);
            int ptr = 0;
            int a = _boardData[x, y];
            for (int i = 0; i != _size; ++i)
            {
                if ((a & 1) == 1)
                {
                    _possValues[ptr++]=BinaryNr[i];
                }
                a >>= 1;
            }
            return _possValues[_rand.Next(length)];
        }
        
        private int GetIntFromCurrent(int x, int y)
        {
            int a = _boardData[x, y];
            for (int i = 0; i != _size; ++i)
            {
                if ((a & 1) == 1)
                {
                    return i;
                }
                a >>= 1;
            }
            return 0;
        }
        
        private void MarkLine(int y, int nr)
        {
            nr = -nr;
            --nr;
            for (int i = 0; i != _size; ++i)
            {
                _counterBoard[i, y] = NrOfPossibleValues(i, y);
                if (_counterBoard[i, y] == 1) continue;
                _boardData[i, y] &= nr;
            }

        }
        
        private void MarkCol(int x, int nr)
        {
            nr = -nr;
            --nr;
            for (int i = 0; i != _size; ++i)
            {
                _counterBoard[x,i] = NrOfPossibleValues(x,i);
                if(_counterBoard[x,i] == 1) continue;
                _boardData[x, i] &= nr;
            }
        }
        
        private void MarkSector(int x, int y, int nr)
        {
            nr = -nr;
            --nr;
            
            int sideX = (int) Math.Sqrt(_size);

            int xs = x / sideX;
            int ys = y / sideX;
            int ik,jk;

            for (int i = 0; i != sideX; ++i)
            {
                ik = i + sideX * xs;
                for (int j = 0; j != sideX; ++j)
                {
                    jk = j + sideX * ys;
                    _counterBoard[jk, ik] = NrOfPossibleValues(jk, ik);
                    if(_counterBoard[jk, ik] == 1) continue;
                    _boardData[jk,ik] &= nr;
                }
            }
        }

        private bool CheckBoard()
        {
            foreach (bool i in _checkerBoard)
            {
                if (!i) return false;
            }

            for (int index = 0; index != _size; ++index)
            {
                if (!CheckCol(index)) return false;
                if (!CheckLine(index)) return false;
                if (!_flag && !CheckSector(index)) return false;
            }
            return true;
        }
        
        public void GenerateBoard()
        {
            int x = _rand.Next(_size);
            int y = _rand.Next(_size);
            int n = _size * _size;
            
            for (int cnt = 0; cnt != n; ++cnt)
            {
                int nr = GetValueFromCurrent(x,y);

                MarkLine(y, nr);
                MarkCol(x, nr);
                if (_flag)
                {
                    MarkSector( y,x, nr);
                }
                
                _boardData[x, y] ^= _boardData[x,y];
                _boardData[x, y] |= nr;
                _checkerBoard[x, y] = true;
                _counterBoard[x, y] = 1;


                int i;
                int j;
                for (i = 0; i != _size; ++i)
                {
                    for (j = 0; j != _size; ++j)
                    {
                        if (_checkerBoard[x, y])
                        {
                            x = i;
                            y = j;
                        }
                        else if (_counterBoard[i,j] < _counterBoard[x, y])
                        {
                            if (!_checkerBoard[i, j])
                            {
                                x = i;
                                y = j;
                            }
                        }
                    }
                }
            }
        }

        public void GenerateStable()
        {
            do
            {
                Clear();
                GenerateBoard();
            } while (!CheckBoard());
        }

        public void Clear()
        {
            int s = 0;
            for (int i = 0; i != _size; ++i)
            {
                s |= BinaryNr[i];
            }
            
            for (int i = 0; i != _size; ++i)
            {
                for (int j = 0; j != _size; ++j)
                {
                    _checkerBoard[i, j] = false;
                    _boardData[i, j] = s;
                    _counterBoard[i, j] = _size;
                }
            }
        }
        
        public int[,] ToFullSudokuBoard()
        {
            int[,] board = new int[_size, _size];
            for (int i = 0; i != _size; ++i)
            {
                for (int j = 0; j != _size; ++j)
                {
                    board[i,j] = GetIntFromCurrent(i,j)+1;
                }
            }

            return board;
        }
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i != _size; ++i)
            {
                for (int j = 0; j != _size; ++j)
                {
                    builder.Append('|');
                    builder.Append(GetIntFromCurrent(i,j)+1);
                }

                builder.Append('|');
                builder.AppendLine();
            }

            return builder.ToString();
        }
        
        
        private bool CheckLine(int y)
        {
            int value = 0;
            for (int i = 0; i != _size; ++i)
            {
                value ^= _boardData[i, y];
            }
            for (int i = 0; i != _size; ++i)
            {
                if ((value & 1) != 1)
                {
                    return false;
                }
                value >>= 1;
            }

            return true;

        }
        
        private bool CheckCol(int x)
        {
            int value = 0;
            for (int i = 0; i != _size; ++i)
            {
                value ^= _boardData[x, i];
            }
            for (int i = 0; i != _size; ++i)
            {
                if ((value & 1) != 1)
                {
                    return false;
                }
                value >>= 1;
            }

            return true;
        }
        
        private bool CheckSector(int index)
        {
            int sideX = (int) Math.Sqrt(_size);

            int x = index % sideX;
            int y = index / sideX;

            int value = 0;
            for (int i = 0; i != sideX; ++i)
            {
                for (int j = 0; j != sideX; ++j)
                {
                    value ^= _boardData[j + sideX * y, i + sideX * x];
                }
            }

            for (int i = 0; i != _size; ++i)
            {
                if ((value & 1) != 1)
                {
                    return false;
                }
                value >>= 1;
            }

            return true;
        }
    }
}