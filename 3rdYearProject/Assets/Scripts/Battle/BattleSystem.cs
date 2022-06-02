using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
  // control the entire Battle
  [SerializeField] BattleUnit playerUnit;
  [SerializeField] BattleUnit enemyUnit;
  [SerializeField] BattleHUD playerHUD;
  [SerializeField] BattleHUD enemyHUD;

  private void Start()
  {
    SetupBattle();
  }

  public void SetupBattle()
  {
    //calls on playerUnit from BattleUnit to show the animal
    playerUnit.Setup();
    enemyUnit.Setup();
    //set the HUD data of the animal called in the previous line from BattleUnit
    playerHUD.SetData(playerUnit.Animal);
    enemyHUD.SetData(enemyUnit.Animal);
  }
}
