using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.LaDefenseDesTours.Player
{
    public sealed class PlayerBase : MonoBehaviour
    {
        private static PlayerBase instance;

        public string name { get; private set; }
        public double health { get; private set; }
        public double score { get; private set; }
        public double currency { get; private set; }

        private PlayerBase()
        {
            name = "DefaultPlayer";
            health = 100.0;
            score = 0.0;
            currency = 0.0;
            Debug.Log("Player name is: " + name + " with health: " + health + " score: " + score + " currency: " + currency);
        }

        public double GetHealth()
        {
            return health;
        }

        public static PlayerBase GetInstance()
        {
            instance ??= new PlayerBase();
            return instance;
        }

    }
}
