using Assets.Scripts.Core.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LaDefenseDesTours.UI.HUD
{
    public class LeaderboardMenuPage : SimpleMainMenuPage
    {
        [SerializeField] private Transform leaderboardContainer; 
        [SerializeField] private GameObject entryPrefab;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Text rankText;
        private void Start()
        {
            LoadLeaderboard();
        }

        private void LoadLeaderboard()
        {
            Leaderboard leaderboard = new Leaderboard();
            List<LeaderboardEntry> entries = leaderboard.GetEntries();

            int startIndex = 1; 
            int existingCount = leaderboardContainer.childCount;

            for (int i = 0; i < entries.Count; i++)
            {
                int rank = i + startIndex;

                if (i + 1 < existingCount)
                {
                    Transform entryTransform = leaderboardContainer.GetChild(i + 1);
                    LeaderboardEntryUI entryUI = entryTransform.GetComponent<LeaderboardEntryUI>();
                    entryUI.SetEntry(entries[i], rank);
                }
                else
                {
                    GameObject entryObject = Instantiate(entryPrefab, leaderboardContainer);
                    entryObject.GetComponent<LeaderboardEntryUI>().SetEntry(entries[i], rank);
                }
            }

            while (leaderboardContainer.childCount > entries.Count + 1) 
            {
                DestroyImmediate(leaderboardContainer.GetChild(leaderboardContainer.childCount - 1).gameObject);
            }
        }

    }
}