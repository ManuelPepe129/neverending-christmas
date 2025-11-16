using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;
    [SerializeField] private UnityEvent<float> damageEvent;
    [SerializeField] private UnityEvent deathEvent;

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
        
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, maxHealth);
        print(_currentHealth);
        if (_currentHealth <= 0)
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
}
