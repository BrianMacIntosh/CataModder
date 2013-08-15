using System.Collections.Generic;
using System;
using System.IO;

namespace CataclysmModder
{
    static class IssueTracker
    {
        private static List<Issue> issues;

        private static string logfile = "log.txt";

        static IssueTracker()
        {
            issues = new List<Issue>();

            if (File.Exists(logfile))
                File.Delete(logfile);
            File.Create(logfile);
        }

        public static void PostIssue(string message, IssueLevel level)
        {
            issues.Add(new Issue(message, level));

            //TODO: show these in the interface
            StreamWriter write = null;
            try
            {
                write = new StreamWriter(new FileStream(logfile, FileMode.Append));
                write.WriteLine(level.ToString() + ": " + message);
            }
            catch (IOException)
            {

            }
            finally
            {
                if (write != null)
                    write.Close();
            }
        }

        class Issue
        {
            public string message;
            public IssueLevel level;

            public Issue(string message, IssueLevel level)
            {
                this.message = message;
                this.level = level;
                Console.WriteLine(message);
            }
        }

        public enum IssueLevel
        {
            WARNING,
            ERROR,
            FATAL
        }
    }
}
