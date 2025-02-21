using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystem : MonoBehaviour
{
    private GameObject selectedObject = null;
    public GameObject cookedPrefab; // cooked object (here is cakecuit)
    public Transform spawnPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if it detects a left click from the mouse
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (selectedObject == null && hit.collider.CompareTag("Ingredient"))
                {
                    // Sélectionner l’objet (pâte)
                    selectedObject = hit.collider.gameObject;
                    selectedObject.GetComponent<Renderer>().material.color = Color.white;
                    Debug.Log($" Ingrédient sélectionné : {selectedObject.name}");
                }
                else if (selectedObject != null && hit.collider.CompareTag("Four"))
                {
                    // Lancer la cuisson
                    Debug.Log($" L’ingrédient {selectedObject.name} est mis dans le four !");
                    StartCoroutine(CookIngredient(selectedObject, hit.collider.gameObject));
                    selectedObject = null; // Réinitialise la sélection
                }
            }
        }
    }

    IEnumerator CookIngredient(GameObject ingredient, GameObject four)
    {
        //the ingredient disappears and it looks like it's in the oven
        ingredient.SetActive(false);
        Debug.Log("⏳ Cuisson en cours...");

        // Attendre 3 secondes (avec logs à chaque seconde)
        for (int i = 3; i > 0; i--)
        {
            Debug.Log($"⌛ Temps restant : {i} sec");
            yield return new WaitForSeconds(1f);
        }

        // Créer l’objet cuit (ex: sphère)
        GameObject cookedObject = Instantiate(cookedPrefab, spawnPoint.position, Quaternion.identity);
        cookedObject.GetComponent<Renderer>().material.color = Color.magenta; // Coloré en rose
        Debug.Log("✅ Cuisson terminée ! L’objet cuit est apparu !");
    }
}
