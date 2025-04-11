using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the oven behavior:
/// - Accepts raw cakes
/// - Handles baking and burning over time
/// - Triggers lights, sounds, and animation
/// </summary>
public class FourController : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Light lumiereFour;

    [Header("Audio")]
    public AudioClip sonPorte;
    public AudioClip sonCuisson;
    public AudioClip sonBrule;

    [Header("Cooking Durations")]
    public float dureeCuisson = 7f;       // time to bake
    public float delaiAvantBrule = 5f;    // time before cake burns

    [Header("Cake drop point")]
    public Transform pointDepotGateau;

    // Internals
    private bool gateauDedans = false;
    private GameObject gateauActuel;

    private bool cuissonTerminee = false;
    private bool gateauBrule = false;

    private void Start()
    {
        if (lumiereFour != null)
            lumiereFour.enabled = false;
    }

    /// <summary>
    /// Called when a cake is dropped into the oven trigger zone
    /// </summary>
    public void RecevoirGateau(GameObject gateau)
    {
        if (gateauDedans) return;

        if (sonPorte != null)
            AudioSource.PlayClipAtPoint(sonPorte, transform.position);

        GateauInteractif gateauScript = gateau.GetComponent<GateauInteractif>();
        if (gateauScript == null)
        {
            Debug.LogError("❌ Missing GateauInteractif script on " + gateau.name);
            return;
        }

        // Reject cake if it's already cooked or burnt
        if (gateauScript.EstCuit() || gateauScript.EstBrule())
        {
            Debug.Log("❌ Cake already cooked or burnt, rejected by oven.");
            return;
        }

        // Accept the raw cake
        gateauActuel = gateau;
        gateauDedans = true;
        cuissonTerminee = false;
        gateauBrule = false;

        // Snap position and freeze
        gateau.transform.position = pointDepotGateau.position;
        gateau.transform.rotation = Quaternion.identity;

        Rigidbody rb = gateau.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Disable drag + trigger baking logic
        gateauScript.enabled = false;
        gateauScript.MarquerCommeUtilise();
        gateauScript.Cuire();

        if (sonCuisson != null)
            AudioSource.PlayClipAtPoint(sonCuisson, transform.position);

        if (lumiereFour != null)
            lumiereFour.enabled = true;

        animator.Play("PorteOuverture");

        StartCoroutine(LancerCuisson());
    }

    /// <summary>
    /// Handles baking and burning over time (via coroutine)
    /// </summary>
    private IEnumerator LancerCuisson()
    {
        GameObject gateauTemp = gateauActuel;

        yield return new WaitForSeconds(dureeCuisson);
        cuissonTerminee = true;

        // Allow drag after baking
        Rigidbody rb = gateauTemp.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        GateauInteractif gateau = gateauTemp.GetComponent<GateauInteractif>();
        gateau.enabled = true;

        yield return new WaitForSeconds(delaiAvantBrule);

        // If still in oven and not yet burnt
        if (gateau != null && !gateau.EstBrule())
        {
            gateau.Bruler();

            if (sonBrule != null)
                AudioSource.PlayClipAtPoint(sonBrule, transform.position);

            if (lumiereFour != null)
                lumiereFour.enabled = false;
        }
    }

    /// <summary>
    /// Called when the player removes the cake from the oven
    /// </summary>
    public void GateauRetireDuFour()
    {
        animator.Play("PorteOuverture");

        gateauDedans = false;
        gateauActuel = null;

        if (lumiereFour != null)
            lumiereFour.enabled = false;

        if (sonPorte != null)
            AudioSource.PlayClipAtPoint(sonPorte, transform.position);
    }
}
