using IOCConfig.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOCConfig.Services
{
   public class Calculator: ICalculator, IScopedDependency
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
