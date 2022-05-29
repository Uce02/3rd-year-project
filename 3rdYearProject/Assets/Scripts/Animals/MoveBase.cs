using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Animal/Create a new move")]

public class MoveBase : ScriptableObject
{
  [SerializeField] string name;

  [TextArea]
  [SerializeField] string description;

  [SerializeField] AnimalType type;
  [SerializeField] int power;
  [SerializeField] int accuracy;
  // number of times a move can be performed
  [SerializeField] int pp;


}
