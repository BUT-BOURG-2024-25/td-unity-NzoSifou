using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private Rigidbody rb;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_player)
        {
            var playerDirection = _player.transform.position - transform.position;
            
            var newVelocity = playerDirection.normalized * speed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
            
            var lookAtPosition = _player.transform.position;
            lookAtPosition.y = transform.position.y;
            
            transform.LookAt(lookAtPosition, Vector3.up);
        }
    }
}
