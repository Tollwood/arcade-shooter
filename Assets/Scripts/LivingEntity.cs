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
        Game gm = FindObjectOfType<Game>();
        gm.OnGameOver += Destroy;
	}


    protected virtual void Update()
    {
        if(transform.position.y < -10){
            Die();
        }
    }

    protected void Die(){
        dead = true;
        if(onDeath != null){
            onDeath();
        }
    }

    protected void Destroy()
    {
        if(this != null){
            Destroy(gameObject);    
        }
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
