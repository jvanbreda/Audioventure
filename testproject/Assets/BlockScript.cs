using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

    private GameObject player;
    private const float SCALE_FACTOR = 0.45f;
    private const int TILE_SIZE = 32;
    private const float MOVEMENT_SPEED = SCALE_FACTOR * TILE_SIZE;

    private bool collides;

    private GameObject wallCollider, playerCollider;

    private Vector3 previousPosition;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
        playerCollider = GameObject.Find("PlayerCollision");
        wallCollider = GameObject.Find("WallCollision");
        collides = false;        
    }

    // Update is called once per frame
    void Update() {
        ControlMovement();
        Debug.Log(collides);
    }

    private void ControlMovement() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            playerCollider.transform.position += new Vector3(0, MOVEMENT_SPEED, 0);
            if (!collides) {
                player.transform.position += new Vector3(0, MOVEMENT_SPEED, 0);
            }
            playerCollider.transform.position = player.transform.position;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            playerCollider.transform.position += new Vector3(0, -MOVEMENT_SPEED, 0);
            if (!collides) {
                player.transform.position += new Vector3(0, -MOVEMENT_SPEED, 0);
            }
            playerCollider.transform.position = player.transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            playerCollider.transform.position += new Vector3(-MOVEMENT_SPEED, 0, 0);
            if (!collides) {
                player.transform.position += new Vector3(-MOVEMENT_SPEED, 0, 0);
            }
            playerCollider.transform.position = player.transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            playerCollider.transform.position += new Vector3(MOVEMENT_SPEED, 0, 0);
            if (!collides) {
                player.transform.position += new Vector3(MOVEMENT_SPEED, 0, 0);
            }
            playerCollider.transform.position = player.transform.position; 
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        collides = true;
        Debug.Log("Collided");
    }

    void OnCollisionExit2D(Collision2D other) {
        collides = false;
    }
}
