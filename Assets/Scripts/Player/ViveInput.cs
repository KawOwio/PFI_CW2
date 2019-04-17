using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour
{
    public SteamVR_Controller.Device m_device;
    public Pistol m_pistol;

    private SteamVR_TrackedObject m_trackedObject = null;

    private void Awake()
    {
        m_trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update ()
    {
        //Trigger Value
        m_device = SteamVR_Controller.Input((int)m_trackedObject.index);

        //Trigger down
        if(m_device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            m_pistol.Fire();
        }

        //Trigger up
        if (m_device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            m_pistol.FireRelease();
        }

        //Grip button
        if (m_device.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            StartCoroutine(m_pistol.Reload());
        }

        //Set Trigger
        Vector2 triggerValue = m_device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);
        m_pistol.SetTriggerRotation(triggerValue.x);
	}
}
