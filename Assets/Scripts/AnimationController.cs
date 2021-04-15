using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector3 flipRight = new Vector3(-1, 1, 1);
    private Vector3 flipLeft = new Vector3(1, 1, 1);

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Movement(Vector3 axis)
    {
        if (axis != Vector3.zero)
        {
            animator.SetBool("Move", true);
            if (axis.y > 0) animator.SetBool("Front", false);
            else if (axis.y < 0) animator.SetBool("Front", true);
            if (axis.x > 0) transform.localScale = flipRight;
            else if (axis.x < 0) transform.localScale = flipLeft;
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
