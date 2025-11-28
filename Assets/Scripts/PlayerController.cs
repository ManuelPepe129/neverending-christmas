using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    private float _currentSpeed = 1.0f;

    private Rigidbody _rigidBody;
    private Vector3 _movement;
    private InputSystem_Actions _playerControls;
    
    private Camera mainCamera;

    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float attackCooldown = 3;
    private bool _attackButtonDown = false;
    private bool _canAttack = true;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerControls = new InputSystem_Actions();
    }

    private void Start()
    {
        _currentSpeed = moveSpeed;

        // Setup Attack controls
        _playerControls.Player.Attack.started += _ => StartAttacking();
        _playerControls.Player.Attack.canceled += _ => StopAttacking();
        
        mainCamera = FindFirstObjectByType<Camera>();
    }


    private void Update()
    {
        GetPlayerInput();
        Attack();
    }


    private void FixedUpdate()
    {
        RotateTowardsMouse();
        Move();
    }
    
    private void RotateTowardsMouse()
    {
        Vector2 mouseScreenPos = _playerControls.Player.Rotate.ReadValue<Vector2>();

        // Player position in screen space
        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(_rigidBody.position);

        // Use the player's depth (z in screen-space)
        Vector3 mouseScreenWithDepth = new Vector3(mouseScreenPos.x, mouseScreenPos.y, playerScreenPos.z);

        // Convert to world position
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mouseScreenWithDepth);

        // Direction from player to mouse in world-space
        Vector3 rotateDirection = (worldPos - _rigidBody.position);
        rotateDirection.y = 0f; // Lock to Y-axis

        if (rotateDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(rotateDirection.normalized, Vector3.up);
            _rigidBody.MoveRotation(targetRot);
        }
    }
    
    private void GetPlayerInput()
    {
        var movement2D = _playerControls.Player.Move.ReadValue<Vector2>();
        _movement = new Vector3(movement2D.x, 0, movement2D.y);
    }

    private void Move()
    {
        _rigidBody.MovePosition(_rigidBody.position + _movement * (_currentSpeed * Time.fixedDeltaTime));
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void OnDeath()
    {
        throw new NotImplementedException("Player OnDeath not implemented");
    }

    private void Attack()
    {
        if (_attackButtonDown && _canAttack)
        {
            _canAttack = false;
            // Spawn weapon prefab
            var weaponSpawnPosition = new Vector3(_rigidBody.position.x, _rigidBody.position.y + 0.86f, _rigidBody.position.z);
            Instantiate(weaponPrefab, weaponSpawnPosition, _rigidBody.rotation);
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }
}