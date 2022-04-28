using System.Text;

namespace BoardGame.SingleChessGame;
/// <summary>
/// Вспомогательный класс, хранящий фигуру с ее координатами
/// </summary>
public class CoordinatePiece
{
    private readonly int _x;
    private readonly int _y;
    private readonly SingleChessPiece _piece;
    
    /// <summary>
    /// Коструктор
    /// </summary>
    /// <param name="x">индекс x</param>
    /// <param name="y">индекс y</param>
    /// <param name="piece">ссылка на фигуру</param>
    public CoordinatePiece(int x, int y, SingleChessPiece piece)
    {
        _piece = piece;
        _x = x;
        _y = y;
    }
    /// <summary>
    /// Возвращает текущую фигуру
    /// </summary>
    /// <returns>SingleChessPiece</returns>
    public SingleChessPiece GetPiece()
    {
        return _piece;
    }
    
    /// <summary>
    /// Возвращает координату x
    /// </summary>
    /// <returns>int x</returns>
    public int GetX()
    {
        return _x;
    }
    /// <summary>
    /// Возвращает координату y
    /// </summary>
    /// <returns>int y</returns>
    public int GetY()
    {
        return _y;
    }
    
    /// <summary>
    /// Возвращает строковое представление фигуры с кооддинатами
    /// </summary>
    /// <returns>String</returns>
    /// <example>
    /// N(0,2)
    /// </example>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(_piece);
        builder.Append('(');
        builder.Append(_x);
        builder.Append(',');
        builder.Append(_y);
        builder.Append(") ");
        return builder.ToString();
    }
}