using Unity.VisualScripting;

[UnitCategory("Controllables/Controller Events")]
[UnitTitle("On Controller Created")]
public class OnControllerCreated : ReflectiveEventUnit<OnControllerCreated>
{
    [OutputType(typeof(Controller))]
    public ValueOutput Controller;

    public static void Invoke(Controller controller)
    {
        ModularInvoke(controller.gameObject, ("Controller", controller));
    }
}