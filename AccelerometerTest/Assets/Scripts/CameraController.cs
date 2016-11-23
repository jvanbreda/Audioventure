using UnityEngine;
using System.Collections;
using System;
using Assets;
using Assets.Own_Scripts;
using Assets.Own_Scripts.ControlTypes;

public class CameraController : MonoBehaviour {

    private GameObject camParent;
    private Quaternion heading;

    

    // Use this for initialization
    void Start() {
        // Used for 'calibratrion': this makes sure that the camera behaves 
        // exactly the same as the phone the user is holding
        camParent = new GameObject("camParent");
        camParent.transform.Rotate(Vector3.right, 90);
        transform.parent = camParent.transform;

        switch (GameController.controlMethod) {
            case ControlMethod.Swipe:
                GameController.controller = new SwipeController();
                break;
            case ControlMethod.HeadMounted:
                GameController.controller = new HeadMountedController();
                break;
        }
    }
}
