using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ElevatorTransition : MonoBehaviour
{
    public string nextSceneName = "Level_1"; // Replace with the name of your next scene
    public float transitionDuration = 1.5f;
    public Image FadeImage; // Drag and drop your Image component here

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered trigger zone"); // Add this line for debugging

        if (other.CompareTag("Player")) // Adjust the tag based on your player object
        {
            StartCoroutine(TransitionToNextScene());
        }
    }

    private IEnumerator TransitionToNextScene()
    {
        // Ensure the Image component is assigned
        if (FadeImage == null)
        {
            Debug.LogError("Image");
            yield break;
        }

        // Create a color with alpha set to 0 (fully transparent)
        Color initialColor = new Color(0f, 0f, 0f, 0f);
        FadeImage.color = initialColor;

        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            FadeImage.color = Color.Lerp(initialColor, Color.black, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            FadeImage.color = Color.Lerp(Color.black, initialColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
