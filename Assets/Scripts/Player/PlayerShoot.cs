// Rory Clark - https://rory.games - 2019
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField]
    GameObject m_bulletPrefab = null;

    [SerializeField]
    Transform m_spawnPosition = null;

    [SerializeField]
    KeyCode m_fireKey = KeyCode.Space;

    [SerializeField]
    float m_fireRate = 0.25f;
    float m_currentFireTimer = 0f;

    void Update()
    {
        if (m_currentFireTimer > 0)
        {
            m_currentFireTimer -= Time.deltaTime;
        }

        // GetKeyDown and GetKey will happen at different times
        // GetKeyDown will happen as soon as the user presses the key
        // GetKey will work when it is held
        if ((Input.GetKeyDown(m_fireKey) || Input.GetKey(m_fireKey)) && m_currentFireTimer <= 0)
        {
            m_currentFireTimer = m_fireRate;
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        GameObject go = Instantiate(m_bulletPrefab);
        go.transform.position = m_spawnPosition.position;
    }
   
}
