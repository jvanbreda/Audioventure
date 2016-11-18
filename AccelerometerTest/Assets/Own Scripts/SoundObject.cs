using Assets.Own_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets {
    public class SoundObject : MonoBehaviour {

        public Ray playerRay { get; private set; }
        public AudioModel audioModel = new AudioModel();

        private const float volumeBoost = 1.5f;
        private const float panBoost = 1.5f;
        private const float reverbBoost = 1.5f;

        private const float MAX_DISTANCE = 100f;

        private AudioSource audioSource;
        [SerializeField]
        private Camera camera;
        [SerializeField]
        private int index;
        [SerializeField]
        private CameraController cc;


        void Start() {
            audioSource = GetComponentInChildren<AudioSource>();

        }

        void Update() {
            SendRayFromPlayer();
            UpdateAudioSource();
        }

        void LateUpdate() {
            CheckCollision();
        }

        private void SendRayFromPlayer() {
            playerRay = new Ray(camera.transform.position, transform.position - camera.transform.position);
            Physics.Raycast(playerRay, Vector3.Distance(transform.position, camera.transform.position));
            Debug.DrawRay(camera.transform.position, transform.position - camera.transform.position, Color.red);
        }

        public void UpdateAudioSource() {
            if (cc.counter == index) {
                audioSource.enabled = true;
                if (!audioSource.isPlaying && !GameObject.Find("EndSound").GetComponent<AudioSource>().isPlaying)
                    audioSource.Play(); 
                UpdateVolume();
                UpdatePan();
                UpdateReverb();

            }
        }

        protected void UpdateVolume() {
            //float correctedDistance = (float) Math.Min(Math.Log(audioModel.distance - 4) / (Math.Log(MAX_DISTANCE - 4)), 1f);
            float correctedDistance = (float) Math.Min(Math.Sin((Math.PI * audioModel.distance) / (MAX_DISTANCE * 2)), 0.7f);
            Debug.Log(correctedDistance);
            float correctedVolume = Math.Min(audioModel.angleDifference3D / 360f, 0.5f);
            float newVolume = (1 - correctedVolume - correctedDistance);
            // Makes sure the volume is always a value between 0 and 1
            newVolume = Math.Max(0, newVolume);
            newVolume = Math.Min(1, newVolume);
            SetVolume(newVolume);
        }

        protected void UpdatePan() {
            float angleDifference = audioModel.angleDifference2D;
            if (audioModel.isAudioLocatedLeft)
                angleDifference *= -1;

            // Math.Sin keeps the pan within the -1 to 1 range
            float newPan = (float) Math.Sin(angleDifference * Math.PI / 180f) * panBoost;
            newPan = Math.Max(-1, newPan);
            newPan = Math.Min(1, newPan);
            SetPan(newPan);
        }

        private void UpdateReverb() {
            float angleDifference = audioModel.angleDifference2D;
            float newReverbZoneMix = Math.Min(1.05f, reverbBoost * (angleDifference / 180f));
            SetReverbZoneMix(newReverbZoneMix);
        }

        private void CheckCollision() {
            if (audioModel.distance < 5 && cc.counter == index) {
                GameObject.Find("CoinSound").GetComponent<AudioSource>().Play();
                audioSource.enabled = false;
                cc.counter++;
            }

            if (cc.counter > 4) {
                GameObject.Find("EndSound").GetComponent<AudioSource>().Play();
                cc.counter = 0;
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
