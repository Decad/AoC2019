using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AoC9
{
    enum ReadMode
    {
        Position,
        Imidiate,
        Relative
    }

    public class Memory
    {
        private Dictionary<long, long> mem;

        public Memory(int[] m)
        {
            mem = m.Select((v, i) => new { v, i })
                .ToDictionary(x => (long)x.i, x => (long)x.v);
        }

        public long Get(long index)
        {
            if (mem.ContainsKey(index))
            {
                return mem[index];
            }
            return 0;
        }

        public long this[long i]
        {
            get => Get(i);
            set => mem[i] = value;
        }
    }

    public class IntcodeComputer
    {
        public Memory Mem { get; }
        public string Name { get; }
        public BlockingCollection<long> StdIn { get; private set; }
        public BlockingCollection<long> StdOut { get; private set; }
        public long LastOutput = 0;

        private long pc = 0;
        private long offset = 0;
        private Stack<ReadMode> modes = new Stack<ReadMode>();
        private readonly Dictionary<long, Action> opcodes;
        private bool halted = false;

        public IntcodeComputer(int[] mem, string name = "IC-1")
        {
            Mem = new Memory(mem);
            Name = name;

            StdIn = new BlockingCollection<long>(new ConcurrentQueue<long>());
            StdOut = new BlockingCollection<long>(new ConcurrentQueue<long>());

            opcodes = new Dictionary<long, Action>() {
                { 1, Add },
                { 2, Mult },
                { 3, Input },
                { 4, Output },
                { 5, JmpTrue },
                { 6, JmpFalse },
                { 7, LessThan },
                { 8, Equal },
                { 9, Offset },
                { 99, () => halted = true },
            };
        }


        public long Run()
        {
            while (!halted)
            {
                var instruction = Mem[pc++];
                var opcode = instruction % 100;

                var m = (instruction / 100).ToString()
                    .PadLeft(3, '0')
                    .ToCharArray()
                    .Select(x => (ReadMode)int.Parse(x.ToString()));

                modes = new Stack<ReadMode>(m);

                opcodes[opcode]();
            }

            return LastOutput;
        }

        public long Dump(int index)
        {
            return Mem[index];
        }

        public void Pipe(BlockingCollection<long> stdIn)
        {
            StdIn = stdIn;
        }

        private void Add()
        {
            var p1 = Read();
            var p2 = Read();
            Store(checked(p1 + p2));
        }

        private void Mult()
        {
            var p1 = Read();
            var p2 = Read();
            Store(checked(p1 * p2));
        }

        private void Input()
        {
            var input = StdIn.Take();
            Store(input);
        }

        private void Output()
        {
            LastOutput = Read();
            StdOut.Add(LastOutput);
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
            Store(p1 < p2 ? 1 : 0);
        }

        private void Equal()
        {
            var p1 = Read();
            var p2 = Read();
            Store(p1 == p2 ? 1 : 0);
        }

        private void Offset()
        {
            var p1 = Read();
            offset += p1;
        }

        private void Store(long value)
        {
            var mode = modes.Pop();
            long index = 0;
            switch (mode)
            {
                case ReadMode.Position:
                case ReadMode.Imidiate:
                    index = Mem[pc];
                    break;
                case ReadMode.Relative:
                    index = offset + Mem[pc];
                    break;
            }
            pc++;

            Mem[index] = value;
        }

        private long Read(ReadMode mode)
        {
            long output = 0;
            switch (mode)
            {
                case ReadMode.Position:
                    output = Mem[Mem[pc]];
                    break;
                case ReadMode.Imidiate:
                    output = Mem[pc];
                    break;
                case ReadMode.Relative:
                    output = Mem[offset + Mem[pc]];
                    break;
            }
            pc++;
            return output;
        }

        private long Read()
        {
            return Read(modes.Pop());
        }
    }
}