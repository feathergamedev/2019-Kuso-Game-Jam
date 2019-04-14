using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCall : MonoBehaviour
{
  public Action OnFinishedCallback = null;

  public void OnFinishedEvent()
  {
    if (OnFinishedCallback != null)
      OnFinishedCallback();
  }
}
