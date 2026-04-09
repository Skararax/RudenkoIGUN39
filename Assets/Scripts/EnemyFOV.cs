using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float viewRadius = 10f;

    [Range(0, 360)]
    public float viveAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform == null)
        {
            Debug.Log("player transform == null!");
        }
    }

    void Update()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        float disToTarget = Vector3.Distance(transform.position, playerTransform.position);
        if (disToTarget <= viewRadius)
        {
            Vector3 dirToTarget = (playerTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viveAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                {
                    Debug.Log("player detect!");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viveAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viveAngle / 2, false);

        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsglobal) 
    {
        if (!angleIsglobal) 
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
