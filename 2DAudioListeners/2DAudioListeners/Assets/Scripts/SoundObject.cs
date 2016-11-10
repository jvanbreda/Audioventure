using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class SoundObject : MonoBehaviour {
        [SerializeField]
        public AudioSource audioSource;
        [SerializeField]
        public int angle;
        const float volumeBoost = 2;

        public virtual void Init() { }

        public void UpdateAudioSource(float currentCameraAngle) {
            UpdateVolume(currentCameraAngle);
            UpdatePan(currentCameraAngle);
        }

        protected void UpdatePan(float currentCameraAngle) {
            float angleDifference = -1 * (currentCameraAngle - angle);
            float pan = angleDifference / 180.1f;
            // TODO: pan cannot be > 1 || < -1
            //Debug.Log("Pan: " + pan);
            SetPan(pan);
        }

        protected void UpdateVolume(float currentCameraAngle) {
            if (!TimeChecker.audioMuted) {
                float angleDifference = Math.Abs(currentCameraAngle - angle);
                //Use angle to change volume
                float newVolume = 1 - volumeBoost * (angleDifference / 180f);
                newVolume = Math.Max(0, newVolume);
                //Debug.Log("Volume: " + newVolume);
                // set audio volume based on angle
                SetVolume(newVolume);
            }
        }

        // set audio 
        public void SetVolume(float volume) {
            audioSource.volume = volume;
        }

        // set panning 
        public void SetPan(float panStereo) {
            audioSource.panStereo = panStereo;
        }

        public float GetVolume() {
            return audioSource.volume; 
        }
    }
}
