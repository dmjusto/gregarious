using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DotClass))]
public class PlayerController : MonoBehaviour 
{

  private Rigidbody2D rigidbody2D;
  private float xDirection = 0.0f;
  private float yDirection = 0.0f;
  [SerializeField]
  private float speed = 1.0f;


  // Use this for initialization
  void Start () 
  {
    rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    gameObject.GetComponent<DotClass>().SetColor(1, 72, 88);
    
  }
  
  // Update is called once per frame
  void Update () 
  {
    xDirection = Input.GetAxisRaw("Horizontal");
    yDirection = Input.GetAxisRaw("Vertical");
  }

  private void FixedUpdate()
  {
    var velocityX = xDirection * speed;
    var velocityY = yDirection * speed;
    rigidbody2D.velocity = new Vector2(velocityX, velocityY);
  }
}
