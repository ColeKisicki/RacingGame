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
        GameObject vehicleInstance = Instantiate(vehiclePrefab, transform.position, transform.rotation);
        Vehicle customizableVehicle = vehicleInstance.GetComponent<Vehicle>();

        customizableVehicle.vehicleBody = testBody;
        customizableVehicle.wheel = testWheel;
        customizableVehicle.engine = testEngine;
        
    }
}
