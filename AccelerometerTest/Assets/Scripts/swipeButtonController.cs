using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Assets.Own_Scripts;

public class swipeButtonController : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData)
    {
        Button swipeButton = GameObject.Find("SwipeButton").GetComponent<Button>();
        Debug.Log("Test");

        swipeButton.GetComponent<Image>().color = Color.blue;

        GameController.controller = new SwipeController();
        SceneManager.LoadScene("AccelerometerTest");


    }
}
