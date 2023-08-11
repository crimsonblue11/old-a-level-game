using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private float expolosionForce = 700f;
    [SerializeField] private float blastRadius = 5f;

    private GameObject ExplosionEffect;
    private float countdown;
    private bool hasExploded = false;


    void Start()
    {
        ExplosionEffect = (GameObject)Resources.Load("prefabs/Explosion", typeof(GameObject));

        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }


    void Explode()
    {
        GameObject ExplosionInst = Instantiate(ExplosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(expolosionForce, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
        Destroy(ExplosionInst, 3f);

    }
}
