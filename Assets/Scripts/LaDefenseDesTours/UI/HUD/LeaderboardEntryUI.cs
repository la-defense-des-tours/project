using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LaDefenseDesTours.UI.HUD
{
    public class LeaderboardEntryUI : MonoBehaviour
    {
        [SerializeField] private Text playerNameText;
        [SerializeField] private Text levelReachedText;
        [SerializeField] private Text timeSurvivedText;

        public void SetEntry(LeaderboardEntry entry)
        {
            playerNameText.text = entry.playerName;
            levelReachedText.text = entry.levelReached.ToString();
            timeSurvivedText.text = entry.timeSurvived;
        }
    }
}