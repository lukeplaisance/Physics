using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBehaviour : MonoBehaviour
{
    public string colliderTag;

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(colliderTag))
        {
            Debug.Log("Boids hit the boundary");
        }
    }
}
