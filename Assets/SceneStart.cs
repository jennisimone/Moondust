using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour {

    public void StartGame() {
        SceneManager.LoadScene("Main");
    }
}
    