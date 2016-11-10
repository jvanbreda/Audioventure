using UnityEngine;
using System.Collections;
using Assets.Scripts;

namespace Assets.Scripts {
    public class VRCameraController : MonoBehaviour {
        public Camera camera;
        private Ray ray;
        private RaycastHit hit;

        [SerializeField]
        public SoundObject[] soundObjects;
        timeBasedVolumeController tbc;
        public float currentCameraAngle;

        void Start() {
            GetComponent<TimeChecker>().Init(this);
            foreach (SoundObject source in soundObjects) {
                source.Init();
            }
        }

        // Update is called once per frame
        void Update() {
            currentCameraAngle = camera.transform.eulerAngles.y;
            if (currentCameraAngle > 180)
                currentCameraAngle -= 360;

            foreach (SoundObject source in soundObjects) {
                source.UpdateAudioSource(currentCameraAngle);
            }
        }
    }
}