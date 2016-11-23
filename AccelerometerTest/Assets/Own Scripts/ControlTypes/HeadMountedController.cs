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

        }
    }
}
