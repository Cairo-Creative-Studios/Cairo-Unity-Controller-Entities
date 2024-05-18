using Unity.VisualScripting;
using UnityEngine;
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
            var go = new GameObject("Device Control Source");
            var controlSource = go.AddComponent<DeviceControlSource>();
            controlSource.AssignActions(flow.GetValue<InputActionAsset>(InputActionAsset));
            flow.SetValue(DeviceControlSource, controlSource);
            return Out;
        });
        Out = ControlOutput("");

        InputActionAsset = ValueInput<InputActionAsset>("Input Action Asset", null);
        DeviceControlSource = ValueOutput<DeviceControlSource>("Control Source");
    }
}