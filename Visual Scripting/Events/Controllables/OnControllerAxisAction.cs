using Unity.VisualScripting;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controllable Events")]
[UnitTitle("On Controllable Axis Action")]
public class OnControllableAxisAction : ReflectiveEventUnit<OnControllableAxisAction>
{
    [OutputType(typeof(Controllable))]
    public ValueOutput Controllable;
    [OutputType(typeof(InputActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(float))]
    public ValueOutput Value;

    public static void Invoke(Controllable controllable, InputActionPhase phase, float value)
    {
        ModularInvoke(controllable.gameObject, ("Controllable", controllable), ("Phase", phase), ("Value", value));
    }
}