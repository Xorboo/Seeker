using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;


public class Log : Singleton<Log>
{
    // TODO Fix file logging for mobiles
    public static string FileName = "game.log";
    public static bool WriteToFile = false;

    void Awake()
    {
        if (ShowOnScreen)
            Application.logMessageReceived += HandleLog;
        Log.CreateInstance();
    }

    static bool WriteInLog
    {
        get { return true; }
    }

    static bool ShowOnScreen
    {
        get { return true; }
    }

    public static void Write(string s, bool printOnScreen = true)
    {
        if (WriteInLog)
            Debug.Log(s);

        if (printOnScreen && Application.isPlaying && ShowOnScreen)
            AddMessage(s);

        if (WriteToFile)
            File.AppendAllText(FileName, s + "\n");
    }

    public static void Warning(string s, bool printOnScreen = true)
    {
        if (WriteInLog)
            Debug.LogWarning(s);

        if (printOnScreen && Application.isPlaying && ShowOnScreen)
            AddMessage("W: " + s, LogType.Warning);

        if (WriteToFile)
            File.AppendAllText(FileName, "W: " + s + "\n");
    }

    public static void Error(string s, bool printOnScreen = true)
    {
        if (WriteInLog)
            Debug.LogError(s);

        if (printOnScreen && Application.isPlaying && ShowOnScreen)
            AddMessage("E: " + s, LogType.Error);

        if (WriteToFile)
            File.AppendAllText(FileName, "E: " + s + "\n");
    }

    private void HandleLog(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            AddMessage(String.Format("{0}: {1}\n{2}", type, condition, stackTrace), type);
        }
    }

    static void AddMessage(string text, LogType level = LogType.Log)
    {
        ScreenLogger.Instance.AddMessage(text, level);
    }
}
