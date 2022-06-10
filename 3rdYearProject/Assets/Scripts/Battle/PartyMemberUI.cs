using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberUI : MonoBehaviour
{
  [SerializeField] Text nameText;
  [SerializeField] Text levelText;
  [SerializeField] HPBar hpBar;
  [SerializeField] Image partyUnit;

  Animal _animal;

  public void SetData(Animal animal)
  {
    _animal = animal;

    nameText.text = animal.Base.Name;
    levelText.text = "Lvl " + animal.Level;
    hpBar.SetHP((float) animal.HP / animal.MaxHP);
    partyUnit.sprite = animal.Base.FrontSprite;
  }
}
