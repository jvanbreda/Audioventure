using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Assets.Own_Scripts;

public class TouchButtonController : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData)
    {
        Button TouchButton = GameObject.Find("TouchButton").GetComponent<Button>();
        Debug.Log("Test");

        TouchButton.GetComponent<Image>().color = Color.blue;

        GameController.controller = new TouchController();
        SceneManager.LoadScene("AccelerometerTest");


    }
}
