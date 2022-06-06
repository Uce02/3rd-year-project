using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// store gameState. FreeRoam will be shown first as it is first in the enum
public enum GameState { FreeRoam, Battle }

public class GameController : MonoBehaviour
{
  [SerializeField] PlayerController playerController;
  [SerializeField] BattleSystem battleSystem;
  [SerializeField] Camera worldCamera;

  GameState state;

  private void Start()
  {
    // subscribe to the event and call function StartBattle when this event is fired
    // (when animal has been encountered using the code in PlayerController, run StartBattle)
    playerController.OnEncounter += StartBattle;
    // subscribe to the event and call function EndBattle when this event is fired
    // (when animal loses or wins found out by the code in BattleSystem, run EndBattle)
    battleSystem.OnBattleOver += EndBattle;

  }

  void StartBattle()
  {
    state = GameState.Battle;
    // enable battleSystem as it's disabled when the game starts. diable main Camera
    // as you can only have one camera active at a time
    battleSystem.gameObject.SetActive(true);
    worldCamera.gameObject.SetActive(false);

    battleSystem.StartBattle();
  }

  void EndBattle(bool won)
  {
    state = GameState.FreeRoam;
    // disable battleSystem to start the main camera again to continue the game
    battleSystem.gameObject.SetActive(false);
    worldCamera.gameObject.SetActive(true);
  }

  private void Update()
  {
    if (state == GameState.FreeRoam)
    {
      playerController.HandleUpdate();
    }
    else if (state == GameState.Battle)
    {
      battleSystem.HandleUpdate();
    }
  }
}
