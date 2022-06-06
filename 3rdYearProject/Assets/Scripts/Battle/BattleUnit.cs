using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
  [SerializeField] AnimalBase _base;
  [SerializeField] int level;
  [SerializeField] bool isPlayerUnit;

  public Animal Animal { get; set; }

  Image image;
  Vector3 ogPos;
  Color ogCol;

  private void Awake()
  {
    image = GetComponent<Image>();
    ogPos = image.transform.localPosition;
    ogCol = image.color;
  }

  public void Setup()
  {
    // makes animal appear during the battle scene
    Animal = new Animal(_base, level);
    if (isPlayerUnit)
        image.sprite = Animal.Base.BackSprite;
    else
        image.sprite = Animal.Base.FrontSprite;

    image.color = ogCol;
    PlayEnterAnimation();
  }

  public void PlayEnterAnimation()
  {
    // keeps player and enemy outside of view and smoothly transistions them into place to start the battle
    if (isPlayerUnit)
        image.transform.localPosition = new Vector3(-450f, ogPos.y);
    else
        image.transform.localPosition = new Vector3(450f, ogPos.y);

    // 1st param where you want the image to go, 2nd param the time it takes to move there
    image.transform.DOLocalMoveX(ogPos.x, 1f);
  }

  public void PlayAttackAnimation()
  {
    // play multiple animations one by one. move player to the right and enemy left when attacking and
    // go back to ogPos straight after
    var sequence = DOTween.Sequence();
    if (isPlayerUnit)
        sequence.Append(image.transform.DOLocalMoveX(ogPos.x + 50f, 0.25f));
    else
        sequence.Append(image.transform.DOLocalMoveX(ogPos.x - 50f, 0.25f));

    sequence.Append(image.transform.DOLocalMoveX(ogPos.x, 0.25f));
  }

  public void PlayHitAnimation()
  {
    // make the hit player turn grey very quickly to show that its been hit
    var sequence = DOTween.Sequence();
    sequence.Append(image.DOColor(Color.gray, 0.15f));
    sequence.Append(image.DOColor(ogCol, 0.15f));
  }

  public void PlayFaintAnimation()
  {
    // take the animal off screen by moving it down and fading it to nothing
    // Join() to perform both animations at the same time (fade and move down)
    var sequence = DOTween.Sequence();
    sequence.Append(image.transform.DOLocalMoveY(ogPos.y - 150f, 0.5f));
    sequence.Join(image.DOFade(0f, 0.5f));
  }
}
