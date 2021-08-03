using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{

  public static StackController instance;
  [SerializeField] List<GameObject> leftStilts = new List<GameObject>();
  [SerializeField] List<GameObject> rightStilts = new List<GameObject>();
  [SerializeField] int currentNumberOfStilts = 0;

  void Awake()
  {

    instance = this;
    for (int i = 0; i < leftStilts.Count; i++)
    {
      leftStilts[i].SetActive(false);
      rightStilts[i].SetActive(false);
    }
  }

  public void AddStilts()
  {
    for (int i = 0; i < leftStilts.Count; i++)
    {
      if (i == currentNumberOfStilts && i <= leftStilts.Count)
      {
        Debug.Log("Up Arrow key was pressed.");
        leftStilts[i].SetActive(true);
        rightStilts[i].SetActive(true);
        currentNumberOfStilts++;
        return;
      }
    }
  }

  public void RemoveStilts()
  {
    for (int i = 0; i < leftStilts.Count; i++)
    {
      if (i == currentNumberOfStilts - 1 && i <= leftStilts.Count)
      {
        Debug.Log("Down Arrow key was released.");
        leftStilts[i].SetActive(false);
        rightStilts[i].SetActive(false);
        currentNumberOfStilts--;
        return;
      }
    }
  }

  void Update()
  {

    if (Input.GetKeyDown(KeyCode.UpArrow))
    {


      AddStilts();

    }

    if (Input.GetKeyUp(KeyCode.DownArrow))
    {

      RemoveStilts();


    }

  }
}
