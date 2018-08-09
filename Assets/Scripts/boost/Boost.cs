using UnityEngine;

public abstract class Boost : MonoBehaviour {

    public float boostValue;

    private void OnTriggerEnter(Collider otherCollider)
    {
        boost(otherCollider);
    }

    protected abstract void boost(Collider otherCollider);
}
