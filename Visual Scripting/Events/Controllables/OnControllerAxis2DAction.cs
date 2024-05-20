using Cairo.CacheBoxing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controllable Events")]
[UnitTitle("On Controllable Axis 2D Action")]
public class OnControllableAxis2DAction : ReflectiveEventUnit<OnControllableAxis2DAction>
{
    public ValueInput InputName;
    [OutputType(typeof(Controllable))]
    public ValueOutput Controllable;
    [OutputType(typeof(ControllerActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(BoxedVector2))]
    public ValueOutput Value;

    public static void Invoke(Controllable controllable, string InputName, ControllerActionPhase phase, BoxedVector2 value)
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