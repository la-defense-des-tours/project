using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System;
using Assets.Scripts.LaDefenseDesTours;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int levelReached;
    public string timeSurvived; 

    public LeaderboardEntry(string name, int level, float time)
    {
        playerName = name;
        levelReached = level;
        timeSurvived = FormatTime(time);
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}