using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AoC7
{
    class AmplificationCircuit
    {
        readonly int[] Mem;
        readonly int[] Phase;
        List<IntcodeComputer> Amps;

        public AmplificationCircuit(int[] mem, int[] phase)
        {
            Mem = mem;
            Phase = phase;
            Amps = new List<IntcodeComputer>();

            for (var i = 0; i < 5; i++)
            {
                Amps.Add(new IntcodeComputer((int[])Mem.Clone(), $"AMP {i}"));
            }

            Amps[1].Pipe(Amps[0].StdOut);
            Amps[2].Pipe(Amps[1].StdOut);
            Amps[3].Pipe(Amps[2].StdOut);
            Amps[4].Pipe(Amps[3].StdOut);
            Amps[0].Pipe(Amps[4].StdOut);

        }

        public int Run()
        {
            for (var i = 0; i < Phase.Length; i++)
            {
                Amps[i].StdIn.Add(Phase[i]);
            }

            Amps[0].StdIn.Add(0);

            var tasks = new List<Task>();
            foreach (var amp in Amps)
            {
                tasks.Add(Task.Run(() =>
                {
                    amp.Run();
                }));
            }

            Task.WaitAll(tasks.ToArray());
            return Amps[4].LastOutput;
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            //string code = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            string code = "3,8,1001,8,10,8,105,1,0,0,21,46,59,72,93,110,191,272,353,434,99999,3,9,101,4,9,9,1002,9,3,9,1001,9,5,9,102,2,9,9,1001,9,5,9,4,9,99,3,9,1002,9,5,9,1001,9,5,9,4,9,99,3,9,101,4,9,9,1002,9,4,9,4,9,99,3,9,102,3,9,9,101,3,9,9,1002,9,2,9,1001,9,5,9,4,9,99,3,9,1001,9,2,9,102,4,9,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,99";
            //string code = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            int[] mem = code.Split(',').Select(x => int.Parse(x)).ToArray();

            var permutations = GetPermutations(Enumerable.Range(5, 5));

            var output = permutations.Aggregate(0, (best, phase) =>
            {
                var thrust = new AmplificationCircuit(mem, phase.ToArray()).Run();
                return Math.Max(best, thrust);
            });
            Console.WriteLine(output);

            Console.ReadLine();
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> collection) where T : IComparable
        {
            if (!collection.Any())
            {
                return new List<IEnumerable<T>>() { Enumerable.Empty<T>() };
            }

            var sequence = collection.OrderBy(s => s).ToArray();
            return sequence.SelectMany(s => GetPermutations(sequence.Where(s2 => !s2.Equals(s)))
                .Select(sq => (new T[] { s })
                .Concat(sq)));
        }
    }
}
