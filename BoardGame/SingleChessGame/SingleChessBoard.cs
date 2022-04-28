using System.Text;

namespace BoardGame.SingleChessGame;

public class SingleChessBoard
{
    private SingleChessSquare[,] _boardData;
    private int[]? _selectedSquare;
    private Stack<CoordinatePiece[]> _history;

    public SingleChessBoard()
    {
        _boardData = new SingleChessSquare[8, 8];
        for (int i = 0; i != 8; ++i)
        {
            for (int j = 0; j != 8; ++j)
            {
                _boardData[i,j] = new SingleChessSquare(j+1,8-i);
            }
        }
        _selectedSquare = null;
        _history = new Stack<CoordinatePiece[]>();
    }

    public SingleChessBoard(SingleChessBoard board)
    {
        _boardData = (SingleChessSquare[,]) board._boardData.Clone();
        _selectedSquare = board._selectedSquare;
        _history = board._history;
    }

    public void AddPiece(int coord1, int coord2, char piece)
    {
        _boardData[coord1, coord2].SetPiece(piece);
    }


    public int GetCellIndex(int x, int y)
    {
        return x + y * 8;
    }

    public int GetCellXByIndex(int index)
    {
        return index % 8;
    }

    public int GetCellYByIndex(int index)
    {
        return index / 8;
    }
    public void RecreateBoard()
    {
        foreach (var i in _boardData)
        {
            i.RemovePiece();
        }
    }
    public void RecreateBoard(int a)
    {
        RecreateBoard();
    }

    public void RecreateBoard(int x, int y)
    {
        RecreateBoard();
    }

    public int GetCount()
    {
        return 64;
    }

    public int GetXSide()
    {
        return 8;
    }

    public int GetYSide()
    {
        return 8;
    }

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

    public void RandomFillBoard()
    {
        throw new NotImplementedException();
    }

    public void SetCell(int index, int val)
    {
        throw new NotImplementedException();
    }

    public void SetCell(int x, int y, int val)
    {
        throw new NotImplementedException();
    }

    public int GetCell(int x, int y)
    {
        throw new NotImplementedException();
    }

    public int GetCell(int index)
    {
        throw new NotImplementedException();
    }
    
    public SingleChessSquare GetSquare(int x, int y)
    {
        return _boardData[x, y];
    }

    public SingleChessSquare GetSquare(int index)
    {
        return _boardData[index % 8, index / 8];
    }

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

    public void SelectSquare(int x, int y)
    {
        int i;
        int j;
        if (_selectedSquare != null)
        {
            UnselectSquare(_selectedSquare[0],_selectedSquare[1]);
        }
        
        _selectedSquare = new int[2]{x, y};

        if (!_boardData[x, y].IsOccupied())
        {
            _boardData[x,y].InvSelectSquare();
            return;
        }

        if (_boardData[x, y].GetPiece()!.GetRemainingMoves() == 0)
        {
            _boardData[x,y].InvSelectSquare();
            return;
        }
        switch (_boardData[x,y].GetPiece()!.GetPieceChar())
        {
            case 'K':
                _boardData[x,y].InvSelectSquare();
                
                if (x != 0 && y != 0) _boardData[x-1,y-1].InvMarkSquare();
                if (x != 0) _boardData[x-1,y].InvMarkSquare();
                if (y != 0) _boardData[x,y-1].InvMarkSquare();
                if (x != 8 && y != 8) _boardData[x+1,y+1].InvMarkSquare();
                if (x != 8) _boardData[x+1,y].InvMarkSquare();
                if (y != 8) _boardData[x,y+1].InvMarkSquare();
                if (x != 0 && y != 8) _boardData[x-1,y+1].InvMarkSquare();
                if (x != 8 && y != 0) _boardData[x+1,y-1].InvMarkSquare();
                
                break;
            
            case 'Q':
                _boardData[x,y].InvSelectSquare();
                
                i = x;
                j = y;
                while (i != 0 && j != 0)
                {
                    _boardData[--i,--j].InvMarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }

                i = x;
                j = y;
                while (i != 0)
                {
                    _boardData[--i,y].InvMarkSquare();
                    if (_boardData[i, y].IsOccupied()) break;
                }

                while (j != 0)
                {
                    _boardData[x,--j].InvMarkSquare();
                    if (_boardData[x, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 7 && j != 7)
                {
                    _boardData[++i,++j].InvMarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 7)
                {
                    _boardData[++i,y].InvMarkSquare();
                    if (_boardData[i, y].IsOccupied()) break;
                }

                while (j != 7)
                {
                    _boardData[x,++j].InvMarkSquare();
                    if (_boardData[x, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 0 && j != 7)
                {
                    _boardData[--i,++j].InvMarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 7 && j != 0)
                {
                    _boardData[++i,--j].InvMarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                break;
            
            case 'R':
                _boardData[x,y].InvSelectSquare();
                i = x;
                j = y;
                while (i != 0)
                {
                    _boardData[--i,y].InvMarkSquare();
                    if (_boardData[i, y].IsOccupied()) break;
                }

                while (j != 0)
                {
                    _boardData[x,--j].InvMarkSquare();
                    if (_boardData[x, j].IsOccupied()) break;
                }
                i = x;
                j = y;
                while (i != 7)
                {
                    _boardData[++i,y].InvMarkSquare();
                    if (_boardData[i, y].IsOccupied()) break;
                }

                while (j != 7)
                {
                    _boardData[x,++j].InvMarkSquare();
                    if (_boardData[x, j].IsOccupied()) break;
                }
                
                break;
            
            case 'B':
                _boardData[x,y].InvSelectSquare();
                
                i = x;
                j = y;
                while (i != 0 && j != 0)
                {
                    _boardData[--i,--j].InvMarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }

                i = x;
                j = y;
                while (i != 7 && j != 7)
                {
                    _boardData[++i,++j].InvMarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 0 && j != 7)
                {
                    _boardData[--i,++j].InvMarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 7 && j != 0)
                {
                    _boardData[++i,--j].InvMarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                break;
            
            case 'N':
                _boardData[x,y].InvSelectSquare();
                if (x-2 >= 0 && y-1 >= 0) _boardData[x-2,y-1].InvMarkSquare();
                if (x-1 >= 0 && y-2 >= 0) _boardData[x-1,y-2].InvMarkSquare();
                
                if (x+2 < 7 && y+1 < 7) _boardData[x+2,y+1].InvMarkSquare();
                if (x+1 < 7 && y+2 < 7) _boardData[x+1,y+2].InvMarkSquare();
                
                if (x-2 >= 0 && y+1 < 7) _boardData[x-2,y+1].InvMarkSquare();
                if (x+1 < 7 && y-2 >= 0) _boardData[x+1,y-2].InvMarkSquare();
                
                if (x+2 < 7 && y-1 >= 0) _boardData[x+2,y-1].InvMarkSquare();
                if (x-1 >= 0 && y+2 < 7) _boardData[x-1,y+2].InvMarkSquare();
                break;
        }
    }
    
    private void UnselectSquare(int x, int y)
    {
        if (!_boardData[x, y].IsOccupied())
        {
            _boardData[x,y].UnselectSquare();
            return;
        }

        int i;
        int j;
        
        switch (_boardData[x,y].GetPiece()!.GetPieceChar())
        {
            case 'K':
                _boardData[x,y].UnselectSquare();
                
                if (x != 0 && y != 0) _boardData[x-1,y-1].UnmarkSquare();
                if (x != 0) _boardData[x-1,y].UnmarkSquare();
                if (y != 0) _boardData[x,y-1].UnmarkSquare();
                if (x != 8 && y != 8) _boardData[x+1,y+1].UnmarkSquare();
                if (x != 8) _boardData[x+1,y].UnmarkSquare();
                if (y != 8) _boardData[x,y+1].UnmarkSquare();
                if (x != 0 && y != 8) _boardData[x-1,y+1].UnmarkSquare();
                if (x != 8 && y != 0) _boardData[x+1,y-1].UnmarkSquare();
                
                break;
            
            case 'Q':
                _boardData[x,y].UnselectSquare();
                
                i = x;
                j = y;
                while (i != 0 && j != 0)
                {
                    _boardData[--i,--j].UnmarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }

                i = x;
                j = y;
                while (i != 0)
                {
                    _boardData[--i,y].UnmarkSquare();
                    if (_boardData[i, y].IsOccupied()) break;
                }

                while (j != 0)
                {
                    _boardData[x,--j].UnmarkSquare();
                    if (_boardData[x, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 7 && j != 7)
                {
                    _boardData[++i,++j].UnmarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 7)
                {
                    _boardData[++i,y].UnmarkSquare();
                    if (_boardData[i, y].IsOccupied()) break;
                }

                while (j != 7)
                {
                    _boardData[x,++j].UnmarkSquare();
                    if (_boardData[x, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 0 && j != 7)
                {
                    _boardData[--i,++j].UnmarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 7 && j != 0)
                {
                    _boardData[++i,--j].UnmarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                break;
            
            case 'R':
                _boardData[x,y].UnselectSquare();
                i = x;
                j = y;
                while (i != 0)
                {
                    _boardData[--i,y].UnmarkSquare();
                    if (_boardData[i, y].IsOccupied()) break;
                }

                while (j != 0)
                {
                    _boardData[x,--j].UnmarkSquare();
                    if (_boardData[x, j].IsOccupied()) break;
                }
                i = x;
                j = y;
                while (i != 7)
                {
                    _boardData[++i,y].UnmarkSquare();
                    if (_boardData[i, y].IsOccupied()) break;
                }

                while (j != 7)
                {
                    _boardData[x,++j].UnmarkSquare();
                    if (_boardData[x, j].IsOccupied()) break;
                }
                
                break;
            
            case 'B':
                _boardData[x,y].UnselectSquare();
                
                i = x;
                j = y;
                while (i != 0 && j != 0)
                {
                    _boardData[--i,--j].UnmarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }

                i = x;
                j = y;
                while (i != 7 && j != 7)
                {
                    _boardData[++i,++j].UnmarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 0 && j != 7)
                {
                    _boardData[--i,++j].UnmarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                i = x;
                j = y;
                while (i != 7 && j != 0)
                {
                    _boardData[++i,--j].UnmarkSquare();
                    if (_boardData[i, j].IsOccupied()) break;
                }
                
                break;
            
            case 'N':
                _boardData[x,y].UnselectSquare();
                if (x-2 >= 0 && y-1 >= 0) _boardData[x-2,y-1].UnmarkSquare();
                if (x-1 >= 0 && y-2 >= 0) _boardData[x-1,y-2].UnmarkSquare();
                
                if (x+2 < 7 && y+1 < 7) _boardData[x+2,y+1].UnmarkSquare();
                if (x+1 < 7 && y+2 < 7) _boardData[x+1,y+2].UnmarkSquare();
                
                if (x-2 >= 0 && y+1 < 7) _boardData[x-2,y+1].UnmarkSquare();
                if (x+1 < 7 && y-2 >= 0) _boardData[x+1,y-2].UnmarkSquare();
                
                if (x+2 < 7 && y-1 >= 0) _boardData[x+2,y-1].UnmarkSquare();
                if (x-1 >= 0 && y+2 < 7) _boardData[x-1,y+2].UnmarkSquare();
                break;
        }
    }

    public void MovePiece(int x, int y)
    {
        if (_selectedSquare == null) return;
        if (!_boardData[_selectedSquare[0], _selectedSquare[1]].IsOccupied()) return;
        if (!_boardData[x, y].IsMarked()) return;
        if (_boardData[_selectedSquare[0], _selectedSquare[1]].GetPiece()!.GetRemainingMoves() == 0) return;
        if (_boardData[x, y].GetPiece()!.GetPieceChar() == 'K') return;
        
        StoreBoard();
        UnselectSquare(_selectedSquare[0],_selectedSquare[1]);
        _boardData[x, y].SetPiece(_boardData[_selectedSquare[0], _selectedSquare[1]].GetPiece()!.Move());
        _boardData[_selectedSquare[0],_selectedSquare[1]].RemovePiece();
        _selectedSquare = null;
    }

    private void StoreBoard()
    {
        int n = 0;
        foreach (var i in _boardData)
        {
            if (i.IsOccupied()) ++n;
        }

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
        _history.Push(currData);
    }

    private void ReceiveBoard()
    {
        if (!_history.TryPop(out CoordinatePiece[]? currData)) return;
        
        RecreateBoard();

        foreach (var i in currData)
        {
            _boardData[i.GetX(),i.GetY()].SetPiece(i.GetPiece());
        }
    }

    public void HistoryBackward()
    {
        ReceiveBoard();
    }

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