using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Utilities
{
    [RequireComponent(typeof(AudioSource))]
    public class LevelSoundManager : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip normalMusic;
        public AudioClip bossMusic;
        public AudioClip victoryMusic;
        private Coroutine fadeCoroutine;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            PreloadSounds();
        }

        public void PreloadSounds()
        {
            audioSource.clip = bossMusic;
            audioSource.Play();
            audioSource.Stop();

            audioSource.clip = victoryMusic;
            audioSource.Play();
            audioSource.Stop();

            audioSource.clip = normalMusic;
            audioSource.Play();
            audioSource.Stop();
        }
        private IEnumerator FadeMusic(AudioClip newClip,bool isLoop)
        {
            if (audioSource == null || newClip == null) yield break;

            float startVolume = audioSource.volume;

            audioSource.clip = newClip;
            audioSource.Play();
            audioSource.loop = isLoop;
            audioSource.volume = startVolume;
        }

        public void PlayNormalMusic()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeMusic(normalMusic, true));
        }

        public void PlayBossMusic()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeMusic(bossMusic, false));
        }

        public IEnumerator PlayVictoryThenNormalMusic()
        {
            yield return StartCoroutine(FadeMusic(victoryMusic, false));
            yield return new WaitForSeconds(victoryMusic.length);
            yield return StartCoroutine(FadeMusic(normalMusic, true));
        }
    }
}

