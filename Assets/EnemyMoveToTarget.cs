using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToTarget : MonoBehaviour {

    private float m_minMovementSpeed = 10.0f, m_maxMovementSpeed = 50.0f;
    private float m_minRotationSpeed = -50.0f, m_maxRotationSpeed = 50.0f;

    float speed;
    Vector3 rotation;

    private void Awake()
    {
        rotation = new Vector3(Random.Range(m_minRotationSpeed, m_maxRotationSpeed),
            Random.Range(m_minRotationSpeed, m_maxRotationSpeed),
            Random.Range(m_minRotationSpeed, m_maxRotationSpeed));

        
    }

    //Moves enemies towards the player
    void Update ()
    {
        Vector3 pos = new Vector3(-2.0f, 1.0f, 0.0f);
        float speed = Random.Range(m_minMovementSpeed, m_maxMovementSpeed) * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
            pos, speed);

        RandomRotation();

        if (gameObject.transform.position.x <= -1.5f)
        {
            Destroy(gameObject);
        }
    }

    
    private void RandomRotation()
    {
        //Keep it for aliens;
        //gameObject.transform.Rotate(new Vector3(Random.Range(m_minRotationSpeed, m_maxRotationSpeed),
        //    Random.Range(m_minRotationSpeed, m_maxRotationSpeed),
        //    Random.Range(m_minRotationSpeed, m_maxRotationSpeed)));

        gameObject.transform.Rotate(rotation * Time.deltaTime);
    }
}
