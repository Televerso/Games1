namespace BoardGame;

public interface IBoard
{
    int GetCellIndex(int x, int y);
    int GetCellXByIndex(int index);
    int GetCellYByIndex(int index);

    void RecreateBoard(int a);
    void RecreateBoard(int x, int y);
    
    int GetCount();
    int GetXSide();
    int GetYSide();
    int[,]? GetBoard();

    String ToString();

    void RandomFillBoard();

    void SetCell(int index, int val);
    void SetCell(int x, int y, int val);

    int GetCell(int x, int y);
    int GetCell(int index);

    bool CheckBoard();
}