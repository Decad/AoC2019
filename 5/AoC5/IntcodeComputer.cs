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
        private Dictionary<int, Action> opcodes;
        private bool halted = false;

        public IntcodeComputer(int[] mem)
        {
            this.mem = mem;
            this.opcodes = new Dictionary<int, Action>() {
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

        public void Run()
        {
            while (!halted)
            {
                int instruction = mem[pc++];
                int opcode = instruction % 100;

                var m = (instruction / 100).ToString()
                    .PadLeft(3, '0')
                    .ToCharArray()
                    .Select(x => x.Equals('1'));

                modes = new Stack<bool>(m);

                opcodes[opcode]();
            }
        }

        public string Dump()
        {
            return string.Join(',', mem.Select(x => x.ToString()));
        }

        public int Dump(int index)
        {
            return mem[index];
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
            Console.Write("Input:");
            var input = int.Parse(Console.ReadLine());
            Store(Read(true), input);
        }

        private void Output()
        {
            Console.WriteLine($"Ouput: {Read()}");
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
            mem[index] = value;
        }

        private int Read(bool i = false)
        {
            var output = modes.Pop() || i ? mem[pc] : mem[mem[pc]];
            pc++;
            return output;
        }

    }
}