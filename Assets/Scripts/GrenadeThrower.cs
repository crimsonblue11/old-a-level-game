using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] private float throwForce = 40f;
    [SerializeField] private int GrenadeNumber = 5;

    private GameObject grenade;

    private Text grenadeCounter;

    private void Start()
    {
        grenade = (GameObject)Resources.Load("prefabs/Grenade", typeof(GameObject));

        grenadeCounter = GameObject.Find("GrenadeCounter").GetComponent<Text>();

        grenadeCounter.text = GrenadeNumber.ToString();

    }

    void Update()
    {
        if (Input.GetKeyDown("g") && GrenadeNumber > 0)
        {
            ThrowGrenade();
            GrenadeNumber--;
            grenadeCounter.text = GrenadeNumber.ToString();
        }
    }

    private void ThrowGrenade()
    {
        GameObject g = Instantiate(grenade, transform.position, transform.rotation);
        g.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
