using UnityEngine;
using System.Collections;

namespace Assets.Scripts {
    public static class AudioFadeOut {

        const float MIN_VOL_AUDIO = 0.3f;

        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
            float startVolume = audioSource.volume;

            while (audioSource.volume > MIN_VOL_AUDIO) {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }
            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }
}
