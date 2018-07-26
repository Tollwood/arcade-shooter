using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform muzzle;
    public GameObject projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    float nextShotTime;

    public void Shoot()
    {

        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            GameObject go = Instantiate(projectile, muzzle.position, muzzle.rotation) as GameObject;
            Projectile newProjectile = go.GetComponent<Projectile>();

            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
