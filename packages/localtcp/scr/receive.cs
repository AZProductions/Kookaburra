using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

            //Code starts here.
            //AZ Productions 2021
            void receivefile(string ip)
            {
                try
                {
                    TcpClient tcpclnt = new TcpClient();
                    Console.WriteLine("Connecting.....");

                    tcpclnt.Connect("127.0.0.1", 8001);
                    // use the ipaddress as in the server program

                    Console.WriteLine("Connected");
                    Console.Write("Send: ");

                    String str = Console.ReadLine();
                    Stream stm = tcpclnt.GetStream();

                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    Console.WriteLine("Transmitting.....");

                    stm.Write(ba, 0, ba.Length);

                    byte[] bb = new byte[100];
                    int k = stm.Read(bb, 0, 100);

                    for (int i = 0; i < k; i++)
                        Console.Write(Convert.ToChar(bb[i]));

                    tcpclnt.Close();

                    Thread.Sleep(1000);
                    Console.WriteLine("");
                }

                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);
                }
            }
