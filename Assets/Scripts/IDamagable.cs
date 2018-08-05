using UnityEngine;

public interface IDamagable {
   void TakeHit(int damage, RaycastHit hit);

    void TakeDamage(int damage);
}
