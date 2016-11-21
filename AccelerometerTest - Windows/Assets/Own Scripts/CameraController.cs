using UnityEngine;
using System.Collections;
using System;
using Assets;
using Assets.Own_Scripts;

public class CameraController : MonoBehaviour {

    private GameObject camParent;
    private int currentCameraAngle;
    private Quaternion heading;
    private const int MOVEMENT_SPEED = 10;

    private const int CAMERA_SPEED = 10;

    public Ray headingRay { get; private set; }
    public Ray rightRay { get; private set; }

    [SerializeField]
    public SoundObject[] soundObjectArray;

    public int counter;
    

    // Use this for initialization
    void Start() {
        counter = 0;
        // Used for 'calibratrion': this makes sure that the camera behaves 
        // exactly the same as the phone the user is holding
        //camParent = new GameObject("camParent");
        //camParent.transform.Rotate(Vector3.right, 90);
        //transform.parent = camParent.transform;

        // Gyroscope must be manually enabled to be used and the location functionality must
        // be used
        Input.gyro.enabled = true;
        Input.location.Start();

        // Some 'standard' configuration: the back button can now be used to close the app,
        // and the smartphone will not go to sleep during the usage of the app.
        Input.backButtonLeavesApp = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update() {
        UpdateHeading();
        Move();
        SendRays();
        UpdateAudioSources();
    }

    private void Move() {
        // Gives the user the possibility to choose between touch or usb mouse control
        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) {
            if (!GameObject.Find("Footsteps").GetComponent<AudioSource>().isPlaying) {
                GameObject.Find("Footsteps").GetComponent<AudioSource>().Play();
                transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * MOVEMENT_SPEED;
            }
        }
    }

    private void UpdateHeading() {
        heading = Input.gyro.attitude;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, new Quaternion(heading.x * CAMERA_SPEED, heading.y * CAMERA_SPEED, -heading.z * CAMERA_SPEED, -heading.w * CAMERA_SPEED), Time.deltaTime);
        currentCameraAngle = 360 - (int) transform.eulerAngles.y;

        //GameObject.Find("Compass").transform.localRotation = Quaternion.Euler(new Vector3(90, currentCameraAngle, -90));

        //GameObject.Find("Degrees text").GetComponent<TextMesh>().text = currentCameraAngle.ToString();
    }

    private void SendRays() {
        headingRay = new Ray(transform.position, transform.forward);
        Physics.Raycast(headingRay, 3);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);

        rightRay = new Ray(transform.position, transform.right);
        Physics.Raycast(rightRay, 3);
        Debug.DrawRay(transform.position, transform.right, Color.blue);
    }

    private void UpdateAudioSources() {
        foreach (SoundObject source in soundObjectArray) {
            source.audioModel = CalculateAngleDifference(source);
        }
    }

    private AudioModel CalculateAngleDifference(SoundObject source) {
        AudioModel model = new AudioModel();
        model.angleDifference2D = Vector2.Angle(new Vector2(headingRay.direction.x, headingRay.direction.z), new Vector2(source.playerRay.direction.x, source.playerRay.direction.z));
        model.angleDifference3D = Vector3.Angle(headingRay.direction, source.playerRay.direction);
        model.isAudioLocatedLeft = Vector3.Angle(rightRay.direction, source.playerRay.direction) > 90;
        model.distance = Vector3.Distance(transform.position, source.transform.position);

        return model;
    }
}
