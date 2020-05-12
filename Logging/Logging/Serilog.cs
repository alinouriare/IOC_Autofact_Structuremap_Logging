using Logging.Contracts;
using Logging.Enums;
using Microsoft.Extensions.Configuration;
using PartonetMLM.Core.Enums;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Logging.Logging
{
   public class Serilog<T> : ISerilog<T> where T : class
    {
        #region Prop
        private string connectionStringLogginDb = @"Data Source =10.201.10.110\final; Initial Catalog = LogHealthnotion; Integrated Security = False; User ID = PartonetTehran; Password = PartonetTehran123-+()POLum650; MultipleActiveResultSets = True;";
        private string customLoggingLevel = "Debug";
        private string loggingTable = "Serilogs";
        #endregion

        #region Ctor
        public Serilog()
        {

        }
        #endregion

        #region BestMethodInSerilog
        public void LogInformation(LogLevels eventLevel, string information, Exception exInfo = null, params object[] values)
        {



            ValidateMandatoryEntries(connectionStringLogginDb, customLoggingLevel, loggingTable);

            var columnOptions = GetColumnOptions();


            Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));

            var levelSwitch = new Serilog.Core.LoggingLevelSwitch();
            levelSwitch.MinimumLevel = getLogEventLevel(customLoggingLevel);
            var logLevel = getLogEventLevel(eventLevel.ToString());

            using (var log = GetLog(connectionStringLogginDb, loggingTable, columnOptions, levelSwitch))
            {
                if (log.IsEnabled(logLevel))
                {
                    if (exInfo != null)
                    {
                        log.Write(logLevel, exInfo, information, values);
                    }
                    else
                    {
                        log.Write(logLevel, information, values);
                    }
                }
                else
                {
                    var result = $"Logger: eventLevel:{nameof(logLevel)} is not enabled";
                    System.Diagnostics.Debug.WriteLine(result);
                }
            }
        }
        #endregion

        #region Method:Error&Warrning&Info
        public void Error(ProjectEnum project, string message)
        {



            string Source = project.ToString();
            using (var log = GetLogOver(connectionStringLogginDb, loggingTable))
            {

                log.Error(string.Format("Owner:{0}-Message:{1}", Source, message));


            }
        }

        public void Warrning(ProjectEnum project, string message)
        {
            string Source = project.ToString();
            using (var log = GetLogOver(connectionStringLogginDb, loggingTable))
            {

                log.Warning(string.Format("Owner:{0}-Message:{1}", Source, message));


            }
        }

        public void Info(ProjectEnum project, string message)
        {
            string Source = project.ToString();
            using (var log = GetLogOver(connectionStringLogginDb, loggingTable))
            {

                log.Information(string.Format("Owner:{0}-Message:{1}", Source, message));


            }
        }
        #endregion

        #region Private Method&Static


        private static Serilog.Core.Logger GetLog(string connectionStringLoggingDb,
            string loggingTable, Serilog.Sinks.MSSqlServer.ColumnOptions columnOptions, Serilog.Core.LoggingLevelSwitch levelSwitch)
        {
            return new LoggerConfiguration()

                           .MinimumLevel.ControlledBy(levelSwitch)

                            .WriteTo.MSSqlServer(connectionStringLoggingDb, loggingTable,
                                    columnOptions: columnOptions,
                                    autoCreateSqlTable: false
                                    )
                           .CreateLogger();
        }


        private static Serilog.Core.Logger GetLogOver(string connectionStringLoggingDb,
        string loggingTable)
        {
            return new LoggerConfiguration()

                            .WriteTo.MSSqlServer(connectionStringLoggingDb, loggingTable,

                                    autoCreateSqlTable: false
                                    )
                           .CreateLogger();
        }

        private void ValidateMandatoryEntries(string connectionStringLoggingDb, string customLoggingLevel, string loggingTable)
        {
            if (string.IsNullOrEmpty(connectionStringLoggingDb))
            {
                throw new ArgumentNullException("ConnectionStrings:LoggingDatabase null or empty");
            }

            if (string.IsNullOrEmpty(customLoggingLevel))
            {
                throw new ArgumentNullException("CustomLoggingLevel null or empty");
            }

            if (string.IsNullOrEmpty(loggingTable))
            {
                throw new ArgumentNullException("LoggingTable null or empty");
            }
        }


        private Serilog.Events.LogEventLevel getLogEventLevel(string logLevel)
        {
            var level = Serilog.Events.LogEventLevel.Debug;
            try
            {
                level = (Serilog.Events.LogEventLevel)Enum.Parse(typeof(Serilog.Events.LogEventLevel), logLevel);
            }
            catch (Exception ex)
            {
                var result = $"Serilog.Test.Logger.getLogEventLevel(): exception getting customlogging level, defaulted to Debug Level. logLevel:{logLevel}, exception:{ex}";
                System.Diagnostics.Debug.WriteLine(result);
            }
            return level;
        }


        private Serilog.Sinks.MSSqlServer.ColumnOptions GetColumnOptions()
        {
            return new Serilog.Sinks.MSSqlServer.ColumnOptions()
            {
                AdditionalDataColumns = new List<DataColumn>
                {
                    new DataColumn { ColumnName="Source", DataType = System.Type.GetType("System.String")}
                }
            };
        }
        #endregion
    }
}
