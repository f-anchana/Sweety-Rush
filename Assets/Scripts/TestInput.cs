using UnityEngine;

public class InputTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tu as appuyé sur e tamere !");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(" Clic gauche détecté !");
        }
    }
}
