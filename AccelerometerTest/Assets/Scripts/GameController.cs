using Assets.Own_Scripts.ControlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Own_Scripts {
    public class GameController : MonoBehaviour {

        //[SerializeField]
        public SoundObject[] soundObjects;
        //[SerializeField]
        public static Camera camera;

        public const float MOVING_SPEED = 10f;
        public const float CAMERA_SPEED = 10f;
        private const int RAY_LENGTH = 3;

        public Ray headingRay;
        public Ray orientationRay;
        public Ray rightRay;

        public Ray[] playerRays;

        public int counter;
        public static AbstractController controller;

        public static GameObject headingController;

        void Start() {
            DontDestroyOnLoad(GameObject.Find("GameController"));
            counter = 0;
            playerRays = new Ray[5];
        }

        void Update() {
            if (SceneManager.GetActiveScene().name == "AccelerometerTest")
            {
                ShootRays();
                GameObject.Find("TouchCanvas").GetComponent<Canvas>().enabled = (controller.GetType().Equals(typeof(TouchController)));
                controller.UpdateOrientation();
                controller.Move();
            }
            
        }

        void LateUpdate() {
            if (SceneManager.GetActiveScene().name == "AccelerometerTest")
            {
                UpdateCurrentAudioSource();
                UpdateAudioModel();
            }
        }

        private void ShootRays() {
            ShootHeadingRays();
            ShootSoundObjectRays();
        }

        private void ShootHeadingRays() {
            //orientationRay = new Ray(camera.transform.position, camera.transform.forward);
            //Physics.Raycast(orientationRay, RAY_LENGTH);
            //Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.blue);

            //rightRay = new Ray(camera.transform.position, camera.transform.right);
            //Physics.Raycast(rightRay, RAY_LENGTH);
            //Debug.DrawRay(camera.transform.position, camera.transform.right, Color.blue);

            headingRay = new Ray(headingController.transform.position, headingController.transform.up);
            Physics.Raycast(headingRay, RAY_LENGTH);
            Debug.DrawRay(headingController.transform.position, headingController.transform.up, Color.blue);
        }

        private void ShootSoundObjectRays() {
            for (int i = 0; i < soundObjects.Length; i++) {
                playerRays[i] = new Ray(camera.transform.position, soundObjects[i].transform.position - camera.transform.position);
                Physics.Raycast(playerRays[i], Vector3.Distance(soundObjects[i].transform.position, camera.transform.position));
                Debug.DrawRay(camera.transform.position, soundObjects[i].transform.position - camera.transform.position, Color.red);
            } 
        }

        private void UpdateCurrentAudioSource() {
            if (!soundObjects[counter].audioSource.enabled)
                soundObjects[counter].audioSource.enabled = true;
                
        }

        private void UpdateAudioModel() {
            for (int i = 0; i < soundObjects.Length; i++) {
                AudioModel model = soundObjects[i].audioModel;
                model.angleDifference2D = Vector2.Angle(new Vector2(orientationRay.direction.x, orientationRay.direction.z), new Vector2(playerRays[i].direction.x, playerRays[i].direction.z));
                model.angleDifference3D = Vector3.Angle(orientationRay.direction, playerRays[i].direction);
                model.isAudioLocatedLeft = Vector3.Angle(rightRay.direction, playerRays[i].direction) > 90;
                model.distance = Vector3.Distance(camera.transform.position, soundObjects[i].transform.position);
                soundObjects[i].audioModel = model;
            }
            
        }
    }
}
