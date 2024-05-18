using Cairo.CacheBoxing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controllable Events")]
[UnitTitle("On Controllable Button Action")]
public class OnControllableButtonAction : ReflectiveEventUnit<OnControllableAxis2DAction>
{
    public ValueInput InputName;
    [OutputType(typeof(Controllable))]
    public ValueOutput Controllable;
    [OutputType(typeof(InputActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(BoxedBool))]
    public ValueOutput Value;

    public static void Invoke(Controllable controllable, string InputName, InputActionPhase phase, BoxedBool value)
    {
        ModularInvoke(controllable.gameObject, ("InputName", InputName), ("Controllable", controllable), ("Phase", phase), ("Value", value));
    }

    protected override bool ShouldTrigger(Flow flow, SerializableDictionary<string, object> args)
    {
        if (flow.GetValue<string>(InputName) != (string)args["InputName"]) return false;
        if (EventTarget == null || EventTarget.connection == null) return true;
        var target = flow.GetValue<object>(EventTarget);
        return (target == InvokationTarget || target == null);
    }
    protected override void Definition()
    {
        base.Definition();
        InputName = ValueInput<string>("Input Name", " ");
    }
}