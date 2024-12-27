using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Assets.Scripts.LaDefenseDesTours
{
    /// <summary>
    /// Simple component to select lower quality post processing configurations on mobile
    /// </summary>
    [RequireComponent(typeof(PostProcessVolume))]
    public class PostProcessorConfigurationSelector : MonoBehaviour
    {
        public PostProcessProfile highQualityProfile; 
        public PostProcessProfile lowQualityProfile; 
        protected virtual void Awake()
        {
            var postProcessVolume = GetComponent<PostProcessVolume>();

            // Ensure the volume is global
            postProcessVolume.isGlobal = true;

            // Select the appropriate profile based on platform
            PostProcessProfile selectedProfile;

#if UNITY_STANDALONE
            selectedProfile = highQualityProfile;
#else
            selectedProfile = lowQualityProfile;
#endif

            postProcessVolume.profile = selectedProfile;
        }
    }
}
