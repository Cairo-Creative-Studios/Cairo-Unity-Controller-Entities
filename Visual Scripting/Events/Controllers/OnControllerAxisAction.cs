using Unity.VisualScripting;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controller Events")]
[UnitTitle("On Controller Axis Action")]
public class OnControllerAxisAction : ReflectiveEventUnit<OnControllerAxisAction>
{
    [OutputType(typeof(Controller))]
    public ValueOutput Controller;
    [OutputType(typeof(InputActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(float))]
    public ValueOutput Value;

    public static void Invoke(Controller controller, InputActionPhase phase, float value)
    {
        ModularInvoke(controller.gameObject, ("Controller", controller), ("Phase", phase), ("Value", value));
    }
}