using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static TimerScript instance; // Singleton instance

    public float totalTime = 60.0f; // Set the total time in seconds
    private float currentTime;

    public Text timerText; // Reference to the Text element for displaying time

    private bool timerActive = true; // Flag to control whether the timer is active

    void Awake()
    {
        // Ensure only one instance of TimerScript exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        // Check if the timer is active
        if (timerActive)
        {
            // Update the time only if the timer is active
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;

                // Update the text to display the remaining time
                if (timerText != null)
                {
                    timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
                }
                else
                {
                    Debug.LogError("timerText is null!");
                }
            }
            else
            {
                // Timer has reached zero, transition to another scene
                SceneManager.LoadScene("BadEnd");

                // Destroy the timer GameObject
                Destroy(gameObject);
            }
        }

        // Check if the scene has changed
        if (SceneManager.GetActiveScene().name != "Level_0" && SceneManager.GetActiveScene().name != "GoodEnd")
        {
            // Find the Text component in the new scene and update its text
            Text newText = FindObjectOfType<Text>();
            if (newText != null)
            {
                newText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
            }
        }

        // Check if the current scene is "GoodEnd" and stop the timer
        if (SceneManager.GetActiveScene().name == "GoodEnd")
        {
            timerActive = false;

            // Destroy the TimerController component (this script) along with its GameObject
            Destroy(gameObject);
        }
    }
}