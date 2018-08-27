using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBreak : MonoBehaviour {
    [SerializeField]
    GameObject GuardBreaker;

   void GuardBreak_Start()
    {
        GuardBreaker.SetActive(true);
    }
    void GuardBreak_End()
    {
        GuardBreaker.SetActive(false);
    }

}
