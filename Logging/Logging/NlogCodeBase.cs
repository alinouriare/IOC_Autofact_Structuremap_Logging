using Logging.Contracts;
using NLog;
using NLog.Config;
using NLog.Targets;
using PartonetMLM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Logging
{
   public class NlogCodeBase<T>: INlogCodeBase<T> where T : class
    {

        #region Prop
        Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Ctor
        public NlogCodeBase()
        {

        }
        #endregion

        #region PrivateMethod
        private static void ConfigureLogging()
        {
            var logConfig = new LoggingConfiguration();


            var dbTarget = new DatabaseTarget();
            dbTarget.ConnectionString = @"data source=10.201.10.110\final;initial catalog=LogHealthnotion;user id=PartonetTehran;password=PartonetTehran123-+()POLum650;multipleactiveresultsets=True;application name=EntityFramework";
                      dbTarget.CommandText = @"
    INSERT INTO [dbo].[Nlogs]
    ([MachineName]
    ,[SiteName]
    ,[Logged]
    ,[Level]
    ,[UserName]
    ,[Message]
    ,[Logger]
    ,[Properties]
    ,[ServerName]
    ,[Port]
    ,[Url]
    ,[Https]
    ,[ServerAddress]
    ,[RemoteAddress]
    ,[Callsite]
    ,[CustomerId]
    ,[Exception])
        VALUES (
    @machineName,
    @siteName,
    @logged,
    @level,
    @userName,
    @message,
    @logger,
    @properties,
    @serverName,
    @port,
    @url,
    @https,
    @serverAddress,
    @remoteAddress,
    @callSite,
    @CustomerId,
    @exception
    )
";
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@MachineName", "${machinename}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@SiteName", "n/a"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Logged", "${date}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Level", "${level}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Username", "${identity}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Message", "${message}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Logger", "${logger}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Properties", "${all-event-properties:separator=|}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@ServerName", "n/a"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Port", "n/a"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Url", "n/a"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Https", "0"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@ServerAddress", "n/a"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@RemoteAddress", "n/a"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@CallSite", "${callsite}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@CustomerId", "${mdc:item=CustomerId}"));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Exception", "${exception:tostring"));



            var ruleDebug = new LoggingRule("*", LogLevel.Debug, dbTarget);
            logConfig.LoggingRules.Add(ruleDebug);
            LogManager.Configuration = logConfig;
        }
        #endregion

        #region Method:Error&Warrning&Info
        public void Error(ProjectEnum project, string message)
        {
            ConfigureLogging();

            logger.Error(string.Format("Owner:{0}-Message:{1}", project.ToString(), message));


        }

       
        public void Warrning(ProjectEnum project, string message)
        {
            ConfigureLogging();
            logger.Warn(string.Format("Owner:{0},Message{1}", project.ToString(), message));
        }

        public void Info(ProjectEnum project, string message)
        {
            ConfigureLogging();
            logger.Info(string.Format("Owner:{0},Message{1}", project.ToString(), message));
        }
        #endregion
    }

  
}
