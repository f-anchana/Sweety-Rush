using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [Header("Les différents modèles de clients")]
    public GameObject[] prefabsClients;

    [Header("Point de spawn du client")]
    public Transform pointSpawn;

    [Header("Destination vers le comptoir")]
    public Transform pointDestination;

    public void FaireVenirUnClient()
    {
        int index = Random.Range(0, prefabsClients.Length);
        GameObject prefabChoisi = prefabsClients[index];

        GameObject nouveauClient = Instantiate(prefabChoisi, pointSpawn.position, Quaternion.identity);

        ClientController controleur = nouveauClient.GetComponent<ClientController>();
        if (controleur != null)
        {
            controleur.pointDestination = pointDestination;
        }
    }
}
