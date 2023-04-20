using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{
    [Header("Car Properties")]
    public float motorTorque = 300f;
    public float maxSteerAngle = 30f;
    public float brakeForce = 2000f;
    public float driftForce = 5f;
    public DriveType driveType = DriveType.RWD;

    [Header("Wheels")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;
    public Transform frTransform;
    public Transform flTransform;
    public Transform brTransform;
    public Transform blTransform;

    private Rigidbody _carRigidbody;

    private void Start()
    {
        _carRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool brakeInput = Input.GetKey(KeyCode.Space);

        ApplySteer(horizontalInput);
        ApplyMotorTorque(verticalInput);
        ApplyBrakes(brakeInput);
        ApplyDrift(horizontalInput, verticalInput, brakeInput);
    }

    private void ApplySteer(float input)
    {
        float steerAngle = maxSteerAngle * input;
        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;
        
        UpdateWheelDirection(frontLeftWheel, flTransform);
        UpdateWheelDirection(frontRightWheel, frTransform);
        UpdateWheelDirection(rearLeftWheel, blTransform);
        UpdateWheelDirection(rearRightWheel, brTransform);
    }

    private void ApplyMotorTorque(float input)
    {
        float torque = motorTorque * input;

        if (driveType is DriveType.FWD or DriveType.AWD) 
        {
            frontLeftWheel.motorTorque = torque;
            frontRightWheel.motorTorque = torque;
        }
        if (driveType is DriveType.RWD or DriveType.AWD) 
        {
            rearLeftWheel.motorTorque = torque;
            rearRightWheel.motorTorque = torque;
        }
    }

    private void ApplyBrakes(bool brakeInput)
    {
        float brakeTorque = brakeInput ? brakeForce : 0f;
        rearLeftWheel.brakeTorque = brakeTorque;
        rearRightWheel.brakeTorque = brakeTorque;
        frontLeftWheel.brakeTorque = brakeTorque;
        frontRightWheel.brakeTorque = brakeTorque;
    }

    private void ApplyDrift(float horizontalInput, float verticalInput, bool brakeInput)
    {
        if (Mathf.Abs(horizontalInput) > 0.5f && verticalInput > 0.5f && brakeInput)
        {
            _carRigidbody.AddForce(-transform.right * (driftForce * horizontalInput), ForceMode.Force);
        }
    }

    private void UpdateWheelDirection(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);
        trans.position = position;
        trans.rotation = rotation;
    }
}