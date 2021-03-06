using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//classes will only be shown in the inspector if we use this attbiute
[System.Serializable]

public class Animal
{
  // to make them show up in the inspector they need to be SerializeField
  [SerializeField] AnimalBase _base;
  [SerializeField] int level;

  public AnimalBase Base {
    get { return _base; }
  }

  public int Level {
    get { return level; }
  }

  public int HP { get; set; }

  public List<Move> Moves { get; set; }

  // constructor won't be called as we're making objects of the animal class in the inspector
  // so use init to add all initialisation code
  public void Init()
  {
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

  public DamageDetails TakeDamage(Move move, Animal attacker)
  {
    // critical hit is of 6.25% chance and deals double damange
    float critical = 1f;
    if (Random.value * 100f <= 6.25f)
        critical = 2f;

    float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

    var damageDetails = new DamageDetails()
    {
      Type = type,
      Critical = critical,
      Fainted = false
    };

    // if the move is special, use spAttack and spDefence values
    // '?' shorthand version of writing if else
    float attack = (move.Base.isSpecial) ? attacker.SpAttack : attacker.Attack;
    float defence = (move.Base.isSpecial) ? SpDefence : Defence;

    // calculates damage including any changes due to type from GetEffectiveness fucntion in animal base
    float modifiers = Random.Range(0.85f, 1f) * type * critical;
    float a = (2 * attacker.Level + 10) / 250f;
    float d = a * move.Base.Power * ((float)attack / defence) + 2;
    int damage = Mathf.FloorToInt(d * modifiers);

    // reduce the damange from HP
    HP -= damage;
    if (HP <= 0)
    {
      // animal fainted
      HP = 0;
      damageDetails.Fainted = true;
    }

    return damageDetails;
  }

  public Move GetRandomMove()
  {
    // generate random number from 0 to the length of the animal moves and return move at that index
    int r = Random.Range(0, Moves.Count);
    return Moves[r];
  }
}

public class DamageDetails
{
  public bool Fainted { get; set; }

  public float Critical { get; set; }

  public float Type { get; set; }
}
