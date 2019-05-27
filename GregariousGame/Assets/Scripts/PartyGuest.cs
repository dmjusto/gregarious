using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DotClass))]
public class PartyGuest : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
  {
    var _hue = Random.Range(1, 360);
    var _saturation = 50;
    var _brightness = Random.Range(60, 100);
    gameObject.GetComponent<DotClass>().SetColor(_hue, _saturation, _brightness);
  }
	
	// Update is called once per frame
	void Update () 
  {
		
	}
}
