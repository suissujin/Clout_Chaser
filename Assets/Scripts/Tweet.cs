using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using UnityEngine;

public class Tweet
{
    public static void Post()
    {
        // Process process = new Process();
        // // process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        // // process.StartInfo.CreateNoWindow = true;
        // process.StartInfo.UseShellExecute = false;
        // process.StartInfo.RedirectStandardInput = true;
        // process.StartInfo.RedirectStandardOutput = true;
        // process.StartInfo.FileName = "cmd.exe";
        // process.StartInfo.Arguments = "/c \"" + @"C:\Users\Yana\Clout Chaser\Python\python.exe C:\Users\Yana\Clout Chaser\Assets\Scripts\tweet.py" + "\"";
        // // process.EnableRaisingEvents = true;
        // process.Start();
        // process.WaitForExit();

        ProcessStartInfo startInfo = new()
        {
            FileName = "cmd.exe",
            Arguments = "/c \"" + @"C:\Users\Yana\Clout Chaser\Python\python.exe C:\Users\Yana\Clout Chaser\Assets\Scripts\tweet.py" + "\"",
            UseShellExecute = true,
            CreateNoWindow = true,
        };

        Process process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();
    }
}