using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomepageSettings : MonoBehaviour
{
    // to load the screen after the home screen
    public void LoadBakeryScene()
    {
        Debug.Log("Chargement de la sc�ne Bakery...");
        SceneManager.LoadScene("Bakery"); // Change to the Bakery scene
    }
}
