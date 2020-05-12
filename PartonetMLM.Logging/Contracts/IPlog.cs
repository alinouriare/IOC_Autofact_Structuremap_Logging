using PartonetMLM.Logging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartonetMLM.Logging.Contracts
{
    public interface IPlog<T> where T : class
    {

        #region Method
        void Warrning(ProjectEnum project, string message);
        void Info(ProjectEnum project, string message);
        void Error(ProjectEnum project, string message);
        #endregion
    }
}
