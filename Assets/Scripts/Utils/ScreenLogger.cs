using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;

[RequireComponent(typeof(Text))]
public class ScreenLogger : Singleton<ScreenLogger>
{
    [Tooltip("Maximum amount of shown messages")]
    public int MaxMessages = 15;
    [Tooltip("Time of message display")]
    public float ShowTime = 10.0f;
    [Tooltip("Should timeouts use reduced time value?")]
    public bool UseReducedTime = false;
    [Tooltip("Reduced time of message display")]
    public float ShowTimeReduced = 1.0f;
    [Tooltip("Current time will be appended in the beginning of the message")]
    public bool AppendTime = true;

    [SerializeField]
    Color WarningColor = Color.yellow;
    string ColorWarningTag;
    [SerializeField]
    Color ErrorColor = Color.red;
    string ColorErrorTag;
    string ColorCloseTag = "</color>";

    Text Text;
    List<string> Messages = new List<string>();

    void Awake()
    {
        ColorWarningTag = "<color=" + WarningColor.ToHexString(true) + ">";
        ColorErrorTag = "<color=" + ErrorColor.ToHexString(true) + ">";
        
        Text = GetComponent<Text>();
        UpdateText();
    }

    public void AddMessage(string text, LogType level = LogType.Log)
    {
        switch(level)
        {
            case LogType.Assert:
            case LogType.Error:
            case LogType.Exception:
                text = ColorErrorTag + text + ColorCloseTag;
                break;
            case LogType.Warning:
                text = ColorWarningTag + text + ColorCloseTag;
                break;
        }

        if (AppendTime)
        {
            text = GetTime() + text;
        }
        Messages.Add(text);

        if (Messages.Count > MaxMessages)
        {
            RemoveOldestMessage();
        }
        else
        {
            Invoke("RemoveOldestMessage", UseReducedTime ? ShowTimeReduced : ShowTime);
        }

        UpdateText();
    }

    void RemoveOldestMessage()
    {
        Messages.RemoveAt(0);
        UpdateText();
    }

    string GetTime()
    {
        var time = System.DateTime.Now;
        return System.String.Format("{0:D2}:{1:D2}> ", time.Hour, time.Minute);
    }

    void UpdateText()
    {
        Text.text = string.Join("\n", Messages.ToArray());
    }
}
