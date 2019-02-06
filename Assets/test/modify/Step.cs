using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Step
{
    public bool isStepping;
    float moveSpeed;
    float moveDistance;
    float nowDistance;
    Transform transform;
    Vector3 direction;
    float invincibleFrame;
    public float invincibleProperty
    {
        get { return invincibleFrame; }
    }
    float startFrame, stepFrame, endFrame;
    public enum Transition
    {
        Start, Stepping, End
    }
    Transition transition;
    public Transition transitionProperty
    {
        get { return transition; }
    }

    public Step(Transform transform)
    {
        this.transform = transform;
    }
    public void Start(float moveSpeed, float invincibleFrame, Vector3 direction, bool isStepping)
    {
        this.moveSpeed = moveSpeed;
        this.direction = direction;
        this.invincibleFrame = invincibleFrame;

        startFrame = 3f; stepFrame = 5f; endFrame = 5f;
        this.isStepping = isStepping;
        if (isStepping)
        {
            transition = Transition.Start;
        }
    }

    public void Stepping(bool[] is_in_front_of_wall)
    {
        switch (transition)
        {
            case Transition.Start:
                startFrame--;
                if (startFrame <= 0)
                {
                    transition = Transition.Stepping;
                }
                break;
            case Transition.Stepping:
                invincibleFrame--;
                stepFrame--;

                if (stepFrame <= 0)
                {

                    transition = Transition.End;

                }
                else
                {
                    if (!is_in_front_of_wall[0] && direction.z > 0)//前
                    {
                        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;

                    }
                    if (!is_in_front_of_wall[1] && direction.z < 0)//後
                    {
                        transform.position += -transform.forward * moveSpeed * Time.fixedDeltaTime;

                    }
                    if (!is_in_front_of_wall[2] && direction.x < 0)//左
                    {
                        transform.position += -transform.right * moveSpeed * Time.fixedDeltaTime;

                    }
                    if (!is_in_front_of_wall[3] && direction.x > 0)//右
                    {
                        transform.position += transform.right * moveSpeed * Time.fixedDeltaTime;
                    }
                }
                break;
            case Transition.End:
                endFrame--;
                if (endFrame <= 0)
                {
                    isStepping = false;
                }
                break;
        }
    }
}
