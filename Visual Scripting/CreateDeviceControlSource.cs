using Unity.VisualScripting;
using UnityEngine.InputSystem;

[UnitCategory("Controllables/Sources")]
[UnitTitle("Create Device Control Source")]
public class CreateDeviceControlSource : Unit
{
    public ControlInput In;
    public ControlOutput Out;
    public ValueInput InputActionAsset;
    public ValueOutput DeviceControlSource;

    protected override void Definition()
    {
        In = ControlInput("", (flow) =>
        {
            flow.SetValue(DeviceControlSource, new DeviceControlSource(flow.GetValue<InputActionAsset>(InputActionAsset)));
            return Out;
        });
        Out = ControlOutput("");

        InputActionAsset = ValueInput<InputActionAsset>("Input Action Asset", null);
        DeviceControlSource = ValueOutput<DeviceControlSource>("Control Source");
    }
}