using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  public int playersHight;
  public bool didLoose;

  void Awake()
  {
    instance = this;
    didLoose = false;
  }

  public void WinRoutine()
  {
    Debug.Log("Win");
  }

  public void LooseRoutine()
  {
    Debug.Log("Loose");
    didLoose = true;
  }
}
