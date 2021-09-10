using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
  [Header("Movement")]
  [SerializeField] float clampDelta;

  [SerializeField] AudioClip CollectingDiamonds;
  [SerializeField] AudioClip CollectingStilts;
  [SerializeField] AudioClip Losing;

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

  public Transform rope;
  //bool moveRope = false;
  bool moveJump = false;

  public float playerYPos;

  public bool canMove;

  private bool canTrigger = true;
  private bool didExitFire = false;

  private void Awake()
  {
    instance = this;
    rb = GetComponent<Rigidbody>();

    physicsCorrector = GetComponent<PhysicsCorrector>();
    audioSource = GetComponent<AudioSource>();

    GameManager.instance.m_GameState = GameState.Game;
  }


  //   public Vector3 jumpPosition = new Vector3(-0.2499999f, 5f, 127.536f);
  public Vector3 ropeFinalPosition = new Vector3(-0.56f, 2f, 196.1f);
  private void Update()
  {
    if (GameManager.instance.m_GameState == GameState.Game)
    {
      TouchToMove();
      playerYPos = gameObject.transform.localPosition.y;



      if (moveWinWalls == true)
      {
        float step = 3f * Time.deltaTime;
        Wall1.transform.position = Vector3.MoveTowards(Wall1.transform.position, new Vector3(-0.56f, 2.1f, 196.1f), step);
        Wall2.transform.position = Vector3.MoveTowards(Wall2.transform.position, new Vector3(125, -19.5f, 205.1f), step);
      }
    }

    if (GameManager.instance.m_GameState == GameState.Win)
    {
      if (Input.GetMouseButtonDown(0) && gameObject.transform.localPosition.y < 2f)
      {
        anim.SetTrigger("jumpToRope");
        // gameObject.transform.SetParent(rope);
        physicsCorrector.speed = physicsCorrector.speed * 4;
        physicsCorrector.enabled = true;
        DisableConstantForce();

        gameObject.transform.localPosition = new Vector3(-0.2499999f, 4f, 127.536f);// Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(-0.2499999f, 5f, 127.536f), 40f * Time.deltaTime);

        // rope.localPosition = Vector3.zero;

        rope.transform.localPosition = new Vector3(0, -0.65f, -5.25f);
        // rope.SetParent(gameObject.transform);
      }
    }


  }

  private void FixedUpdate()
  {
    if (GameManager.instance.m_GameState == GameState.Game)
    {
      Movement();
      transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.7f, 4.8f), transform.position.y, transform.position.z);
    }
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

    didExitFire = false;

    if (other.tag == "Stilts" && canTrigger == true)
    {
      audioSource.PlayOneShot(CollectingStilts);
      StackController.instance.AddStilts();
      Destroy(other.gameObject);

      canTrigger = false;
    }

    if ((other.tag == "FireRing" || other.tag == "LastFireRing") && GameManager.instance.playersHight < 3 && canTrigger == true)
    {
      audioSource.PlayOneShot(Losing);
      GameManager.instance.LooseRoutine();
      canTrigger = false;

      Debug.Log("Loosing - " + other.name + " Hight " + GameManager.instance.playersHight);
    }

    if (other.tag == "Flame" && GameManager.instance.playersHight < 3 && canTrigger == true)
    {
      audioSource.PlayOneShot(Losing);
      GameManager.instance.LooseRoutine();
      //   Debug.Log("Loosing - " + other.name);

    }

    if (other.tag == "Move")
    {
    }



    if (other.tag == "Diamond")
    {
      ScoreText.diamondAmount += 5;
      Destroy(other.gameObject);
      audioSource.PlayOneShot(CollectingDiamonds);
    }

    if (other.tag == "End")
    {

      physicsCorrector.speed = physicsCorrector.speed / 4;
      GetComponent<Rigidbody>().velocity = Vector3.zero;
      physicsCorrector.enabled = false;
      EnableConstantforce();
      rope.gameObject.SetActive(false);
      anim.SetTrigger("fallFromRope");
      //  Debug.Log("End");
    }


    if (other.tag == "Finish")
    {
      physicsCorrector.enabled = false;
      canMove = false;
      StartCoroutine(RopeRoutine());
    }

  }

  IEnumerator RopeRoutine()
  {
    rope.SetParent(gameObject.transform);
    float step = 10 * Time.deltaTime;

    while (rope.transform.position != ropeFinalPosition)
    {
      rope.transform.position = Vector3.MoveTowards(rope.transform.position, ropeFinalPosition, step);
      yield return new WaitForEndOfFrame();

    }

    GameManager.instance.m_GameState = GameState.Win;
  }



  void OnTriggerStay(Collider other)
  {
    canTrigger = true;
  }




  private void OnTriggerExit(Collider other)
  {

    // canTrigger = true;

    if ((other.tag == "FireRing" || other.tag == "LastFireRing") && didExitFire == false)
    {
      for (int i = 0; i < 3; i++)
      {
        StackController.instance.RemoveOneStilt();
      }

      if (other.tag == "LastFireRing" && GameManager.instance.didLoose == false)
      {
        // GameManager.instance.WinRoutine();
      }

      didExitFire = true;
      other.enabled = false;

    }

  }

  public void EnableConstantforce()
  {
    GetComponent<ConstantForce>().enabled = true;
  }

  public void DisableConstantForce()
  {
    GetComponent<ConstantForce>().enabled = false;
  }

}