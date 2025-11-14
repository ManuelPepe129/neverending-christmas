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

    private void OnTakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, maxHealth);
        if (_currentHealth <= 0)
        {
            OnDeath();
        }
        else
        {
            damageEvent.Invoke(damage);
        }
    }
    
    private void OnDeath()
    {
        deathEvent.Invoke();
    }
}
