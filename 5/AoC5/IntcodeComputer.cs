using System;
using System.Linq;
using System.Collections.Generic;

namespace AoC5
{
    public class IntcodeComputer
    {
        public int[] mem;
        private int pc = 0;
        private Stack<bool> modes = new Stack<bool>();

        public IntcodeComputer(int[] mem)
        {
            this.mem = mem;
        }

        internal int[] Run()
        {
            bool running = true;
            while (running)
            {
                int instruction = mem[pc];
                int opcode = instruction % 100;

                var m = (instruction / 100).ToString()
                    .PadLeft(3, '0')
                    .ToCharArray()
                    .Select(x => x.Equals('1'));

                modes = new Stack<bool>(m);

                pc++;

                int p1 = 0;
                int p2 = 0;
                switch (opcode)
                {
                    case 1:
                        p1 = Read();
                        p2 = Read();
                        Store(mem[pc], checked(p1 + p2));
                        pc++;
                        break;
                    case 2:
                        p1 = Read();
                        p2 = Read();
                        Store(mem[pc], checked(p1 * p2));
                        pc++;
                        break;
                    case 3:
                        Console.Write("Input:");
                        var input = int.Parse(Console.ReadLine());
                        modes.Push(true);
                        Store(Read(), input);
                        break;
                    case 4:
                        Console.WriteLine($"Ouput: {Read()}");
                        break;
                    case 99:
                        running = false;
                        break;
                }

            }

            return mem;
        }

        internal int Read()
        {
            var output = modes.Pop() ? mem[pc] : mem[mem[pc]];
            pc++;
            return output;
        }

        internal void Store(int index, int value)
        {
            mem[index] = value;
        }

        internal string Dump()
        {
            return string.Join(',', mem.Select(x => x.ToString()));
        }

        internal int Dump(int index)
        {
            return mem[index];
        }

    }
}