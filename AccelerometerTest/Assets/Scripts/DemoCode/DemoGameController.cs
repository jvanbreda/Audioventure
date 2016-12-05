using Assets.Own_Scripts.ControlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Own_Scripts {
    public class DemoGameController : MonoBehaviour {

        public DemoSoundObject soundObject;
        [SerializeField]
        public Camera camera;
        [SerializeField]

        public const float MOVING_SPEED = 10f;
        public const float CAMERA_SPEED = 10f;
        private const int RAY_LENGTH = 3;

        public Ray headingRay;
        public Ray orientationRay;
        public Ray rightRay;

        public Ray playerRay;

        public int counter;
        public static AbstractController controller;

        public GameObject headingController;

        void Start() {
            DontDestroyOnLoad(GameObject.Find("GameController"));
            counter = 0;
            playerRay = new Ray();
        }

        void Update() {
                ShootRays();
                //controller.UpdateOrientation();
                //controller.Move();

        }

        void LateUpdate() {
                UpdateAudioModel();
        }

        private void ShootRays() {
            ShootHeadingRays();
            ShootSoundObjectRays();
        }

        private void ShootHeadingRays() {
            orientationRay = new Ray(camera.transform.position, camera.transform.forward);
            Physics.Raycast(orientationRay, RAY_LENGTH);
            Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.blue);

            rightRay = new Ray(camera.transform.position, camera.transform.right);
            Physics.Raycast(rightRay, RAY_LENGTH);
            Debug.DrawRay(camera.transform.position, camera.transform.right, Color.blue);

            headingRay = new Ray(headingController.transform.position, headingController.transform.forward);
            Physics.Raycast(headingRay, RAY_LENGTH);
            Debug.DrawRay(headingController.transform.position, headingController.transform.forward, Color.blue);
        }

        private void ShootSoundObjectRays() {
                playerRay = new Ray(camera.transform.position, soundObject.transform.position - camera.transform.position);
                Physics.Raycast(playerRay, Vector3.Distance(soundObject.transform.position, camera.transform.position));
                Debug.DrawRay(camera.transform.position, soundObject.transform.position - camera.transform.position, Color.red);
        }

        private void UpdateAudioModel() {
                AudioModel model = soundObject.audioModel;
                model.angleDifference2D = Vector2.Angle(new Vector2(orientationRay.direction.x, orientationRay.direction.z), new Vector2(playerRay.direction.x, playerRay.direction.z));
                model.angleDifference3D = Vector3.Angle(orientationRay.direction, playerRay.direction);
                model.isAudioLocatedLeft = Vector3.Angle(rightRay.direction, playerRay.direction) > 90;
                model.distance = Vector3.Distance(camera.transform.position, soundObject.transform.position);
                soundObject.audioModel = model;

        }
    }
}
