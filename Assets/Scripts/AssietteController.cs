using UnityEngine;

/// <summary>
/// Handles cake placement into the plate zone.
/// Snaps the cake into place if it is cooked or burnt.
/// </summary>
public class AssietteController : MonoBehaviour
{
    [Header("Where the cake should snap")]
    public Transform pointSnap;

    /// <summary>
    /// Triggered when a cake enters the plate zone.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Ignore anything that's not a cake
        if (!other.CompareTag("Gateau")) return;

        GateauInteractif gateau = other.GetComponent<GateauInteractif>();
        if (gateau == null) return;

        // Accept only cooked or burnt cakes
        if (!gateau.EstCuit() && !gateau.EstBrule()) return;

        Debug.Log("🍽️ Cake placed on the plate: " + other.name);

        // Snap position and reset rotation
        other.transform.position = pointSnap.position;
        other.transform.rotation = Quaternion.identity;

        // Freeze the cake
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        // Disable dragging
        gateau.enabled = false;

        // ✅ Marque comme "posé sur assiette"
        gateau.EstSurAssiette = true;
    }

}
