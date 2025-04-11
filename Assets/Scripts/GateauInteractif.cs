using UnityEngine;

/// <summary>
/// Allows the player to pick up and drag a cake.
/// Handles interaction with oven and state transitions (raw → cooked → burnt).
/// </summary>
public class GateauInteractif : MonoBehaviour
{
    // Drag state
    private bool estAttrape = false;
    private Vector3 offset;
    private float zDistance;

    private GateauSpawner spawnerOrigine;
    private FourController fourCible = null;


    public enum EtatGateau { Cru, Cuit, Brule }
    public EtatGateau etat = EtatGateau.Cru;

    public GameObject decoPistache;
    public bool EstSurAssiette { get; set; }

    private void Start()
    {
        if (decoPistache != null)
            decoPistache.SetActive(false);
    }


    /// <summary>
    /// Called by the spawner when the cake is created.
    /// </summary>
    public void SetSpawner(GateauSpawner spawner)
    {
        spawnerOrigine = spawner;
    }

    /// <summary>
    /// Called when player clicks on the cake.
    /// </summary>
    private void OnMouseDown()
    {
        Debug.Log($"👆 CLICK GATEAU — Etat: {etat} | Kinematic: {GetComponent<Rigidbody>().isKinematic}");

        // Allow movement only if cooked or burnt
        if (etat == EtatGateau.Cuit || etat == EtatGateau.Brule)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;
        }

        estAttrape = true;

        // Compute drag offset
        zDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance)
        );
        offset = transform.position - mouseWorld;


    }

    /// <summary>
    /// While dragging the cake with the mouse
    /// </summary>
    private void OnMouseDrag()
    {
        if (!estAttrape) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance)
        );
        transform.position = mouseWorld + offset;
    }

    /// <summary>
    /// Called when player releases the mouse button.
    /// </summary>
    private void OnMouseUp()
    {
        estAttrape = false;

        // Snap into oven only if cake is raw
        if (fourCible != null && etat == EtatGateau.Cru)
        {
            fourCible.RecevoirGateau(gameObject);
        }
    }

    /// <summary>
    /// Detects entry into oven trigger zone.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ZoneDetectionGateau")
        {
            Debug.Log("✅ Cake entered oven trigger zone");
            FourController four = other.GetComponentInParent<FourController>();
            if (four != null)
            {
                fourCible = four;
            }
        }
    }

    /// <summary>
    /// Detects exit from oven trigger zone.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "ZoneDetectionGateau")
        {
            Debug.Log("🚪 Cake exited oven zone");
            FourController four = other.GetComponentInParent<FourController>();
            if (four != null)
            {
                four.GateauRetireDuFour();
            }

            if (four == fourCible)
            {
                fourCible = null;
            }
        }
    }

    /// <summary>
    /// Releases the original spawner when cake is used.
    /// </summary>
    public void MarquerCommeUtilise()
    {
        if (spawnerOrigine != null)
            spawnerOrigine.LibererGateau();
    }

    private void OnDestroy()
    {
        MarquerCommeUtilise();
    }

    // === State management ===

    public void Cuire()
    {
        if (etat == EtatGateau.Cru)
        {
            etat = EtatGateau.Cuit;
            Debug.Log("🟡 Cake is now cooked");
        }
    }

    public void Bruler()
    {
        if (etat == EtatGateau.Cuit)
        {
            etat = EtatGateau.Brule;
            Debug.Log("⚫ Cake is now burnt");
        }
    }

    public bool EstCuit() => etat == EtatGateau.Cuit;
    public bool EstBrule() => etat == EtatGateau.Brule;

    public void AppliquerPistache()
    {
        if (decoPistache != null)
            decoPistache.SetActive(true);
    }
}


