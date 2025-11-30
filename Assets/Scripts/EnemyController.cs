using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float damage = 10;

    private GameManager _manager;

    /// <summary>
    /// To avoid multiple Death calls
    /// </summary>
    private bool _isDead = false;

    public void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void OnDeath()
    {
        if (_isDead) return;
        _isDead = true;

        _manager.enemiesSpawned--;
        Destroy(gameObject);
    }

    public void OnBossDeath()
    {
        var gameManager = GameObject.FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnLevelCompleted();
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (_isDead) return;
        if (other.gameObject.CompareTag("Player"))
        {
            var playerHealthComponent = other.gameObject.GetComponent<HealthComponent>();
            if (playerHealthComponent != null)
            {
                playerHealthComponent.TakeDamage(damage);
            }
        }
    }
}