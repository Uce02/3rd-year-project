using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Animal/Create a new move")]

public class MoveBase : ScriptableObject
{
  // serialize all these to show them in the inspector, using get will not show them in the inspector
  [SerializeField] string name;

  [TextArea]
  [SerializeField] string description;

  [SerializeField] AnimalType type;
  [SerializeField] int power;
  [SerializeField] int accuracy;
  // number of times a move can be performed
  [SerializeField] int pp;

  public string Name {
    get { return name; }
  }

  public string Description {
    get { return description; }
  }

  public AnimalType Type {
    get { return type; }
  }

  public int Power {
    get { return power; }
  }

  public int Accuracy {
    get { return accuracy; }
  }

  public int PP {
    get { return pp; }
  }
}
