// Rory Clark - https://rory.games - 2019
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField]
    float m_damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // Damage the enemy if we hit one, destroy ourselves
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.DamageEnemy(m_damage);
            Destroy(gameObject);
        }
    }
}
