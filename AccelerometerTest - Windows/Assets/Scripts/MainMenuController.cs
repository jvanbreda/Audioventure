using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void ButtonClicked() {
        SceneManager.LoadScene("AccelerometerTest");
    }

}
