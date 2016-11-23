using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class swipeButtonController : MonoBehaviour, IPointerDownHandler {


    public void OnPointerDown(PointerEventData eventData)
    {
        Button swipeButton = GameObject.Find("SwipeButton").GetComponent<Button>();
        SceneManager.LoadScene("AccelerometerTest");
        Debug.Log("Test");

        swipeButton.GetComponent<Image>().color = Color.blue;
    }
}
