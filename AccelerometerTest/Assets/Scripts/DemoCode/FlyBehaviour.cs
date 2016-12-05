using UnityEngine;
using System.Collections;

public class FlyBehaviour : MonoBehaviour {

    float timeCounter = 0;

    float speed;
    float radius;
    [SerializeField]
    private Rigidbody test;
    float posx;
    float posy;
    float posz;

    // Use this for initialization
    void Start() {
        speed = 0.5f;
        radius = 100;
        posx = test.transform.position.x;
        posy = test.transform.position.y;
        posz = test.transform.position.z;
    }

    // Update is called once per frame
    void Update() {
        timeCounter += Time.deltaTime * speed;

        float z = Mathf.Cos(timeCounter) * radius + posz;
        float x = Mathf.Sin(timeCounter) * radius + posx;
        float y = 0 + posy;

        transform.position = new Vector3(x, y, z);

    }
}
