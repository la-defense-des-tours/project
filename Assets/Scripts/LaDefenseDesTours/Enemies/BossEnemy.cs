using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class BossEnemy : Enemy
    {

        public  override float health { get; set; } = 100f;
        public override float speed { get; set; } = 1;
        public override float acceleration { get; set; } = 3;

        // TODO: implement attack behavior of the boss enemy
       
    }
}