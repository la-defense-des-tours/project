using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public interface Wave
    {
        Wave SetNext(Wave nextWave);
        public void GenerateWave(Vector3 targetPosition);
    }
}