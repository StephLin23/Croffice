using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RandomVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips; // Array of VideoClip objects

    void Start()
    {
        // Subscribe to the videoPlayer loopPointReached event
        videoPlayer.loopPointReached += OnVideoEnd;

        // Start playing a random video
        PlayRandomVideo();
    }

    public void PlayRandomVideo()
    {
        // Choose a random VideoClip from the array
        int randomIndex = Random.Range(0, videoClips.Length);
        VideoClip randomVideoClip = videoClips[randomIndex];

        // Set the VideoClip to play
        videoPlayer.clip = randomVideoClip;

        // Play the selected video
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Stop the video player
        vp.Stop();

        // Load the last scene when the video ends
        LoadLastScene();
    }

    void LoadLastScene()
    {
        // Get the name of the scene that was active before the current scene
        string lastSceneName = GetPreviousSceneName();

        // Check if a valid last scene name is obtained
        if (!string.IsNullOrEmpty(lastSceneName))
        {
            // Load the last scene
            SceneManager.LoadScene(lastSceneName);
        }
        else
        {
            Debug.LogError("Unable to determine the last scene name.");
        }
    }

    string GetPreviousSceneName()
    {
        // instead, read player pref that stores current (floor) level
        // Check if there is at least one scene in the build settings
        if (SceneManager.sceneCountInBuildSettings > 1)
        {
            // Get the scene index of the current scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Check if the current scene is not the first scene
            if (currentSceneIndex > 0)
            {
                Debug.Log("returning to " + SceneManager.GetSceneByBuildIndex(currentSceneIndex - 1).name);
                // Return the name of the scene before the current scene
                return SceneManager.GetSceneByBuildIndex(currentSceneIndex - 1).name;
            }
        }

        // If conditions are not met, return an empty string or handle it according to your needs
        return "";
    }
}
