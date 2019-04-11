// Rory Clark - https://rory.games - 2019
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float m_movementRate = 3f;

    float m_currentHorizontalAxisValue = 0;

    Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // We get the movement value based on Unity's input system, which turns our A + D and Left + Right keys into a float value.
        // Using AxisRaw will return -1, 0 or 1, while Axis will return a smoothly changing value between -1 and 1
        m_currentHorizontalAxisValue = -Input.GetAxisRaw("Horizontal");
        
        if (m_currentHorizontalAxisValue != 0)
        {
            // If you remove Time.deltaTime it will be tied to your frame rate = VERY BAD
            // Try removing it and then compare setting your quality settings Vsync to Half and off, you'll see a big difference
            m_rigidbody.AddForce(new Vector3(0, 0, (m_currentHorizontalAxisValue * m_movementRate) * Time.deltaTime));
        }        
    }
}
