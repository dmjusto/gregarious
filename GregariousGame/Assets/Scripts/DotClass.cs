using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DotClass : MonoBehaviour {

  [Range(0.0f, 1.0f)]
  public  float hue;
  [Range(0.3f, 1.0f)]
  public  float saturation;
  [Range(0.0f, 1.0f)]
  public  float brightness;
  private SpriteRenderer sprite;

  //constructor
  public DotClass(float _hue, float _saturation, float _brightness)
  {
    SetColor(_hue, _saturation, _brightness);
  }


	// Use this for initialization
	void Start () 
  {
    sprite = gameObject.GetComponent<SpriteRenderer>();
    SetColor(229, 30, 96);
	}
	
	// Update is called once per frame
	void Update () 
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
