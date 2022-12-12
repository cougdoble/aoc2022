using AOC2022.Utils;

namespace AOC2022.Solutions.Day12;

public class Solution : SolutionBase
{
    static int N = 4;
    static int M = 4;

    public Solution() : base(12, 2022, "", true)
    { }

    protected override string SolvePartOne()
    {
        var data = Input.SplitByNewline();
        var length = Convert.ToInt32(data[0].Length);
        var depth = Convert.ToInt32(data.Length);

        char[,] input = new char[depth, length];

        for (var i = 0; i < data.Length; i++)
        {
            var line = data[i].ToCharArray();
            for (var j = 0; j < line.Length; j++)
            {
                input[i, j] = data[i][j];
            }
        }

        Console.Write(minDistance(input));
        return "";
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
    public class QItem
    {
        public int row;
        public int col;
        public int dist;

        public QItem(int x, int y, int w)
        {
            this.row = x;
            this.col = y;
            this.dist = w;
        }
    }

    public static int minDistance(char[,] grid)
    {
        QItem source = new QItem(0, 0, 0);

        // To keep track of visited QItems. Marking
        // blocked cells as visited.
        bool[,] visited = new bool[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                // Finding source
                if (grid[i, j] == 'S')
                {
                    source.row = i;
                    source.col = j;
                }
            }
        }

        // applying BFS on matrix cells starting from source
        Queue<QItem> q = new Queue<QItem>();
        q.Enqueue(source);
        visited[source.row, source.col] = true;
        while (q.Count > 0)
        {
            QItem p = q.Peek();
            q.Dequeue();

            // Destination found;
            if (grid[p.row, p.col] == 'D')
            {
                return p.dist;
            }

            // moving up
            if (p.row - 1 >= 0
                && visited[p.row - 1, p.col] == false)
            {
                if (grid[p.row, p.col] <= grid[p.row, p.col])
                {
                    q.Enqueue(new QItem(p.row - 1, p.col,
                                        p.dist + 1));
                    visited[p.row - 1, p.col] = true;
                }
            }

            // moving down
            if (p.row + 1 < N
                && visited[p.row + 1, p.col] == false)
            {
                if (grid[p.row, p.col] <= grid[p.row, p.col])
                {
                    q.Enqueue(new QItem(p.row + 1, p.col,
                                        p.dist + 1));
                    visited[p.row + 1, p.col] = true;
                }
            }

            // moving left
            if (p.col - 1 >= 0
                && visited[p.row, p.col - 1] == false)
            {
                if (grid[p.row, p.col] <= grid[p.row, p.col])
                {
                    q.Enqueue(new QItem(p.row, p.col - 1,
                                        p.dist + 1));
                    visited[p.row, p.col - 1] = true;
                }
            }

            // moving right
            if (p.col + 1 < M
                && visited[p.row, p.col + 1] == false)
            {
                if (grid[p.row, p.col] <= grid[p.row, p.col])
                {
                    q.Enqueue(new QItem(p.row, p.col + 1,
                                        p.dist + 1));
                    visited[p.row, p.col + 1] = true;
                }
            }
        }
        return -1;
    }

}

