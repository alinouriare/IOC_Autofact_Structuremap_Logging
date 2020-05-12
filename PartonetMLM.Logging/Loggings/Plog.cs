using NLog;
using PartonetMLM.Logging.Contracts;
using PartonetMLM.Logging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartonetMLM.Logging.Loggings
{
    public class Plog<T> : IPlog<T> where T : class
    {

        #region Prop
        Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Ctor
        public Plog()
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
