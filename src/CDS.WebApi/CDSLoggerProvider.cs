using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.WebApi
{
    public class CDSLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CDSLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }

    public class CDSLogger : ILogger
    {
        public CDSLogger(string cateogryName)
        {
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new DisposableScope();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //TODO: handle log write
            Console.WriteLine(formatter(state, exception));
        }

        private class DisposableScope : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
