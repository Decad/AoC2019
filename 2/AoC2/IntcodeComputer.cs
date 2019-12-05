using System;
using System.Linq;
using System.Collections.Generic;

namespace AoC2
{
    public class IntcodeComputer
    {
        private int[] mem;

        public IntcodeComputer(int[] mem)
        {
            this.mem = mem;
        }

        internal void Run()
        {
            var pc = 0;

            while (mem[pc] != 99)
            {
                switch (mem[pc])
                {
                    case 1:
                        Store(Read(pc + 3), DeRef(pc + 1) + DeRef(pc + 2));
                        break;
                    case 2:
                        Store(Read(pc + 3), DeRef(pc + 1) * DeRef(pc + 2));
                        break;
                    case 99:
                        break;
                }

                pc += 4;
            }
        }

        internal int Read(int index)
        {
            return mem[index];
        }

        internal int DeRef(int index)
        {
            return Read(mem[index]);
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