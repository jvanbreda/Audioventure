using UnityEngine;
using System.Collections;
using System;
using Assets;
using Assets.Own_Scripts;
using Assets.Own_Scripts.ControlTypes;

public class CameraController : MonoBehaviour {

    private GameObject camParent;    

    // Use this for initialization
    void Start() {
        GameController.camera = GameObject.Find("HeadingController").GetComponentInChildren<Camera>();
        GameController.headingController = GameObject.Find("HeadingController");
        GameController.headingController.transform.rotation = GameController.camera.transform.localRotation;
    }
}
