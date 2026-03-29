using UnityEngine;

public class PlayerMover1 : MonoBehaviour
{
    [SerializeField] private Transform _player;

    public float speed = 2;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 move = (_player.transform.right.normalized * speed) * Time.deltaTime;
            _player.transform.position += move;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 move = -(_player.transform.right.normalized * speed) * Time.deltaTime;
            _player.transform.position += move;
        }
    }
}
