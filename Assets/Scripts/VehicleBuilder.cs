using System;
using UnityEditor;
using UnityEngine;
public class VehicleBuilder : MonoBehaviour
{
    public VehicleWheel wheel = null;
    public VehicleBody body = null;
    public VehicleEngine engine = null;
    public GameObject vehiclePrefab = null;
    
    private void Start()
    {
        GameState.GetGameState()._playerVehicleRef = Build();
    }
    public VehicleBuilder SetWheels(VehicleWheel inWheel)
    {
        this.wheel = inWheel;
        return this;
    }

    public VehicleBuilder SetBody(VehicleBody inBody)
    {
        this.body = inBody;
        return this;
    }

    public VehicleBuilder SetEngine(VehicleEngine inEngine)
    {
        this.engine = inEngine;
        return this;
    }

    public VehicleBuilder SetVPrefab(GameObject inVehiclePrefab)
    {
        this.vehiclePrefab = inVehiclePrefab;
        return this;
    }
    
    public Vehicle Build()
    {
        if (wheel == null || body == null || engine == null || vehiclePrefab == null)
        {
            throw new InvalidOperationException("Wheels, body, and engine must be set before building the vehicle.");
        }
        
        GameObject vehicleInstance = Instantiate(vehiclePrefab, transform.position, transform.rotation);
        Vehicle vehicle = vehicleInstance.GetComponent<Vehicle>();
        vehicle.vehicleBody = body;
        vehicle.wheel = wheel;
        vehicle.engine = engine;

        return vehicle;
    }
}
