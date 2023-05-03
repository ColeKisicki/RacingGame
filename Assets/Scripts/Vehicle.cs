using System;
using Unity.VisualScripting;
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

    public Rigidbody rbRef;

    private void BuildVehicle()
    {
        // Instantiate body and set weight
        bodyInstance = Instantiate(vehicleBody.bodyPrefab, transform);
        rbRef = bodyInstance.AddComponent<Rigidbody>();
        rbRef.mass = vehicleBody.weight;

        // Instantiate wheels and set wheel colliders
        GetWheelPositions(bodyInstance);
        
        
        //copy all collider settings and make new collider attached to body

        //instantiate all wheels and enable colliders
        GameObject frontRightWheelInstance = Instantiate(wheel.wheelPrefab, frontRightTransform.position, frontRightTransform.rotation, bodyInstance.transform);
        WheelCollider frcolliderRef = frontRightWheelInstance.GetComponent<WheelCollider>();
        frontRightCollider = frontRightTransform.AddComponent<WheelCollider>();
        frontRightCollider.transform.rotation = frcolliderRef.transform.rotation;
        CopyWheelSettingsToNew(frcolliderRef, frontRightCollider);
        Destroy(frcolliderRef);
        frontRightCollider.enabled = true;
        frontRightTransform = frontRightWheelInstance.transform;
        
        GameObject frontLeftWheelInstance = Instantiate(wheel.wheelPrefab, frontLeftTransform.position, frontLeftTransform.rotation, bodyInstance.transform);
        frontLeftCollider = frontLeftTransform.AddComponent<WheelCollider>();
        frontLeftCollider.transform.rotation = frontLeftTransform.transform.rotation;
        CopyWheelSettingsToNew(frontRightCollider, frontLeftCollider);
        Destroy(frontLeftWheelInstance.GetComponent<WheelCollider>());
        frontLeftCollider.enabled = true;
        frontLeftTransform = frontLeftWheelInstance.transform;
        
        GameObject rearRightWheelInstance = Instantiate(wheel.wheelPrefab, rearRightTransform.position, rearRightTransform.rotation, bodyInstance.transform);
        rearRightCollider = rearRightTransform.AddComponent<WheelCollider>();
        rearRightCollider.transform.rotation = rearRightTransform.transform.rotation;
        CopyWheelSettingsToNew(frontRightCollider, rearRightCollider);
        Destroy(rearRightWheelInstance.GetComponent<WheelCollider>());
        rearRightCollider.enabled = true;
        rearRightTransform = rearRightWheelInstance.transform;
        
        GameObject rearLeftWheelInstance = Instantiate(wheel.wheelPrefab, rearLeftTransform.position, rearLeftTransform.rotation, bodyInstance.transform);
        rearLeftCollider = rearLeftTransform.AddComponent<WheelCollider>();
        rearLeftCollider.transform.rotation = rearLeftTransform.transform.rotation;
        CopyWheelSettingsToNew(frontRightCollider, rearLeftCollider);
        Destroy(rearLeftWheelInstance.GetComponent<WheelCollider>());
        rearLeftCollider.enabled = true;
        rearLeftTransform = rearLeftWheelInstance.transform;

    }

    private void CopyWheelSettingsToNew(WheelCollider referenceCollider, WheelCollider newCollider)
    {
        newCollider.radius = referenceCollider.radius;
        newCollider.mass = referenceCollider.mass;
        newCollider.wheelDampingRate = referenceCollider.wheelDampingRate;
        newCollider.center = referenceCollider.center;
        newCollider.suspensionDistance = referenceCollider.suspensionDistance;
        newCollider.forwardFriction = referenceCollider.forwardFriction;
        newCollider.suspensionSpring = referenceCollider.suspensionSpring;
        newCollider.sidewaysFriction = referenceCollider.sidewaysFriction;
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
