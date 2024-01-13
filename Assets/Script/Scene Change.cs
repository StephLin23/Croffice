using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}