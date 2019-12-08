using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC7
{
    public class IntcodeComputer
    {
        public int[] Mem { get; }
        public Stack<int> StdIn { get; }
        public List<int> StdOut { get; }
        
        private int pc = 0;
        private Stack<bool> modes = new Stack<bool>();
        private readonly Dictionary<int, Action> opcodes;
        private bool halted = false;

        public IntcodeComputer(int[] mem, List<int> inputs)
        {
            Mem = mem;
            inputs.Reverse();
            StdIn = new Stack<int>(inputs);
            StdOut = new List<int>();
            opcodes = new Dictionary<int, Action>() {
                { 1, Add },
                { 2, Mult },
                { 3, Input },
                { 4, Output },
                { 5, JmpTrue },
                { 6, JmpFalse },
                { 7, LessThan },
                { 8, Equal },
                { 99, () => halted = true },
            };
        }

        public List<int> Run()
        {
            while (!halted)
            {
                int instruction = Mem[pc++];
                int opcode = instruction % 100;

                var m = (instruction / 100).ToString()
                    .PadLeft(3, '0')
                    .ToCharArray()
                    .Select(x => x.Equals('1'));

                modes = new Stack<bool>(m);

                opcodes[opcode]();
            }

            return StdOut;
        }

        public string Dump()
        {
            return string.Join(',', Mem.Select(x => x.ToString()));
        }

        public int Dump(int index)
        {
            return Mem[index];
        }

        private void Add()
        {
            var p1 = Read();
            var p2 = Read();
            Store(Read(true), checked(p1 + p2));
        }

        private void Mult()
        {
            var p1 = Read();
            var p2 = Read();
            Store(Read(true), checked(p1 * p2));
        }

        private void Input()
        {
            Store(Read(true), StdIn.Pop());
        }

        private void Output()
        {
            StdOut.Add(Read());
        }

        private void JmpTrue()
        {
            var p1 = Read();
            var p2 = Read();
            if (p1 != 0)
            {
                pc = p2;
            }
        }

        private void JmpFalse()
        {
            var p1 = Read();
            var p2 = Read();
            if (p1 == 0)
            {
                pc = p2;
            }
        }

        private void LessThan()
        {
            var p1 = Read();
            var p2 = Read();
            Store(Read(true), p1 < p2 ? 1 : 0);
        }

        private void Equal()
        {
            var p1 = Read();
            var p2 = Read();
            Store(Read(true), p1 == p2 ? 1 : 0);
        }

        private void Store(int index, int value)
        {
            Mem[index] = value;
        }

        private int Read(bool i = false)
        {
            var output = modes.Pop() || i ? Mem[pc] : Mem[Mem[pc]];
            pc++;
            return output;
        }
    }
}