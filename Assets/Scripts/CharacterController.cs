using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator _animator;
    private bool isMoving = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
            isMoving = false;
        }
        else if (Input.touchCount == 2)
        {
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isRunning", false);
            isMoving = true;
        }
        else if (Input.touchCount == 1 && !isMoving)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", true);
            isMoving = true;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                    transform.LookAt(targetPosition);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 1f);
                }
            }
        }
    }
}