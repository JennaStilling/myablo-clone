using UnityEngine;

public class BasicAnimator : MonoBehaviour
{
    [SerializeField] protected Animator thisAnimator;
    protected Vector3 oldPos = Vector3.zero;
    protected Vector3 deltaPos = Vector3.zero;

    public void SetWalking(bool val)
    {
        thisAnimator.SetBool("Walking", val);
    }

    public void TriggerAttack()
    {
        thisAnimator.SetTrigger("Attack");
    }

    public void TriggerDeath()
    {
        thisAnimator.SetTrigger("Die");
    }
    public void TriggerRevive()
    {
        thisAnimator.SetTrigger("Revive");
    }
}
