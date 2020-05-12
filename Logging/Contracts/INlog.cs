using PartonetMLM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Contracts
{
    public interface INlog<T> where T : class
    {

        #region Method
        void Warrning(ProjectEnum project, string message);
        void Info(ProjectEnum project, string message);
        void Error(ProjectEnum project, string message);
        #endregion
    }
}
