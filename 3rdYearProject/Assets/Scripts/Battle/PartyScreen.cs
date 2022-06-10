using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyScreen : MonoBehaviour
{
  // reference to message text
  [SerializeField] Text messageText;

  PartyMemberUI[] memberSlots;

  // assign memberSlots in a public function init. returns all PartyMemberUI components attached
  // to the child objects of the party screen (don't have to manually assign them from the inspector)
  public void Init()
  {
    memberSlots = GetComponentsInChildren<PartyMemberUI>();
  }

  public void SetPartyData(List<Animal> animals)
  {
    for (int i = 0; i < memberSlots.Length; i++)
    {
      // SetData for the animal at that index location and if it's empty, get rid of the slot
      if (i < animals.Count)
          memberSlots[i].SetData(animals[i]);
      else
          memberSlots[i].gameObject.SetActive(false);
    }

    messageText.text = "Choose an animal";
  }
}
