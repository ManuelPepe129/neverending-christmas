using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_player == null)
        {
            Debug.LogError("Player not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _enemy.SetDestination(_player.position);
    }
}
