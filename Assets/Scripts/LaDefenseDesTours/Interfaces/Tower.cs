using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class Tower : MonoBehaviour
    {
        public int level { get; set; }
        public float health { get; set; }
        public int price { get; set; }
        public abstract void Upgrade();
        public abstract void Attack();
    }
}