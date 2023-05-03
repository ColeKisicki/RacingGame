using Unity.VisualScripting;
using UnityEngine;

public class PlayerVehicleSpawner : MonoBehaviour
{
    public GameObject vehiclePrefab;

    public VehicleBody testBody;
    public VehicleWheel testWheel;
    public VehicleEngine testEngine;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 5, -10);
    [SerializeField] private Quaternion cameraRotation = Quaternion.Euler(20, 0, 0);
    VehicleBuilder builder;

    private void Start()
    {
        builder = this.gameObject.AddComponent<VehicleBuilder>();
        if(GameState.GetGameState().selectedBody != null)
            testBody = GameState.GetGameState().selectedBody;
        if(GameState.GetGameState().selectedWheel != null)
            testWheel = GameState.GetGameState().selectedWheel;
        if(GameState.GetGameState().selectedEngine != null)
            testEngine = GameState.GetGameState().selectedEngine;
        CreateTestVehicle();
    }

    private void CreateTestVehicle()
    {
        GameObject newCamera = new GameObject("Camera");
        newCamera.AddComponent<Camera>();
        
        VehicleCameraController cameraController = newCamera.AddComponent<VehicleCameraController>();
        
        
        Vehicle createdVehicle = builder.SetVPrefab(vehiclePrefab).SetWheels(testWheel).SetBody(testBody).SetEngine(testEngine).Build();
        newCamera.transform.position = createdVehicle.transform.position + cameraOffset;
        newCamera.transform.rotation = cameraRotation;
        
        cameraController.vehicle = createdVehicle;
        cameraController.body = createdVehicle.bodyInstance;
        GameState.GetGameState()._playerVehicleRef = createdVehicle;
    }
}
