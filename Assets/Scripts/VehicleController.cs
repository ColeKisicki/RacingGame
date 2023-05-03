using Unity.VisualScripting;
using UnityEngine;

public enum DriveType : short
{
    FWD,
    RWD,
    AWD
};

[RequireComponent(typeof(Vehicle))]
public class VehicleController : MonoBehaviour
{
    private Vehicle _vehicle;

    [SerializeField] public float motorTorque = 500f;
    [SerializeField] public float maxSteerAngle = 45f;
    [SerializeField] public float brakeTorque = 500f;
    [SerializeField] public float maxVelocity = 50f;
    [SerializeField] public DriveType drive = DriveType.AWD;
    private bool _wheelsInitailized;
    private Rigidbody rbRef;
    
    //controller uses strategy to dertermine difficulty(max speed)
    private IDifficultyStrategy _difficultyStrategy = new MediumDifficulty();



    private void Awake()
    {
        _wheelsInitailized = false;
        _vehicle = GetComponent<Vehicle>();
        //subscribing to vehicle built event
        _vehicle.OnVehicleBuilt += OnVehicleBuilt;
        SetDifficultyStrategy(_difficultyStrategy);
    }
    
    public void SetDifficultyStrategy(IDifficultyStrategy strategy)
    {
        _difficultyStrategy = strategy;
        maxVelocity = _difficultyStrategy.AdjustMaxSpeed(maxVelocity);
    }
    
    private void OnVehicleBuilt()
    {
        _wheelsInitailized = true;
        motorTorque = _vehicle.engine.engineTorque;
        maxVelocity = _vehicle.engine.maxSpeed;
        brakeTorque = _vehicle.wheel.brakeTorque;
        rbRef = _vehicle.rbRef;
        SetDifficultyStrategy(GameState.GetGameState().Difficulty);
    }
    
    // private void ApplyDrift(float horizontalInput, float verticalInput, bool brakeInput)
    // {
    //     if (Mathf.Abs(horizontalInput) > 0.5f && verticalInput > 0.5f && brakeInput)
    //     {
    //         _vehicle.AddForce(-transform.right * (driftForce * horizontalInput), ForceMode.Force);
    //     }
    // }

    private void FixedUpdate()
    {
        float throttle = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        bool brake = Input.GetKey(KeyCode.Space);
        
        if (!_wheelsInitailized) return;
        
        float currentSpeed = rbRef.velocity.magnitude;
        if (currentSpeed > maxVelocity && throttle > 0)
        {
            throttle = 0;
        }
        
        
        Drive(throttle, steer, brake);
    }

    private void Drive(float throttle, float steer, bool brake)
    {
        //add torque to front and back wheels depending on drive type
        ApplyMotorTorque(throttle);
        //add brake torque
        ApplyBrakes(brake);
        //set current turn angle for front wheels and update wheel positions
        ApplySteer(steer);
    }
    
    private void ApplyMotorTorque(float input)
    {
        float torque = motorTorque * input;

        if (drive is DriveType.FWD or DriveType.AWD) 
        {
            _vehicle.frontLeftCollider.motorTorque = torque;
            _vehicle.frontRightCollider.motorTorque = torque;
        }
        if (drive is DriveType.RWD or DriveType.AWD) 
        {
            _vehicle.rearRightCollider.motorTorque = torque;
            _vehicle.rearLeftCollider.motorTorque = torque;
        }
    }
    
    private void ApplyBrakes(bool brakeInput)
    {
        float brakeForce = brakeInput ? brakeTorque : 0f;
        _vehicle.frontRightCollider.brakeTorque = brakeForce;
        _vehicle.frontLeftCollider.brakeTorque = brakeForce;
        _vehicle.rearRightCollider.brakeTorque = brakeForce;
        _vehicle.rearLeftCollider.brakeTorque = brakeForce;
    }
    
    private void ApplySteer(float input)
    {
        float steerAngle = maxSteerAngle * input;
        _vehicle.frontLeftCollider.steerAngle = steerAngle;
        _vehicle.frontRightCollider.steerAngle = steerAngle;
        
        UpdateWheel(_vehicle.frontLeftCollider, _vehicle.frontRightTransform);
        UpdateWheel(_vehicle.frontRightCollider, _vehicle.frontLeftTransform);
        UpdateWheel(_vehicle.rearRightCollider, _vehicle.rearRightTransform);
        UpdateWheel(_vehicle.rearLeftCollider, _vehicle.rearLeftTransform);
    }

    private void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);
        trans.position = position;
        trans.rotation = rotation;
    }
}