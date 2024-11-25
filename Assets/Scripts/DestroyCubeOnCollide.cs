using UnityEngine;

public class DestroyCubeOnCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Cube"))
        {
            Destroy(collision.gameObject);
        }
    }
}
