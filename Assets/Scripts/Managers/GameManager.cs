using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.GameManager.Enable();
        inputActions.GameManager.Restart.performed += OnRestart;
    }

    private void OnRestart(InputAction.CallbackContext context)
    {
        Managers.Instance?.SceneLoader.LoadMainMenu();
    }

    private void OnDestroy()
    {
        inputActions.GameManager.Disable();
    }
}