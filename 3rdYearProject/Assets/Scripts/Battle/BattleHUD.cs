using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
  // reference to name, level and hp
  [SerializeField] Text nameText;
  [SerializeField] Text levelText;
  [SerializeField] HPBar hpBar;

  Animal _animal;

  public void SetData(Animal animal)
  {
    _animal = animal;

    nameText.text = animal.Base.Name;
    levelText.text = "Lvl " + animal.Level;
    hpBar.SetHP((float) animal.HP / animal.MaxHP);
  }

  public IEnumerator UpdateHP()
  {
    yield return hpBar.SetHPSmooth((float) _animal.HP / _animal.MaxHP);
  }
}
