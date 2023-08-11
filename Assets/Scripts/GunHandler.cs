using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private string type = "pistol";

    private float Range = 100f;
    private float ImpactForce = 300f;
    private float fireRate = 15f;
    private int maxAmmo = 10;
    private float reloadTime = 1f;
    private bool isAuto = false;

    private int currentAmmo;
    private bool isReloading = false;
    private Vector3 defaultPos;

    private GameObject impactEffect;
    private ParticleSystem flareEffect;

    private Text ammoText;
    private Text reloadingText;

    private float nextTimeToFire = 0f;

    void Start()
    {
        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
        reloadingText = GameObject.Find("ReloadText").GetComponent<Text>();

        impactEffect = (GameObject)Resources.Load("prefabs/flare", typeof(GameObject));
        flareEffect = transform.Find("Flare").GetComponent<ParticleSystem>();

        if (type == "pistol")
        {
            Range = 100f;
            ImpactForce = 300f;
            fireRate = 15f;
            maxAmmo = 10;
            reloadTime = 1f;
        } else if(type == "rifle")
        {
            Range = 50f;
            ImpactForce = 300f;
            fireRate = 15f;
            maxAmmo = 30;
            reloadTime = 1f;
            isAuto = true;
        } else if(type == "sniper")
        {
            Range = 200f;
            ImpactForce = 2000f;
            fireRate = 1f;
            maxAmmo = 6;
            reloadTime = 2f;
        }

        defaultPos = gameObject.transform.localPosition;
        currentAmmo = maxAmmo;
        reloadingText.enabled = false;
    }

    void Update()
    {
        bool isPaused;

        ammoText.text = (currentAmmo + "/" + maxAmmo);
        isPaused = GameObject.Find("Canvas").GetComponent<PauseMenuScript>().IsPaused();

        if (isReloading || isPaused)
        {
            return;
        }

        if ((Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo) || currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if (((isAuto && Input.GetButton("Fire1")) || Input.GetButtonDown("Fire1")) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadingText.enabled = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        reloadingText.enabled = false;
        isReloading = false;
    }

    void Shoot()
    {
        currentAmmo--;
        flareEffect.Play();

        GetComponent<AudioSource>().Play();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }

            GameObject ImpactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(ImpactGo, 0.5f);
        }
    }

    public Vector3 GetDefaultPos() { return defaultPos; }
}
