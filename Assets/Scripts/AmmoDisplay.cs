using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    public GameObject m_bulletPrefab;
    private List<GameObject> m_clip = new List<GameObject>();
    private bool[] bulletCreated;
    public Pistol pistol;

    private void Awake()
    {
        //pistol = GetComponentInParent<Pistol>();
        Pistol.OnFire += UpdateDisplay;

        int magazineSize = pistol.GetMagazineSize();
        m_clip = CreateAmmo(magazineSize);
    }

    private void OnDestroy()
    {
        Pistol.OnFire -= UpdateDisplay;
    }

    //private void Update()
    //{
    //    if (pistol.GetAmmo() < 8 && pistol.GetReloadStatus() == true)
    //    {
    //        int magazineSize = pistol.GetMagazineSize();
    //        m_clip = CreateAmmo(magazineSize);
    //    }
    //}

    public List<GameObject> CreateAmmo(int magazineSize)
    {
        Debug.Log("MS: " + magazineSize);
        Debug.Log("CreatingAmmo");
        List<GameObject> allAmmo = new List<GameObject>();

        float xStart = magazineSize / 2;
        xStart *= -1;

        int a = pistol.GetAmmo();
        for(int i = a; i < magazineSize; i++)
        {
            //Create
            GameObject newAmmo = Instantiate(m_bulletPrefab, transform);

            //Position
            float xFinal = (xStart + i) * 0.04f;
            newAmmo.transform.localPosition = new Vector3(xFinal, 0, 0);

            //Save it
            //bulletCreated[i] = true;
            allAmmo.Add(newAmmo);
        }

        return allAmmo;
    }

    private void UpdateDisplay()
    {
        int t = pistol.GetAmmo();
        int i = m_clip.Count - t;
        Destroy(m_clip[i], 2.0f);

        //collision
        //m_clip[i].GetComponent<Collider>().enabled = true;

        //physics
        Rigidbody rigidbody = m_clip[i].GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;

        rigidbody.AddForce(new Vector3(Random.Range(-50, 50), 150.0f, Random.Range(-50, 50)));
        rigidbody.AddTorque(transform.up * 50);
        rigidbody.AddTorque(transform.right * 50);
    }
}
