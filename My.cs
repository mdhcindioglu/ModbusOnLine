using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modbus
{
    public static class My
    {
        public static int Random(int length = 2)
        {
            var rand = new Random();
            var max = Int32.Parse((Math.Pow(10, length) - 1).ToString());
            return rand.Next(0, max);
        }
    }
}
