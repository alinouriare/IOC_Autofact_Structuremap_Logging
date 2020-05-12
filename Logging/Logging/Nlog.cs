using Logging.Contracts;
using NLog;
using PartonetMLM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Logging
{
    public class Nlog<T> : INlog<T> where T : class
    {

        #region Prop
        Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Ctor
        public Nlog()
        {

        }
        #endregion

        #region Method:Error&Warrning&Info
        public void Error(ProjectEnum project, string message)
        {
            logger.Error(string.Format("Owner:{0}-Message:{1}", project.ToString(), message));


        }

        public void Warrning(ProjectEnum project, string message)
        {
            logger.Warn(string.Format("Owner:{0},Message{1}", project.ToString(), message));
        }

        public void Info(ProjectEnum project, string message)
        {
            logger.Info(string.Format("Owner:{0},Message{1}", project.ToString(), message));
        }
        #endregion
    }
}
