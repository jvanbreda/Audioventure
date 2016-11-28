using Assets.Own_Scripts.ControlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Own_Scripts {
    class TouchController : AbstractController {
        private Touch initialTouch = new Touch();
        private float tapTime = 0;
        private bool isTapping = false;

        private float xAxisDifference;

        public TouchController() {
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

        private void CheckScreenInteraction() {
            if(Input.touchCount > 0) {
                TouchPhase phase = Input.GetTouch(0).phase;
                switch (phase) {
                    case TouchPhase.Moved:
                        xAxisDifference = Math.Max(Math.Abs(xAxisDifference), Math.Abs(Input.GetTouch(0).deltaPosition.x));
                        UpdateHeading();
                        break;
                    case TouchPhase.Ended:
                        isTapping = xAxisDifference < 0.35;
                        xAxisDifference = 0;
                        break;

                }
            }
        }

        public override void Move() {
            CheckScreenInteraction();
            Debug.Log(xAxisDifference);
            if (isTapping) {
                if (!GameObject.Find("Footsteps").GetComponent<AudioSource>().isPlaying) {
                    GameObject.Find("Footsteps").GetComponent<AudioSource>().Play();
                    GameController.headingController.transform.position += new Vector3(GameController.headingController.transform.up.x, 0, GameController.headingController.transform.up.z) * GameController.MOVING_SPEED;
                    isTapping = false;
                }
            }
        }

        public override void UpdateHeading() {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
                Vector2 touchDelta = Input.GetTouch(0).deltaPosition;
                GameController.headingController.transform.Rotate(0, 0, -touchDelta.x * 0.6f);
            }
        }

        public override void UpdateOrientation() {
            orientation = Input.gyro.attitude;
            GameController.camera.transform.localRotation = Quaternion.Lerp(GameController.camera.transform.localRotation, new Quaternion(orientation.x, orientation.y, -orientation.z, -orientation.w), Time.deltaTime);
            currentCameraAngle = 360 - (int)GameController.camera.transform.eulerAngles.y;
            UpdateHeading();
        }
    }
}

