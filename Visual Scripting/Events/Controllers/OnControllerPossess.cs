using Unity.VisualScripting;

[UnitCategory("Controllables/Controller Events")]
[UnitTitle("On Controller Possesed Controllable")]
public class OnControllerPossess : ReflectiveEventUnit<OnControllerPossess>
{
    [OutputType(typeof(Controller))]
    public ValueOutput Controller;
    [OutputType(typeof(Controllable))]
    public ValueOutput Controllable;

    public static void Invoke(Controller controller, Controllable controllable)
    {
        ModularInvoke(controllable.gameObject, ("Controller", controller), ("Controllable", controllable));
    }
}