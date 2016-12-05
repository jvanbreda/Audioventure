using Assets.Own_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets {
    public class DemoSoundObject : MonoBehaviour {

        [SerializeField]
        private int index;

        public AudioModel audioModel = new AudioModel();

        private const float volumeBoost = 1.5f;
        private const float panBoost = 1f;
        private const float reverbBoost = 1.5f;

        private const float MAX_DISTANCE = 500f;

        public AudioSource audioSource;

        private DemoGameController demoGameController;


        void Start() {
            audioSource = GetComponentInChildren<AudioSource>();
            demoGameController = GameObject.Find("GameController").GetComponent<DemoGameController>();
            demoGameController.soundObject = this;
        }

        void Update() {
            UpdateAudioSource();
        }

        void LateUpdate() {
            //CheckCollision();
        }

        public void UpdateAudioSource() {
            if (!audioSource.isPlaying && audioSource.enabled && !GameObject.Find("EndSound").GetComponent<AudioSource>().isPlaying && !GameObject.Find("PickupSound").GetComponent<AudioSource>().isPlaying)
                audioSource.Play();
            UpdateVolume();
            UpdatePan();
            UpdateReverb();
        }

        protected void UpdateVolume() {
            //float correctedDistance = (float) Math.Min(Math.Log(audioModel.distance - 4) / (Math.Log(MAX_DISTANCE - 4)), 1f);
            float correctedDistance = (float)Math.Min(Math.Sin((Math.PI * audioModel.distance) / (MAX_DISTANCE * 2)), 0.6f);
            float correctedVolume = Math.Min(audioModel.angleDifference3D / 360f, 0.5f);
            float newVolume = (1 - correctedVolume - correctedDistance);
            // Makes sure the volume is always a value between 0 and 1
            newVolume = Mathf.Clamp(newVolume, 0, 1);
            SetVolume(newVolume);
        }

        protected void UpdatePan() {
            float angleDifference = audioModel.angleDifference2D;
            if (audioModel.isAudioLocatedLeft)
                angleDifference *= -1;

            // Math.Sin keeps the pan within the -1 to 1 range
            float newPan = (float)Math.Sin(angleDifference * Math.PI / 180f) * panBoost;
            newPan = Mathf.Clamp(newPan, -1, 1);
            SetPan(newPan);

            if (audioSource.isPlaying)
                Debug.Log("Angle: " + angleDifference + " Pan: " + newPan);
        }

        private void UpdateReverb() {
            if (GameObject.Find("Toggle").GetComponent<Toggle>().isOn) {
                float angleDifference = audioModel.angleDifference2D;
                float newReverbZoneMix = Math.Min(1.05f, reverbBoost * (angleDifference / 180f));
                SetReverbZoneMix(newReverbZoneMix);
            }
            else {
                SetReverbZoneMix(0);
            }
        }

        private void CheckCollision() {
            if (audioModel.distance < 5 && demoGameController.counter == index) {
                GameObject.Find("PickupSound").GetComponent<AudioSource>().Play();
                audioSource.enabled = false;
                demoGameController.counter++;
            }

            if (demoGameController.counter > 4) {
                GameObject.Find("EndSound").GetComponent<AudioSource>().Play();
                demoGameController.counter = 0;
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

        private void SetReverbZoneMix(float reverbZoneMix) {
            audioSource.reverbZoneMix = reverbZoneMix;
        }
    }
}
