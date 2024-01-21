using UnityEngine;
using UnityEngine.UI;

public class CoffeeImageScript : MonoBehaviour
{
    public static CoffeeImageScript instance; // Singleton instance

    public Image coffeeImage; // Reference to the Image component for coffee images
    public Sprite[] coffeeSprites; // Array of coffee sprites indicating different states

    private int currentSpriteIndex = 0; // Index of the current coffee sprite
    private static float timer = .1f; // Static timer to track sprite change intervals
    public float timeBetweenChanges = 60.0f; // Time between sprite changes

    void Awake()
    {
        // Ensure only one instance of CoffeeImageScript exists
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
        // Load the last saved sprite index from PlayerPrefs
        if (PlayerPrefs.HasKey("SpriteIndex"))
        {
            currentSpriteIndex = PlayerPrefs.GetInt("SpriteIndex");
        }

        // Set the initial coffee sprite
        if (coffeeImage != null && coffeeSprites.Length > 0)
        {
            coffeeImage.sprite = coffeeSprites[currentSpriteIndex];
        }
        else
        {
            Debug.LogError("coffeeImage or coffeeSprites is not set in the Inspector!");
        }
    }

    void Update()
    {
        // Update the timer across all instances
        timer += Time.deltaTime;

        // Check if it's time to change the coffee sprite
        if (timer >= timeBetweenChanges)
        {
            // Reset the timer
            timer = 0.0f;

            // Change to the next coffee sprite
            ChangeCoffeeSprite();
        }
    }

    void ChangeCoffeeSprite()
    {
        // Change to the next coffee sprite
        currentSpriteIndex++;

        // Save the current sprite index to PlayerPrefs
        PlayerPrefs.SetInt("SpriteIndex", currentSpriteIndex);

        // Update the coffee sprite
        if (coffeeImage != null)
        {
            coffeeImage.sprite = coffeeSprites[currentSpriteIndex];
            Debug.Log("Changed to sprite index: " + currentSpriteIndex);
        }
    }
}
