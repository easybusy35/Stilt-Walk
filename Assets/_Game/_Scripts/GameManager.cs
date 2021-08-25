using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
  Home,
  Play,
  Win,
  Loose
}

public class GameManager : MonoBehaviour
{

  public static GameManager instance;
  public GameState m_GameState;
  public PhysicsCorrector physicsCorrector;

  public int playersHight;
  public bool didLoose;

  void Awake()
  {
    instance = this;
    didLoose = false;
    m_GameState = GameState.Play;
  }

  public void WinRoutine()
  {
    Debug.Log("Win");
    m_GameState = GameState.Win;
  }

  public void LooseRoutine()
  {
    Debug.Log("Loose");
    physicsCorrector.enabled = false;
    PlayerController.instance.anim.SetBool("isFalling", true);
    didLoose = true;
    m_GameState = GameState.Loose;

  }
}
