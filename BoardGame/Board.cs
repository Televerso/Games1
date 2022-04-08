namespace BoardGame
{

    public interface IBoard
    {
        /// <summary>
        /// Возвращает индекс поля с координатами (x,y)
        /// </summary>
        /// <param name="x">координата x</param>
        /// <param name="y">координата y</param>
        /// <returns>index</returns>
        int GetCellIndex(int x, int y);

        /// <summary>
        /// Возвращает координату X поля с индексом index
        /// </summary>
        /// <param name="index">индекс поля</param>
        /// <returns>координата x</returns>
        int GetCellXByIndex(int index);

        /// <summary>
        /// Возвращает координату Y поля с индексом index
        /// </summary>
        /// <param name="index">индекс поля</param>
        /// <returns>координата y</returns>
        int GetCellYByIndex(int index);

        /// <summary>
        /// Пересоздает доску
        /// </summary>
        /// <param name="a">длина стороны новой доски</param>
        void RecreateBoard(int a);

        /// <summary>
        /// Пересоздает доску
        /// </summary>
        /// <param name="x">длина стороны x новой доски</param>
        /// <param name="y">длина стороны y новой доски</param>
        void RecreateBoard(int x, int y);

        /// <summary>
        /// Возвращает количество клеток доски
        /// </summary>
        /// <returns>количество полей</returns>
        int GetCount();

        /// <summary>
        /// Возвращает длину стороны X доски
        /// </summary>
        /// <returns>длина X</returns>
        int GetXSide();

        /// <summary>
        /// Возвращает длину стороны Y доски
        /// </summary>
        /// <returns>длина Y</returns>
        int GetYSide();

        /// <summary>
        /// Возвращает данные поля
        /// </summary>
        /// <returns>текущие данные поля</returns>
        int[,]? GetBoard();

        /// <summary>
        /// Метод ToString
        /// </summary>
        /// <returns>строковое представление доски</returns>
        string ToString();

        /// <summary>
        /// Заполняет доску случайными значениями
        /// </summary>
        void RandomFillBoard();

        /// <summary>
        /// Играет передаваемое значение на поле
        /// </summary>
        /// <param name="index">индекс поля</param>
        /// <param name="val">играемое значение</param>
        void SetCell(int index, int val);

        /// <summary>
        /// Играет передаваемое значение на поле
        /// </summary>
        /// <param name="x">координата x поля</param>
        /// <param name="y">координата y поля</param>
        /// <param name="val">играемое значение</param>
        void SetCell(int x, int y, int val);

        /// <summary>
        /// Получает информацию о состоянии поля
        /// </summary>
        /// <param name="x">координата x поля</param>
        /// <param name="y">координата y поля</param>
        /// <returns>текущее состояние поля</returns>
        int GetCell(int x, int y);

        /// <summary>
        /// Получает информацию о состоянии поля
        /// </summary>
        /// <param name="index">индекс поля</param>
        /// <returns>текущее состояние поля</returns>
        int GetCell(int index);

        /// <summary>
        /// Проверяет завершенность доски
        /// </summary>
        bool CheckBoard();
    }
}