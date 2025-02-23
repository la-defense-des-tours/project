using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class LightningEffect : TowerDecorator
    {
        public LightningEffect(Tower tower) : base(tower) { }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Lightning effect is attacking");

            // Appliquer l'état Paralyzed à l'ennemi
            if (m_shooter != null && m_shooter.target != null)
            {
                Enemy enemy = m_shooter.target.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TransitionTo(new Paralyzed());
                }
            }
        }
    }
}