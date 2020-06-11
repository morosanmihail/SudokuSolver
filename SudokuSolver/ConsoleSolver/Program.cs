using Kermalis.SudokuSolver.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConsoleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string PathSpecs = args[0];
            var RandSeed = int.Parse(args[1]);
            var ReceivedParams = args[2].Split(',').Select(a => int.Parse(a) < 10 ? int.Parse(a) : 0).ToList();

            // dynamic Specs = JsonConvert.DeserializeObject(File.ReadAllText(PathSpecs));

            Puzzle puzzle = Puzzle.Load(ReceivedParams.ToArray());
            var solver = new Solver(puzzle);

            var badRegionCount = puzzle.BadRegions();
            bool solved = false;
            if (badRegionCount == 0)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                solved = solver.SolveSync();
                stopwatch.Stop();
            }

            Dictionary<string, object> collection = new Dictionary<string, object>()
            {
                {"Steps", solver.Puzzle.Actions.Count},
                {"Given", ReceivedParams.Count(a => a > 0)},
                {"Solved", solved ? 1 : 0 },
                {"BadRegions", badRegionCount },
                {"Remaining", solver.Puzzle.RemainingUnsolvedValues() }
            };

            JObject Result = new JObject(
                new JProperty("metrics",
                    JObject.FromObject(collection)
                )
            );

            Console.WriteLine("BEGIN METRICS");
            Console.WriteLine(Result.ToString());
            Console.WriteLine("END METRICS");
        }
    }
}
