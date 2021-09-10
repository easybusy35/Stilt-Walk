using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{

  public static StackController instance;
  [SerializeField] List<GameObject> leftStilts = new List<GameObject>();
  [SerializeField] List<GameObject> rightStilts = new List<GameObject>();
  public int currentNumberOfStilts = 0;
  public float stiltHight;

  void Awake()
  {
    instance = this;
    for (int i = 0; i < leftStilts.Count; i++)
    {
      leftStilts[i].SetActive(false);
      rightStilts[i].SetActive(false);
    }

    stiltHight = leftStilts[0].transform.localScale.y * 2;
  }

  public void AddStilts()
  {
    for (int i = 0; i < leftStilts.Count; i++)
    {
      if (i == currentNumberOfStilts && i <= leftStilts.Count && i <= rightStilts.Count)
      {
        leftStilts[i].SetActive(true);
        rightStilts[i].SetActive(true);
        currentNumberOfStilts += 1;
        GameManager.instance.playersHight = currentNumberOfStilts;
        PlayerController.instance.playerYPos += stiltHight;
        //   Debug.Log(currentNumberOfStilts);
        return;
      }
    }


  }

  public void RemoveOneStilt()
  {
    for (int i = 0; i < leftStilts.Count; i++)
    {
      if (i == currentNumberOfStilts - 1 && i <= leftStilts.Count && i <= rightStilts.Count)
      {
        leftStilts[i].SetActive(false);
        rightStilts[i].SetActive(false);
        currentNumberOfStilts -= 1;
        GameManager.instance.playersHight = currentNumberOfStilts;
        PlayerController.instance.playerYPos -= stiltHight;
        // Debug.Log(currentNumberOfStilts);
        return;
      }
    }

  }

  void Update()
  {

    if (Input.GetKeyDown(KeyCode.UpArrow))
    {


      //AddStilts();

    }

    if (Input.GetKeyUp(KeyCode.DownArrow))
    {

      //RemoveOneStilt();


    }

  }
}
