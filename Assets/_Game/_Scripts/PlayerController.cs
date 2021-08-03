using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [Header("Movement")]
  [SerializeField] float clampDelta;

  private Animator anim;
  private Rigidbody rb;
  private PlayerController script;
  private PhysicsCorrector physicsCorrector;
  private Vector3 lastMousePosition;
  private bool canMove, finish;


  public float speed;
  public float sensitivity;


  public GameObject UIHand;
  public GameObject UISwipe;

  private void Awake()
  {
    rb = GetComponent<Rigidbody>();
    script = GetComponent<PlayerController>();
    anim = GetComponent<Animator>();
    physicsCorrector = GetComponent<PhysicsCorrector>();
  }

  private void Update()
  {
    TouchToMove();
  }

  private void FixedUpdate()
  {
    Movement();
    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.7f, 4.8f), transform.position.y, transform.position.z);
  }

  private void TouchToMove()
  {
    if (!canMove && !finish)
    {
      if (Input.GetMouseButtonDown(0))
      {
        canMove = true;
        anim.SetBool("isRunning", true);
      }
      else
      {
        anim.SetBool("isRunning", false);
      }
    }
  }

  private void Movement()
  {
    if (canMove)
    {
      if (Input.GetMouseButton(0))
      {
        Vector3 pos = lastMousePosition - Input.mousePosition;
        pos = new Vector3(pos.x, 0, 0);

        lastMousePosition = Input.mousePosition;

        Vector3 moveForce = Vector3.ClampMagnitude(pos, clampDelta);
        rb.AddForce(-moveForce * sensitivity * speed * Time.deltaTime, ForceMode.VelocityChange);
        UIHand.SetActive(false);
        UISwipe.SetActive(false);
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    /*if (other.gameObject.CompareTag("Trigger"))
    {
      canMove = false;
      script.enabled = !script.enabled;
      physicsCorrector.enabled = false;
      anim.SetBool("isRunning", false);

      Debug.Log(other.name);
    }    */

    if (other.tag == "Stilts")
    {
      StackController.instance.AddStilts();
      Destroy(other.gameObject);
    }
  }

  private void OnTriggerExit(Collider other)
  {

    /* if (other.tag == "Stilts")
     {
       StackController.instance.AddStilts();
       Destroy(other.gameObject);
     }       */
  }
}