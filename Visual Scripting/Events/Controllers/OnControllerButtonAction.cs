using Cairo.CacheBoxing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Controller Events")]
[UnitTitle("On Controller Button Action")]
public class OnControllerButtonAction : ReflectiveEventUnit<OnControllerAxis2DAction>
{
    public ValueInput InputName;
    [OutputType(typeof(Controller))]
    public ValueOutput Controller;
    [OutputType(typeof(ControllerActionPhase))]
    public ValueOutput Phase;
    [OutputType(typeof(BoxedBool))]
    public ValueOutput Value;

    public static void Invoke(Controller controller, string InputName, ControllerActionPhase phase, BoxedBool value)
    {
        ModularInvoke(controller.gameObject, ("InputName", InputName), ("Controller", controller), ("Phase", phase), ("Value", value));
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