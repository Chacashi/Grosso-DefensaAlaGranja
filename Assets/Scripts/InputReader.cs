using UnityEngine;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, MainInput.IPlayerActions, MainInput.IUIActions

{
    public event Action<Vector2> MovementEvent;
    public event Action InteractEvent;
    public event Action PauseEvent;
    public event Action ResumeEvent;
    private MainInput gameInput;



    private void OnEnable()
    {
        if(gameInput == null)
        {
            gameInput = new MainInput();
            gameInput.Player.SetCallbacks(this);
            gameInput.UI.SetCallbacks(this);
            gameInput.Player.Enable();  
        }
        
    }


    private void OnDisable()
    {
        if (gameInput != null)
        {
            gameInput.Player.Disable();
            gameInput.UI.Disable();
        }
        
    }

    public void SetGameplay()
    {
        gameInput.Player.Disable();
        gameInput.UI.Enable();
    }

    public void SetUI()
    {
        gameInput.UI.Disable();
        gameInput.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            InteractEvent?.Invoke();

    }


    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
        
    }


    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();  
        }
            
    }




}
