using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [Header("Movement")]
  [SerializeField] float clampDelta;
  [SerializeField] AudioClip CollectingDiamonds;
  [SerializeField] AudioClip CollectingStilts;

  public static PlayerController instance;

  AudioSource audioSource;
  public Animator anim;
  public Rigidbody rb;
  // private PlayerController script;
  private PhysicsCorrector physicsCorrector;
  private Vector3 lastMousePosition;

  public float speed;
  public float sensitivity;

  public GameObject UIHand;
  public GameObject UISwipe;

  public Transform Wall1;
  public Transform Wall2;
  bool moveWinWalls = false;

  float originalYpos;

  public bool canMove;
  public bool canTrigger;

  private void Awake()
  {
    instance = this;
    rb = GetComponent<Rigidbody>();
    //script = GetComponent<PlayerController>();
    physicsCorrector = GetComponent<PhysicsCorrector>();
    audioSource = GetComponent<AudioSource>();
  }

  private void Update()
  {
    TouchToMove();
    originalYpos = gameObject.transform.localPosition.y;

    if (moveWinWalls == true)
    {
      float step = 3f * Time.deltaTime;
      Wall1.transform.position = Vector3.MoveTowards(Wall1.transform.position, new Vector3(-125, -19.5f, 205.1f), step);
      Wall2.transform.position = Vector3.MoveTowards(Wall2.transform.position, new Vector3(125, -19.5f, 205.1f), step);
    }

  }

  private void FixedUpdate()
  {
    Movement();
    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.7f, 4.8f), transform.position.y, transform.position.z);
  }

  private void TouchToMove()
  {
    if (!canMove)
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
    if (other.tag == "Stilts" && canTrigger == true)
    {
      audioSource.PlayOneShot(CollectingStilts);
      StackController.instance.AddStilts();
      Destroy(other.gameObject);

      canTrigger = false;
    }

    if ((other.tag == "FireRing" || other.tag == "LastFireRing") && GameManager.instance.playersHight < 3 && canTrigger == true)
    {
      GameManager.instance.LooseRoutine();
      canTrigger = false;
    }

    if (other.tag == "Flame")
    {
      GameManager.instance.LooseRoutine();
    }

    if (other.tag == "Move")
    {
      moveWinWalls = true;
    }

    if (other.tag == "Finish")
    {
      physicsCorrector.enabled = false;
      // script.enabled = false;
      canMove = false;
      anim.SetBool("isRunning", false);
      anim.SetBool("isDancing", true);
    }

    if (other.tag == "Diamond")
    {
      ScoreText.diamondAmount += 5;
      Destroy(other.gameObject);
      audioSource.PlayOneShot(CollectingDiamonds);
    }
  }

  void OnTriggerStay(Collider other)
  {
    canTrigger = true;

    if (other.tag == "Roll")
    {
      gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, originalYpos, gameObject.transform.localPosition.z);
    }
  }

  private void OnTriggerExit(Collider other)
  {

    canTrigger = true;

    if (other.tag == "FireRing" || other.tag == "LastFireRing")
    {
      for (int i = 0; i < StackController.instance.currentNumberOfStilts; i++)
      {
        StackController.instance.RemoveOneStilt();
      }

      if (other.tag == "LastFireRing" && GameManager.instance.didLoose == false)
      {
        GameManager.instance.WinRoutine();
      }

    }

  }
}