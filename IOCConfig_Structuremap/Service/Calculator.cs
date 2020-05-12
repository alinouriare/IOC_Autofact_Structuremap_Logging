using IOCConfig_Structuremap.NewFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOCConfig_Structuremap.Service

{
   public  class Calculator:ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;

        }
    }
}
