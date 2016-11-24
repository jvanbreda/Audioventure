using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Own_Scripts;

public class HeadmountedButtonController : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData)
    {
        Button headmountedButton = GameObject.Find("HeadmountedButton").GetComponent<Button>();
        
        Debug.Log("Test");

        headmountedButton.GetComponent<Image>().color = Color.blue;

        GameController.controller = new HeadMountedController();
        SceneManager.LoadScene("AccelerometerTest");
    }
}
