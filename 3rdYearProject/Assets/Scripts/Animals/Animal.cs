using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{

  public AnimalBase Base { get; set; }

  public int Level { get; set; }

  public int HP { get; set; }

  public List<Move> Moves { get; set; }

  public Animal(AnimalBase aBase, int aLevel)
  {
    Base = aBase;
    Level = aLevel;
    HP = MaxHP;

    // generate moves based on animals level
    Moves = new List<Move>();
    foreach (var move in Base.LearnableMoves)
    {
      if (move.Level <= Level)
          Moves.Add(new Move(move.Base));

      if (Moves.Count >= 4)
          break;
    }
  }

  public int Attack {
    // calculate attack for each level by multiplying base stat with the level as a % + 5. FloorToInt to remove decimal
    get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
  }

  public int Defence {
    get { return Mathf.FloorToInt((Base.Defence * Level) / 100f) + 5; }
  }

  public int SpAttack {
    get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
  }

  public int SpDefence {
    get { return Mathf.FloorToInt((Base.SpDefence * Level) / 100f) + 5; }
  }

  public int Speed {
    get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
  }

  public int MaxHP {
    get { return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10; }
  }


}
