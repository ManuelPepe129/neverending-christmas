using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
}
