using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class DotClass : MonoBehaviour
{

  [Range(0.0f, 1.0f)]
  [SerializeField]
  private float hue;
  [Range(0.3f, 1.0f)]
  [SerializeField]
  private float saturation;
  [Range(0.0f, 1.0f)]
  [SerializeField]
  private float brightness;
  private SpriteRenderer sprite;





  //constructor
  //public DotClass(float _hue, float _saturation, float _brightness)
  //{
  //  hue = 229;
  //  saturation = 30;
  //  brightness = 96;
  //}


  // Use this for initialization
  void Start()
  {
    sprite = gameObject.GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    sprite.color = Color.HSVToRGB(hue, saturation, brightness);
  }

  public void SetColor(float _hue, float _saturation, float _brightness)
  {
    hue = _hue / 360;
    saturation = _saturation / 100;
    brightness = _brightness / 100;

  }
}
