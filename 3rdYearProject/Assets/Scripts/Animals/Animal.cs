using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{

  AnimalBase _base;
  int level;

  public int HP { get; set; }

  public List<Move> Moves { get; set; }

  public Animal(AnimalBase aBase, int aLevel)
  {
    _base = aBase;
    level = aLevel;
    HP = _base.MaxHP;

    // generate moves based on animals level
    Moves = new List<Move>();
    foreach (var move in _base.LearnableMoves)
    {
      if (move.Level <= level)
          Moves.Add(new Move(move.Base));

      if (Moves.Count >= 4)
          break;
    }
  }

  public int Attack {
    // calculate attack for each level by multiplying base stat with the level as a % + 5. FloorToInt to remove decimal
    get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
  }

  public int Defence {
    get { return Mathf.FloorToInt((_base.Defence * level) / 100f) + 5; }
  }

  public int SpAttack {
    get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5; }
  }

  public int SpDefence {
    get { return Mathf.FloorToInt((_base.SpDefence * level) / 100f) + 5; }
  }

  public int Speed {
    get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }
  }

  public int MaxHP {
    get { return Mathf.FloorToInt((_base.MaxHP * level) / 100f) + 10; }
  }


}
