using UnityEngine;

public class SpawnBoost : Boost
{
    protected override void boost(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {

            GameObject enemySpawner = GameObject.FindWithTag("EnemySpawner");
            if(enemySpawner != null){
                enemySpawner.GetComponent<SpawnTimeFeature>().reduceSpawnSpeed(boostValue);
            }
            Destroy(gameObject);
        }
    }
}
