using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VechicleCustomizer : MonoBehaviour
{
    [SerializeField]public GameObject displayVehicle;
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 100f, 0f);
    
    
    [SerializeField] public GameObject vehiclePrefab;
    [SerializeField] public List<VehicleBody> bodyList;
    [SerializeField] public List<VehicleWheel> wheelList;
    [SerializeField] public List<VehicleEngine> engineList;
    private VehicleBuilder builder;

    private VehicleBody selectedBody;
    private int selectedBodyIdx = 0;
    
    private VehicleWheel selectedWheel;
    private int selectedWheelIdx = 0;
    
    private VehicleEngine selectedEngine;
    private int selectedEngineIdx = 0;
    
    //cycles through availible bodies
    public void GetNextBody()
    {
        selectedBodyIdx++;
        if (selectedBodyIdx >= bodyList.Count)
            selectedBodyIdx = 0;
        selectedBody = bodyList[selectedBodyIdx];
        UpdateDisplayCar();
    }
    public void GetPreviousBody()
    {
        selectedBodyIdx--;
        if (selectedBodyIdx < 0)
            selectedBodyIdx = bodyList.Count-1;
        selectedBody = bodyList[selectedBodyIdx];
        UpdateDisplayCar();
    }
    
    //cycles through availible wheels
    public void GetNextWheel()
    {
        selectedWheelIdx++;
        if (selectedWheelIdx >= wheelList.Count)
            selectedWheelIdx = 0;
        selectedWheel = wheelList[selectedWheelIdx];
        UpdateDisplayCar();
    }
    public void GetPreviousWheel()
    {
        selectedWheelIdx--;
        if (selectedWheelIdx < 0)
            selectedWheelIdx = wheelList.Count-1;
        selectedWheel = wheelList[selectedWheelIdx];
        UpdateDisplayCar();
    }
    
    //cycles through availible engines
    public void GetNextEngine()
    {
        selectedEngineIdx++;
        if (selectedEngineIdx >= engineList.Count)
            selectedEngineIdx = 0;
        selectedEngine = engineList[selectedEngineIdx];
        UpdateDisplayCar();
    }
    public void GetPreviousEngine()
    {
        selectedEngineIdx--;
        if (selectedEngineIdx < 0)
            selectedEngineIdx = engineList.Count-1;
        selectedEngine = engineList[selectedEngineIdx];
        UpdateDisplayCar();
    }

    //Updates the display car on the menu with the current body, wheels, and engine by building a new car with those features
    public void UpdateDisplayCar()
    {
        Vehicle createdVehicle = builder.SetVPrefab(vehiclePrefab).SetWheels(selectedWheel).SetBody(selectedBody).SetEngine(selectedEngine).Build();
        Destroy(createdVehicle.GetComponent<Camera>());
        //make sure updated vehicle spawns in same position and rotation
        createdVehicle.gameObject.transform.rotation = displayVehicle.transform.rotation;
        createdVehicle.gameObject.transform.position = displayVehicle.transform.position;
        createdVehicle.gameObject.transform.localScale = displayVehicle.transform.localScale;


        //destroys the display vehicle
        Destroy(displayVehicle);

        displayVehicle = createdVehicle.gameObject;
        
        GameState.GetGameState()._playerVehicleRef = createdVehicle;
    }

    //saves selection from menu to gamestate
    public void SaveChangesToState()
    {
        GameState.GetGameState().selectedBody = selectedBody;
        GameState.GetGameState().selectedWheel = selectedWheel;
        GameState.GetGameState().selectedEngine = selectedEngine;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        builder = this.gameObject.AddComponent<VehicleBuilder>();
        //assigns first body, wheel, and engine to start
        if (GameState.GetGameState().selectedBody == null)
            selectedBody = bodyList[selectedBodyIdx];
        else
            selectedBody = GameState.GetGameState().selectedBody;
        
        if (GameState.GetGameState().selectedEngine == null)
            selectedEngine = engineList[selectedEngineIdx];
        else
            selectedEngine = GameState.GetGameState().selectedEngine;
        
        if (GameState.GetGameState().selectedWheel == null)
            selectedWheel = wheelList[selectedWheelIdx];
        else
            selectedWheel = GameState.GetGameState().selectedWheel;
        
        UpdateDisplayCar();
    }

    // Update is called once per frame
    void Update()
    {
        //rotates display car
        displayVehicle.transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
