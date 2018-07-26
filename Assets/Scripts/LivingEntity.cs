using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable {

    public float startingHealth;
    protected float health;
    protected bool dead;

    public void TakeHit(float damage, RaycastHit hit)
    {
        health = health - damage;
        if (health <= 0 && !dead) {
            Die();
        }
    }

    protected virtual void Start () {
        health = startingHealth;
	}
	
	void Update () {
		
	}

    protected void Die(){
        Destroy(gameObject);
        dead = true;
    }
}
