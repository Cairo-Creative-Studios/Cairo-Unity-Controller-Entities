using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controllable Events")]
[UnitTitle("On Controllable Axis 2D Action")]
public class OnControllableAxis2DAction : ReflectiveEventUnit<OnControllableAxis2DAction>
{
    [OutputType(typeof(Controllable))]
    public ValueOutput Controllable;
    [OutputType(typeof(InputActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(Vector2))]
    public ValueOutput Value;

    public static void Invoke(Controllable controllable, InputActionPhase phase, Vector2 value)
    {
        ModularInvoke(controllable.gameObject, ("Controllable", controllable), ("Phase", phase), ("Value", value));
    }
}