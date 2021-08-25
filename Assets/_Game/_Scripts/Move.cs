using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

  float speed = 0.2f;
  Collider other;

  private void OnTriggerEnter(Collider _other)
  {
    if (other.tag == "Move")
    {
      other = _other;
      //other.transform.position = new Vector3(140, 0, 0);
    }
  }

  void Update()
  {
    if (other != null)
    {
      float step = speed * Time.deltaTime;
      other.transform.position = Vector3.MoveTowards(other.transform.position, new Vector3(140, 0, 0), step);
    }

  }

}
