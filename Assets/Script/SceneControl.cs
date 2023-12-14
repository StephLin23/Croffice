using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneControl: MonoBehaviour 
{  
    public void LoadScene(string Level_0) 
    {  
        SceneManager.LoadScene(Level_0);  
    }  
   
}
