using Kermalis.SudokuSolver.Core;
using System;
using System.Diagnostics;

namespace ConsoleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var filename = "E:/github/SudokuSolverSharp/Puzzles/Regular/4112.txt";
            Puzzle puzzle = Puzzle.Load(filename);
            var solver = new Solver(puzzle);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var solved = solver.SolveSync();
            stopwatch.Stop();

            Console.WriteLine(string.Format("Solver finished in {0} seconds, doing {1} steps.", stopwatch.Elapsed.TotalSeconds, solver.Puzzle.Actions.Count));
        }
    }
}
