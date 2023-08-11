using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour
{
    [SerializeField] private int gunState = 1;
    int currGun;
    private Text GunText;

    private GameObject canvas;

    void Start()
    {
        currGun = gunState;
        SelectWeapon();

        canvas = GameObject.Find("Canvas");
        GunText = canvas.transform.Find("GunText").GetComponent<Text>();
    }

    void Update()
    {
        currGun = gunState;

        if (canvas.GetComponent<PauseMenuScript>().IsPaused())
        {
            return;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (gunState >= transform.childCount - 1)
            {
                gunState = 0;
            }
            else
            {
                gunState++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (gunState <= 0)
            {
                gunState = transform.childCount - 1;
            }
            else
            {
                gunState--;

            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 3)
        {
            gunState = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 3)
        {
            gunState = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            gunState = 2;
        }

        if (currGun != gunState)
        {
            SelectWeapon();
        }

        switch (gunState)
        {
            case 0:
                GunText.text = "Pistol";
                break;
            case 1:
                GunText.text = "SMG";
                break;
            case 2:
                GunText.text = "Sniper";
                break;
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        Animator anim;
        foreach (Transform weapon in transform)
        {
            if (i == gunState)
            {
                weapon.gameObject.SetActive(true);
            }
            else if (i == currGun)
            {
                anim = weapon.gameObject.GetComponent<Animator>();
                anim.SetTrigger("awayTrigger");
                StartCoroutine(GunWait());
                weapon.gameObject.SetActive(false);
                weapon.localPosition = weapon.GetComponent<GunHandler>().GetDefaultPos();
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }


    }

    IEnumerator GunWait()
    {
        yield return new WaitForSeconds(3);
    }


}

