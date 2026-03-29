using UnityEngine;

public class PlayerMover2 : MonoBehaviour
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
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 move = (_player.transform.up.normalized * speed) * Time.deltaTime;
            _player.transform.position += move;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 move = -(_player.transform.up.normalized * speed) * Time.deltaTime;
            _player.transform.position += move;
        }
    }
}
