using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogueBox : MonoBehaviour
{
  [SerializeField] int letterPerSecond;
  [SerializeField] Color highlightedColour;

  [SerializeField] Text dialogueText;
  [SerializeField] GameObject actionSelector;
  [SerializeField] GameObject moveSelector;
  [SerializeField] GameObject moveDetails;

  [SerializeField] List<Text> actionTexts;
  [SerializeField] List<Text> moveTexts;

  [SerializeField] Text ppText;
  [SerializeField] Text typeText;

  public void SetDialogue(string dialogue)
  {
    dialogueText.text = dialogue;
  }

  public IEnumerator TypeDialogue(string dialogue)
  {
    dialogueText.text = "";
    // loop through each letter and them one by one to make the dialogue scroll along
    foreach (var letter in dialogue.ToCharArray())
    {
      dialogueText.text += letter;
      // wait for 1/30th of a second after displaying each letter (30 letters in one second)
      yield return new WaitForSeconds(1f/letterPerSecond);
    }
    // anywhere a dialogue is shown it waits one second after displaying it to be read
    yield return new WaitForSeconds(1f);
  }

  public void EnableDialougeText(bool enabled)
  {
    dialogueText.enabled = enabled;
  }

  public void EnableActionSelector(bool enabled)
  {
    actionSelector.SetActive(enabled);
  }

  public void EnableMoveSelector(bool enabled)
  {
    moveSelector.SetActive(enabled);
    moveDetails.SetActive(enabled);
  }

  public void UpdateActionSelection(int selectedAction)
  {
    // loop through action texts and if current action is selected change colour
    for (int i=0; i<actionTexts.Count; ++i)
    {
      if (i == selectedAction)
          actionTexts[i].color = highlightedColour;
      else
          actionTexts[i].color = Color.black;
    }
  }

  public void UpdateMoveSelection(int selectedMove, Move move)
  {
    // loop through move texts and if current move is selected change colour
    for (int i=0; i<moveTexts.Count; ++i)
    {
      if (i == selectedMove)
          moveTexts[i].color = highlightedColour;
      else
          moveTexts[i].color = Color.black;
    }
    // show current PP of the move and base PP (how many moves you have left out of total times it can be used)
    ppText.text = $"PP {move.PP}/{move.Base.PP}";
    // show type of current move
    typeText.text = move.Base.Type.ToString();
  }

  public void SetMoveNames(List<Move> moves)
  {
    for (int i=0; i<moveTexts.Count; ++i)
    {
      if (i < moves.Count)
          moveTexts[i].text = moves[i].Base.Name;
      else
      // if all moves of the animal have been displayed  and there is still an empty space put this in its place
          moveTexts[i].text = "-";
    }
  }
}
