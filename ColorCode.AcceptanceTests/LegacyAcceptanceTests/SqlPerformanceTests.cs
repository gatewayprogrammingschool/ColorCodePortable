﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xunit;

namespace MDS.ColorCode.SqlAcceptanceTests
{
    public class SqlPerformanceTests
    {
        public class Transform
        {
            [Fact]
            public void WillStyleLargeSourceTextIn1SecondOrLess()
            {
                string appPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                string source = File.ReadAllText(Path.Combine(appPath, @"..\..\LegacyAcceptanceTests\large.sql"));
                Stopwatch sw = new Stopwatch();
                sw.Start();

                new CodeColorizer().Colorize(source, Languages.Sql);

                sw.Stop();
                TimeSpan elapsed = sw.Elapsed;
                Assert.True(elapsed.Seconds <= 1);
            }
        }
    }
}
