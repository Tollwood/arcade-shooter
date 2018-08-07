using UnityEngine;

public class SpawnBoost : Boost
{
    protected override void boost(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {
            FindObjectOfType<Spawner>().reduceSpawnSpeed(boostValue);
            Destroy(gameObject);
        }
    }
}
