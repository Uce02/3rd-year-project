using System;
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
  [SerializeField] PartyScreen partyScreen;

  // bool to let GameController know whether player won or lost battle
  public event Action<bool> OnBattleOver;

  BattleState state;
  // variables to store current action and current move
  int currentAction;
  int currentMove;

  AnimalParty playerParty;
  Animal wildAnimal;

  public void StartBattle(AnimalParty playerParty, Animal wildAnimal)
  {
    // pass player party and wild animal while calling the StartBattle function
    this.playerParty = playerParty;
    this.wildAnimal = wildAnimal;
    StartCoroutine(SetupBattle());
  }

  public IEnumerator SetupBattle()
  {
    playerUnit.Setup(playerParty.GetHealthyAnimal());
    enemyUnit.Setup(wildAnimal);
    //set the HUD data of the animal called in the previous line from BattleUnit
    playerHUD.SetData(playerUnit.Animal);
    enemyHUD.SetData(enemyUnit.Animal);

    partyScreen.Init();

    // passing moves of current animal to the SetMoveNames function
    dialogueBox.SetMoveNames(playerUnit.Animal.Moves);

    yield return dialogueBox.TypeDialogue($"A wild {enemyUnit.Animal.Base.Name} appeared!");

    PlayerAction();
  }

  void PlayerAction()
  {
    state = BattleState.PlayerAction;
    // when going back to PlayerAction from move selection don't animate the text every time, just display it
    dialogueBox.SetDialogue("Choose an action");
    dialogueBox.EnableActionSelector(true);
  }

  void OpenPartyScreen()
  {
    partyScreen.SetPartyData(playerParty.Animals);
    partyScreen.gameObject.SetActive(true);
  }

  void PlayerMove()
  {
    state = BattleState.PlayerMove;
    dialogueBox.EnableActionSelector(false);
    dialogueBox.EnableDialougeText(false);
    dialogueBox.EnableMoveSelector(true);
    // only show move selector, disable any other text from showing up in the dialogue box
  }

  IEnumerator PerformPlayerMove()
  {
    // so player can't change move once selected
    state = BattleState.Busy;

    // player animal will perform attack and enemy will take damage
    // reference to the move selectedMove
    var move = playerUnit.Animal.Moves[currentMove];
    move.PP--;
    yield return dialogueBox.TypeDialogue($"{playerUnit.Animal.Base.Name} used {move.Base.Name}");

    playerUnit.PlayAttackAnimation();
    yield return new WaitForSeconds(1f);

    enemyUnit.PlayHitAnimation();
    var damageDetails = enemyUnit.Animal.TakeDamage(move, playerUnit.Animal);
    yield return enemyHUD.UpdateHP();
    yield return ShowDamageDetails(damageDetails);

    if (damageDetails.Fainted)
    {
      // if above bool is true then enemy fainted
      yield return dialogueBox.TypeDialogue($"{enemyUnit.Animal.Base.Name} fainted");
      enemyUnit.PlayFaintAnimation();
      // if enemy faints wait 2 secs and pass true to OnBattleOver as enemy fainted and player won
      yield return new WaitForSeconds(2f);
      OnBattleOver(true);
    }
    else
    {
      StartCoroutine(EnemyMove());
    }
  }

  IEnumerator EnemyMove()
  {
    state = BattleState.EnemyMove;

    // select random move from enemy move class
    var move = enemyUnit.Animal.GetRandomMove();
    move.PP--;
    yield return dialogueBox.TypeDialogue($"{enemyUnit.Animal.Base.Name} used {move.Base.Name}");

    enemyUnit.PlayAttackAnimation();
    yield return new WaitForSeconds(1f);

    playerUnit.PlayHitAnimation();
    var damageDetails = playerUnit.Animal.TakeDamage(move, playerUnit.Animal);
    yield return playerHUD.UpdateHP();
    yield return ShowDamageDetails(damageDetails);

    if (damageDetails.Fainted)
    {
      // if above bool is true then enemy fainted
      yield return dialogueBox.TypeDialogue($"{playerUnit.Animal.Base.Name} fainted");
      playerUnit.PlayFaintAnimation();

      // pass false to OnBattleOver to indicate player lost
      yield return new WaitForSeconds(2f);

      // before ending the battle check if there is another healthy animal to send out
      // if there is SetupBattle
      var nextAnimal = playerParty.GetHealthyAnimal();
      if (nextAnimal != null)
      {
        playerUnit.Setup(nextAnimal);
        //set the HUD data of the animal called in the previous line from BattleUnit
        playerHUD.SetData(nextAnimal);
        // passing moves of current animal to the SetMoveNames function
        dialogueBox.SetMoveNames(nextAnimal.Moves);

        yield return dialogueBox.TypeDialogue($"Go {nextAnimal.Base.Name}!");

        PlayerAction();
      }
      else
      {
        // if there is no more helathy animals to send out, then the player lost the battle
        OnBattleOver(false);
      }
    }
    else
    {
      // if no one has died let the player choose another move
      PlayerAction();
    }
  }

  IEnumerator ShowDamageDetails(DamageDetails damageDetails)
  {
    if (damageDetails.Critical > 1f)
        yield return dialogueBox.TypeDialogue("A critical hit!");

    if (damageDetails.Type > 1f)
        yield return dialogueBox.TypeDialogue("It's super effective!");
    else if (damageDetails.Type < 1f)
        yield return dialogueBox.TypeDialogue("It's not very effective!");

  }

  public void HandleUpdate()
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
  // now have 4 options so need to use all 4 arrow keys to allow for selection like HandleMoveSelection
  // clamp specifies how many variables there are to select and so works out whether you can move down
  // if you're already on the bottom row, for example, which you can't so only allows left, right, or up
  // depending on the situation as opposed to the hardcoded version for this I had in the HandleMoveSelection
  // function below.
  {
    if (Input.GetKeyDown(KeyCode.RightArrow))
        ++currentAction;
    else if (Input.GetKeyDown(KeyCode.LeftArrow))
        --currentAction;
    else if (Input.GetKeyDown(KeyCode.DownArrow))
        currentAction += 2;
    else if (Input.GetKeyDown(KeyCode.UpArrow))
        currentAction -= 2;

    currentAction = Mathf.Clamp(currentAction, 0, 3);


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
        // Bag
      }
      else if (currentAction == 2)
      {
        // Animal
        OpenPartyScreen();
      }
      else if (currentAction == 3)
      {
        // Run
      }
    }
  }

  void HandleMoveSelection()
  {
    if (Input.GetKeyDown(KeyCode.RightArrow))
        ++currentMove;
    else if (Input.GetKeyDown(KeyCode.LeftArrow))
        --currentMove;
    else if (Input.GetKeyDown(KeyCode.DownArrow))
        currentMove += 2;
    else if (Input.GetKeyDown(KeyCode.UpArrow))
        currentMove -= 2;

    currentMove = Mathf.Clamp(currentMove, 0, playerUnit.Animal.Moves.Count - 1);

    dialogueBox.UpdateMoveSelection(currentMove, playerUnit.Animal.Moves[currentMove]);

    // perform the move when pressed
    if (Input.GetKeyDown(KeyCode.Z))
    {
      dialogueBox.EnableMoveSelector(false);
      dialogueBox.EnableDialougeText(true);
      StartCoroutine(PerformPlayerMove());
    }

    // exit move selection and go back to aciton selection by pressing x
    else if (Input.GetKeyDown(KeyCode.X))
    {
      dialogueBox.EnableMoveSelector(false);
      dialogueBox.EnableDialougeText(true);
      PlayerAction();
    }
  }
}
