using UnityEngine.InputSystem;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
{
    private GameInput gameinput;

    private void OnEnable()
    {
        if (gameinput == null)
        {
            gameinput = new GameInput();

            gameinput.Gameplay.SetCallbacks(this);
            gameinput.UI.SetCallbacks(this);

            SetGameplay();
        }
    }

    public void SetGameplay()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameinput.Gameplay.Enable();
        gameinput.UI.Disable();
    }
    public void SetUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameinput.UI.Enable();
        gameinput.Gameplay.Disable();
    }

    public event Action<Vector2> MoveEvent;

    public event Action<Vector2> LookEvent;

    public event Action JumpEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;

    // Movement
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke();
    }

    // Look Controls
    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    // Pause/Resume
    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }
    public void OnResume(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();
        }
    }
}
