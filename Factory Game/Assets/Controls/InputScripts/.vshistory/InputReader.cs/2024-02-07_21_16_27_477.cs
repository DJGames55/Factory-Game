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
        gameinput.Gameplay.Enable();
        gameinput.UI.Disable();
    }

    public void SetUI()
    {
        gameinput.UI.Enable();
        gameinput.Gameplay.Disable();
    }

    public event Action<Vector2> MoveEvent;

    public event Action JumpEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    public void OnPause(InputAction.CallbackContext context)
    {

    }

    public void OnResume(InputAction.CallbackContext context)
    {

    }
}
