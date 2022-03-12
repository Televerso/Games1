using System.Security.Cryptography;

namespace BoardGame.MinesweeperGame;

public class MinesweeperBoard : IBoard
{
    private int _sideX;
    private int _sideY;
    private int[,]? _boardData;
    private int[,]? _layout;
    
    private void CreateBoard(int x, int y)
    {
        _boardData = new int[x, y];
        _layout = null;
        _sideX = x;
        _sideY = y;
        for (int i = 0; i != _sideX*_sideY; ++i)
        {
            _boardData[i % _sideX, i / _sideY] = 0;
        }
    }
    
    public MinesweeperBoard()
    {
        _sideX = 0;
        _sideY = 0;
        _boardData = null;
        _layout = null;
    }
    public MinesweeperBoard(int x, int y)
    {
        if (x < 0 || y < 0) throw new ArgumentOutOfRangeException();
        CreateBoard(x,y);
    }
    public MinesweeperBoard(MinesweeperBoard mb)
    {
        if (mb._boardData == null) throw new ArgumentNullException();
        if (mb._layout == null) throw new ArgumentNullException();
        _sideX = mb._sideX;
        _sideY = mb._sideY;
        _boardData = (int[,]) mb._boardData.Clone();
        _layout = (int[,]) mb._layout.Clone();
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
        return index % _sideY;
    }
    
    public void RecreateBoard(int a)
    {
        if (a < 0) throw new ArgumentOutOfRangeException();
        CreateBoard(a,a);
    }
    public void RecreateBoard(int x, int y)
    {
        if (x < 0) throw new ArgumentOutOfRangeException();
        if (y < 0) throw new ArgumentOutOfRangeException();
        CreateBoard(x,y);
    }

    public int GetCount()
    {
        return _sideX * _sideY;
    }

    public int GetXSide()
    {
        return _sideX;
    }

    public int GetYSide()
    {
        return _sideY;
    }

    public int[,]? GetBoard()
    {
        return _boardData;
    }

    public int[,]? GetLayout()
    {
        return _layout;
    }

    public void RandomFillBoard()
    {
        if (_layout == null) throw new NullReferenceException();
        
        for (int i = 0; i < _sideX * _sideY; ++i)
        {
            _layout[i % _sideX, i / _sideY] = Random.Shared.Next(0, 2);
        }
    }

    public void SetCell(int index, int val)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
        if (val < 0 || val > 1) throw new ArgumentException();
        _boardData[index % _sideX, index / _sideY] = val;;
    }

    public void SetCell(int x, int y, int val)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        if (val < 0 || val > 1) throw new ArgumentException();
        _boardData[x, y] = val;
    }

    public int GetCell(int x, int y)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (x < 0 || x >= _sideX) throw new ArgumentOutOfRangeException();
        if (y < 0 || y >= _sideY) throw new ArgumentOutOfRangeException();
        return _boardData[x,y];
    }

    public int GetCell(int index)
    {
        if (_boardData == null) throw new NullReferenceException();
        if (index < 0 || index >= _sideX * _sideY) throw new ArgumentOutOfRangeException();
        return _boardData[index % _sideX, index / _sideY];
    }

    public bool CheckBoard()
    {
        if (_boardData == null) throw new NullReferenceException();
        if (_layout == null) throw new NullReferenceException();
        return _boardData.Equals(_layout);
    }
}