using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Transform m_barrel;
    public Transform m_trigger;

    private Animator m_animator;

    public float m_sensetivity;
    public float m_damage;

    private int m_currentAmmo = 0;
    private int m_magazineSize = 8;

    private bool m_isReloading = false;
    private bool m_magazineEmpty = false;

    public delegate void FireAction();
    public static event FireAction OnFire;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_currentAmmo = m_magazineSize;
    }

    //Animation of the trigger on press
    public void SetTriggerRotation(float triggerValue)
    {
        float targetX = triggerValue * m_sensetivity;
        m_trigger.transform.localEulerAngles = new Vector3(targetX, 0, 0);
    }

    public void Fire()
    {
        //Can shoot if it has at least 1 bullet, not currently reloading and not firing
        bool fired = m_animator.GetBool("hasFired");
        if (m_currentAmmo > 0 && m_isReloading == false && !fired)
        {
            if(OnFire != null)
            {
                OnFire();
            }

            m_currentAmmo--;
            if(m_currentAmmo <= 0)
            {
                m_animator.SetBool("magazineEmpty", true);
            }
            Debug.Log("Ammo: " + m_currentAmmo);

            RaycastHit hit;
            Ray ray = new Ray(m_barrel.position, m_barrel.forward);
            Debug.DrawRay(m_barrel.position, m_barrel.forward, Color.red, 5.0f);

            m_animator.SetBool("hasFired", true);

            if (Physics.Raycast(ray, out hit))
            {
                //Check for damage
                CheckForDamage(hit.collider.gameObject);
            }
        }
    }

    public void FireRelease()
    {
        m_animator.SetBool("hasFired", false);
    }

    //Reload with a one second "cast"
    public IEnumerator Reload()
    {
        m_isReloading = true;
        yield return new WaitForSeconds(1.0f);
        m_currentAmmo = m_magazineSize;
        m_isReloading = false;
        m_animator.SetBool("magazineEmpty", false);
    }

    //Check if you dealt damage to an enemy
    private void CheckForDamage(GameObject hitObject)
    {
        if(hitObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = hitObject.GetComponent<EnemyHealth>();
            enemy.DamageEnemy(this);
        }
    }

    public int GetAmmo()
    {
        return m_currentAmmo;
    }

    public int GetMagazineSize()
    {
        return m_magazineSize;
    }

    public bool GetReloadStatus()
    {
        return m_isReloading;
    }
}