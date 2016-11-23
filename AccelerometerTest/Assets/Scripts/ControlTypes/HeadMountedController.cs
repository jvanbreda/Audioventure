using Assets.Own_Scripts.ControlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Own_Scripts {
    class HeadMountedController : AbstractController {

        private Quaternion heading;
        private int currentCameraAngle;

        public HeadMountedController() {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }

        public override void Move() {
            if (Input.GetMouseButtonDown(0)) {
                if (!GameObject.Find("Footsteps").GetComponent<AudioSource>().isPlaying) {
                    GameObject.Find("Footsteps").GetComponent<AudioSource>().Play();
                    gameController.camera.transform.position += new Vector3(gameController.camera.transform.forward.x, 0, gameController.camera.transform.forward.z) * GameController.MOVING_SPEED;
                }
            }
        }

        public override void UpdateHeading() {
            heading = Input.gyro.attitude;
            gameController.camera.transform.localRotation = Quaternion.Lerp(gameController.camera.transform.localRotation, new Quaternion(heading.x * GameController.CAMERA_SPEED, heading.y * GameController.CAMERA_SPEED, -heading.z * GameController.CAMERA_SPEED, -heading.w * GameController.CAMERA_SPEED), Time.deltaTime);
            currentCameraAngle = 360 - (int)gameController.camera.transform.eulerAngles.y;
        }
    }
}
