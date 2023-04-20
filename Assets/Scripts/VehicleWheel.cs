using UnityEngine;
[CreateAssetMenu(menuName = "VehicleComponents/VehicleWheel")]

public class VehicleWheel : ScriptableObject
{
    // Start is called before the first frame update
    
    public string wheelName;
    public GameObject wheelPrefab;
    public float brakeTorque;
    
}
