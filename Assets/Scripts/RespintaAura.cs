using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RespintaAura : MonoBehaviour
{
    [Header("Aura Settings")]
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private float auraDuration = 1.5f;

    private SphereCollider auraCollider;
    private bool auraActive = false;

    private void Awake()
    {
        auraCollider = GetComponent<SphereCollider>();
        auraCollider.isTrigger = true;

        // Aura starts OFF
        auraCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter with: " + other.name);
        if (!auraActive) {
            Debug.Log("Aura NOT active → ignoring");
            return;
        }
       

        // Check if it is an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detected! Applying push.");
            Rigidbody rb = other.attachedRigidbody;

            if (rb != null)
            {
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay with: " + other.name);
    }

    // PUBLIC — Call from outside

    public void ActivateAura()
    {
        StartCoroutine(AuraRoutine(auraDuration));
    }

    public void ActivateAura(float duration)
    {
        StartCoroutine(AuraRoutine(duration));
    }

    private System.Collections.IEnumerator AuraRoutine(float duration)
    {
        Debug.Log("AuraRoutine START");
        auraActive = true;
        auraCollider.enabled = true;
        Debug.Log("Collider enabled: " + auraCollider.enabled);

        yield return new WaitForSeconds(duration);

        auraActive = false;
        auraCollider.enabled = false;
        Debug.Log("AuraRoutine END");
        Debug.Log("Collider enabled: " + auraCollider.enabled);
    }

}


