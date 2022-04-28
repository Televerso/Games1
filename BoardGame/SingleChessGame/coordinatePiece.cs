using System.Text;

namespace BoardGame.SingleChessGame;

public class CoordinatePiece
{
    private readonly int _x;
    private readonly int _y;
    private readonly SingleChessPiece _piece;

    public CoordinatePiece(int x, int y, SingleChessPiece piece)
    {
        _piece = piece;
        _x = x;
        _y = y;
    }

    public SingleChessPiece GetPiece()
    {
        return _piece;
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append('(');
        builder.Append(_x);
        builder.Append(',');
        builder.Append(_y);
        builder.Append(") ");
        builder.Append(_piece);
        return builder.ToString();
    }
}