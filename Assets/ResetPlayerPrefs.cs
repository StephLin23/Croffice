using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted Player Prefs!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
