using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Animal/Create new animal")]

public class AnimalBase : ScriptableObject
{
  [SerializeField] string aName;

  [TextArea]
  [SerializeField] string description;

  [SerializeField] Sprite frontSprite;
  [SerializeField] Sprite backSprite;

  [SerializeField] AnimalType type1;
  [SerializeField] AnimalType type2;

  // Base stats
  [SerializeField] int maxHp;
  [SerializeField] int attack;
  [SerializeField] int defence;
  [SerializeField] int spAttack;
  [SerializeField] int spDefence;
  [SerializeField] int speed;

  public string Name
  { get { return name; }
  }

  public string Description {
    get { return description; }
  }

  public Sprite FrontSprite {
    get { return frontSprite; }
  }

  public Sprite BackSprite {
    get { return backSprite; }
  }

  public AnimalType Type1 {
    get { return type1; }
  }

  public AnimalType Type2 {
    get { return type2; }
  }

  public int MaxHP {
    get { return maxHp; }
  }

  public int Attack {
    get { return attack; }
  }

  public int SpAttack {
    get { return spAttack; }
  }

  public int Defence {
    get { return defence; }
  }

  public int SpDefence {
    get { return spDefence; }
  }

  public int Speed {
    get { return speed; }
  }

}

public enum AnimalType
{
  None,
  Normal,
  Fire,
  Water,
  Electric,
  Grass,
  Ice,
  Fighting,
  Poison,
  Ground,
  Flying,
  Bug,
  Rock

  // excluded psychic, ghost and dragon

}
