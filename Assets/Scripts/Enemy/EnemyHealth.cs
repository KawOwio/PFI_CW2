// Rory Clark - https://rory.games - 2019
using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float m_health = 20f;

    public void DamageEnemy(Pistol pistol)
    {
        m_health -= pistol.m_damage;

        StartCoroutine(FlashDamage());

        if(m_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashDamage()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(0.25f);

        //GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
