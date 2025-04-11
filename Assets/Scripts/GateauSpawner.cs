using UnityEngine;

/// <summary>
/// Spawns a new cake when the player clicks, if none is already active.
/// </summary>
public class GateauSpawner : MonoBehaviour
{
    [Header("Prefab & Spawn Point")]
    public GameObject gateauPrefab;
    public Transform pointSpawn;

    private GameObject gateauActuel;

    private void OnMouseDown()
    {
        // Block if cake is already active or references are missing
        if (gateauActuel != null || gateauPrefab == null || pointSpawn == null) return;

        // Instantiate the cake
        gateauActuel = Instantiate(gateauPrefab, pointSpawn.position, Quaternion.identity);
        gateauActuel.tag = "Gateau";

        // Attach behavior
        GateauInteractif interactif = gateauActuel.GetComponent<GateauInteractif>();
        if (interactif == null)
            interactif = gateauActuel.AddComponent<GateauInteractif>();

        interactif.SetSpawner(this);

        // Optional: log or feedback
        Debug.Log("🍰 Nouveau gâteau spawné !");
    }

    /// <summary>
    /// Called by the cake when it is used, destroyed, or placed.
    /// </summary>
    public void LibererGateau()
    {
        gateauActuel = null;
    }
}
