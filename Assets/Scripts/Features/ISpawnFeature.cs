using UnityEngine;

public abstract class ISpawnFeature: MonoBehaviour
{

    public float spawnDelay = 1;
    public float tileFlashSpeed = 4;
    public Color flashColour = Color.red;

    public abstract void Spawned(GameObject spawnedGo);
    public abstract bool ShouldSpawn();
}
