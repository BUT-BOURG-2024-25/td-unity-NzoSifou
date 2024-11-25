using UnityEngine;

public class DestroyOnDamager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damager"))
        {
            Destroy(gameObject);
        }
    }
}
