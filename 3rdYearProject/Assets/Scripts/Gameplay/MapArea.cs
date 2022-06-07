using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
  // will store all wild animals in this area
  [SerializeField] List<Animal> wildAnimals;

  //return random wild animal
  public Animal GetRandomWildAnimal()
  {
    var wildAnimal = wildAnimals[Random.Range(0, wildAnimals.Count)];
    wildAnimal.Init();
    return wildAnimal;
  }
}
