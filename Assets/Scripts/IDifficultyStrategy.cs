
public interface IDifficultyStrategy
{
    float AdjustMaxSpeed(float maxSpeed);
    
}

public class EasyDifficulty : IDifficultyStrategy
{
    public float AdjustMaxSpeed(float maxSpeed)
    {
        return maxSpeed * 0.8f;
    }
}

public class MediumDifficulty : IDifficultyStrategy
{
    public float AdjustMaxSpeed(float maxSpeed)
    {
        return maxSpeed;
    }
}

public class HardDifficulty : IDifficultyStrategy
{
    public float AdjustMaxSpeed(float maxSpeed)
    {
        return maxSpeed * 1.2f;
    }
}