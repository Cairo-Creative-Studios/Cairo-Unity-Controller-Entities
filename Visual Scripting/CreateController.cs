using Unity.VisualScripting;
using UnityEngine.InputSystem;

[UnitCategory("Controllables")]
[UnitTitle("Create Controller")]
public class CreateController : Unit
{
    public ControlInput In;
    public ControlOutput Out;
    public ValueInput Name;
    public ValueOutput CreatedController; 

    protected override void Definition()
    {
        In = ControlInput("", (flow) =>
        {
            flow.SetValue(CreatedController, Controller.CreateController(flow.GetValue<string>(Name)));
            return Out;
        });
        Out = ControlOutput("");

        Name = ValueInput<string>("Name");
        CreatedController = ValueOutput<Controller>("Created Controller");
    }
}