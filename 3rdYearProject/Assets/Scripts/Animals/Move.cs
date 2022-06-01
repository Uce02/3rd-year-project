using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// contain the data of the moves that change during the battle e.g. pp

public class Move
{

  public MoveBase Base { get; set; }

  public int PP { get; set; }

  public Move (MoveBase aBase)
  {
    Base = aBase;
    PP = aBase.PP;
  }
}
