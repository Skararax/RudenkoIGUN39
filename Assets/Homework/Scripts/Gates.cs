using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    private int _ballCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) 
        { 
            Destroy(other.gameObject);
            _ballCounter++;
            Debug.Log($"{_ballCounter}");
        }
    }
}
