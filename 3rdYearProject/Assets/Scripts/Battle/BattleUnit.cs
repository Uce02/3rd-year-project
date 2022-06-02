using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
  [SerializeField] AnimalBase _base;
  [SerializeField] int level;
  [SerializeField] bool isPlayerUnit;

  public Animal Animal { get; set; }

  public void Setup()
  {
    // makes animal appear during the battle scene
    Animal = new Animal(_base, level);
    if (isPlayerUnit)
        GetComponent<Image>().sprite = Animal.Base.BackSprite;
    else
        GetComponent<Image>().sprite = Animal.Base.FrontSprite;

  }
}
