using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SessionInfoAPI
{
    public static class Logger
    {
        private static readonly log4net.ILog _log = GetLogger(typeof(Logger));

        public static ILog GetLogger(Type type)
        {
            Logger.GetLoggerClass();
            return LogManager.GetLogger(type);
        }//GetLogger

        public static void Error(object message)
        {
            _log.Error(message);
        }//Error

        public static void Info(object message)
        {
            _log.Info(message);
        }//Info

        public static void GetLoggerClass()
        {
            ILoggerRepository repository = log4net.LogManager.GetRepository(Assembly.GetCallingAssembly());

            var fileInfo = new FileInfo(@"log4net.config");

            log4net.Config.XmlConfigurator.Configure(repository, fileInfo);
        }//GetLoggerClass
    }
}
