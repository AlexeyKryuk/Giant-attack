using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeAnalytics : MonoBehaviour
{
    private const string SAVED_REG_DAY = "RegDay";
    private const string SAVED_REG_DAY_FULL = "RegDayFull";
    private const string SAVED_SESSION_ID = "SessionID";

    [SerializeField] private Level _level;

    private Amplitude _amplitude;
    private int _startTime;

    private string _regDay
    {
        get { return PlayerPrefs.GetString(SAVED_REG_DAY, DateTime.Today.ToString("dd/MM/yy")); }
        set { PlayerPrefs.SetString(SAVED_REG_DAY, value); }
    }

    private string _regDayFull
    {
        get { return PlayerPrefs.GetString(SAVED_REG_DAY_FULL, DateTime.Today.ToString()); }
        set { PlayerPrefs.SetString(SAVED_REG_DAY_FULL, value); }
    }

    private int _sessionID
    {
        get { return PlayerPrefs.GetInt(SAVED_SESSION_ID, 0); }
        set { PlayerPrefs.SetInt(SAVED_SESSION_ID, value); }
    }

    private void Awake()
    {
        _amplitude = Amplitude.Instance;
        _amplitude.logging = true;
        _amplitude.init("5871f7715b2a74d2ad4d9a973a7cfa79");

        if (StartScene.IsStart)
            GameStart();
    }

    private void OnEnable()
    {
        _level.LevelStarted += OnLevelStarted;
        _level.LevelWon += OnLevelWon;
        _level.LevelLost += OnlevelLost;
    }

    private void OnDisable()
    {
        _level.LevelStarted -= OnLevelStarted;
        _level.LevelWon -= OnLevelWon;
        _level.LevelLost -= OnlevelLost;
    }

    public void GameStart()
    {
        StartScene.IsStart = false;

        if (_level.CurrentLevel == 1)
        {
            _regDay = DateTime.Today.ToString("dd/MM/yy");
            _regDayFull = DateTime.Today.ToString();
            _amplitude.setOnceUserProperty("reg_day", _regDay);
        }

        SetBasicProperty();
        IDictionary<string, object> property = new Dictionary<string, object>();
        property.Add("Session_ID", _sessionID);
        FireEvent("Game_start", property);
    }

    private void OnlevelLost()
    {
        int timeSpent = (int)Time.time - _startTime;

        IDictionary<string, object> properties = new Dictionary<string, object>();
        properties.Add("Level", _level.CurrentLevel);
        properties.Add("Time_spent", timeSpent);

        FireEvent("Level_fail", properties);
    }

    private void OnLevelWon()
    {
        int timeSpent = (int)Time.time - _startTime;

        IDictionary<string, object> properties = new Dictionary<string, object>();
        properties.Add("Level", _level.CurrentLevel);
        properties.Add("Time_spent", timeSpent);

        FireEvent("Level_complete", properties);
    }

    private void OnLevelStarted()
    {
        _startTime = (int)Time.time;

        IDictionary<string, object> property = new Dictionary<string, object>();
        property.Add("Level", _level.CurrentLevel);

        FireEvent("Level_start", property);
    }

    private void SetBasicProperty()
    {
        _sessionID = _sessionID + 1;
        _amplitude.setUserProperty("session_id", _sessionID);

        int daysAfter = DateTime.Today.Subtract(DateTime.Parse(_regDayFull)).Days;
        _amplitude.setUserProperty("days_after", daysAfter);
    }

    private void FireEvent(string eventName, IDictionary<string, object> properties)
    {
        SettingUserProperties();
        _amplitude.logEvent(eventName, properties);
    }

    private void SettingUserProperties()
    {
        int lastLevel = _level.CurrentLevel;
        _amplitude.setUserProperty("level_last", lastLevel);
    }
}
