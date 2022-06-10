using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalParty : MonoBehaviour
{
  [SerializeField] List<Animal> animals;

  public List<Animal> Animals {
    get { return animals; }
  }

  private void Start()
  {
    // loop through all animals and initialise each one
    foreach (var animal in animals)
    {
      animal.Init();
    }
  }

  public Animal GetHealthyAnimal()
  {
    // Where loops through the animals and finds which ones meet the condition of
    // having health >0. If no healthy animals it will return null. An easier for loop
    return animals.Where(x => x.HP > 0).FirstOrDefault();
  }
}
