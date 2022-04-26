using System.Text;

namespace BoardGame.SingleChessGame;

public class coordinatePiece
{
    private readonly int X;
    private readonly int Y;
    private readonly SingleChessPiece _piece;

    public coordinatePiece(int x, int y, SingleChessPiece piece)
    {
        _piece = piece;
        X = x;
        Y = y;
    }

    public SingleChessPiece GetPiece()
    {
        return _piece;
    }

    public int GetX()
    {
        return X;
    }

    public int GetY()
    {
        return Y;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append('(');
        builder.Append(X);
        builder.Append(',');
        builder.Append(Y);
        builder.Append(") ");
        builder.Append(_piece);
        return builder.ToString();
    }
}