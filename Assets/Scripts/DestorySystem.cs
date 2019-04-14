using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestorySystem : MonoBehaviour
{
  public float DestoryTime = 1F;

  private void Start()
  {
    Set();
  }

  public void Set()
  {
    Destroy(this.gameObject, DestoryTime);
  }
}
