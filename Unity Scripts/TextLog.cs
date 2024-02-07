using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLog : MonoBehaviour
{
    public Text logText;
    public Text latestLogText; // New reference to another text component
    public GameObject logPanel;

    private const int maxLines = 25;

    void OnEnable()
    {
        Application.logMessageReceived += LogCallBack;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallBack;
    }

    void LogCallBack(string logString, string stackTrace, LogType type)
    {
        // Add log text with timestamp
        string logEntry = $"{System.DateTime.Now:HH:mm:ss} - {logString}\r\n";
        logText.text += logEntry;

        // Update the latest log text with the most recent entry
        if (latestLogText != null)
        {
            latestLogText.text = logEntry;
        }

        // Check line count and clear old logs if necessary
        string[] lines = logText.text.Split('\n');
        if (lines.Length > maxLines)
        {
            int removeCount = lines.Length - maxLines;
            logText.text = string.Join("\n", lines, removeCount, maxLines);
        }
    }

    public void ToggleLogPanel()
    {
        if (logPanel != null)
        {
            logPanel.SetActive(!logPanel.activeSelf);
        }
    }

    void Update()
    {
        if (!logText)
        {
            return;
        }
    }
}
