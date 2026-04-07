using Unity.VisualScripting;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 60f;
    public float detectCD = 2f;
    public float rayDist = 2f;

    public LayerMask layerMask;

    private Rigidbody rb;
    private float cdTime;
    private bool _isObstacleDetect = false;
    private float _targetRotationY = 0f;
    private bool _isRotating = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cdTime = detectCD;
    }

    private void Update()
    {
        if (!_isObstacleDetect && !_isRotating)
        {
            Move();

            cdTime += Time.deltaTime;

            if (cdTime >= detectCD)
            {
                DetectionObstacle();
            }
        }
        else if (_isRotating)
        {
            PerformSmoothRotation();
        }
    }

    private void Move()
    {
        Vector3 newPosition = rb.position + transform.forward * speed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    private void ChangeRotation()
    {
        float randomAngle = Random.Range(-90f, 90f);
        _targetRotationY = transform.eulerAngles.y + randomAngle;

        _isRotating = true;
        _isObstacleDetect = true;
    }

    private void PerformSmoothRotation()
    {
        float step = rotationSpeed * Time.deltaTime;
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, _targetRotationY, 0);

        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, step);

        if (Quaternion.Angle(currentRotation, targetRotation) < 0.1f)
        {
            _isRotating = false;
            _isObstacleDetect = false;
        }
    }

    private void DetectionObstacle()
    {
        RaycastHit hitRight;
        Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

        foreach (var direction in directions) 
        {
            Debug.DrawRay(transform.position, direction * rayDist, Color.green);
            if (Physics.Raycast(transform.position, direction, out hitRight, rayDist, layerMask))
            {
                if (hitRight.collider.CompareTag("Obstacle"))
                {
                    _isObstacleDetect = true;
                    ChangeRotation();
                    cdTime = 0f;
                    return;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Garbage")) 
        {
            Destroy(other.gameObject);
        }
    }
}
