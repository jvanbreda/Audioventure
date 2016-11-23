using UnityEngine;
using System.Collections;
using System;
using Assets;
using Assets.Own_Scripts;
using Assets.Own_Scripts.ControlTypes;

public class CameraController : MonoBehaviour {

    private GameObject camParent;
    private Quaternion heading;

    private ControlMethod controlMethod;
    private AbstractController controller;

    // Use this for initialization
    void Start() {
        // Used for 'calibratrion': this makes sure that the camera behaves 
        // exactly the same as the phone the user is holding
        camParent = new GameObject("camParent");
        camParent.transform.Rotate(Vector3.right, 90);
        transform.parent = camParent.transform;

        controlMethod = ControlMethod.Swipe;

        switch (controlMethod) {
            case ControlMethod.Swipe:
                controller = new SwipeController();
                break;
            case ControlMethod.HeadMounted:
                controller = new HeadMountedController();
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        controller.Move();
        controller.UpdateHeading();
    }
}
