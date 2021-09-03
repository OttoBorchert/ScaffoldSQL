using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace CapstoneIdeas.Pages.QuestionMaker
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Boolean ParsonsOn { get; set; } = true;

        [BindProperty]
        public String Test { get; set; }

        [BindProperty]
        public String Question { get; set; }

        [BindProperty]
        public String SecretWord { get; set; }

        [BindProperty]
        public String ParsonsSecretWord { get; set; }

        [BindProperty]
        public String testcase1 { get; set; }

        [BindProperty]
        public String testcase11 { get; set; }

        [BindProperty]
        public String testcase2 { get; set; }

        [BindProperty]
        public String testcase22 { get; set; }

        [BindProperty]
        public String testcase3 { get; set; }

        [BindProperty]
        public String testcase33 { get; set; }
        [BindProperty]
        public String testcase4 { get; set; }

        [BindProperty]
        public String testcase44 { get; set; }

        [BindProperty]
        public String testcase5 { get; set; }

        [BindProperty]
        public String testcase55 { get; set; }
        [BindProperty]
        public String testcase6 { get; set; }

        [BindProperty]
        public String testcase66 { get; set; }

        [BindProperty]
        public String testcase1111 { get; set; }

        [BindProperty]
        public String testcase2222 { get; set; }

        [BindProperty]
        public String testcase3333 { get; set; }

        [BindProperty]
        public String testcase4444 { get; set; }

        [BindProperty]
        public String testcase5555 { get; set; }

        [BindProperty]
        public String testcase6666 { get; set; }

        [BindProperty]
        public String firstParsonsCommand { get; set; }

        [BindProperty]
        public String firstParsonsOptions { get; set; }

        [BindProperty]
        public String secondParsonsCommand { get; set; }

        [BindProperty]
        public String secondParsonsOptions { get; set; }

        [BindProperty]
        public String thirdParsonsCommand { get; set; }

        [BindProperty]
        public String thirdParsonsOptions { get; set; }

        [BindProperty]
        public String fourthParsonsCommand { get; set; }

        [BindProperty]
        public String fourthParsonsOptions { get; set; }

        [BindProperty]
        public String fithParsonsCommand { get; set; }

        [BindProperty]
        public String fithParsonsOptions { get; set; }

        [BindProperty]
        public String sixthParsonsCommand { get; set; }

        [BindProperty]
        public String sixthParsonsOptions { get; set; }

        [BindProperty]
        public bool testcasetype3 { get; set; }

        [BindProperty]
        public bool testcasetype4 { get; set; }

        [BindProperty]
        public bool testcasetype5 { get; set; }

        [BindProperty]
        public bool testcasetype6 { get; set; }

        [BindProperty]
        public bool needsSingle1 { get; set; } = true;
        [BindProperty]
        public bool needsSingle2 { get; set; } = true;
        [BindProperty]
        public bool needsSingle3 { get; set; } = true;
        [BindProperty]
        public bool needsSingle4 { get; set; } = true;
        [BindProperty]
        public bool needsSingle5 { get; set; } = true;
        [BindProperty]
        public bool needsSingle6 { get; set; } = true;

        [BindProperty]
        public bool testCasesON { get; set; } = true;

        [BindProperty]
        public bool Acsending { get; set; } = false;

        [BindProperty]
        public bool Descending { get; set; } = false;



        public void OnGet()
        {
        }
        public void OnPost()
        {
            try
            {
                if (Question==null ||Question.Equals("Parsons"))
                {
                    Question += "1";
                }
                String textToBeWritten = Question + "\n" + ParsonsOn + "\n" + testCasesON + "\n" + SecretWord + "\n"+ ParsonsSecretWord +  "\nL R " +
                    testcase1 + "\nL C " + testcase2 + "\n";
                if (testcase3 != null)
                {
                    if (testcasetype3)
                        textToBeWritten += "C " + testcase3 + " == " + testcase3333 + "\n";
                    else
                        textToBeWritten += "V " + testcase3 + "," + testcase33 + " == " + testcase3333 + "\n";
                }
                if (testcase4 != null)
                {
                    if (testcasetype4)
                        textToBeWritten += "C " + testcase4 + " == " + testcase4444 + "\n";
                    else
                        textToBeWritten += "V " + testcase4 + "," + testcase44 + " == " + testcase4444 + "\n";
                }
                if (testcase5 != null)
                {
                    if (testcasetype5)
                        textToBeWritten += "C " + testcase5 + " == " + testcase5555 + "\n";
                    else
                        textToBeWritten += "V " + testcase5 + "," + testcase55 + " == " + testcase5555 + "\n";
                }
                if (testcase6 != null)
                {
                    if (testcasetype6)
                        textToBeWritten += "C " + testcase6 + " == " + testcase6666 + "\n";
                    else
                        textToBeWritten += "V " + testcase6 + "," + testcase66 + " == " + testcase6666 + "\n";
                }
                textToBeWritten += "Parsons\n";
                //done
                #region one
                List<string> options = firstParsonsOptions.Split(',').ToList<string>();
                if (secondParsonsOptions != null)
                {
                    //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                    if (firstParsonsCommand.Contains("INSERT INTO"))
                    {
                        textToBeWritten += "INSERT INTO $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$($$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$,$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$,$$toggle::" + options[9] + "::" + options[10] + "::" + options[11] + "$$\n";
                    }
                    else if (firstParsonsCommand.Contains("WHERE") || firstParsonsCommand.Contains("AND"))
                    {
                        if ((options[3].All(char.IsDigit)))
                            textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ = $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$\n";
                        else
                            textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] +             "$$ = '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$'\n";
                    }
                    else if (firstParsonsCommand.Equals("ON"))
                    {
                        textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[2] + "::" + options[3] + "$$ = $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$\n";
                    }
                    else if (firstParsonsCommand.Contains("VALUES"))
                    {
                        //We have to check for strings here but for only three places, and all the combos 1,2,3 and 1,2 and 1,3 and 2,3 and all the individuals

                        //perfectly all numbers
                        if ((options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && (options[7].All(char.IsDigit)))
                        {
                            //all integers
                            // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                            textToBeWritten += "VALUES ($$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$ , $$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$ )\n";
                            // textToBeWritten += "\" VALUES"
                        }
                        //checks to see if just the third needs it
                        else if ((options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && !(options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$ , '$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$' )\n";
                        }
                        //checks to see if the second needs it
                        else if ((options[0].All(char.IsDigit)) && !(options[3].All(char.IsDigit)) && (options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$' , $$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$ )\n";
                        }
                        //checks to see if the first needs it
                        else if (!(options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && (options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$ , $$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$ )\n";
                        }
                        //checks to see if the first and the second need it
                        else if (!(options[0].All(char.IsDigit)) && !(options[3].All(char.IsDigit)) && (options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$' , $$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$ )\n";
                        }
                        //checks to see if the first and the third need it
                        else if (!(options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && !(options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$ , '$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$' )\n";
                        }
                        //checks to see if the second and the third need it
                        else if (!(options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && !(options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$' , '$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$' )\n";
                        }
                        else
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$' , '$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$' )\n";
                        }
                    }
                    else
                    {
                        if (options.Count > 3)
                        {
                            if (((options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit))) || !needsSingle1)
                            {
                                textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$\n";
                            }
                            else
                            {
                                if ((!options[0].All(char.IsDigit)) && !(options[3].All(char.IsDigit)))
                                {
                                    textToBeWritten += firstParsonsCommand + " '$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$'\n";
                                }
                                else if (options[0].All(char.IsDigit))
                                {
                                    textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$'\n";
                                }
                                else if ((options[0].All(char.IsDigit)))
                                {
                                    textToBeWritten += firstParsonsCommand + " '$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$\n";
                                }

                            }

                        }
                        else
                            textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$\n";
                    }

                }
                else
                {
                    //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                    if (firstParsonsCommand.Contains("INSERT INTO"))
                    {
                        textToBeWritten += "INSERT INTO $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$($$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$,$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$,$$toggle::" + options[9] + "::" + options[10] + "::" + options[11] + "$$\n";
                    }

                    else if (firstParsonsCommand.Contains("WHERE") || firstParsonsCommand.Contains("AND"))
                    {
                        if ((options[3].All(char.IsDigit)))
                            textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[2] + "::" + options[3] + "$$ = $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$\n";
                        else
                            textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[2] + "::" + options[3] + "$$ = '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$'\n";
                    }
                    else if (firstParsonsCommand.Equals("ON"))
                    {
                        textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[2] + "::" + options[3] + "$$ = $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$\n";
                    }
                    else if (firstParsonsCommand.Contains("VALUES"))
                    {
                        //We have to check for strings here but for only three places, and all the combos 1,2,3 and 1,2 and 1,3 and 2,3 and all the individuals

                        //perfectly all numbers
                        if ((options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && (options[7].All(char.IsDigit)))
                        {
                            //all integers
                            // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                            textToBeWritten += "VALUES ($$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$ , $$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$ )\n";
                            // textToBeWritten += "\" VALUES"
                        }
                        //checks to see if just the third needs it
                        else if ((options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && !(options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$ , '$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$' )\n";
                        }
                        //checks to see if the second needs it
                        else if ((options[0].All(char.IsDigit)) && !(options[3].All(char.IsDigit)) && (options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$' , $$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$ )\n";
                        }
                        //checks to see if the first needs it
                        else if (!(options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && (options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$ , $$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$ )\n";
                        }
                        //checks to see if the first and the second need it
                        else if (!(options[0].All(char.IsDigit)) && !(options[3].All(char.IsDigit)) && (options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$' , $$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$ )\n";
                        }
                        //checks to see if the first and the third need it
                        else if (!(options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && !(options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$ , '$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$' )\n";
                        }
                        //checks to see if the second and the third need it
                        else if (!(options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit)) && !(options[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$' , '$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$' )\n";
                        }
                        else
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$' , '$$toggle::" + options[6] + "::" + options[7] + "::" + options[8] + "$$' )\n";
                        }
                    }
                    else
                    {
                        if (options.Count > 3)
                        {
                            if (((options[0].All(char.IsDigit)) && (options[3].All(char.IsDigit))) || !needsSingle1)
                            {
                                textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$\n";
                            }
                            else
                            {
                                if ((!options[0].All(char.IsDigit)) && !(options[3].All(char.IsDigit)))
                                {
                                    textToBeWritten += firstParsonsCommand + " '$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$'\n";
                                }
                                else if (options[0].All(char.IsDigit))
                                {
                                    textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$ , '$$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$'\n";
                                }
                                else if ((options[0].All(char.IsDigit)))
                                {
                                    textToBeWritten += firstParsonsCommand + " '$$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$' , $$toggle::" + options[3] + "::" + options[4] + "::" + options[5] + "$$\n";
                                }

                            }

                        }
                        else
                            textToBeWritten += firstParsonsCommand + " $$toggle::" + options[0] + "::" + options[1] + "::" + options[2] + "$$\n";
                    }

                }
                #endregion
                //done
                #region two
                if(secondParsonsOptions != null)
                {
                    List<string> options2 = secondParsonsOptions.Split(',').ToList<string>();
                    if (thirdParsonsOptions != null)
                    {
                        //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                        if (secondParsonsCommand.Contains("INSERT INTO"))
                        {
                            textToBeWritten += "INSERT INTO $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$($$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$,$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$,$$toggle::" + options2[9] + "::" + options2[10] + "::" + options2[11] + "$$\n";
                        }
                        else if (secondParsonsCommand.Contains("WHERE") || firstParsonsCommand.Contains("AND"))
                        {
                            if ((options[3].All(char.IsDigit)))
                                textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ = $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$\n";
                            else
                                textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ = '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$'\n";
                        }
                        else if (secondParsonsCommand.Equals("ON"))
                        {
                            textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[2] + "::" + options2[3] + "$$ = $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$\n";
                        }
                        else if (secondParsonsCommand.Contains("VALUES"))
                        {
                            //We have to check for strings here but for only three places, and all the combos 1,2,3 and 1,2 and 1,3 and 2,3 and all the individuals
                            //perfectly all numbers
                            if ((options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && (options2[6].All(char.IsDigit)))
                            {
                                //all integers
                                // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                                textToBeWritten += "VALUES ($$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$ , $$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$)\n";
                                // textToBeWritten += "\" VALUES"
                            }
                            //checks to see if just the third needs it
                            else if ((options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && !(options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$ , '$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$')\n";
                            }
                            //checks to see if the second needs it
                            else if ((options2[0].All(char.IsDigit)) && !(options2[3].All(char.IsDigit)) && (options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$' , $$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$ )\n";
                            }
                            //checks to see if the first needs it
                            else if (!(options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && (options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$ , $$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$ )\n";
                            }
                            //checks to see if the first and the second need it
                            else if (!(options2[0].All(char.IsDigit)) && !(options2[3].All(char.IsDigit)) && (options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$' , $$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$ )\n";
                            }
                            //checks to see if the first and the third need it
                            else if (!(options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && !(options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$ , '$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$' )\n";
                            }
                            //checks to see if the second and the third need it
                            else if (!(options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && !(options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$' , '$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$' )\n";
                            }
                            else
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$' , '$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$' )\n";
                            }
                        }
                        else
                        {
                            if (options2.Count > 3)
                            {
                                if (((options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit))) || !needsSingle2)
                                {
                                    textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$\n";
                                }
                                else
                                {
                                    if ((!options2[0].All(char.IsDigit)) && !(options2[3].All(char.IsDigit)))
                                    {
                                        textToBeWritten += secondParsonsCommand + " '$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$'\n";
                                    }
                                    else if (options2[0].All(char.IsDigit))
                                    {
                                        textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$'\n";
                                    }
                                    else if ((options2[0].All(char.IsDigit)))
                                    {
                                        textToBeWritten += secondParsonsCommand + " '$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$\n";
                                    }


                                }

                            }
                            else
                                textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$\n";
                        }

                    }
                    else
                    {
                        //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                        if (secondParsonsCommand.Contains("INSERT INTO"))
                        {
                            textToBeWritten += "INSERT INTO $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$($$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$,$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$,$$toggle::" + options2[9] + "::" + options2[10] + "::" + options2[11] + "$$\n";
                        }
                        else if (secondParsonsCommand.Contains("WHERE") || firstParsonsCommand.Contains("AND"))
                        {
                            if ((options[3].All(char.IsDigit)))
                                textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[3] + "$$ = $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$\n";
                            else
                                textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[1] + "$$ = '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$'\n";
                        }
                        else if (secondParsonsCommand.Equals("ON"))
                        {
                            textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[2] + "::" + options2[3] + "$$ = $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$\n";
                        }
                        else if (secondParsonsCommand.Contains("VALUES"))
                        {
                            //We have to check for strings here but for only three places, and all the combos 1,2,3 and 1,2 and 1,3 and 2,3 and all the individuals
                            //perfectly all numbers
                            if ((options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && (options2[6].All(char.IsDigit)))
                            {
                                //all integers
                                // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                                textToBeWritten += "VALUES ($$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$ , $$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$)\n";
                                // textToBeWritten += "\" VALUES"
                            }
                            //checks to see if just the third needs it
                            else if ((options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && !(options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$ , '$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$')\n";
                            }
                            //checks to see if the second needs it
                            else if ((options2[0].All(char.IsDigit)) && !(options2[3].All(char.IsDigit)) && (options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$' , $$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$ )\n";
                            }
                            //checks to see if the first needs it
                            else if (!(options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && (options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$ , $$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$ )\n";
                            }
                            //checks to see if the first and the second need it
                            else if (!(options2[0].All(char.IsDigit)) && !(options2[3].All(char.IsDigit)) && (options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$' , $$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$ )\n";
                            }
                            //checks to see if the first and the third need it
                            else if (!(options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && !(options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$ , '$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$' )\n";
                            }
                            //checks to see if the second and the third need it
                            else if (!(options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit)) && !(options2[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$' , '$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$' )\n";
                            }
                            else
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$' , '$$toggle::" + options2[6] + "::" + options2[7] + "::" + options2[8] + "$$' )\n";
                            }
                        }
                        else
                        {
                            if (options2.Count > 3)
                            {
                                if (((options2[0].All(char.IsDigit)) && (options2[3].All(char.IsDigit))) || needsSingle2)
                                {
                                    textToBeWritten += secondParsonsCommand + "$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$\n";
                                }
                                else
                                {
                                    if ((!options2[0].All(char.IsDigit)) && !(options2[3].All(char.IsDigit)))
                                    {
                                        textToBeWritten += secondParsonsCommand + "'$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$'\n";
                                    }
                                    else if (options2[0].All(char.IsDigit))
                                    {
                                        textToBeWritten += secondParsonsCommand + "$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$ , '$$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$'\n";
                                    }
                                    else if ((options2[0].All(char.IsDigit)))
                                    {
                                        textToBeWritten += secondParsonsCommand + "'$$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$' , $$toggle::" + options2[3] + "::" + options2[4] + "::" + options2[5] + "$$\n";
                                    }


                                }

                            }
                            else
                                textToBeWritten += secondParsonsCommand + " $$toggle::" + options2[0] + "::" + options2[1] + "::" + options2[2] + "$$\n";
                        }

                    }
                }
                #endregion
                //done
                #region three
                if (thirdParsonsOptions != null)
                {
                    List<string> options3 = thirdParsonsOptions.Split(',').ToList<string>();
                    if (fourthParsonsOptions != null)
                    {
                        //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                        if (thirdParsonsCommand.Contains("INSERT INTO"))
                        {
                            textToBeWritten += "INSERT INTO $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$($$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$,$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$,$$toggle::" + options3[9] + "::" + options3[10] + "::" + options3[11] + "$$\n";
                        }
                        else if (thirdParsonsCommand.Contains("WHERE") || thirdParsonsCommand.Contains("AND"))
                        {
                            if ((options[3].All(char.IsDigit)))
                                textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ = $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$\n";
                            else
                                textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ = '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$'\n";
                        }
                        else if (thirdParsonsCommand.Equals("ON"))
                        {
                            textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ = $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$\n";
                        }
                        else if (thirdParsonsCommand.Contains("VALUES"))
                        {
                            //We have to check for strings here but for only three places, and all the combos 1,2,3 and 1,2 and 1,3 and 2,3 and all the individuals
                            //perfectly all numbers
                            if ((options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && (options3[6].All(char.IsDigit)))
                            {
                                //all integers
                                // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                                textToBeWritten += "VALUES ($$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ , $$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$)\n";
                                // textToBeWritten += "\" VALUES"
                            }
                            //checks to see if just the third needs it
                            else if ((options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && !(options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ , '$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$')\n";
                            }
                            //checks to see if the second needs it
                            else if ((options3[0].All(char.IsDigit)) && !(options3[3].All(char.IsDigit)) && (options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$' , $$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$ )\n";
                            }
                            //checks to see if the first needs it
                            else if (!(options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && (options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ , $$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$ )\n";
                            }
                            //checks to see if the first and the second need it
                            else if (!(options3[0].All(char.IsDigit)) && !(options3[3].All(char.IsDigit)) && (options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$' , $$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$ )\n";
                            }
                            //checks to see if the first and the third need it
                            else if (!(options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && !(options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ , '$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$' )\n";
                            }
                            //checks to see if the second and the third need it
                            else if (!(options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && !(options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$' , '$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$' )\n";
                            }
                            else
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$' , '$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$' )\n";
                            }
                        }
                        else
                        {
                            if (options3.Count > 3)
                            {
                                if (((options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit))) || !needsSingle3)
                                {
                                    textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$\n";
                                }
                                else
                                {
                                    if ((!options3[0].All(char.IsDigit)) && !(options3[3].All(char.IsDigit)))
                                    {
                                        textToBeWritten += thirdParsonsCommand + " '$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$'\n";
                                    }
                                    else if (options3[0].All(char.IsDigit))
                                    {
                                        textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$'\n";
                                    }
                                    else if ((options3[0].All(char.IsDigit)))
                                    {
                                        textToBeWritten += thirdParsonsCommand + " '$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ \n";
                                    }


                                }

                            }
                            else
                                textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$\n";
                        }

                    }
                    else
                    {
                        //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                        if (thirdParsonsCommand.Contains("INSERT INTO"))
                        {
                            textToBeWritten += "INSERT INTO $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$($$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$,$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$,$$toggle::" + options3[9] + "::" + options3[10] + "::" + options3[11] + "$$\n";
                        }
                        else if (thirdParsonsCommand.Contains("WHERE") || thirdParsonsCommand.Contains("AND"))
                        {
                            if ((options[3].All(char.IsDigit)))
                                textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ = $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$\n";
                            else
                                textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ = '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$'\n";
                        }
                        else if (thirdParsonsCommand.Equals("ON"))
                        {
                            textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ = $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$\n";
                        }
                        else if (thirdParsonsCommand.Contains("VALUES"))
                        {
                            //We have to check for strings here but for only three places, and all the combos 1,2,3 and 1,2 and 1,3 and 2,3 and all the individuals
                            //perfectly all numbers
                            if ((options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && (options3[6].All(char.IsDigit)))
                            {
                                //all integers
                                // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                                textToBeWritten += "VALUES ($$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ , $$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$)\n";
                                // textToBeWritten += "\" VALUES"
                            }
                            //checks to see if just the third needs it
                            else if ((options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && !(options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ , '$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$')\n";
                            }
                            //checks to see if the second needs it
                            else if ((options3[0].All(char.IsDigit)) && !(options3[3].All(char.IsDigit)) && (options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$' , $$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$ )\n";
                            }
                            //checks to see if the first needs it
                            else if (!(options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && (options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ , $$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$ )\n";
                            }
                            //checks to see if the first and the second need it
                            else if (!(options3[0].All(char.IsDigit)) && !(options3[3].All(char.IsDigit)) && (options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$' , $$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$ )\n";
                            }
                            //checks to see if the first and the third need it
                            else if (!(options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && !(options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ , '$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$' )\n";
                            }
                            //checks to see if the second and the third need it
                            else if (!(options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit)) && !(options3[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$' , '$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$' )\n";
                            }
                            else
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$' , '$$toggle::" + options3[6] + "::" + options3[7] + "::" + options3[8] + "$$' )\n";
                            }
                        }
                        else
                        {
                            if (options3.Count > 3)
                            {
                                if (((options3[0].All(char.IsDigit)) && (options3[3].All(char.IsDigit))) || !needsSingle3)
                                {
                                    textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$\n";
                                }
                                else
                                {
                                    if ((!options3[0].All(char.IsDigit)) && !(options3[3].All(char.IsDigit)))
                                    {
                                        textToBeWritten += thirdParsonsCommand + " '$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$'\n";
                                    }
                                    else if (options3[0].All(char.IsDigit))
                                    {
                                        textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$ , '$$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$'\n";
                                    }
                                    else if ((options3[0].All(char.IsDigit)))
                                    {
                                        textToBeWritten += thirdParsonsCommand + " '$$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$' , $$toggle::" + options3[3] + "::" + options3[4] + "::" + options3[5] + "$$ \n";
                                    }
                                }

                            }
                            else
                                textToBeWritten += thirdParsonsCommand + " $$toggle::" + options3[0] + "::" + options3[1] + "::" + options3[2] + "$$\n";
                        }
                    }
                }
                #endregion
                //done
                #region four
                if(fourthParsonsOptions != null)
                {
                    List<string> options4 = fourthParsonsOptions.Split(',').ToList<string>();
                    if (fithParsonsOptions != null)
                    {
                        //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                        if (fourthParsonsCommand.Contains("INSERT INTO"))
                        {
                            textToBeWritten += "INSERT INTO $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$($$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$,$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$,$$toggle::" + options4[9] + "::" + options4[10] + "::" + options4[11] + "$$\n";
                        }
                        else if (fourthParsonsCommand.Contains("WHERE") || fourthParsonsCommand.Contains("AND"))
                        {
                            if ((options[4].All(char.IsDigit)))
                                textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[4] + "::" + options4[4] + "$$ = $$toggle::" + options4[4] + "::" + options4[4] + "::" + options4[5] + "$$\n";
                            else
                                textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[4] + "::" + options4[4] + "$$ = '$$toggle::" + options4[4] + "::" + options4[4] + "::" + options4[5] + "$$'\n";
                        }
                        else if (fourthParsonsCommand.Equals("ON"))
                        {
                            textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[4] + "::" + options4[4] + "$$ = $$toggle::" + options4[4] + "::" + options4[4] + "::" + options4[5] + "$$\n";
                        }
                        else if (fourthParsonsCommand.Contains("VALUES"))
                        {
                            //We have to check for strings here but for only three places, and all the combos 1,2,3 and 1,2 and 1,3 and 2,3 and all the individuals
                            //perfectly all numbers
                            if ((options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && (options4[6].All(char.IsDigit)))
                            {
                                //all integers
                                // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                                textToBeWritten += "VALUES ($$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ , $$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$)\n";
                                // textToBeWritten += "\" VALUES"
                            }
                            //checks to see if just the third needs it
                            else if ((options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && !(options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ , '$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$')\n";
                            }
                            //checks to see if the second needs it
                            else if ((options4[0].All(char.IsDigit)) && !(options4[3].All(char.IsDigit)) && (options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$' , $$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$ )\n";
                            }
                            //checks to see if the first needs it
                            else if (!(options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && (options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ , $$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$ )\n";
                            }
                            //checks to see if the first and the second need it
                            else if (!(options4[0].All(char.IsDigit)) && !(options4[3].All(char.IsDigit)) && (options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$' , $$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$ )\n";
                            }
                            //checks to see if the first and the third need it
                            else if (!(options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && !(options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ , '$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$' )\n";
                            }
                            //checks to see if the second and the third need it
                            else if (!(options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && !(options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$' , '$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$' )\n";
                            }
                            else
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$' , '$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$' )\n";
                            }
                        }
                        else
                        {
                            if (options4.Count > 3)
                            {
                                if (((options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit))) || !needsSingle4)
                                {
                                    textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$\n";
                                }
                                else
                                {
                                    if ((!options4[0].All(char.IsDigit)) && !(options4[3].All(char.IsDigit)))
                                    {
                                        textToBeWritten += fourthParsonsCommand + " '$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$'\n";
                                    }
                                    else if (options4[0].All(char.IsDigit))
                                    {
                                        textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$'\n";
                                    }
                                    else if ((options4[0].All(char.IsDigit)))
                                    {
                                        textToBeWritten += fourthParsonsCommand + " '$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ \n";
                                    }


                                }

                            }
                            else
                                textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$\n";
                        }

                    }
                    else
                    {
                        //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                        if (fourthParsonsCommand.Contains("INSERT INTO"))
                        {
                            textToBeWritten += "INSERT INTO $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$($$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$,$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$,$$toggle::" + options4[9] + "::" + options4[10] + "::" + options4[11] + "$$\n";
                        }
                        else if (fourthParsonsCommand.Contains("WHERE") || fourthParsonsCommand.Contains("AND"))
                        {
                            if ((options4[3].All(char.IsDigit)))
                                textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ = $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$\n";
                            else
                                textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ = '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$'\n";
                        }
                        else if (fourthParsonsCommand.Equals("ON"))
                        {
                            textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ = $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$\n";
                        }
                        else if (fourthParsonsCommand.Contains("VALUES"))
                        {
                            //We have to check for strings here but for only three places, and all the combos 1,2,3 and 1,2 and 1,3 and 2,3 and all the individuals
                            //perfectly all numbers
                            if ((options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && (options4[6].All(char.IsDigit)))
                            {
                                //all integers
                                // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                                textToBeWritten += "VALUES ($$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ , $$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$)\n";
                                // textToBeWritten += "\" VALUES"
                            }
                            //checks to see if just the third needs it
                            else if ((options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && !(options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ , '$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$')\n";
                            }
                            //checks to see if the second needs it
                            else if ((options4[0].All(char.IsDigit)) && !(options4[3].All(char.IsDigit)) && (options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$' , $$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$ )\n";
                            }
                            //checks to see if the first needs it
                            else if (!(options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && (options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ , $$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$ )\n";
                            }
                            //checks to see if the first and the second need it
                            else if (!(options4[0].All(char.IsDigit)) && !(options4[3].All(char.IsDigit)) && (options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$' , $$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$ )\n";
                            }
                            //checks to see if the first and the third need it
                            else if (!(options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && !(options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ , '$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$' )\n";
                            }
                            //checks to see if the second and the third need it
                            else if (!(options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit)) && !(options4[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$' , '$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$' )\n";
                            }
                            else
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$' , '$$toggle::" + options4[6] + "::" + options4[7] + "::" + options4[8] + "$$' )\n";
                            }
                        }
                        else
                        {
                            if (options4.Count > 3)
                            {
                                if (((options4[0].All(char.IsDigit)) && (options4[3].All(char.IsDigit))) || !needsSingle4)
                                {
                                    textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$\n";
                                }
                                else
                                {
                                    if ((!options4[0].All(char.IsDigit)) && !(options4[3].All(char.IsDigit)))
                                    {
                                        textToBeWritten += fourthParsonsCommand + " '$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$'\n";
                                    }
                                    else if (options4[0].All(char.IsDigit))
                                    {
                                        textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$ , '$$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$'\n";
                                    }
                                    else if ((options4[0].All(char.IsDigit)))
                                    {
                                        textToBeWritten += fourthParsonsCommand + " '$$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$' , $$toggle::" + options4[3] + "::" + options4[4] + "::" + options4[5] + "$$ \n";
                                    }


                                }

                            }
                            else
                                textToBeWritten += fourthParsonsCommand + " $$toggle::" + options4[0] + "::" + options4[1] + "::" + options4[2] + "$$\n";
                        }

                    }
                }
                #endregion
                //done
                #region five
                if (fithParsonsOptions != null)
                {
                    List<string> options5 = fithParsonsOptions.Split(',').ToList<string>();
                    if (sixthParsonsOptions != null)
                    {
                        //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                        if (fithParsonsCommand.Contains("INSERT INTO"))
                        {
                            textToBeWritten += "INSERT INTO $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$($$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$,$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$,$$toggle::" + options5[9] + "::" + options5[10] + "::" + options5[11] + "$$\n";
                        }
                        else if (fithParsonsCommand.Contains("WHERE") || fithParsonsCommand.Contains("AND"))
                        {
                            if ((options5[5].All(char.IsDigit)))
                                textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ = $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$\n";
                            else
                                textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ = '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$'\n";
                        }
                        else if (fithParsonsCommand.Equals("ON"))
                        {
                            textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ = $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$\n";
                        }
                        else if (fithParsonsCommand.Contains("VALUES"))
                        {
                            //We have to check for strings here but for only three places, and all the combos 1,2,5 and 1,2 and 1,5 and 2,5 and all the individuals
                            //perfectly all numbers
                            if ((options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && (options5[6].All(char.IsDigit)))
                            {
                                //all integers
                                // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                                textToBeWritten += "VALUES ($$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ , $$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$)\n";
                                // textToBeWritten += "\" VALUES"
                            }
                            //checks to see if just the fith needs it
                            else if ((options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && !(options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ , '$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$')\n";
                            }
                            //checks to see if the second needs it
                            else if ((options5[0].All(char.IsDigit)) && !(options5[5].All(char.IsDigit)) && (options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$' , $$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$ )\n";
                            }
                            //checks to see if the first needs it
                            else if (!(options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && (options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ , $$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$ )\n";
                            }
                            //checks to see if the first and the second need it
                            else if (!(options5[0].All(char.IsDigit)) && !(options5[5].All(char.IsDigit)) && (options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$' , $$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$ )\n";
                            }
                            //checks to see if the first and the fith need it
                            else if (!(options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && !(options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ , '$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$' )\n";
                            }
                            //checks to see if the second and the fith need it
                            else if (!(options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && !(options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$' , '$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$' )\n";
                            }
                            else
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$' , '$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$' )\n";
                            }
                        }
                        else
                        {
                            if (options5.Count > 5)
                            {
                                if (((options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit))) || !needsSingle5)
                                {
                                    textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$\n";
                                }
                                else
                                {
                                    if ((!options5[0].All(char.IsDigit)) && !(options5[5].All(char.IsDigit)))
                                    {
                                        textToBeWritten += fithParsonsCommand + " '$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$'\n";
                                    }
                                    else if (options5[0].All(char.IsDigit))
                                    {
                                        textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$'\n";
                                    }
                                    else if ((options5[0].All(char.IsDigit)))
                                    {
                                        textToBeWritten += fithParsonsCommand + " '$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ \n";
                                    }


                                }

                            }
                            else
                                textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$\n";
                        }

                    }
                    else
                    {
                        //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                        if (fithParsonsCommand.Contains("INSERT INTO"))
                        {
                            textToBeWritten += "INSERT INTO $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$($$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$,$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$,$$toggle::" + options5[9] + "::" + options5[10] + "::" + options5[11] + "$$\n";
                        }
                        else if (fithParsonsCommand.Contains("WHERE") || fithParsonsCommand.Contains("AND"))
                        {
                            if ((options5[5].All(char.IsDigit)))
                                textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ = $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$\n";
                            else
                                textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ = '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$'\n";
                        }
                        else if (fithParsonsCommand.Equals("ON"))
                        {
                            textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ = $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$\n";
                        }
                        else if (fithParsonsCommand.Contains("VALUES"))
                        {
                            //We have to check for strings here but for only three places, and all the combos 1,2,5 and 1,2 and 1,5 and 2,5 and all the individuals
                            //perfectly all numbers
                            if ((options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && (options5[6].All(char.IsDigit)))
                            {
                                //all integers
                                // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                                textToBeWritten += "VALUES ($$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ , $$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$)\n";
                                // textToBeWritten += "\" VALUES"
                            }
                            //checks to see if just the fith needs it
                            else if ((options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && !(options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ , '$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$')\n";
                            }
                            //checks to see if the second needs it
                            else if ((options5[0].All(char.IsDigit)) && !(options5[5].All(char.IsDigit)) && (options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$' , $$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$ )\n";
                            }
                            //checks to see if the first needs it
                            else if (!(options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && (options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ , $$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$ )\n";
                            }
                            //checks to see if the first and the second need it
                            else if (!(options5[0].All(char.IsDigit)) && !(options5[5].All(char.IsDigit)) && (options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$' , $$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$ )\n";
                            }
                            //checks to see if the first and the fith need it
                            else if (!(options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && !(options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ , '$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$' )\n";
                            }
                            //checks to see if the second and the fith need it
                            else if (!(options5[0].All(char.IsDigit)) && (options5[5].All(char.IsDigit)) && !(options5[6].All(char.IsDigit)))
                            {
                                textToBeWritten += "VALUES ($$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$' , '$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$' )\n";
                            }
                            else
                            {
                                textToBeWritten += "VALUES ('$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$' , '$$toggle::" + options5[6] + "::" + options5[7] + "::" + options5[8] + "$$' )\n";
                            }
                        }
                        else
                        {
                            if (options5.Count > 5)
                            {
                                if (((options5[0].All(char.IsDigit)) && (options5[3].All(char.IsDigit))) || !needsSingle5)
                                {
                                    textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$\n";
                                }
                                else
                                {
                                    if ((!options5[0].All(char.IsDigit)) && !(options5[5].All(char.IsDigit)))
                                    {
                                        textToBeWritten += fithParsonsCommand + " '$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$'\n";
                                    }
                                    else if (options5[0].All(char.IsDigit))
                                    {
                                        textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$ , '$$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$'\n";
                                    }
                                    else if ((options5[0].All(char.IsDigit)))
                                    {
                                        textToBeWritten += fithParsonsCommand + " '$$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$' , $$toggle::" + options5[3] + "::" + options5[4] + "::" + options5[5] + "$$ \n";
                                    }


                                }

                            }
                            else
                                textToBeWritten += fithParsonsCommand + " $$toggle::" + options5[0] + "::" + options5[1] + "::" + options5[2] + "$$\n";
                        }
                    }
                }
                #endregion
                //done
                #region six
                if (sixthParsonsOptions != null)
                {
                    List<string> options6 = sixthParsonsOptions.Split(',').ToList<string>();

                    //This will work fine because you can choose three columns to update into, but just make the user pass null values if they don't want to use three colummns
                    if (sixthParsonsCommand.Contains("INSERT INTO"))
                    {
                        textToBeWritten += "INSERT INTO $$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$($$toggle::" + options6[3] + "::" + options6[4] + "::" + options6[5] + "$$,$$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$,$$toggle::" + options6[9] + "::" + options6[10] + "::" + options6[11] + "$$\n";
                    }
                    else if (sixthParsonsCommand.Contains("WHERE") || sixthParsonsCommand.Contains("AND"))
                    {
                        if ((options6[6].All(char.IsDigit)))
                            textToBeWritten += sixthParsonsCommand + " $$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ = $$toggle::" + options6[3] + "::" + options6[4] + "::" + options6[5] + "$$\n";
                        else
                            textToBeWritten += sixthParsonsCommand + " $$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ = '$$toggle::" + options6[3] + "::" + options6[4] + "::" + options6[5] + "$$'\n";
                    }
                    else if (sixthParsonsCommand.Equals("ON"))
                    {
                        textToBeWritten += sixthParsonsCommand + " $$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ = $$toggle::" + options6[3] + "::" + options6[4] + "::" + options6[5] + "$$\n";
                    }
                    else if (secondParsonsCommand.Contains("VALUES"))
                    {
                        //We have to check for strings here but for only three places, and all the combos 1,2,6 and 1,2 and 1,6 and 2,6 and all the individuals
                        //perfectly all numbers
                        if ((options6[0].All(char.IsDigit)) && (options6[3].All(char.IsDigit)) && (options6[6].All(char.IsDigit)))
                        {
                            //all integers
                            // "VALUES ('$$toggle::Rakem$$' , " + "$$toggle::1$$ , " + "$$toggle::0$$ );\n" +
                            textToBeWritten += "VALUES ($$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ , $$toggle::" + options6[3] + "::" + options6[4] + "::" + options6[5] + "$$ , $$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$)\n";
                            // textToBeWritten += "\" VALUES"
                        }
                        //checks to see if just the sixth needs it
                        else if ((options6[0].All(char.IsDigit)) && (options6[3].All(char.IsDigit)) && !(options6[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ , $$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$ , '$$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$')\n";
                        }
                        //checks to see if the second needs it
                        else if ((options6[0].All(char.IsDigit)) && !(options6[6].All(char.IsDigit)) && (options6[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ , '$$toggle::" + options6[3] + "::" + options6[4] + "::" + options6[5] + "$$' , $$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$ )\n";
                        }
                        //checks to see if the first needs it
                        else if (!(options6[0].All(char.IsDigit)) && (options6[3].All(char.IsDigit)) && (options6[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$' , $$toggle::" + options6[3] + "::" + options6[4] + "::" + options6[5] + "$$ , $$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$ )\n";
                        }
                        //checks to see if the first and the second need it
                        else if (!(options6[0].All(char.IsDigit)) && !(options6[6].All(char.IsDigit)) && (options6[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$' , '$$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$' , $$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$ )\n";
                        }
                        //checks to see if the first and the sixth need it
                        else if (!(options6[0].All(char.IsDigit)) && (options6[6].All(char.IsDigit)) && !(options6[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$' , $$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$ , '$$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$' )\n";
                        }
                        //checks to see if the second and the sixth need it
                        else if (!(options6[0].All(char.IsDigit)) && (options6[6].All(char.IsDigit)) && !(options6[6].All(char.IsDigit)))
                        {
                            textToBeWritten += "VALUES ($$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ , '$$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$' , '$$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$' )\n";
                        }
                        else
                        {
                            textToBeWritten += "VALUES ('$$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$' , '$$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$' , '$$toggle::" + options6[6] + "::" + options6[7] + "::" + options6[8] + "$$' )\n";
                        }
                    }
                    else
                    {
                        if (options6.Count > 6)
                        {
                            if ((options6[0].All(char.IsDigit)) && (options6[6].All(char.IsDigit)))
                            {
                                textToBeWritten += sixthParsonsCommand + " $$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ , $$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$\n";
                            }
                            else
                            {
                                if (((!options6[0].All(char.IsDigit)) && !(options6[6].All(char.IsDigit))) || !needsSingle6)
                                {
                                    textToBeWritten += sixthParsonsCommand + " '$$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$' , '$$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$'\n";
                                }
                                else if (options6[0].All(char.IsDigit))
                                {
                                    textToBeWritten += sixthParsonsCommand + " $$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$ , '$$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$'\n";
                                }
                                else if ((options6[0].All(char.IsDigit)))
                                {
                                    textToBeWritten += sixthParsonsCommand + " '$$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$' , $$toggle::" + options6[6] + "::" + options6[4] + "::" + options6[5] + "$$ \n";
                                }


                            }

                        }
                        else
                            textToBeWritten += sixthParsonsCommand + " $$toggle::" + options6[0] + "::" + options6[1] + "::" + options6[2] + "$$\n";
                    }

                }
                #endregion
                #region ascending
                if (Acsending && Descending) 
                {
                    //do nothing
                }
                else if (Acsending)
                {
                    textToBeWritten += "asc";
                }
                else if (Descending)
                {
                    textToBeWritten += "desc";
                }
                #endregion
                string fileName = Test;
                fileName = String.Concat(fileName, ".txt");
                string destPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", fileName);
                if (fileName.Equals(""))
                {

                }
                else
                {
                    System.IO.File.WriteAllText(destPath, textToBeWritten);
                }


            }

            catch (Exception f)
            {
                System.Diagnostics.Debug.Write(f);
            }
        }
    }

}

