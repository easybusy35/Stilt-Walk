using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
  [SerializeField] GameObject player;
  RigidbodyConstraints originalRigidbodyConstraints;

  private void Awake()
  {
    originalRigidbodyConstraints = player.GetComponent<Rigidbody>().constraints;
  }
  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      transform.SetParent(player.transform);
      player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;

    }

    if (other.tag == "Floor")
    {
      transform.SetParent(other.transform);
      player.GetComponent<Rigidbody>().constraints = originalRigidbodyConstraints;
    }
  }
}
