// Rory Clark - https://rory.games - 2019
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float m_health = 20f;

    public void DamageEnemy(float _damage)
    {
        m_health -= _damage;
        if(m_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
