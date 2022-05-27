using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed;
  private bool isMoving;
  private Vector2 input;

  private Animator animator;

  public LayerMask solidObjectsLayer;
  public LayerMask grassLayer;


  private void Awake()
  {
    animator = GetComponent<Animator>();
  }

  private void Update()
  {
    if (!isMoving)
    {
      // GetAxisRaw makes movement +/- 1
      input.x = Input.GetAxisRaw("Horizontal");
      input.y = Input.GetAxisRaw("Vertical");

      // remove diagonal movement
      if (input.x != 0) input.y = 0;

      if (input != Vector2.zero)
      {
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);

        // add the input above to current position if input is 1 (add one/minus one to the current position)
        var targetPos = transform.position;
        targetPos.x += input.x;
        targetPos.y += input.y;

        if (isWalkable(targetPos))
          StartCoroutine(Move(targetPos));
      }
    }

    animator.SetBool("isMoving", isMoving);
  }

  IEnumerator Move(Vector3 targetPos)
  {
    isMoving = true;
    // while input is taken (targetPos - transform.position = 1 when player is moved
    // but there is no targetPos if the player isn't moved) move the player.
    while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
    {
      transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
      yield return null;
    }
    transform.position = targetPos;

    isMoving = false;

    CheckForEncounters();
  }

  // will check the targetPos and see if the tile at that position isWalkable and will assume it is otherwise
  private bool isWalkable(Vector3 targetPos)
  {
    if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
    {
      return false;
    }

    return true;
  }

  private void CheckForEncounters()
  {
    // if not null then player walked on a grass tile. If the player walks on the grass, 1/10 we will trigger a battle
    if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
    {
      if (Random.Range(1, 101) <= 10)
      {
        Debug.Log("Encountered a wild animal");
      }
    }
  }
}
