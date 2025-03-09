using UnityEngine;

namespace Core.Utilities
{
    public class SkyBoxController : MonoBehaviour
    {
        private static readonly int Rotation = Shader.PropertyToID("_Rotation");
        [SerializeField] private float skyboxSpeed;
        void Update()
        {
            RenderSettings.skybox.SetFloat(Rotation, Time.time * skyboxSpeed);
        }
    }
}
