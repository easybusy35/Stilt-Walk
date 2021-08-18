using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
  [SerializeField] GameObject player;
  RigidbodyConstraints originalRigidbodyConstraints;

    public Animator anim;

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
          anim.SetBool("isRunning", false);
          anim.SetBool("isWalking", true);

      }

      if (other.tag == "Floor")
      {
          transform.SetParent(other.transform);
          player.GetComponent<Rigidbody>().constraints = originalRigidbodyConstraints;
          anim.SetBool("isRunning", true);
          anim.SetBool("isWalking", false);
      }
  }
}
