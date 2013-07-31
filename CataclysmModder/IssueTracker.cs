using System.Collections.Generic;
using System;

namespace CataclysmModder
{
    static class IssueTracker
    {
        private static List<Issue> issues;

        static IssueTracker()
        {
            issues = new List<Issue>();
        }

        public static void PostIssue(string message, IssueLevel level)
        {
            issues.Add(new Issue(message, level));
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
