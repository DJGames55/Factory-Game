using UnityEngine.InputSystem;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions, GameInput.IBuildActions
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

    public void SetBuild()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameinput.UI.Disable();
        gameinput.Build.Enable();
    }
    
    // Movement
    public event Action<Vector2> MoveEvent;
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    // Look Controls
    public event Action<Vector2> LookEvent;
    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    // Jump
    public event Action JumpEvent;
    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke();
    }

    // Interact
    public event Action InteractEvent;
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) InteractEvent?.Invoke();
    }

    // Pause/Resume
    public event Action PauseEvent;
    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }

    public event Action ResumeEvent;
    public void OnResume(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();
        }
    }

    // Building
    public event Action OpenBuildEvent;
    public void OnOpenBuild(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) OpenBuildEvent?.Invoke();
    }
}
