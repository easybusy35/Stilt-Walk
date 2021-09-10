using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
  [SerializeField] GameObject player;
  RigidbodyConstraints originalRigidbodyConstraints;
  bool leftplayer;

  public Animator anim;

  private void Awake()
  {
    leftplayer = false;
    originalRigidbodyConstraints = player.GetComponent<Rigidbody>().constraints;
  }
  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player" && leftplayer == false)
    {

      player.GetComponent<PlayerController>().DisableConstantForce();
      transform.SetParent(player.transform);
      player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;//| RigidbodyConstraints.FreezePositionZ;
      anim.SetBool("isRunning", false);
      anim.SetBool("isWalking", true);

      Debug.Log("PLAYER");
    }

    else if (other.tag == "Floor")
    {
      leftplayer = true;
      transform.SetParent(other.transform);
      player.GetComponent<PlayerController>().EnableConstantforce();
      player.GetComponent<Rigidbody>().constraints = originalRigidbodyConstraints;
      anim.SetBool("isRunning", true);
      anim.SetBool("isWalking", false);

      player.transform.localPosition = new Vector3(player.transform.localPosition.x, PlayerController.instance.playerYPos + 1f, player.transform.localPosition.z + 1.5f);

      Debug.Log("FLOOR");
    }
  }
}
