using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _gravity = -9.8f;

    [SerializeField]
    private float _groundCheckRayLenght = 0.2f;

    [SerializeField]
    private Vector3 _groundCheckRayOffest = Vector3.zero;

    private InputActions _inputActions;

    private bool _isGrounded;

    [SerializeField]
    [Range(0f, 20f)]
    private float _lookSpeed = 5f;

    private Vector3 _move;

    private float _playerPrevSpeed;

    [SerializeField]
    [Range(0f, 20f)]
    private float _speed = 5f;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private LayerMask groundLayerMask;

    public int keys { get; private set; } = 0;

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _move = new Vector3(input.x, 0, input.y);
    }

    private void Awake()
    {
        if (!_animator)
            _animator = GetComponentInChildren<Animator>();
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) + _groundCheckRayOffest, _groundCheckRayLenght, groundLayerMask);
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void OnEnable()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Move.performed += Move;
        _inputActions.Player.Move.canceled += Move;
    }

    private void OnLevelComplete()
    {
        LogService.Log("Level Complete");
        _inputActions.Disable();

        // TODO: show Game over screen
        Managers.Instance.SceneLoader.LoadMainMenu();
    }

    private void OnTriggerEnter(Collider other)
    {
        Key _key = other.gameObject.GetComponent<Key>();
        if (_key)
        {
            if (_key.Collect(this))
            {
                keys += 1;
            }
            return;
        }

        Gate _gate = other.gameObject.GetComponent<Gate>();
        if (_gate)
        {
            if (_gate.Open(this))
            {
                keys -= 1;
            }
            return;
        }

        // 10 = finish point;
        if (other.gameObject.layer == 10)
        {
            OnLevelComplete();
            return;
        }
    }

    private void Update()
    {
        CheckGrounded();

        if (_isGrounded && _move.y < 0)
            _move.y = 0;

        characterController.Move(_move * Time.deltaTime * _speed);

        // For smooth animation transition
        _playerPrevSpeed = Mathf.Lerp(_playerPrevSpeed, characterController.velocity.magnitude / _speed, Time.deltaTime * 10f);
        _animator.SetFloat("Velocity", _playerPrevSpeed);

        Vector3 _lookDirection = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);
        if (_lookDirection != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_lookDirection), Time.deltaTime * _lookSpeed);

        _move.y += _gravity * Time.deltaTime;
        characterController.Move(Vector3.up * _move.y * Time.deltaTime);
    }
}