﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SautinSoft.Document;

namespace TagsCloudContainer
{
    public class DocumentReader : ISource
    {
        private readonly string filename;

        public DocumentReader(string filename)
        {
            this.filename = filename;
        }

        public string[] GetWords()
        {
            if (filename == null)
                return new string[0];
            var dc = DocumentCore.Load(filename);
            var allWords = new List<string>();

            foreach (Run element in dc.GetChildElements(true, ElementType.Run))
            {
                var matches = Regex.Matches(element.Text, @"\b[\w']+\b");

                var words = from m in matches.Cast<Match>()
                    select m.Value;

                allWords.AddRange(words);
            }

            return allWords.ToArray();
        }
    }
}
