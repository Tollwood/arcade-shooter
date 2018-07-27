using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable {

    public float startingHealth;
    protected float health;
    protected bool dead;

    public System.Action onDeath;

    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }



    protected virtual void Start () {
        health = startingHealth;
	}
	
	void Update () {
		
	}

    protected void Die(){
        dead = true;
        if(onDeath != null){
            onDeath();
        }
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }
}
