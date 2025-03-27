using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomepageSettings : MonoBehaviour
{
    // to load the screen after the home screen
    public void LoadBakeryScene()
    {
        Debug.Log("Chargement de la scène Bakery...");
        SceneManager.LoadScene("Bakery"); // Change to the Bakery scene
    }
}
