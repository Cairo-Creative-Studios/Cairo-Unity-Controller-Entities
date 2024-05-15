using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controller Events")]
[UnitTitle("On Controller Button Action")]
public class OnControllerButtonAction : ReflectiveEventUnit<OnControllerAxis2DAction>
{
    [OutputType(typeof(Controller))]
    public ValueOutput Controller;
    [OutputType(typeof(InputActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(bool))]
    public ValueOutput Value;

    public static void Invoke(Controller controller, InputActionPhase phase, bool value)
    {
        ModularInvoke(controller.gameObject, ("Controller", controller), ("Phase", phase), ("Value", value));
    }
}