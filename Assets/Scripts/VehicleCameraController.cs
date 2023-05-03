using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class VehicleCameraController : MonoBehaviour
{
    [SerializeField] public Vehicle vehicle;
    public GameObject body;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float horizontalSpeed = 2.0f;
    [SerializeField] private float verticalSpeed = 2.0f;
    [SerializeField] private float minYAngle = -20.0f;
    [SerializeField] private float maxYAngle = 80.0f;
    [SerializeField] private LayerMask cameraCollisionLayer;

    private float horizontalAngle = 0.0f;
    private float verticalAngle = 0.0f;
    private float currentDistance;

    void Start()
    {
        currentDistance = distance;
    }
    

    void LateUpdate()
    {
        if (body == null)
        {
            body = GameState.GetGameState()._playerVehicleRef.bodyInstance;
        }
        Debug.Log(body.transform.position);
        horizontalAngle += Input.GetAxis("Mouse X") * horizontalSpeed;
        verticalAngle -= Input.GetAxis("Mouse Y") * verticalSpeed;
        verticalAngle = Mathf.Clamp(verticalAngle, minYAngle, maxYAngle);

        Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);
        Vector3 targetPosition = body.transform.position - (rotation * Vector3.forward * distance);
        RaycastHit hit;

        if (Physics.Linecast(body.transform.position, targetPosition, out hit, cameraCollisionLayer))
        {
            currentDistance = Mathf.Clamp(hit.distance * 0.9f, 0.5f, distance);
        }
        else
        {
            currentDistance = distance;
        }

        transform.position = body.transform.position - (rotation * Vector3.forward * currentDistance);
        transform.LookAt(body.transform);
    }
}
