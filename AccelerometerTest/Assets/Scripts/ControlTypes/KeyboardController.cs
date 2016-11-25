using System;
using Assets.Own_Scripts.ControlTypes;
using UnityEngine;

namespace Assets.Own_Scripts
{
    class KeyboardController : AbstractController
    {

        public KeyboardController()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }

        public override void Move()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!GameObject.Find("Footsteps").GetComponent<AudioSource>().isPlaying)
                {
                    GameObject.Find("Footsteps").GetComponent<AudioSource>().Play();
                    Debug.Log(GameController.headingController.transform.forward);
                    GameController.headingController.transform.position += new Vector3(GameController.headingController.transform.up.x, 0, GameController.headingController.transform.up.z) * GameController.MOVING_SPEED;
                }
            }
        }

        public override void UpdateHeading(string direction)
        {
            throw new NotImplementedException();
        }

        public override void UpdateOrientation()
        {
            throw new NotImplementedException();
        }
    }
}