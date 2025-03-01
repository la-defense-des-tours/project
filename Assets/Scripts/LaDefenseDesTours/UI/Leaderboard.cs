using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public class Leaderboard
{
    private List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    public void AddEntry(string playerName, int levelReached, float timeSurvived)
    {
        var entry = new LeaderboardEntry(playerName, levelReached, timeSurvived);
        entries.Add(entry);

        // Sort the leaderboard by level (descending) and time (descending)
        entries = entries
            .OrderByDescending(e => e.levelReached)
            .ThenByDescending(e => e.timeSurvived)
            .ToList();
    }

    public List<LeaderboardEntry> GetEntries()
    {
        return entries;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(new LeaderboardDataWrapper(entries));
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            string json = PlayerPrefs.GetString("Leaderboard");
            var wrapper = JsonUtility.FromJson<LeaderboardDataWrapper>(json);
            entries = wrapper.entries;
        }
    }

    // Wrapper class for serialization
    [System.Serializable]
    private class LeaderboardDataWrapper
    {
        public List<LeaderboardEntry> entries;

        public LeaderboardDataWrapper(List<LeaderboardEntry> entries)
        {
            this.entries = entries;
        }
    }
}