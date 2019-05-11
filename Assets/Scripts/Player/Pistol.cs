using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pistol : MonoBehaviour
{
    //Positions of the trigger and the barrel
    [SerializeField] private Transform m_barrel;
    [SerializeField] private Transform m_trigger;

    [SerializeField] private Score score;

    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource reload;
    [SerializeField] private AudioSource empty;

    private Animator m_animator;
    private ParticleSystem m_particles;

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
        m_particles = GetComponent<ParticleSystem>();
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
            m_particles.Play();
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

    //Reload with a one 1.25 second "cast" time
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

    //Check if you dealt damage to an enemy or hit any button
    private void CheckForDamage(GameObject hitObject)
    {
        if(hitObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = hitObject.GetComponent<EnemyHealth>();
            m_enemiesKilled += enemy.DamageEnemy(this);
            Debug.Log(m_enemiesKilled);
        }
        else if(hitObject.CompareTag("PlayButton"))
        {
            Debug.Log("Play");
            Invoke("Play", 0.1f);  
        }
        else if (hitObject.CompareTag("MainMenu"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                PlayerPrefs.SetInt("LastScore", m_enemiesKilled);
            }
                Invoke("MainMenu", 0.1f);
        }
        else if(hitObject.CompareTag("QuitButton"))
        {
            Invoke("Quit", 0.5f);
        }
        else if(hitObject.CompareTag("ResetButton"))
        {
            score.Reset();
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