﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElDorado.Console
{
    public struct CredentialStore
    {
        private Dictionary<string, string> _credentials;

        public string this[string i]
        {
            get { return _credentials[i]; }
        }
        public CredentialStore(string credentialFileLines)
        {
            _credentials = SliceIntoLines(credentialFileLines).ToDictionary(line => Token(line, 0), line => Token(line, 1));
        }

        private static string Token(string line, int index)
        {
            const string unlikeklyToAppearInNormalString = "!@#$%";
            const string escapedSeparator = "::";

            return line.Replace(escapedSeparator, unlikeklyToAppearInNormalString).Split(':')[index].Replace(unlikeklyToAppearInNormalString, ":");
        }

        private static IEnumerable<string> SliceIntoLines(string targetFile)
        {
            return Regex.Split(targetFile, "\r\n|\r|\n");
        }
    }
}
