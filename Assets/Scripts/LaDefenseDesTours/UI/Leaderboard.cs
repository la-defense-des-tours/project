using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class Leaderboard
{
    private List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    public void AddEntry(string playerName, int levelReached, float timeSurvived)
    {
        var entry = new LeaderboardEntry(playerName, levelReached, timeSurvived);
        entries.Add(entry);

        entries = entries
            .OrderByDescending(e => e.levelReached)
            .ThenByDescending(e => e.timeSurvived)
            .ToList();

        SaveToJson();
    }

    public List<LeaderboardEntry> GetEntries()
    {
        return LoadExistingEntries(Path.Combine(Application.persistentDataPath, "leaderboard.json"));
    }

    private void SaveToJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");

        try
        {
            List<LeaderboardEntry> allEntries = LoadExistingEntries(filePath);
            allEntries.AddRange(entries);

            LeaderboardDataWrapper dataWrapper = new LeaderboardDataWrapper(allEntries);
            string json = JsonUtility.ToJson(dataWrapper, true);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to save leaderboard to file: " + ex.Message);
        }
    }

    private List<LeaderboardEntry> LoadExistingEntries(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<LeaderboardEntry>();
        }

        try
        {
            string json = File.ReadAllText(filePath);
            LeaderboardDataWrapper dataWrapper = JsonUtility.FromJson<LeaderboardDataWrapper>(json);
            return dataWrapper?.entries ?? new List<LeaderboardEntry>();
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to load existing leaderboard entries: " + ex.Message);
            return new List<LeaderboardEntry>();
        }
    }

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