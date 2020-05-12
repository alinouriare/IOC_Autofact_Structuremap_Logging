using Logging.Enums;
using PartonetMLM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Contracts
{
   public interface ISerilog<T> where T : class
    {

        #region Method
        void LogInformation(LogLevels eventLevel, string information, Exception ex = null, params object[] values);

        void Error(ProjectEnum project, string message);


        void Warrning(ProjectEnum project, string message);


        void Info(ProjectEnum project, string message);
        #endregion
    }
}
