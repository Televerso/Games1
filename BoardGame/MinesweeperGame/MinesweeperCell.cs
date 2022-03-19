namespace BoardGame.MinesweeperGame;

/// <summary>
/// Клетка доски для игры в сапер<br/>
/// </summary>
/// <remarks>
/// _hasFlag<br/>
/// _hasMine<br/>
/// _isHidden<br/>
/// _adjacentMines
/// </remarks>
public class MinesweeperCell
{
    // Показывает, имеется ли флаг на данном поле
    private bool _hasFlag;
    // Показывает, заминировано ли данное поле
    private bool _hasMine;
    // Показывает, скрыто ли данное поле
    private bool _isHidden;
    // Количество мин на соседних полях
    private int _adjacentMines;

    /// <summary>
    /// Конструктор по умолчанию<br/>
    /// Создает пустую клетку с начальными параметрами
    /// </summary>
    public MinesweeperCell()
    {
        _hasFlag = false;
        _hasMine = false;
        _isHidden = true;
        _adjacentMines = 0;
    }
    /// <summary>
    /// Конструктор<br/>
    /// Создает клетку с возможностью установки мины
    /// </summary>
    /// <param name="mine">будет ли установлена мина на данном поле</param>
    public MinesweeperCell(bool mine)
    {
        _hasFlag = false;
        _hasMine = mine;
        _isHidden = true;
        _adjacentMines = 0;
    }
    
    /// <summary>
    /// Устанавливает значение для мины на данном поле<br/>
    /// НЕ ПЕРЕСЧИТЫВАЕТ КОЛИЧЕСТВО МИН ДЛЯ СОСЕДНИХ ПОЛЕЙ
    /// </summary>
    /// <param name="mine">мина</param>
    internal void SetMine(bool mine)
    {
        _hasMine = mine;
    }
    /// <summary>
    /// Устанавливает значение флага на данном поле
    /// </summary>
    /// <param name="flag">флаг</param>
    public void SetFlag(bool flag)
    {
        _hasFlag = flag;
    }
    /// <summary>
    /// Прячет или открывает данное поле
    /// </summary>
    /// <param name="hide">прятать ли поле</param>
    public void Hide(bool hide)
    {
        _isHidden = hide;
    }
    /// <summary>
    /// Устанавливает количество мин на соседних полях<br/>
    /// НЕ УСТАНАВЛИВАЕТ МИНЫ НА СОСЕДНИЕ ПОЛЯ
    /// </summary>
    /// <param name="mines">количество мин</param>
    /// <exception cref="ArgumentException">если передан некорректный аргумент</exception>
    internal void SetAdjacentMines(int mines)
    {
        if (mines < 0 || mines > 9) throw new ArgumentException();
        _adjacentMines = mines;
    }
    /// <summary>
    /// Возвращает значение мины для данного поля
    /// </summary>
    /// <returns>мину</returns>
    public bool GetMine()
    {
        return _hasMine;
    }
    /// <summary>
    /// Возвращает значение флага для данного поля
    /// </summary>
    /// <returns>флаг</returns>
    public bool GetFlag()
    {
        return _hasFlag;
    }
    /// <summary>
    /// Спрятано ли данное поле
    /// </summary>
    /// <returns>спрятано ли поле</returns>
    public bool IsHidden()
    {
        return _isHidden;
    }
    /// <summary>
    /// Возвращает количество соседних мин
    /// </summary>
    /// <returns></returns>
    public int GetAdjacentMines()
    {
        return _adjacentMines;
    }
    /// <summary>
    /// Метод ToString<br/>
    /// P - флажок<br/>
    /// ■ - скрытое поле<br/>
    /// * - мина
    /// 1-9 - соседние мины
    /// </summary>
    /// <returns>String</returns>
    public override string ToString()
    {
        if (_hasFlag) return "P";
        if (_isHidden) return char.ConvertFromUtf32(9632);
        if (_hasMine) return "*";
        return _adjacentMines == 0 ? " " : _adjacentMines.ToString();
    }
}