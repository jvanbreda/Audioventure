using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeadmountedButtonController : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData)
    {
        Button headmountedButton = GameObject.Find("HeadmountedButton").GetComponent<Button>();
        SceneManager.LoadScene("AccelerometerTest");
        Debug.Log("Test");

        headmountedButton.GetComponent<Image>().color = Color.blue;
    }
}
