// Rory Clark - https://rory.games - 2019
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float m_health = 10.0f;

    public int DamageEnemy(Pistol pistol)
    {
        m_health -= pistol.m_damage;

        if (m_health <= 0)
        {
            Destroy(gameObject);
            return 1;
        }
        return 0;
    }
}
