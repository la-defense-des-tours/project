using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class IceEffect : TowerDecorator
    {
        public IceEffect(Tower tower) : base(tower) { }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Ice effect is attacking");

            // Appliquer l'état Slow à l'ennemi
            if (m_shooter != null && m_shooter.target != null)
            {
                Enemy enemy = m_shooter.target.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TransitionTo(new Slowed());
                }
            }
        }
    }
}