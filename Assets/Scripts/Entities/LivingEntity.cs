using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable {

    public int startingHealth;
    public int health { get; protected set; }
    protected bool dead;

    public System.Action onDeath;

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

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }
}
