using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controllable Events")]
[UnitTitle("On Controllable Button Action")]
public class OnControllableButtonAction : ReflectiveEventUnit<OnControllableAxis2DAction>
{
    [OutputType(typeof(Controllable))]
    public ValueOutput Controllable;
    [OutputType(typeof(InputActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(bool))]
    public ValueOutput Value;

    public static void Invoke(Controllable controllable, InputActionPhase phase, bool value)
    {
        ModularInvoke(controllable.gameObject, ("Controllable", controllable), ("Phase", phase), ("Value", value));
    }
}