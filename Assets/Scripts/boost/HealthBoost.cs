using UnityEngine;

public class HealthBoost : Boost
{
    protected override void boost(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {
            otherCollider.gameObject.GetComponent<LivingEntity>().healtBoost((int)boostValue);
            Destroy(gameObject);
        }
    }
}
