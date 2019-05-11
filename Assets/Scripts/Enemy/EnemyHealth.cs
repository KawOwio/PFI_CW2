// Rory Clark - https://rory.games - 2019
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float m_health = 10.0f;

    public int DamageEnemy(Pistol pistol)
    {
        m_health -= pistol.m_damage;

        //If enemy's health is less than 0 then destroy it
        if (m_health <= 0)
        {
            Destroy(gameObject);
            return 1;   //returns +1 to score
        }
        return 0;
    }
}
