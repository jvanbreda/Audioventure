using Assets.Own_Scripts.ControlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Own_Scripts {
    class HeadMountedController : AbstractController {

        private int previousCameraAngle;

        public HeadMountedController() {
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
            if (Input.GetMouseButtonDown(0)) {
                if (!GameObject.Find("Footsteps").GetComponent<AudioSource>().isPlaying) {
                    GameObject.Find("Footsteps").GetComponent<AudioSource>().Play();
                    GameController.headingController.transform.position += new Vector3(GameController.headingController.transform.forward.x, 0, GameController.headingController.transform.forward.z) * GameController.MOVING_SPEED;
                }
            }
        }

        public override void UpdateOrientation() {
            orientation = Input.gyro.rotationRateUnbiased;
            GameController.headingController.transform.Rotate(-orientation.x, -orientation.y, orientation.z);
            currentCameraAngle = (int)GameController.camera.transform.eulerAngles.y;
        }
    }
}
