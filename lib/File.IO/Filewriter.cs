else if (s.StartsWith("new filewriter(") && s.EndsWith(")"))
                                    {
                                        if (Packages.FileIO)
                                        {
                                            script_var.Ifcount = 1;
                                            string res1 = s.Replace("new filewriter", "");
                                            string res2 = res1.TrimStart('(');
                                            string res3 = res2.TrimEnd(')');
                                            List<string> Result = new List<string>();
                                            try
                                            {
                                                string res4 = res3.Replace(", ", "/n");
                                                string[] valuearray = res4.Split("/n");
                                                if (valuearray[0].StartsWith('"') && valuearray[0].EndsWith('"'))
                                                {
                                                    //loc
                                                    string loc1 = valuearray[0].TrimStart('"');
                                                    string loc2 = loc1.TrimEnd('"');
                                                    Result.Add(loc2);
                                                }
                                                else
                                                {
                                                    int num = 0;
                                                    int num2 = 0;
                                                    foreach (string lines in Stringname)
                                                    {
                                                        num++;
                                                        if (lines == valuearray[0])
                                                        {
                                                            foreach (string lines2 in Stringvalue)
                                                            {
                                                                num2++;
                                                                //Console.WriteLine("num=" + num + " num2=" + num2);
                                                                if (num == num2) { Result.Add(lines2); }
                                                            }
                                                            //Console.WriteLine("match");
                                                        }
                                                    }
                                                }

                                                if (valuearray[1].StartsWith('"') && valuearray[1].EndsWith('"'))
                                                {
                                                    //value
                                                    string loc1 = valuearray[1].TrimStart('"');
                                                    string loc2 = loc1.TrimEnd('"');
                                                    Result.Add(loc2);
                                                }
                                                else
                                                {
                                                    int num = 0;
                                                    int num2 = 0;
                                                    foreach (string lines in Stringname)
                                                    {
                                                        num++;
                                                        if (lines == valuearray[1])
                                                        {
                                                            foreach (string lines2 in Stringvalue)
                                                            {
                                                                num2++;
                                                                //Console.WriteLine("num=" + num + " num2=" + num2);
                                                                if (num == num2) { Result.Add(lines2); }
                                                            }
                                                            //Console.WriteLine("match");
                                                        }
                                                    }
                                                }

                                                Filewriter(Result[0], Result[1]);

                                                void Filewriter(string path, string value)
                                                {
                                                    if (!File.Exists(path))
                                                    {
                                                        // Create a file to write to.
                                                        using (StreamWriter sw = File.CreateText(path))
                                                        {
                                                            sw.WriteLine(value);
                                                        }
                                                    }
                                                    else { File.AppendAllText(path, Environment.NewLine + value); }
                                                }
                                            }
                                            catch { throw new ArgumentException("Synax error at line '" + line + "'." + " C1793"); }

                                        }
                                        else { throw new ArgumentException("Synax error at line '" + line + "'." + " C3965"); }
                                    }
