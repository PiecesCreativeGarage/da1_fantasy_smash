using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Guard : MonoBehaviour
{
    public bool isGuarding;
    public GameObject gobj;
    bool gobj_exist;

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
    public Guard(GameObject guard_Object)
    {

        if (guard_Object != null)
        {
            gobj = Instantiate(guard_Object, Vector3.zero, Quaternion.identity);
            gobj.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
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

        StartFrame = 5f; GuardingFrame = 7f; //時間を設定
        EndFrame = 4f; JustGuardFrame = 3f;

        isGuarding = true;
        transition = Transition.Start;


    }
    public void Guarding(Transform Guard_Posi)
    {
        gobj.transform.position = Guard_Posi.position + new Vector3(0, 1.25f);


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
                if (!Input.GetKey(KeyCode.G) && GuardingFrame <= 0)
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
