using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private void Start() {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        var playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y + 2, playerPosition.z - 5);
    }
}
