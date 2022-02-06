using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text.RegularExpressions;
using ScaffoldingSQLProject.Controllers;

namespace CapstoneIdeas.Pages.QuestionMaker
{
    public class IndexModel : PageModel
    {
        private readonly Regex p_filenameRe = new Regex(@"[0-9]+\.[0-9]+\.txt", RegexOptions.Compiled);

        #region BOUND_PROPERTIES
        /// <summary>
        /// Are the parsons hints enabled?
        /// </summary>
        [BindProperty]
        public bool ParsonsOn { get; set; } = true;

        [BindProperty]
        public string QuestionNumber { get; set; }

        [BindProperty]
        public string UnitNumber { get; set; }

        [BindProperty]
        public string Question { get; set; }

        /// <summary>
        /// What are the secret words for the non-parsons interface, that shall be given to a student upon completing a problem?
        /// </summary>
        [BindProperty]
        public string SecretWord { get; set; }

        /// <summary>
        /// What are the secret words for the parsons interface, that shall be given to a student upon completing a problem?
        /// </summary>
        [BindProperty]
        public string ParsonsSecretWord { get; set; }

        /// <summary>
        /// Row for the test case
        /// </summary>
        [BindProperty]
        public Dictionary<string, int?> TestcaseRow { get; set; }

        /// <summary>
        /// Column for the test case
        /// </summary>
        [BindProperty]
        public Dictionary<string, int?> TestcaseCol { get; set; }

        /// <summary>
        /// What is the value to test for in the test case
        /// </summary>
        [BindProperty]
        public Dictionary<string, string> TestcaseText { get; set; }

        /// <summary>
        /// Are test cases active?
        /// </summary>
        [BindProperty]
        public Dictionary<string, string> TestcaseActive { get; set; }

        /// <summary>
        /// [rowname, conditon]
        /// </summary>
        [BindProperty]
        public Dictionary<string, string> TestcaseConditional { get; set; }

        [BindProperty]
        public string TestCaseNumberOfRows { get; set; }

        [BindProperty]
        public string TestCaseNumberOfCols { get; set; }

        /// <summary>
        ///     Signifies if this represents column names, in which case they must be surrounded by single quotes.
        ///     Although I would like for this to be automatric, it turns out it is way harder than I would like to admit,
        ///     especialy when you allow for sql subqueries. -- Charles Haislip
        /// </summary>
        [BindProperty]
        public Dictionary<string, string> NeedsSingle { get; set; }

        /// <summary>
        /// Are test cases enabled?
        /// </summary>
        [BindProperty]
        public bool TestCasesON { get; set; } = true;

        /// <summary>
        /// [RowName, command]: A dictionary that maps a specified row to a command.
        /// </summary>
        [BindProperty]
        public Dictionary<string, string> Commands { get; set; }

        /// <summary>
        /// [RowName, OptionSet]<br/>
        /// Reserved optionSets: tables, value0<br/>
        /// A Dictionary that binds a given row to a set of strings taht are used to augment an SQL command.
        /// </summary>
        [BindProperty]
        public Dictionary<string, Dictionary<string, string>> CmdOptions { get; set; }

        /// <summary>
        /// What is the database used for this question? 
        /// </summary>
        [BindProperty]
        public string Database { get; set; }

        #endregion BOUND_PROPERTIES

        // If admin is set to true, compile the code for admin
#if ADMIN


        /// <summary>
        /// Convert a string containing values for mates as 'arg1, arg2, arg3, ...'<br/>
        /// into '$$toggle::arg1::arg2::arg3::...$$
        /// </summary>
        /// <param name="command">The command bieng parsed. Used for error reporting only.</param>
        /// <param name="opts">The value sets</param>
        /// <param name="valuesetCanBeEmpty">Can the value set be empty?</param>
        /// <returns>The 'togglefied' string</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Disabled for clarity. I do not believe one should throw from a ternary conitional expression.")]
        static string ToToggle(string command, string opts, bool needsQuotes, bool valuesetCanBeEmpty = false)
        {
            string value = string.Empty;
            // Handle null
            opts ??= string.Empty;
            // Split on commans not between parens and quotes
            /* Explanation:
             * ,: Match a comma
             * (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$): Check for an even number of double quotes
             * (?=(?:[^']*'[^']*')*[^']*$): Check for an even number of single quotes
             * (?=(?:[^`]*`[^`]*`)*[^`]*$): Check for an even number of backticks
             * (?=(?:[^(]*\\)[^(]*\\))*[^\\)]*$): Check taht we are not between a pair of parenthis
             */
            Regex regex = new Regex(
                ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)(?=(?:[^']*'[^']*')*[^']*$)(?=(?:[^`]*`[^`]*`)*[^`]*$)(?=(?:[^(]*\\)[^(]*\\))*[^\\)]*$)"
            );
            List<string> matches = regex.Split(opts)
                // Sanitize colons
                .Select(o => o.Trim().Replace(":", "&colon;"))
                // Get non empty strings
                .Where(o => !string.IsNullOrEmpty(o))
                // Convert to list for the .Count operation (Note: .Count on an IEnumerable can have a major 
                // performance penalty. I am avoiding this.)
                .ToList();
            foreach (string o in matches)
            {
                value += "::" + (needsQuotes ? '\'' + o + '\'' : o);
            }
            // If the value set it empty and it cannot be empty
            if (matches.Count == 0 && !valuesetCanBeEmpty)
            {
                throw new InvalidOperationException("Value set cannot be empty for " + command);
            }
            return matches.Count > 0 ? opts.Trim() : "$$toggle" + value + "$$";
        }

        /// <summary>
        ///     Asserts that the options are not null
        /// </summary>
        /// <param name="options"></param>
        /// <param name="command"></param>
        void AssertOptionsAreValid(Dictionary<string, string> options, string command)
        {
            if (options == null)
            {
                throw new KeyNotFoundException($"Unable to find any command options for {command}. Please make sure that you filled out what options exist for {command}.");
            }
        }

        string CombineOptions(string command, IEnumerable<KeyValuePair<string, string>> options, string joinWith, bool needsQuotes, bool valueSetCanBeEmpty, string startWith = "", string endWith = "") =>
            CombineOptions(command, options.Select(kv => kv.Value), joinWith, needsQuotes, valueSetCanBeEmpty, startWith, endWith);

        string CombineOptions(string command, IEnumerable<string> options, string joinWith, bool needsQuotes, bool valueSetCanBeEmpty, string startWith = "", string endWith = "") =>
            startWith + string.Join(joinWith, options.Select(o => ToToggle(command, o, needsQuotes, valueSetCanBeEmpty))) + endWith;

        /// <summary>
        /// Convert a specified row into the specified parsons format.
        /// </summary>
        /// <param name="RowName"></param>
        /// <returns></returns>
        string GetParsons(string RowName)
        {
            bool needsQuotes = NeedsSingle.ContainsKey(RowName) && NeedsSingle[RowName] == "on";
            bool valueSetsCanBeEmpty = false;
            string command = Commands[RowName], tmp = null;
            string textToWrite = command + ' ';
            Dictionary<string, string> options = CmdOptions.ContainsKey(RowName) ? CmdOptions[RowName] : null;
            // This switch is used to simplify the selection (and perhaps increase efficiency, but that is an
            // irrelevant implementation detail). Note that C# switches are required to end with either 
            // A: break statement
            // B: a return statement
            // C: goto case [condition] (basically, an explicit fall through)
            switch (command)
            {
                // Ignore. No values need to be read.
                case "ASC": case "DESC": case "UNION": case "UNION ALL": case "(": case ")": case ";":
                    break;
                case "DELETE FROM":
                    AssertOptionsAreValid(options, command);
                    textToWrite += ToToggle(command, options["tables1"], needsQuotes, valueSetsCanBeEmpty);
                    break;
                case "INSERT INTO":
                    AssertOptionsAreValid(options, command);
                    // Need to hardcode true because when we fallthrough to values, it needs to be false.
                    // Get the table name
                    textToWrite += ToToggle(command, options.Where(s => s.Key.StartsWith("tables")).First().Value, false);
                    // Get the column names, if available
                    tmp = string.Join(',', options.Where(kv => kv.Key.StartsWith("tables")).Select(kv => ToToggle(command, kv.Value, needsQuotes, true).Skip(1)));
                    // If tmp exists, add parenthesis
                    if (tmp != "")
                    {
                        textToWrite += "(" + tmp + ")";
                    }
                    break;
                // For each value, convert to the format of ($$toggle::val1::val2::...valn$$, $$toggle::val1::val2::...valn$$, ...)
                case "VALUES": case "USING":
                    AssertOptionsAreValid(options, command);
                    textToWrite += CombineOptions(command, options.Where(kv => kv.Key.StartsWith("value")), ",", needsQuotes, valueSetsCanBeEmpty, "(", ")");
                    break;
                case "SELECT": case "SET":
                    AssertOptionsAreValid(options, command);
                    textToWrite += CombineOptions(command, options.Where(kv => kv.Key.StartsWith("value")), ",", needsQuotes, valueSetsCanBeEmpty);
                    break;
                case "WHERE": case "ON":
                    AssertOptionsAreValid(options, command);
                    textToWrite += CombineOptions(command, options.Where(kv => kv.Key.StartsWith("value")), " ", needsQuotes, valueSetsCanBeEmpty);
                    break;
                case string a when a.Contains("JOIN"):
                    // Joins are allowed to not contain any value sets.
                    // Example:
                    // SELECT * FROM Players
                    //     JOIN 
                    //     (
                    //           SELCT * FROM GUILD
                    //     )
                    // ...
                    // Would not use the operator, because the subquery is used here.
                    valueSetsCanBeEmpty = true;
                    // Fall through to the default case
                    goto default;
                // Most SQL can just use a single value.
                default:
                    AssertOptionsAreValid(options, command);
                    var enumerable = CmdOptions[RowName].
                        Where(pair => pair.Key.StartsWith("value")).
                        OrderBy(pair => pair.Key);
                    if (!enumerable.Any())
                    {
                        throw new InvalidOperationException(command + " must have value set(s)");
                    }
                    textToWrite += ToToggle(command, enumerable.First().Value, needsQuotes, valueSetsCanBeEmpty);
                    break;
            }
            return textToWrite + '\n';
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Disabled for clarity. I do not believe one should throw from a ternary conitional expression.")]
        string GetColumnCheck(string key)
        {
            if (TestcaseActive.ContainsKey(key) && TestcaseActive[key] == "on")
            {
                return "C " + TestcaseRow[key];
            }
            else
            {
                if (TestcaseCol[key] == null)
                {
                    throw new ArgumentNullException("Column Number is empty");
                }
                return "V " + TestcaseRow[key] + ',' + TestcaseCol[key];
            }
        }

        static void AssertHasData(string data, string msg)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new InvalidDataException(msg);
            }
        }

        /// <summary>
        ///     Encrypts the comma seprated list via <see cref="FileController.Encrypt"/> into a list of strings seperated by new lines..
        /// </summary>
        /// <param name="str">The string to be encrypted</param>
        /// <returns>The newly encrypted string seperated by new lines</returns>
        static string Encrypt(string str) => string.Join('\n', FileController.Encrypt(str.Split(", ").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray()));

        /// <summary>
        ///     This method is called on the post of the Question Maker page
        /// </summary>
        /// <returns>A json containing information about the error state.</returns>
        public JsonResult OnPost()
        {
            const string file_version = "1";
            try
            {
                AssertHasData(SecretWord, "At least one secret word must be present.");
                AssertHasData(ParsonsSecretWord, "At least one secret word must be present for parsons interface.");
                AssertHasData(TestCaseNumberOfCols, "The number of columns must be present, regardless if testcases are enabled.");
                AssertHasData(TestCaseNumberOfRows, "The number of rows to test must be present, regardless if testcases are enabled.");
                Question ??= "";

                //Hardcoding TextAreaEnable for now
                bool TextAreaEnableOn = true;
            
                string textToBeWritten = Question + "\n" + TextAreaEnableOn + "\n" + TestCasesON + "\n" + ParsonsOn
                    + "\nStartSecrets\n" + Encrypt(SecretWord) + "\nEndSecrets\n"
                    + "StartParsonsSecrets\n" + Encrypt(ParsonsSecretWord) + "\nEndParsonsSecrets\nL R " +
                    TestCaseNumberOfRows + "\nL C " + TestCaseNumberOfCols + "\n";

                // For each set of values per testCaseRow, write a line to the file.
                // There is a list of ID's to ignore. This is done because, otherwise, the list is full of garbage.
                #region PARSE_TEST_CASES
                foreach (string key in TestcaseRow.Keys.Where(key => key.StartsWith("TestCaseRow")))
                {
                    if (TestcaseRow[key] == null)
                    {
                        throw new ArgumentNullException("Row number is empty");
                    }
                    else if (TestcaseText[key] == null)
                    {
                        throw new ArgumentNullException("Value is empty");
                    }
                    // == is currently hardcoded. This was part of the original design from Team 1, but if one looks at the sql_scaffolding.js, you can see that 
                    // it supports other operators. I would have likes to expand this behavior, but we needed to ffocus on other components.
                    // -- Charles Haislip
                    textToBeWritten += GetColumnCheck(key) + " == " + TestcaseText[key] + '\n';
                }
                #endregion

                #region PARSE_PARSONS
                textToBeWritten += "Parsons\n";
                foreach (string row in Commands.Keys.Where(key => key.StartsWith("FormRowParsonsProblem") && !key.EndsWith("Dummy")))
                {
                    textToBeWritten += GetParsons(row);
                }
                textToBeWritten += "EndParsons\n";
                #endregion

                #region DATABASE
                textToBeWritten += "StartDatabase\n" + Database + "\nEndDatabase\n";
                #endregion

                #region FILENAME
                string fileName = $"{UnitNumber}.{QuestionNumber}.txt";
                #endregion

                #region VERSION_NUMBER
                textToBeWritten += file_version + '\n';
                #endregion

                #region WRITE_FILE
                if (p_filenameRe.IsMatch(fileName))
                {
                    // Write to the question file
                    FileController.WriteQuestion(fileName, textToBeWritten);
                }
                else
                {
                    // Notify invalid name
                    System.Diagnostics.Debug.Write("Invalid filename detected. Filename must match [number].[number].txt. Aborting write operation.");
                }
                #endregion
            }
            catch (Exception f)
            {
                // Notify of internal exception
                System.Diagnostics.Debug.Write(f);
                //WriteMessage(400, f.Message);
                Response.StatusCode = 400;
                return new JsonResult(f.Message);
            }
            return new JsonResult("Success");
        }
        #endif

        public IActionResult OnGet()
        {
            #if ADMIN
            return Page();
            #else
            return Redirect("/QuestionPage");
            #endif
        }

    }

}

