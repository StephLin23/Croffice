using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float startingTime = 10f;

    private void Start()
    {
        // Check if the GameManager exists, and create one if not
        if (GameManager.Instance == null)
        {
            GameObject managerObject = new GameObject("GameManager");
            managerObject.AddComponent<GameManager>();
        }

        // Start the countdown in the GameManager
        GameManager.Instance.StartCountdown(startingTime);

        // Get the UI Text component from the GameObject this script is attached to
        Text countdownText = GetComponent<Text>();

        // Set the UI Text component for displaying the countdown in the GameManager
        GameManager.Instance.SetCountdownText(countdownText);
    }
}
