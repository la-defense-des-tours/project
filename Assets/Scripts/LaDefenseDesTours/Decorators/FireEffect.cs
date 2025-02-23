using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class FireEffect : TowerDecorator
    {
        public FireEffect(Tower tower) : base(tower) { }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Fire effect is attacking");

            // Appliquer l'état Burned à l'ennemi
            if (m_shooter != null && m_shooter.target != null)
            {
                Enemy enemy = m_shooter.target.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TransitionTo(new Burned());
                }
            }
        }
    }
}