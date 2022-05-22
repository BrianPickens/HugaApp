using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatorCallback : MonoBehaviour
{

    public Action OnAnimatorComplete;

    public void EndAnimation()
    {
        OnAnimatorComplete?.Invoke();
    }

}
