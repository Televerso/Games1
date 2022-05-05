using System.CodeDom.Compiler;
using System.Text;

namespace BoardGame
{

    public class RandomBoardFiller
    {
        private readonly bool[,] _boardData;
        private readonly int _x;
        private readonly int _y;

        public RandomBoardFiller(int x, int y)
        {
            _boardData = new bool[x, y];
            _x = x;
            _y = y;
        }

        public void Generate(int count)
        {
            if (count < 0 || count > _x * _y) throw new ArgumentOutOfRangeException();

            // Определяем количество максимальных возможных итераций с помощью медленной оперции взятия корня
            int numberOfTries = (int) Math.Sqrt(count);

            // Создаем два разных генератора случайных чисел
            Random rx = new Random();
            Random ry = new Random(rx.Next());


            for (int i = 0; i != count; ++i)
            {
                // Массив, в котором хранятся случайные координаты
                int[] coord = new int[2];

                // Количество итераций в данном цикле
                int triesThisIteration = 0;

                do
                {
                    // Определяем случайные координаты
                    coord[0] = rx.Next(_x);
                    coord[1] = ry.Next(_y);

                    ++triesThisIteration;

                } while (_boardData[coord[0], coord[1]] && triesThisIteration < numberOfTries);

                if (triesThisIteration == numberOfTries && _boardData[coord[0], coord[1]])
                {
                    if (FillTheFirstEmpty(coord[0].GetHashCode() ^ coord[1].GetHashCode())) return;
                }
                _boardData[coord[0], coord[1]] = true;
            }
        }

        private bool FillTheFirstEmpty(int seed)
        {
            Random rand = new Random(seed);
            int direction = rand.Next(0, 2);
            int x = rand.Next(_x);
            int y = rand.Next(_y);


            int i;
            int j;
            switch (direction)
            {
                case 0:
                    i = x;
                    j = y;
                    do
                    {
                        do
                        {
                            if (!_boardData[i, j])
                            {
                                _boardData[i, j] = true;
                                return false;
                            }
                            if (++j == _y) j = 0;
                        }
                        while (j != y) ;
                        if (++i == _x) i = 0;
                    }
                    while (i != x) ;
                    break;
                
                case 1:
                    i = x;
                    j = y;
                    do
                    {
                        do
                        {
                            if (!_boardData[i, j])
                            {
                                _boardData[i, j] = true;
                                return false;
                            }

                            if (--j == -1) j = _y - 1;
                        } 
                        while (j != y );
                        if (--i == -1) i = _x - 1;
                    }
                    while (i != x);
                    break;
            }
            return true;
        }

        public void Clear()
        {
            for (int i = 0; i != _x; ++i)
            {
                for (int j = 0; j != _y; ++j)
                {
                    _boardData[i, j] = false;
                }
            }
        }

        public bool[,] GetBoard()
        {
            return (bool[,]) _boardData.Clone();
        }

        public override string ToString()
        {
            if (_boardData.ToString() == null) return "null";
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i != _x; ++i)
            {
                for (int j = 0; j != _y; ++j)
                {
                    builder.Append(_boardData[i, j] ? 1 : 0);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}