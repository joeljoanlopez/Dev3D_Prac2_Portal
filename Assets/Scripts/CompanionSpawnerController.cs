using UnityEngine;

public class CompanionSpawnerController : MonoBehaviour
{
    public CompanionCube companionCube;

    public void SpawnCube()
    {
        companionCube.Restart();
    }
}