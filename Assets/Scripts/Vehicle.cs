using System;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public VehicleBody vehicleBody;
    public VehicleWheel wheel;
    public VehicleEngine engine;
    
    public event Action OnVehicleBuilt;
    public GameObject bodyInstance;
    
    public WheelCollider frontRightCollider;
    public WheelCollider frontLeftCollider;
    public WheelCollider rearRightCollider;
    public WheelCollider rearLeftCollider;
    
    public Transform frontRightTransform;
    public Transform frontLeftTransform;
    public Transform rearRightTransform;
    public Transform rearLeftTransform;

    private void BuildVehicle()
    {
        // Instantiate body and set weight
        bodyInstance = Instantiate(vehicleBody.bodyPrefab, transform);
        Rigidbody rb = bodyInstance.AddComponent<Rigidbody>();
        rb.mass = vehicleBody.weight;

        // Instantiate wheels and set wheel colliders
        GetWheelPositions(bodyInstance);
        

        //instantiate all wheels and enable colliders
        GameObject frontRightWheelInstance = Instantiate(wheel.wheelPrefab, frontRightTransform.position, frontRightTransform.rotation, bodyInstance.transform);
        //frontRightTransform = frontRightWheelInstance.transform;
        frontRightCollider = frontRightWheelInstance.GetComponent<WheelCollider>();
        frontRightCollider.enabled = true;
        
        GameObject frontLeftWheelInstance = Instantiate(wheel.wheelPrefab, frontLeftTransform.position, frontLeftTransform.rotation, bodyInstance.transform);
        //frontLeftTransform = frontLeftWheelInstance.transform;
        frontLeftCollider = frontLeftWheelInstance.GetComponent<WheelCollider>();
        frontLeftCollider.enabled = true;
        
        GameObject rearRightWheelInstance = Instantiate(wheel.wheelPrefab, rearRightTransform.position, rearRightTransform.rotation, bodyInstance.transform);
        //rearRightTransform = rearRightWheelInstance.transform;
        rearRightCollider = rearRightWheelInstance.GetComponent<WheelCollider>();
        rearRightCollider.enabled = true;
        
        GameObject rearLeftWheelInstance = Instantiate(wheel.wheelPrefab, rearLeftTransform.position, rearLeftTransform.rotation, bodyInstance.transform);
        //rearLeftTransform = rearLeftWheelInstance.transform;
        rearLeftCollider = rearLeftWheelInstance.GetComponent<WheelCollider>();
        rearLeftCollider.enabled = true;
        

    }
    
    void GetWheelPositions(GameObject bodyInstance)
    {
        foreach (Transform child in bodyInstance.transform)
        {
            if (child.name.Equals("FrontRightWheelSocket"))
            {
                frontRightTransform = child;
            }
            if (child.name.Equals("FrontLeftWheelSocket"))
            {
                frontLeftTransform = child;
            }
            if (child.name.Equals("RearRightWheelSocket"))
            {
                rearRightTransform = child;
            }
            if (child.name.Equals("RearLeftWheelSocket"))
            {
                rearLeftTransform = child;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        BuildVehicle();
        OnVehicleBuilt?.Invoke();
    }
    
}
