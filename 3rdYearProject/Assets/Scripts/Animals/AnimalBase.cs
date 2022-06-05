using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Animal/Create new animal")]

public class AnimalBase : ScriptableObject
{
  // serialize all these to show them in the inspector, using get will not show them in the inspector
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

  [SerializeField] List<LearnableMove> learnableMoves;

// which variable is to be reutned and get this to be used/displayed wherever
  public string Name {
    get { return name; }
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

  public List<LearnableMove> LearnableMoves {
    get { return learnableMoves; }
  }

}

[System.Serializable]

public class LearnableMove
{
  // store the moves and at which level they will become available
  [SerializeField] MoveBase moveBase;
  [SerializeField] int level;

  public MoveBase Base {
    get { return moveBase; }
  }

  public int Level {
    get { return level; }
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

public class TypeChart
{
  // 2D array to store weaknesses of each animal type. static so can use directly from the class without creating an object
  static float[][] chart =
  {
    //                    NOR FIR WAT  ELE  GRA  ICE FIG POI
    /*NOR*/ new float[] { 1f, 1f, 1f,  1f,  1f,  1f, 1f, 1f },
    /*FIR*/ new float[] { 1f,0.5f,0.5f,1f,  2f,  2f, 1f, 1f },
    /*WAT*/ new float[] { 1f, 2f, 0.5f,1f,  0.5f,1f, 1f, 1f },
    /*ELE*/ new float[] { 1f, 1f, 2f,  0.5f,0.5f,1f, 1f, 1f },
    /*GRA*/ new float[] { 1f,0.5f,2f,  1f,  0.5f,1f, 1f, 0.5f },
    /*ICE*/ new float[] { 1f,0.5f,0.5f,1f,  2f, 0.5f,1f, 1f },
    /*FIG*/ new float[] { 2f, 1f, 1f,  1f,  1f,  2f, 1f, 0.5f },
    /*POI*/ new float[] { 1f, 1f, 1f,  1f,  2f,  1f, 1f, 0.5f}
  };

  public static float GetEffectiveness(AnimalType attackType, AnimalType defenceType)
  {
    if (attackType == AnimalType.None || defenceType == AnimalType.None)
        return 1;

    // -1 from integer value of each enum type to make it equal the chart
    // chart starts at index value 0 and enums start at 1
    int row = (int)attackType - 1;
    int col = (int)defenceType - 1;

    return chart[row][col];

  }
}
