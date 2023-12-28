using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionDetection : MonoBehaviour
{
    public static event Action OnObjectCollected;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            OnObjectCollected();
            Destroy(gameObject);


        }

    }
}

