using UnityEngine;

public class Eating : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            Destroy(gameObject, 2);
        }
    }
}
