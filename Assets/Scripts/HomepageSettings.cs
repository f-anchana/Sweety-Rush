using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomepageSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadBakeryScene()
    {
        Debug.Log("Chargement de la scène Bakery...");
        SceneManager.LoadScene("Bakery"); // Charge la scène nommée "Bakery"
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
