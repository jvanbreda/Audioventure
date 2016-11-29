using System;
using Assets.Own_Scripts.ControlTypes;
using UnityEngine;
using Assets.Scripts.ControlTypes;

namespace Assets.Own_Scripts {
    class KeyboardController : AbstractHeadingController {

        public KeyboardController() {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();

            // Gyroscope must be manually enabled to be used and the location functionality must
            // be used
            Input.gyro.enabled = true;
            Input.location.Start();

            // Some 'standard' configuration: the back button can now be used to close the app,
            // and the smartphone will not go to sleep during the usage of the app.
            Input.backButtonLeavesApp = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public override void Move() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (!GameObject.Find("Footsteps").GetComponent<AudioSource>().isPlaying) {
                    GameObject.Find("Footsteps").GetComponent<AudioSource>().Play();
                    Debug.Log(GameController.headingController.transform.forward);
                    GameController.headingController.transform.position += new Vector3(GameController.headingController.transform.forward.x, 0, GameController.headingController.transform.forward.z) * GameController.MOVING_SPEED;
                }
            }
        }

        public override void UpdateHeading() {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                GameController.headingController.transform.Rotate(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                GameController.headingController.transform.Rotate(0, 1, 0);
            }
        }

        public override void UpdateOrientation() {
            orientation = Input.gyro.rotationRateUnbiased;
            GameController.headingController.transform.Rotate(-orientation.x, -orientation.y, orientation.z);
            currentCameraAngle = (int)GameController.camera.transform.eulerAngles.y;
            UpdateHeading();
        }
    }
}