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
        public Camera camera;

        public const float MOVING_SPEED = 10f;
        public const float CAMERA_SPEED = 10f;

        public Ray headingRay;
        public Ray rightRay;

        public Ray[] playerRays;

        public int counter;

        public static ControlMethod controlMethod;

        void Start() {
            DontDestroyOnLoad(GameObject.Find("GameController"));
            
            counter = 0;
            playerRays = new Ray[5];

            // Gyroscope must be manually enabled to be used and the location functionality must
            // be used
            Input.gyro.enabled = true;
            Input.location.Start();

            // Some 'standard' configuration: the back button can now be used to close the app,
            // and the smartphone will not go to sleep during the usage of the app.
            Input.backButtonLeavesApp = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        }

        void Update() {
            if (SceneManager.GetActiveScene().name == "AccelerometerTest")
            {
                camera = GameObject.Find("camParent").GetComponentInChildren<Camera>();
                ShootRays();

                GameObject.Find("SwipeCanvas").GetComponent<Canvas>().enabled = (controlMethod == ControlMethod.Swipe);
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
            headingRay = new Ray(camera.transform.position, camera.transform.forward);
            Physics.Raycast(headingRay, 3);
            Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.blue);

            rightRay = new Ray(camera.transform.position, camera.transform.right);
            Physics.Raycast(rightRay, 3);
            Debug.DrawRay(camera.transform.position, camera.transform.right, Color.blue);

        }

        private void ShootSoundObjectRays() {
            for (int i = 0; i < soundObjects.Length; i++) {
                playerRays[i] = new Ray(camera.transform.position, soundObjects[i].transform.position - camera.transform.position);
                Physics.Raycast(playerRays[i], Vector3.Distance(soundObjects[i].transform.position, camera.transform.position));
                Debug.DrawRay(camera.transform.position, soundObjects[i].transform.position - camera.transform.position, Color.red);
            } 
        }

        private void UpdateCurrentAudioSource() {
            if(!soundObjects[counter].audioSource.enabled)
                soundObjects[counter].audioSource.enabled = true;
        }

        private void UpdateAudioModel() {
            for (int i = 0; i < soundObjects.Length; i++) {
                AudioModel model = soundObjects[i].audioModel;
                model.angleDifference2D = Vector2.Angle(new Vector2(headingRay.direction.x, headingRay.direction.z), new Vector2(playerRays[i].direction.x, playerRays[i].direction.z));
                model.angleDifference3D = Vector3.Angle(headingRay.direction, playerRays[i].direction);
                model.isAudioLocatedLeft = Vector3.Angle(rightRay.direction, playerRays[i].direction) > 90;
                model.distance = Vector3.Distance(camera.transform.position, soundObjects[i].transform.position);
                soundObjects[i].audioModel = model;
            }
            
        }
    }
}
