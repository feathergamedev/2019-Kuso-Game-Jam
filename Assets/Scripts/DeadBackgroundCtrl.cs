using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadBackgroundCtrl : MonoBehaviour
{
  public Sprite[] NormalDeadImageArray = new Sprite[0];
  public Sprite[] FullDeadImageArray = new Sprite[0];
  public string[] NormalDeadTextArray = new string[0];
  public string[] FullDeadTextArray = new string[0];

  private const int NORMAL_WIDTH = 1300;
  private const int NORMAL_HEIGHT = 1300;
  private const int FULL_WIDTH = 1920;
  private const int FULL_HEIGHT = 1080;
  private bool _IsOnce = false;

  private Animator _Anim = null;
  private Image _Background = null;
  private Image _FontImage = null;
  private Image _FontLight = null;
  private Image _FontShadow = null;
  private Text _Text = null;
  private Image _FrontBackground = null;

  private Action _OnFinshedCallback = null;
  public void RegisterOnFinshedCallback(Action callback)
  {
    _OnFinshedCallback = callback;
  }

  private void Awake()
  {
    Init();
  }

#if UNITY_EDITOR
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.F1))
    {
      Play();
    }
  }
#endif

  private void Init()
  {
    _Anim = this.transform.Find("Animation").GetComponent<Animator>();
    _Background = this.transform.Find("Animation/Background").GetComponent<Image>();
    _FontImage = this.transform.Find("Animation/FontImage").GetComponent<Image>();
    _FontLight = this.transform.Find("Animation/FontLight").GetComponent<Image>();
    _FontShadow = this.transform.Find("Animation/FontShadow").GetComponent<Image>();
    _Text = this.transform.Find("Animation/Text").GetComponent<Text>();
    _FrontBackground = this.transform.Find("Animation/FrontBackground").GetComponent<Image>();
  }

  public void Play()
  {
    int currIndex = GetRandomIndex();
    bool isFull = currIndex >= NormalDeadImageArray.Length;
    if (isFull == true)
    {
      currIndex -= NormalDeadImageArray.Length;
    }

    Sprite currSprite = GetShowSprite(currIndex, isFull);
    string currText = GetShowText(currIndex, isFull);
    Vector2 currSize = isFull == true ? new Vector2(FULL_WIDTH, FULL_HEIGHT) : new Vector2(NORMAL_WIDTH, NORMAL_HEIGHT);

    _FontImage.rectTransform.sizeDelta = currSize;
    _FontImage.sprite = currSprite;
    _FontLight.rectTransform.sizeDelta = currSize;
    _FontLight.sprite = currSprite;
    _FontShadow.rectTransform.sizeDelta = currSize;
    _FontShadow.sprite = currSprite;
    _Text.text = currText;

    _Anim.SetTrigger("Reset");
    _Anim.enabled = true;
  }

  private int GetRandomIndex()
  {
    if (_IsOnce == false)
    {
      _IsOnce = true;
      return 0;
    }
    int index = UnityEngine.Random.Range(0, NormalDeadImageArray.Length + FullDeadImageArray.Length);
    return index;
  }

  private Sprite GetShowSprite(int index, bool isFull)
  {
    if (isFull == true)
    {
      if (FullDeadImageArray.Length <= index) return null;
      return FullDeadImageArray[index];
    }
    else
    {
      if (NormalDeadImageArray.Length <= index) return null;
      return NormalDeadImageArray[index];
    }
  }

  private string GetShowText(int index, bool isFull)
  {
    if (isFull == true)
    {
      if (FullDeadTextArray.Length <= index) return string.Empty;
      return FullDeadTextArray[index];
    }
    else
    {
      if (NormalDeadTextArray.Length <= index) return string.Empty;
      return NormalDeadTextArray[index];
    }
  }

  private void OnFinished()
  {
    if (_OnFinshedCallback != null)
      _OnFinshedCallback();
  }
}
