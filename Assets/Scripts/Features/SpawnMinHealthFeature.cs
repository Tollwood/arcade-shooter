using UnityEngine;

public class SpawnMinHealthFeature : ISpawnFeature
{
    public int minHealth = 5;
    public int maxHealthBoosts = 3;
    public float timeBetweenChecks = 2;

    LivingEntity player;
    int currentHealthBoostCount = 0;
    private float nextCheckTime;
    private bool spawning = false;

    public override bool ShouldSpawn()
    {
        if(player == null ){
            player = FindObjectOfType<Player>();
        }
        if(player == null){
            return false;
        }

        if(Time.time > nextCheckTime && !spawning ){
            nextCheckTime = Time.time + timeBetweenChecks;
            spawning = player.health <= minHealth & currentHealthBoostCount <= maxHealthBoosts;
            return spawning;
        }
        return false;
    }

    public override void Spawned(GameObject spawnedGo)
    {
        currentHealthBoostCount++;
        spawning = false;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();

    }

}
