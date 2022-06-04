using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//store the state of battle

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
  // control the entire Battle
  [SerializeField] BattleUnit playerUnit;
  [SerializeField] BattleUnit enemyUnit;
  [SerializeField] BattleHUD playerHUD;
  [SerializeField] BattleHUD enemyHUD;
  [SerializeField] BattleDialogueBox dialogueBox;

  BattleState state;
  // variables to store current action and current move
  int currentAction;
  int currentMove;

  private void Start()
  {
    StartCoroutine(SetupBattle());
  }

  public IEnumerator SetupBattle()
  {
    //calls on playerUnit from BattleUnit to show the animal
    playerUnit.Setup();
    enemyUnit.Setup();
    //set the HUD data of the animal called in the previous line from BattleUnit
    playerHUD.SetData(playerUnit.Animal);
    enemyHUD.SetData(enemyUnit.Animal);
    // passing moves of current animal to the SetMoveNames function
    dialogueBox.SetMoveNames(playerUnit.Animal.Moves);

    yield return dialogueBox.TypeDialogue($"A wild {enemyUnit.Animal.Base.Name} appeared!");
    yield return new WaitForSeconds(1f);

    PlayerAction();
  }

  void PlayerAction()
  {
    state = BattleState.PlayerAction;
    StartCoroutine(dialogueBox.TypeDialogue("Choose an action"));
    dialogueBox.EnableActionSelector(true);
  }

  void PlayerMove()
  {
    state = BattleState.PlayerMove;
    dialogueBox.EnableActionSelector(false);
    dialogueBox.EnableDialougeText(false);
    dialogueBox.EnableMoveSelector(true);
    // only show move selector, disable any other text from showing up in the dialogue box
  }

  private void Update()
  {
    if (state == BattleState.PlayerAction)
    {
      HandleActionSelection();
    }
    else if (state == BattleState.PlayerMove)
    {
      HandleMoveSelection();
    }
  }

  void HandleActionSelection()
  {
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
      // if on 0 (fight) and you press down, you're now on 1 and vice versa
      if (currentAction < 1)
          ++currentAction;
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      if (currentAction > 0)
          --currentAction;
    }

    dialogueBox.UpdateActionSelection(currentAction);

    if (Input.GetKeyDown(KeyCode.Z))
    {
      if (currentAction == 0)
      {
        // Fight. if clicked the player will be asked to select a move
        PlayerMove();
      }
      else if (currentAction == 1)
      {
        // Run
      }
    }
  }

  void HandleMoveSelection()
  {
    // update current move based on user input, like above HandleActionSelection function
    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      // one to the right is move 1
      if (currentMove < playerUnit.Animal.Moves.Count - 1)
          ++currentMove;
    }
    else if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      // first move is move 0
      if (currentMove > 0)
          --currentMove;
    }
    else if (Input.GetKeyDown(KeyCode.DownArrow))
    {
      // first down and left is move 3
      if (currentMove < playerUnit.Animal.Moves.Count -2)
          currentMove += 2;
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      // down and to the right is move 4
      if (currentMove > 1)
          currentMove -= 2;
    }

    dialogueBox.UpdateMoveSelection(currentMove, playerUnit.Animal.Moves[currentMove]);
  }
}
