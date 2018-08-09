using UnityEngine;

public class TimeBoost : Boost
{
    protected override void boost(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {
            FindObjectOfType<Game>().increaseRemainingTime(boostValue);
            Destroy(gameObject);
        }
    }
}
