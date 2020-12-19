﻿using System;
using System.IO;

namespace verify_cn
{
    class Program
    {
        static void LogWrite(string UidFile, string Depth, string UID, string VpnAction)
        {
            string PathLogFolder = @"..\log";
            if (!Directory.Exists(PathLogFolder))
            {
                Directory.CreateDirectory(PathLogFolder);
            }
            string PathLogFile = @"..\log\CN_debug.txt";
            DateTime now = DateTime.Now;
            string LogText = now.ToString() + ": " + $"FileName = {UidFile}, Depth = {Depth}, UID = {UID}, Action = {VpnAction}" + "\n";
            System.IO.File.AppendAllText(PathLogFile, LogText);
        }
        static int Main(string[] args)

        {
            string UidFile = args[0];   // FileName
            string Depth = args[1];     // Depth
            string UidInput = args[2];  // CN
            UidInput = UidInput.Substring(3); // cut 3 first symbols, because ovpn sending CN=CLIENTNAME

            if (Depth == "1") // if depth eq 0 need to pass it, because this step is checking CA
            {
                return 0;
            }

            if (Depth == "0")
            {
                                                
                var text = File.ReadAllLines(UidFile);
                foreach (var uids in text)
                {
                    if (uids == UidInput)
                    {
                        // Logging
                        string Allow = "Allow";
                        LogWrite(UidFile, Depth, UidInput, Allow);
                        return 0;
                    }
                }
            }
            // Logging
            string Deny = "Deny";
            LogWrite(UidFile, Depth, UidInput, Deny);
            return 1;
        }
    }



}