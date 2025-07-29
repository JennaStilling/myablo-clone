using UnityEngine;

public class PlayerAnimator : BasicAnimator
{
    // Update is called once per frame
    void Update()
    {
        deltaPos = (transform.position - oldPos);

        if (deltaPos.sqrMagnitude > .01f * Time.deltaTime)
            SetWalking(true);
        else
            SetWalking(false);

        oldPos = transform.position;
    }
}
