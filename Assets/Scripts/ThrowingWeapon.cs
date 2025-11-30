using UnityEngine;

public class ThrowingWeapon : MonoBehaviour
{
    private Transform _playerTransform;
    private Vector3 _targetPosition;
    private bool _goingBack;
    
    [SerializeField] private float damage = 10f;
    [SerializeField] private float throwDistance = 10f;
    [SerializeField] private float throwSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        var player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            _playerTransform = player.transform;
        }
        _targetPosition = _playerTransform.position + _playerTransform.forward * throwDistance;
        _targetPosition.y = transform.position.y;
        _goingBack = false;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        if (Vector3.Distance(transform.position, _targetPosition) <= 0.5f)
        {
            _goingBack = true;
        }
        
        var targetPosition = _goingBack ? _playerTransform.position : _targetPosition;
        _targetPosition.y = transform.position.y;
        // TODO: rotate player according to mouse click position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,throwSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _playerTransform.position) <= 0.5f && _goingBack)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<EnemyFollow>();
        if (!enemy) return;
        var enemyHealth = enemy.gameObject.GetComponent<HealthComponent>();
        enemyHealth?.TakeDamage(damage);
    }
}
