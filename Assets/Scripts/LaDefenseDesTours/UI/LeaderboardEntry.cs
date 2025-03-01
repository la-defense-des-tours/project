using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System;
using Assets.Scripts.LaDefenseDesTours;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int levelReached;
    public float timeSurvived;

    public LeaderboardEntry(string name, int level, float time)
    {
        playerName = name;
        levelReached = level;
        timeSurvived = time;
    }
}