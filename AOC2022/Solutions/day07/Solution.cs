using AOC2022.Utils;

namespace AOC2022.Solutions.Day07;

public class Solution : SolutionBase
{
    private const string SHELL_CMD = "$";
    private const string CHANGE_DIR = "cd";
    private const string DIR_NAME = "dir";
    private const string ROOT = "/";
    private const string PARENT = "..";
    private readonly FileTree _fileTree;

    public Solution() : base(07, 2022, "No Space Left On Device")
    {
        var input = Input.SplitByNewline(true);

        _fileTree = new FileTree();
        var pointer = _fileTree;
        foreach (var line in input)
        {
            var cmdArgs = line.Split(" ");
            switch (cmdArgs[0])
            {
                //command
                case SHELL_CMD:
                {
                    if (cmdArgs[1] == CHANGE_DIR)
                        pointer = cmdArgs[2] switch
                        {
                            ROOT => _fileTree,
                            PARENT => pointer?.Parent,
                            _ => pointer?.Folders[cmdArgs[2]]
                        };

                    break;
                }
                case DIR_NAME:
                    pointer?.AddFolders(cmdArgs[1], new FileTree(), pointer);
                    break;
                default:
                    pointer?.AddFiles(cmdArgs[1], int.Parse(cmdArgs[0]));
                    break;
            }
        }

        _fileTree.CalcFileSizes();
        _fileTree.FindSize();
    }

    protected override string SolvePartOne()
    {
        return FileTree.Sum.ToString();
    }

    protected override string SolvePartTwo()
    {
        Console.WriteLine(30000000 - (70000000 - _fileTree.Size));
        return FileTree.ReqSizes.Min().ToString();
    }

    public class FileTree
    {
        public FileTree()
        {
            Files = new Dictionary<string, int>();
            Folders = new Dictionary<string, FileTree>();
        }

        public static int Sum { get; set; }

        public static List<int> ReqSizes { get; } = new();

        private Dictionary<string, int> Files { get; }

        public Dictionary<string, FileTree> Folders { get; }

        public int Size { get; private set; }

        public FileTree? Parent { get; private set; }

        public void AddFiles(string fileName, int size)
        {
            Files.Add(fileName, size);
        }

        public void AddFolders(string folderName, FileTree child, FileTree parent)
        {
            Folders.Add(folderName, child);
            child.Parent = parent;
        }

        public void FindSize()
        {
            Size = 0;
            foreach (var file in Files) Size += file.Value;

            foreach (var folder in Folders)
            {
                folder.Value.FindSize();
                Size += folder.Value.Size;
            }
        }

        public void CalcFileSizes()
        {
            foreach (var folder in Folders)
            {
                folder.Value.FindSize();
                if (folder.Value.Size >= 2036703) ReqSizes.Add(folder.Value.Size);
                Sum += folder.Value.Size <= 100000 ? folder.Value.Size : 0;
                folder.Value.CalcFileSizes();
            }
        }
    }
}