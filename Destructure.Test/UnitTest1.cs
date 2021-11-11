using DestructureLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using System;
using System.Data;
using System.IO;

namespace Destructure.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMicrosoftLogger_WithOutCodeConfig_NotDisposingContainer_FileEmpty()
        {
            //arrange
            var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", $"log{GetLogFileSuffix()}.json");
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            var serviceProvider = ServiceProviders.CreateServiceProvider(false);
            var logger = serviceProvider.GetService<ILogger<UnitTest1>>();
            
            var dt = new DataTable();
            dt.Columns.Add("Column1");
            dt.Rows.Add(new object[] { "Stuff" });
            var result = new TestResult<DataTable>(ResultReturnCode.Success, "TestLoggerWithOutCodeConfig", dt);

            //act
            logger.LogInformation("{@TestResult}", result);

            //assert            
            string logFileContents = "";
            if (File.Exists(logFilePath))
            {
                logFileContents = File.ReadAllText(logFilePath);
            }

            Assert.IsTrue(string.IsNullOrEmpty(logFileContents));
        }

        [TestMethod]
        public void TestMicrosoftLogger_WithCodeConfig_NotDisposingContainer_FileNotEmpty()
        {
            //arrange
            var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", $"log{GetLogFileSuffix()}.json");
            var logFilePath2 = Path.Combine(AppContext.BaseDirectory, "logs", $"log-Json.txt");
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            if (File.Exists(logFilePath2))
            {
                File.Delete(logFilePath2);
            }

            var serviceProvider = ServiceProviders.CreateServiceProvider(true);
            var logger = serviceProvider.GetService<ILogger<UnitTest1>>();

            var dt = new DataTable();
            dt.Columns.Add("Column1");
            dt.Rows.Add(new object[] { "Stuff" });
            var result = new TestResult<DataTable>(ResultReturnCode.Success, "TestLoggerWithOutCodeConfig", dt);

            //act
            logger.LogInformation("{@TestResult}", result);

            //assert            
            string logFileContents = "";
            if (File.Exists(logFilePath))
            {
                logFileContents = File.ReadAllText(logFilePath);
            }

            //file contents are still empty.  They will get populated after the method exists.  Seems the logger is flushed implicitly when there is a code configuration for a sink
            Assert.IsFalse(string.IsNullOrEmpty(logFileContents));
        }

        [TestMethod]
        public void TestLogger_WithCodeConfig_FileNotEmpty()
        {
            //arrange
            var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", $"log{GetLogFileSuffix()}.json");
            var logFilePath2 = Path.Combine(AppContext.BaseDirectory, "logs", $"log-Json.txt");
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            if (File.Exists(logFilePath2))
            {
                File.Delete(logFilePath2);
            }

            Log.Logger = SeriLogUtil.CreateSeriloggerWithCodeConfig();
            var dt = new DataTable();
            dt.Columns.Add("Column1");
            dt.Rows.Add(new object[] { "Stuff" });
            var result = new TestResult<DataTable>(ResultReturnCode.Success, "TestLoggerWithCodeConfig", dt);

            //act
            Log.Logger.Information("{@TestResult}", result);
            //at this point a log-Json.txt file is created and is 0 bytes
            Log.CloseAndFlush();
            //after calling CloseAndFlush the second log file from the .json config is created and both files are populated

            //assert            
            string logFileContents = "";
            if (File.Exists(logFilePath))
            {
                logFileContents = File.ReadAllText(logFilePath);
            }
            Assert.IsFalse(string.IsNullOrEmpty(logFileContents));
        }

        [TestMethod]
        public void TestLogger_WithOutCodeConfig_FileEmpty()
        {
            //arrange
            var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", $"log{GetLogFileSuffix()}.json");
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }
                
            Log.Logger = SeriLogUtil.CreateSeriloggerWithOutCodeConfig();
            var dt = new DataTable();
            dt.Columns.Add("Column1");
            dt.Rows.Add(new object[] { "Stuff" });
            var result = new TestResult<DataTable>(ResultReturnCode.Success, "TestLoggerWithOutCodeConfig", dt);

            //act
            Log.Logger.Information("{@TestResult}", result);

            //assert            
            string logFileContents = "";
            if (File.Exists(logFilePath))
            {
                logFileContents = File.ReadAllText(logFilePath);
            }

            Assert.IsTrue(string.IsNullOrEmpty(logFileContents));
        }

        static string GetLogFileSuffix()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
    }
}
