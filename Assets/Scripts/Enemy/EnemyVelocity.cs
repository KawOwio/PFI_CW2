// Rory Clark - https://rory.games - 2019
using UnityEngine;

public class EnemyVelocity : MonoBehaviour
{
    Rigidbody m_rigidbody;

    [SerializeField]
    float m_minRandomVelocity = 50, m_maxRandomVelocity = 100;
    float m_maxVelocity;
    [SerializeField]
    float m_minRandomTorque = -10, m_maxRandomTorque = 10;
    

    private void Awake()
    {
        // Set a random max velocity and add random torque
        // Makes the object move at a random speed and rotate randomly
        m_rigidbody = GetComponent<Rigidbody>();
        m_maxVelocity = Random.Range(m_minRandomVelocity, m_maxRandomVelocity);
        m_rigidbody.AddRelativeTorque(new Vector3(Random.Range(m_minRandomTorque, m_maxRandomTorque), Random.Range(m_minRandomTorque, m_maxRandomTorque), Random.Range(m_minRandomTorque, m_maxRandomTorque)));
    }

    void Update()
    {
        if (m_rigidbody.velocity.magnitude > m_maxVelocity)
        {
            m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, m_maxVelocity);
        }
        // Hard coded the position however same principle as in bullet velocity
        if (transform.position.x <= -20)
        {
            Destroy(gameObject);
        }

    }


}
