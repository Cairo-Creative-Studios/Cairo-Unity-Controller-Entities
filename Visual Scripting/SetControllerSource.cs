using Unity.VisualScripting;

public class SetControllerSource : Unit
{
    public ControlInput In;
    public ControlOutput Out;
    public ValueInput TargetController;
    public ValueInput ControlSource;

    protected override void Definition()
    {
        In = ControlInput("", (flow) =>
        {
            var controller = flow.GetValue<Controller>(TargetController);
            var source = flow.GetValue<ControlSource>(ControlSource);
            controller.InputSource = source;
            return Out;
        });
        Out = ControlOutput("");

        ControlSource = ValueInput<ControlSource>("Control Source");
        TargetController = ValueInput<Controller>("Controller");
    }
}