using Assets.Own_Scripts.ControlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Own_Scripts {
    class HeadMountedController : AbstractController {

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

        public override void UpdateHeading(string direction) {
            heading = new Vector3(orientation.x, orientation.y, orientation.z);
        }

        public override void UpdateOrientation() {
            orientation = Input.gyro.attitude;
            GameController.camera.transform.localRotation = Quaternion.Lerp(GameController.camera.transform.localRotation, new Quaternion(orientation.x * GameController.CAMERA_SPEED, orientation.y * GameController.CAMERA_SPEED, -orientation.z * GameController.CAMERA_SPEED, -orientation.w * GameController.CAMERA_SPEED), Time.deltaTime);
            currentCameraAngle = 360 - (int)GameController.camera.transform.eulerAngles.y;
        }
    }
}
