using System;
using System.Linq;
using System.Collections.Generic;

namespace AoC7
{
    class AmplificationCircuit
    {
        readonly int[] Mem;
        readonly int[] Phase;

        public AmplificationCircuit(int[] mem, int[] phase)
        {
            Mem = mem;
            Phase = phase;
        }

        public int Run()
        {
            return Phase.Aggregate(0, (acc, x) => new IntcodeComputer(Mem, new List<int> { x, acc }).Run()[0]);
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            string code = "3,8,1001,8,10,8,105,1,0,0,21,46,59,72,93,110,191,272,353,434,99999,3,9,101,4,9,9,1002,9,3,9,1001,9,5,9,102,2,9,9,1001,9,5,9,4,9,99,3,9,1002,9,5,9,1001,9,5,9,4,9,99,3,9,101,4,9,9,1002,9,4,9,4,9,99,3,9,102,3,9,9,101,3,9,9,1002,9,2,9,1001,9,5,9,4,9,99,3,9,1001,9,2,9,102,4,9,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,99";
            //string code = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            int[] mem = code.Split(',').Select(x => int.Parse(x)).ToArray();

            var permutations = GetPermutations(Enumerable.Range(0, 5));
            
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
