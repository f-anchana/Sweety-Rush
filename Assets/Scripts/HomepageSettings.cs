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
        Debug.Log("Chargement de la sc�ne Bakery...");
        SceneManager.LoadScene("Bakery"); // Charge la sc�ne nomm�e "Bakery"
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
