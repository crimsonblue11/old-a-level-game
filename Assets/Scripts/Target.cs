using UnityEngine;

public class Target : MonoBehaviour
{
    void Update()
    {
        if (gameObject.transform.position.y <= -1)
        {
            Destroy(gameObject);
        }
    }
}
