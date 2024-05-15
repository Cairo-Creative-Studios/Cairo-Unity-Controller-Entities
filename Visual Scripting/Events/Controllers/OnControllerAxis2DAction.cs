using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controller Events")]
[UnitTitle("On Controller Axis 2D Action")]
public class OnControllerAxis2DAction : ReflectiveEventUnit<OnControllerAxis2DAction>
{
    [OutputType(typeof(Controller))]
    public ValueOutput Controller;
    [OutputType(typeof(InputActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(Vector2))]
    public ValueOutput Value;

    public static void Invoke(Controller controller, InputActionPhase phase, Vector2 value)
    {
        ModularInvoke(controller.gameObject, ("Controller", controller), ("Phase", phase), ("Value", value));
    }
}