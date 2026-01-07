using Serilog;
using Serilog.Core;

namespace Inhera.Shared.Util.Logging
{
    public class ConsoleLogger
    {
        public Logger Logger { get; }
        public ConsoleLogger()
        {
            Logger = InitLogger();
        }

        private Logger InitLogger()
        {
            var log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
            return log;
            //    log.Information("Hello, Serilog!");
            //  log.Warning("Goodbye, Serilog.")

        }
    }
}
