using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pistol : MonoBehaviour
{
    //Positions of the trigger and the barrel
    [SerializeField] private Transform m_barrel;
    [SerializeField] private Transform m_trigger;

    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource reload;
    [SerializeField] private AudioSource empty;

    private Animator m_animator;

    public float m_sensetivity;
    public float m_damage;

    private int m_currentAmmo = 0;
    private int m_magazineSize = 8;
    private int m_enemiesKilled = 0;

    private bool m_isReloading = false;

    public delegate void FireAction(int ammoCount);
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
            if (OnFire != null)
            {
                OnFire(m_currentAmmo);
            }

            shoot.Play();
            m_currentAmmo--;
            if (m_currentAmmo <= 0)
            {
                m_animator.SetBool("magazineEmpty", true);
            }
            Debug.Log("Ammo: " + m_currentAmmo);

            RaycastHit hit;
            Ray ray = new Ray(m_barrel.position, m_barrel.forward);

            m_animator.SetBool("hasFired", true);

            if (Physics.Raycast(ray, out hit))
            {
                //Check for damage
                CheckForDamage(hit.collider.gameObject);
            }
        }
        else if (m_currentAmmo <= 0 && m_isReloading == false)
        {
            empty.Play();
        }
    }

    public void FireRelease()
    {
        m_animator.SetBool("hasFired", false);
    }

    //Reload with a one second "cast"
    public IEnumerator Reload()
    {
        if(m_isReloading == false)
        {
            reload.Play();
            m_isReloading = true;
            yield return new WaitForSeconds(1.25f);
            m_currentAmmo = m_magazineSize;
            m_isReloading = false;
            m_animator.SetBool("magazineEmpty", false);
        }
    }

    //Check if you dealt damage to an enemy
    private void CheckForDamage(GameObject hitObject)
    {
        if(hitObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = hitObject.GetComponent<EnemyHealth>();
            m_enemiesKilled += enemy.DamageEnemy(this);
        }
        else if(hitObject.CompareTag("PlayButton"))
        {
            Debug.Log("Play");
            Invoke("Play", 1.0f);  
        }
        else if (hitObject.CompareTag("MainMenu"))
        {
            Invoke("MainMenu", 1.0f);
        }
        else if(hitObject.CompareTag("QuitButton"))
        {
            Invoke("Quit", 1.0f);
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

    public int GetScore()
    {
        return m_enemiesKilled;
    }

    private void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Quit()
    {
        Application.Quit();
    }
}