using Unity.VisualScripting;
using UnityEngine;

public class TestVehicleCreator : MonoBehaviour
{
    public GameObject vehiclePrefab;

    public VehicleBody testBody;
    public VehicleWheel testWheel;
    public VehicleEngine testEngine;

    private void Start()
    {
        CreateTestVehicle();
    }

    private void CreateTestVehicle()
    {
        VehicleBuilder builder = this.AddComponent<VehicleBuilder>();
        builder.SetWheels(testWheel).SetBody(testBody).SetEngine(testEngine).Build();

    }
}
