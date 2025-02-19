using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Dead : State
    {
        private float destroyDelay = 2f;
        private float timeElapsed = 0f;
        private bool effectStarted = false;
        private MeshRenderer[] renderers;
        private SkinnedMeshRenderer[] skinnedRenderers;

        public override void OnStateEnter()
        {
            DisableEnemy();
            base.OnStateEnter();
            effectStarted = true;
        }
        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            if (!effectStarted)
                return;

            enemy.SetSpeed(0);
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= destroyDelay)
                enemy.Die();
        }
        private void DisableEnemy()
        {
            enemy.gameObject.tag = "Untagged";
            renderers = enemy.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in renderers)
                r.enabled = false;

            skinnedRenderers = enemy.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer r in skinnedRenderers)
                r.enabled = false;
        }
    }
}