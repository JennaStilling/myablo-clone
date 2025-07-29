using UnityEngine;

public class EnemyAnimator : BasicAnimator
{
    void Update()
    {
        deltaPos = (transform.position - oldPos);

        if (deltaPos.sqrMagnitude > .001f * Time.deltaTime)
            SetWalking(true);
        else
            SetWalking(false);

        oldPos = transform.position;
    }
}
