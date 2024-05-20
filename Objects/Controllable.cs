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


    public virtual void OnControllerAxisInput(string actionName, float value, ControllerActionPhase phase = ControllerActionPhase.Held)
    {

    }

    public virtual void OnControllerAxis2DInput(string actionName, Vector2 value, ControllerActionPhase phase = ControllerActionPhase.Held)
    {

    }


    public virtual void OnControllerButtonInput(string actionName, bool value, ControllerActionPhase phase = ControllerActionPhase.Held)
    {

    }
}