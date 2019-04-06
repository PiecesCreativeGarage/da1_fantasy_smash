using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Guard : MonoBehaviour
{
    public bool isGuarding;
    public GameObject gobj;
    bool gobj_exist;
    Transform playerTransform;
    PlayerData.GuardData guardData;
    public float GuardingFrame;

    float StartFrame, EndFrame, JustGuardFrame;
    public float JustFrameProperty
    {
        get { return JustGuardFrame; }
    }

    public enum Transition
    {
        Start, Guarding, End,
    }
    Transition transition;

    public Transition transitionProperty
    {
        get { return transition; }
    }
    public Guard(Transform transform, PlayerData.GuardData guardData)
    {
        this.guardData = guardData;
        this.playerTransform = transform;
        if (guardData.guardObject != null)
        {
            gobj = Instantiate(guardData.guardObject, Vector3.zero, Quaternion.identity);
            gobj.transform.localScale = guardData.guardScale;
            gobj_exist = true;
            gobj.SetActive(false);
        }
        else
        {
            gobj_exist = false;
        }
    }

    public void Start()
    {
        gobj.transform.localScale = guardData.guardScale;
        this.StartFrame = guardData.transitionFrames[0]; this.GuardingFrame = guardData.transitionFrames[1]; //時間を設定
        this.EndFrame = guardData.transitionFrames[2]; this.JustGuardFrame = guardData.justGuardFrame;

        isGuarding = true;
        transition = Transition.Start;

    }
    public void Guarding()
    {
        gobj.transform.position = playerTransform.position + guardData.plusGuardPosi;

        switch (transition)
        {
            case Transition.Start:
                StartFrame--;
                if (StartFrame <= 0)
                {
                    transition = Transition.Guarding;
                }
                break;
            case Transition.Guarding:
                if (gobj_exist)
                {
                    gobj.SetActive(true);
                }


                GuardingFrame--;
                JustGuardFrame--;
                if (!Input.GetKey(guardData.keyCode) && GuardingFrame <= 0)
                {

                    if (gobj_exist)
                    {
                        gobj.SetActive(false);
                    }
                    transition = Transition.End;

                }
                break;
            case Transition.End:
                EndFrame--;
                if (EndFrame <= 0)
                {
                    isGuarding = false;
                }
                break;

        }
    }
}
