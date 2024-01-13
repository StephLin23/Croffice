using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject managerObject = new GameObject("GameManager");
                instance = managerObject.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    private float currentTime;
    private Text countdownText;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartCountdown(float startingTime)
    {
        currentTime = startingTime;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            // You can add more functionality here if needed
            currentTime -= Time.deltaTime;
            if (countdownText != null)
                countdownText.text = currentTime.ToString("0");

            yield return null;
        }

        // Switch to another scene when the timer reaches 0
        SwitchToAnotherScene();
    }

    void SwitchToAnotherScene()
    {
        // Replace "YourSceneName" with the name of the scene you want to switch to
        SceneManager.LoadScene("YourSceneName");
    }

    // This method is called to set the Text component for displaying the countdown
    public void SetCountdownText(Text textComponent)
    {
        countdownText = textComponent;
    }
}
