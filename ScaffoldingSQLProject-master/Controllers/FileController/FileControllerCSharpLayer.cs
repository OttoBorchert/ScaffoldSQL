using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ScaffoldingSQLProject.Controllers
{
   /// <summary>
   ///     This file defines what methods are directly available to the C# layer
   /// </summary>
   public partial class FileController
   {

      /**
       * Internal Methods/Data
       */
      /// The standard interface for FileDataProviders. This is for testing purposes. I just love unit testing readonly varaibles. /s

      static readonly Regex
          p_testCaseRE = new Regex(@"\s*^(V\s*[0-9]+,[0-9]+\s*[!=<>]=\s*.+|L [RC].*)\s*$", RegexOptions.Compiled),
          p_questionFileNameNoExt = new Regex(@"^([0-9]+\.){2,}", RegexOptions.Compiled),
          p_storyFileNameNoExt = new Regex(@"^[0-9]+\.story\..*", RegexOptions.Compiled);

      static void ReadLine(List<string> buffer, IEnumerator<string> lines, out bool done, ref int index)
      {
         string trimmed = lines.Current.Trim();
         if (trimmed != "" && !trimmed.StartsWith("//"))
         {
            buffer.Add(trimmed);
         }
         done = !lines.MoveNext();
         ++index;
      }

      static List<string> ReadUntil(IEnumerator<string> lines, string end, ref bool done, ref int index, bool incrementOnEnd = true)
      {
         List<string> buffer = new List<string>();
         while (lines.Current != end && !done)
         {
            ReadLine(buffer, lines, out done, ref index);
         }
         if (incrementOnEnd && lines.Current == end)
         {
            ++index;
            done = !lines.MoveNext();
         }
         return buffer;
      }

      /// <summary>
      ///     Read a range of values, starting with <code>start</code> and ending with <code>end</code>. Single line notation is disabled.
      /// </summary>
      /// <param name="lines"></param>
      /// <param name="start"></param>
      /// <param name="end"></param>
      /// <param name="done"></param>
      /// <param name="index"></param>
      /// <returns></returns>
      static List<string> ReadRange(IEnumerator<string> lines, string start, string end, ref bool done, ref int index)
      {
         List<string> buffer = new List<string>();

         if (lines.Current == start)
         {
            done = !lines.MoveNext();
            ++index;
            buffer = ReadUntil(lines, end, ref done, ref index);
         }

         return buffer;
      }

      /// <summary>
      ///     Read a range of values, starting with <code>start</code> and ending with <code>end</code>. Single line notation is enabled.
      /// </summary>
      /// <param name="lines">The enumerator of the file's contents</param>
      /// <param name="start">The start of the value list</param>
      /// <param name="end">The end of the value list</param>
      /// <param name="done"></param>
      /// <param name="index"></param>
      /// <returns></returns>
      static List<string> ReadRangeOrSingle(IEnumerator<string> lines, string start, string end, ref bool done, ref int index)
      {
         List<string> buffer = new List<string>();

         if (lines.Current != start)
         {
            ReadLine(buffer, lines, out done, ref index);
         }
         else
         {
            buffer = ReadRange(lines, start, end, ref done, ref index);
         }

         return buffer;
      }

      /// <summary>
      ///     Attempt to add a list of elements to the dictionary. If it cannot, or the resulting list is empty, use the default list.
      /// </summary>
      /// <param name="key">The dictionary key</param>
      /// <param name="iter">The enumerator</param>
      /// <param name="defaultValue">The default value</param>
      /// <param name="dict">The Hashtable/dict</param>
      /// <param name="start">The string that denotes the start of the value list</param>
      /// <param name="end">The string that denotes the end of the value list</param>
      /// <param name="done">The boolean refrence that denotes if we finished</param>
      /// <param name="index">The integer refrence that denotes where in the file we are</param>
      static void AttemptAdd(string key, IEnumerator<string> iter, List<string> defaultValue, Hashtable dict, string start, string end, ref bool done, ref int index)
      {
         List<string> result = ReadRange(iter, start, end, ref done, ref index);
         if (result.Count == 0)
         {
            // Result is empty, set to default.
            result = defaultValue;
         }
         dict.Add(key, result);
      }

      static void AttemptAddLine(string key, IEnumerator<string> iter, string defaultValue, Hashtable dict, ref bool done, ref int index, Regex re)
      {
         string line = defaultValue;
         if (!done && re.IsMatch(iter.Current ?? ""))
         {
            line = iter.Current;
            done = !iter.MoveNext();
            ++index;
         }
         dict.Add(key, line);
      }

      static void ReadRemainingWhitespaceAndComments(IEnumerator<string> iter, ref bool done, ref int index)
      {
         string current;
         while (!done && ((current = iter.Current.Trim()) == "" || current.StartsWith("//")))
         {
            done = !iter.MoveNext();
            ++index;
         }
      }

      static string GetQuestionPath(string file) => Path.Combine(P_QuestionDirectory, file);


      /**
       * Public Methods C# layer
       */


      /// <summary>
      ///     Get a list of all files and their paths in the database directory.
      /// </summary>
      public static List<string> DatabaseList() =>
          Directory.GetFiles(P_DbPath)
              .Where(s =>
              {
                 var ext = Path.GetExtension(s);
                 return ext == ".db" || ext == ".sqli";
              })
              .ToList();

      /// <summary>
      ///     Get a list of filenames of each file in the database directory
      /// </summary>
      /// <returns></returns>
      public static List<string> DatabaseListSimple() =>
          DatabaseList()
              .Select(name => Path.GetFileName(name))
              .ToList();


      /// <summary>
      /// Get a list of the files in the question directory with the full path
      /// </summary>
      /// <returns></returns>
      public static List<string> FileList() =>
          Directory.GetFiles(P_QuestionDirectory)
              .Where(s =>
              {
                 return p_questionFileNameNoExt.IsMatch(Path.GetFileName(s)) && Path.GetExtension(s) == p_questionFileExtension;
              })
              .ToList();

      /// <summary>
      /// Get The names of each file in the question direcotry
      /// </summary>
      /// <returns></returns>
      public static List<string> FileListSimple() =>
          FileList()
              .Select(name => Path.GetFileName(name))
              .ToList();

      /**
       * Static Methods For the C# Layer
       */

      /// <summary>
      ///     Gets the file contents stored in a hash table
      /// </summary>
      /// <param name="filename">The name of the file to access.</param>
      /// <returns>
      ///     <code>
      ///     HashTable {                                
      ///         File             -> string
      ///         Number           -> string
      ///         Question         -> string
      ///         TextAreaEnable   -> bool
      ///         TestCaseEnable   -> bool
      ///         ParsonsEnable    -> bool
      ///         SecretWord       -> string (Deliminated by a comma followed by a space)
      ///         SecretWordParson -> string (Deliminated by a comma followed by a space)
      ///         TestCases        -> List&lt;string&gt;
      ///         Parsons          -> List&lt;string&gt;
      ///         Database         -> List&lt;string&gt;
      ///     }
      ///     </code>
      /// </returns>
      /// <remarks>
      ///     All though it is generaly dicouraged to use nongeneric classes in C#, it would have been
      ///     (in my opinion) much more difficult to use Dictionary&lt;string, string&gt; to solve this issue.
      ///     I suppose I could have used Dictionary&lt;string, List&lt;string&gt;&gt;, but that, in my opinion, feels
      ///     off, especially since that means several of the members would have only one element.
      /// </remarks>
      public static Hashtable GetFileContents(string filename)
      {
         string path = GetQuestionPath(filename);
         // Get the file contents in a raw format
         string[] lines = System.IO.File.ReadAllLines(path);
         // Represents the number of entries that are provieded for convienence, such as 'File' and 'Number'
         const int SHORTHAND_ENTRIES = 2;
         const string UPPER_TRUE = "TRUE", UPPER_FALSE = "FALSE";

         string textAreaEnable = lines[1].Trim().ToUpper();
         string testCaseEnable = lines[2].Trim().ToUpper();
         string parsonsEnable = lines[3].Trim().ToUpper();

         // Assert that the value is a boolean
         if (testCaseEnable != UPPER_TRUE && testCaseEnable != UPPER_FALSE)
         {
            throw new InvalidDataException($"Expected true/false value for testCaseEnable, got {testCaseEnable} on line 2");
         }
         else if (parsonsEnable != UPPER_TRUE && parsonsEnable != UPPER_FALSE)
         {
            throw new InvalidDataException($"Expected true/false value for parsonsEnable, got {parsonsEnable} on line 3");
         }
         else if (textAreaEnable != UPPER_TRUE && textAreaEnable != UPPER_FALSE)
         {
            throw new InvalidDataException($"Expected true/false value for textAreaEnable, got {textAreaEnable} on line 1");
         }


         var dict = new Hashtable
            {
                // What file is this?
                { "File", filename },
                // What is the question number?
                { "Number", Path.GetFileNameWithoutExtension(filename) },
                // What is the question to be answered?
                { "Question", lines[0].Trim() },
                // Is the TextArea ability Enabled?
                { "TextAreaEnable", lines[1].Trim().ToUpper() == UPPER_TRUE },
                // Are TestCases Enabled?
                { "TestCaseEnable", lines[2].Trim().ToUpper() == UPPER_TRUE },
                // Are Parsons Hints Enabled?
                { "ParsonsEnable", lines[3].Trim().ToUpper() == UPPER_TRUE },
            };

         // Where in the file are we (for error reporting)
         int index = dict.Count - SHORTHAND_ENTRIES;

         // Instruct C# to cleanup my mess.
         using (var iter = lines.Skip(dict.Count - SHORTHAND_ENTRIES).Where(s => !string.IsNullOrWhiteSpace(s)).GetEnumerator())
         {
            bool done = iter.MoveNext();
            List<string> secretWords = ReadRangeOrSingle(iter, "StartSecrets", "EndSecrets", ref done, ref index);
            List<string> secretWordsParson = ReadRangeOrSingle(iter, "StartParsonsSecrets", "EndParsonsSecrets", ref done, ref index);

            if (secretWords[0] == "StartParsonsSecrets" || secretWords[0] == "EndParsonsSecrets" || p_testCaseRE.IsMatch(secretWords[0]))
            {
               throw new InvalidDataException($"Exception: error in format on line {4}. Expected a secret word or StartSecrets. Found {secretWords[0]}.");
            }

            if (secretWordsParson[0] == "StartParsonsSecrets" || secretWordsParson[0] == "EndParsonsSecrets" || p_testCaseRE.IsMatch(secretWordsParson[0]))
            {
               throw new InvalidDataException($"Exception: error in format on line {4}. Expected a secret word or StartSecrets. Found {secretWordsParson[0]}.");
            }

            // Add secret words
            dict.Add("SecretWord", string.Join(", ", Decrypt(secretWords.ToArray())));
            // Add parsons words
            dict.Add("SecretWordParson", string.Join(", ", Decrypt(secretWordsParson.ToArray())));
            // Get test cases
            if (!done && p_testCaseRE.IsMatch(iter.Current))
            {
               dict.Add("TestCases", ReadUntil(iter, "Parsons", ref done, ref index, false));
            }
            // Get parsons
            AttemptAdd("Parsons", iter, new List<string>(), dict, "Parsons", "EndParsons", ref done, ref index);
            // Get database
            AttemptAdd("Database", iter, new List<string>(new string[] { "fantasy.db" }), dict, "StartDatabase", "EndDatabase", ref done, ref index);
            // get version number
            AttemptAddLine("VersionNumber", iter, "0", dict, ref done, ref index, new Regex(@"([0-9]+\.)*[0-9]+"));

            ReadRemainingWhitespaceAndComments(iter, ref done, ref index);
            if (!done)
            {
               throw new FormatException($"Exception: Error in format on line {index}: '{iter.Current}'");
            }
         }

         return dict;
      }

      /// <summary>
      ///     Allows to enumerate over the unit numbers
      /// </summary>
      /// <param name="searchpattern"></param>
      /// <returns>Returns an enumerable of unit numbers</returns>
      public static IEnumerable<string> GetUnitNumbers() =>
          Directory.GetFiles(P_QuestionDirectory)
                  // Get each file name and end with the first part of the unit number
                  .Where(s => Path.GetExtension(s) == p_questionFileExtension)
                  .Select(s =>
                  {
                     string t = Path.GetFileName(s);
                     return t[0..t.IndexOf('.')];
                  })
                  // Return unique
                  .Distinct();

      /// <summary>
      ///     Get all questions from a specified unit
      /// </summary>
      /// <param name="unit"></param>
      /// <returns>Returns an enumerable of strings</returns>
      public static IEnumerable<string> GetQuestionsFromUnit(string unit) =>
          Directory.GetFiles(P_QuestionDirectory).Select(str =>
          {
             string filename = Path.GetFileName(str);
             int start = filename.IndexOf('.') + 1, stop = filename.LastIndexOf('.');
             return filename[start..stop];
          });

      /// <summary>
      ///     Write the file contents to a question file
      /// </summary>
      /// <param name="file">The file to write to</param>
      /// <param name="contents">The contents of the file</param>
      public static void WriteQuestion(string file, string contents) => System.IO.File.WriteAllText(GetQuestionPath(file), contents);

      /// <summary>
      ///     Write a database file to the Resources/databases directory
      /// </summary>
      /// <param name="db">The database to write.</param>
      public static void WriteDatabase(IFormFile db)
      {
         using var memoryStream = new MemoryStream();
         db.CopyToAsync(memoryStream);
         using var file = System.IO.File.OpenWrite(Path.Combine(P_DbPath, db.FileName));
         var bytes = memoryStream.ToArray();
         file.Write(bytes, 0, bytes.Length);
      }
   }
}
