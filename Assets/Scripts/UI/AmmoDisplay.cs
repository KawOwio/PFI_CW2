using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    private List<GameObject> m_clip = new List<GameObject>();   //List of bullets in the clip for the HUD
    [SerializeField] private Pistol pistol;
    [SerializeField] private GameObject m_bulletPrefab;

    private bool firstRun = true;   //Used to delete the rest of bullets in the clip when reloading

    private void Awake()
    {
        Pistol.OnFire += UpdateDisplay;
        int magazineSize = pistol.GetMagazineSize();
        m_clip = CreateAmmo(magazineSize);
        firstRun = false;
    }

    private void OnDestroy()
    {
        Pistol.OnFire -= UpdateDisplay;
    }

    private void Update()
    {
        //Check for grip button press
        if (pistol.GetAmmo() < 8 && pistol.GetReloadStatus() == true)
        {
            int magazineSize = pistol.GetMagazineSize();
            m_clip = CreateAmmo(magazineSize);
        }
    }

    public List<GameObject> CreateAmmo(int magazineSize)
    {
        //Debug.Log("MS: " + magazineSize);
        //Debug.Log("CreatingAmmo");
        List<GameObject> allAmmo = new List<GameObject>();

        float xStart = magazineSize / 2;
        xStart *= -1;

        for(int i = 0; i < magazineSize; i++)
        {
            //Delete
            if(firstRun == false)
            {
                Destroy(m_clip[i]);
            }

            //Create
            GameObject newAmmo = Instantiate(m_bulletPrefab, transform);

            //Position
            float xFinal = (xStart + i) * 0.04f;
            newAmmo.transform.localPosition = new Vector3(xFinal, 0, 0);

            //Save it
            allAmmo.Add(newAmmo);
        }
        return allAmmo;
    }

    private void UpdateDisplay(int ammoCount)
    {
        int i = m_clip.Count - ammoCount;

        //physics
        Rigidbody rigidbody = m_clip[i].GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;

        rigidbody.AddForce(new Vector3(Random.Range(-50, 50), 150.0f, Random.Range(-50, 50)));
        rigidbody.AddTorque(transform.up * 50);
        rigidbody.AddTorque(transform.right * 50);
    }
}
