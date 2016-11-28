using Assets.Own_Scripts.ControlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Own_Scripts {
    class TouchController : AbstractController {
        private Touch initialTouch = new Touch();

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

        public override void Move() {
            if (!GameObject.Find("Footsteps").GetComponent<AudioSource>().isPlaying) {
                GameObject.Find("Footsteps").GetComponent<AudioSource>().Play();
                GameController.headingController.transform.position += new Vector3(GameController.headingController.transform.up.x, 0, GameController.headingController.transform.up.z) * GameController.MOVING_SPEED;
            }
        }

        public override void UpdateHeading(string direction) {
            foreach (Touch t in Input.touches) {
                if (t.phase == TouchPhase.Began) {
                    initialTouch = t;
                } else if (t.phase == TouchPhase.Moved) {
                    float deltaX = initialTouch.position.x - t.position.x;
                    float deltaY = initialTouch.position.y - t.position.y;
                    bool swipeSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

                    if (swipeSideways && deltaX > 0) {              // Swiped left 
                        Debug.Log("You swiped left!");
                    } else if (swipeSideways && deltaX <= 0) {      // Swiped right
                        Debug.Log("You swiped right!");
                    }
                } else if (t.phase == TouchPhase.Ended) {
                    initialTouch = new Touch();
                }
            }
        }

        public override void UpdateOrientation() {
            orientation = Input.gyro.attitude;
            GameController.camera.transform.localRotation = Quaternion.Lerp(GameController.camera.transform.localRotation, new Quaternion(orientation.x * GameController.CAMERA_SPEED, orientation.y * GameController.CAMERA_SPEED, -orientation.z * GameController.CAMERA_SPEED, -orientation.w * GameController.CAMERA_SPEED), Time.deltaTime);
            currentCameraAngle = 360 - (int)GameController.camera.transform.eulerAngles.y;
            UpdateHeading(null);
        }
    }
}

