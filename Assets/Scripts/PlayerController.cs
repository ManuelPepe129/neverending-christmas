using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    private float _currentSpeed = 1.0f;
    
    private Rigidbody _rigidBody;
    private Vector3 _movement;
    private InputSystem_Actions _playerControls;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerControls = new InputSystem_Actions();
    }

    private void Start()
    {
        _currentSpeed = moveSpeed;
    }

    private void Update()
    {
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
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
        throw  new NotImplementedException("Player OnDeath not implemented");
    }
}
