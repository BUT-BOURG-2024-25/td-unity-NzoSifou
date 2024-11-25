using UnityEngine;

public class DestroyAllCubeOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (var cube in GameObject.FindGameObjectsWithTag("Cube"))
            {
                Destroy(cube);
            }
        }
    }
}
