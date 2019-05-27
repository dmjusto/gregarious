using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{

  public class Node
  {
    private int gridX;
    private int gridY;
    public Vector2 pos;

    public Node parent;


    public Node(int _gridX, int _gridY,Vector2 _position)
    {
      gridX = _gridX;
      gridY = _gridY;
      pos = _position;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
  }
}
