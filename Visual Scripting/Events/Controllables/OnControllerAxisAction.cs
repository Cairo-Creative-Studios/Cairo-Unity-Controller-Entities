using Cairo.CacheBoxing;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controllable Events")]
[UnitTitle("On Controllable Axis Action")]
public class OnControllableAxisAction : ReflectiveEventUnit<OnControllableAxisAction>
{
    public ValueInput InputName;
    [OutputType(typeof(Controllable))]
    public ValueOutput Controllable;
    [OutputType(typeof(ControllerActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(BoxedFloat))]
    public ValueOutput Value;

    public static void Invoke(Controllable controllable, string InputName, ControllerActionPhase phase, BoxedFloat value)
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