using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float totalTime = 60.0f; // Set the total time in seconds
    private float currentTime;

    public Text timerText; // Reference to the Text element for displaying time

    void Start()
    {
        // Check if the current scene is Level_0 before starting the timer
        if (SceneManager.GetActiveScene().name == "Level_0")
        {
            currentTime = totalTime;
        }
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            // Update the text to display the remaining time
            timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
        }
        else
        {
            // Timer has reached zero, transition to another scene
            SceneManager.LoadScene("End");

            // Destroy the timer GameObject
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // Ensure the timer persists between scenes
        DontDestroyOnLoad(gameObject);
    }
}
