using UnityEngine;

public abstract class Boost : MonoBehaviour {

    public float boostValue;

	void Start () {
      Game gm = FindObjectOfType<Game>();
      gm.OnGameOver += OnGameOver;
	}
	
    private void OnGameOver(){
        if(gameObject != null){
            Destroy(gameObject);    
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        boost(otherCollider);
    }

    protected abstract void boost(Collider otherCollider);
}
