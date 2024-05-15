using UnityEngine;
using UnityEngine.InputSystem;

public class Controllable : MonoBehaviour
{
    private Controller _controller;
    public Controller Controller => _controller;

    private string _map = "";
    public string Map => _map;

    public void Possess(Controller controller)
    {
        _controller = controller;
    }

    public void Unpossess(Controller controller)
    {
        _controller = controller;
    }


    public virtual void OnControllerAxisInput(string actionName, float value, InputActionPhase phase = InputActionPhase.Performed)
    {

    }

    public virtual void OnControllerAxis2DInput(string actionName, Vector2 value, InputActionPhase phase = InputActionPhase.Performed)
    {

    }


    public virtual void OnControllerButtonInput(string actionName, bool value, InputActionPhase phase = InputActionPhase.Performed)
    {

    }
}