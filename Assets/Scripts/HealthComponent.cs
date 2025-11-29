using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private UnityEvent<float> damageEvent;
    [SerializeField] private UnityEvent deathEvent;

    // invulnerability set up
    [SerializeField] private float invulnerabilityTime = 1.5f;

    private bool _isInvulnerable = false;
    public bool IsInvulnerable => _isInvulnerable;
    public float CurrentHealth => currentHealth;   // ← ADD THIS

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (damageEvent == null)
        {
            damageEvent = new UnityEvent<float>();
        }
        damageEvent.AddListener(OnTakeDamage);

        if (deathEvent == null)
        {
            deathEvent = new UnityEvent();
        }
        deathEvent.AddListener(OnDeath);
        
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {

        // prevent damage if currently invulnerable

        if (_isInvulnerable) {
            print("now invulnerable");
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

        ActivateInvulnerability(invulnerabilityTime);

        if (currentHealth <= 0)
        {
            deathEvent.Invoke();
        }
        else
        {
            damageEvent.Invoke(damage);
        }
    }

    private void OnTakeDamage(float damage)
    {
        // Event handler assigned in Unity Inspector
    }
    
    private void OnDeath()
    {
        // Event handler assigned in Unity Inspector
    }

    
    // PUBLIC INVULNERABILITY API
 
    public void ActivateInvulnerability()
    {
        if (!_isInvulnerable)
            StartCoroutine(InvulnerabilityRoutine(invulnerabilityTime));
    }

    public void ActivateInvulnerability(float duration)
    {
        StartCoroutine(InvulnerabilityRoutine(duration));
    }

    private System.Collections.IEnumerator InvulnerabilityRoutine(float duration)
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        _isInvulnerable = false;
    }
}
