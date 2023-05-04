using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public RaceController raceController;
    public int checkpointIndex;
    //triggers the checkpointReached function on the raceController
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == raceController.vehicleBody)
        {
            raceController.CheckpointReached(checkpointIndex);
        }
    }
}