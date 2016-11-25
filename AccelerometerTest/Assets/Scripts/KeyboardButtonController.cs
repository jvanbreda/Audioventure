using Assets.Own_Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class KeyboardButtonController : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Button KeyboardButton = GameObject.Find("KeyboardButton").GetComponent<Button>();
            Debug.Log("Test");

            KeyboardButton.GetComponent<Image>().color = Color.red;

            GameController.controller = new KeyboardController();
            SceneManager.LoadScene("AccelerometerTest");
        }
    }
}
