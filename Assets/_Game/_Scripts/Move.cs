using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
         if (other.tag == "Move")
        {
            other.transform.position = new Vector3(140, 0, 0);
        }
    }
}
