using UnityEngine;

public class ClientController : MonoBehaviour
{
    public Transform pointDestination; // ← C’EST ÇA QUI MANQUAIT
    public float vitesse = 2f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (pointDestination == null) return;

        Vector3 direction = pointDestination.position - transform.position;
        direction.y = 0;

        float distance = direction.magnitude;

        if (distance > 0.1f)
        {
            transform.position += direction.normalized * vitesse * Time.deltaTime;
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);
            anim?.SetFloat("Speed", 1f);
        }
        else
        {
            anim?.SetFloat("Speed", 0f);
        }
    }
}
