using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtil
{
    public static bool ErrorNotice(string Class_name,string variable_name, object variable)
    {
        if (variable == null)
        {                
            Debug.Log("<color=red> " + Class_name + "の" + variable_name + " が指定されてない</color>");
            return true;
        }
        
        return false;
    }
}
