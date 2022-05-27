using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed;

  private bool isMoving;
  private Vector2 input;

  private Animator animator;

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
  }
}
