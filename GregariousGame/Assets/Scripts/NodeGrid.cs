using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{

  public class NodeGrid : MonoBehaviour
  {
    public float cellSize = 0.2f;
    private float gridOffset;
    private Vector2 currentPosition = new Vector2(0, 0);
    private int numPointsX;// number of grid points from left to right
    private int numPointsY; //number of grid points from top to bottom
    private Rect playField = new Rect(0, 0, Screen.width, Screen.height);
    private Node[,] grid;
    private List<Node> availableNodes = new List<Node>();
    private List<Node> unavailableNodes = new List<Node>();
    private Vector2 startPosition;
    public int numGuests = 30;


    public GameObject partyGuest;
    public GameObject player;


    // Use this for initialization
    void Awake()
    {
      gridOffset = cellSize;
      var cameraObj = GameObject.FindWithTag("MainCamera");
      Camera camera = cameraObj.GetComponent<Camera>();
      startPosition = camera.ScreenToWorldPoint(new Vector2(0, 0)) + new Vector3(gridOffset,gridOffset,0);
      var screenSize = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth,camera.pixelHeight));
      screenSize.x *= 2;
      screenSize.y *= 2;
      numPointsX = (int)Mathf.Round((screenSize.x - gridOffset) / cellSize);
      numPointsY = (int)Mathf.Round((screenSize.y - gridOffset) / cellSize);
      grid = new Node[numPointsX, numPointsY];

      GenerateGrid();
      GetNeighborNodes();



      Instantiate(player, new Vector2(0, 0), Quaternion.identity);
      PopulateParty(numGuests);


    }

    void Update()
    {
      DrawNodeConnections();
    }

    private void GenerateGrid()
    {

      for(int i = 0; i < numPointsX; i++)
      {
        for(int j = 0; j < numPointsY; j++)
        {
          currentPosition.x = startPosition.x + (cellSize * i);
          currentPosition.y = startPosition.y + (cellSize * j);
          Node newNode = new Node(i, j, currentPosition);
          grid[i, j] = newNode;
          availableNodes.Add(newNode);
          //Instantiate(partyGuest, grid[i,j].pos, Quaternion.identity);
        }
      }

    }



    private void GetNeighborNodes()
    {
      foreach(Node _node in grid)
      {
        for(int i = -1; i <=1; i++)
        {
          for(int j = -1; j <= 1; j++)
          {
            if(_node.gridX + i < grid.GetLength(0) && _node.gridY + j < grid.GetLength(1))
            {
              if(_node.gridX + i >= 0 && _node.gridY + j >= 0)
              {
                _node.neighborNodes.Add(grid[_node.gridX + i, _node.gridY + j]);
              }
            }
          }
        }
      }
    }


    private void DrawNodeConnections()
    {
      Dictionary<Node[], bool> alreadyDrawn = new Dictionary<Node[], bool>();
      foreach (Node _node in grid)
      {
        foreach(Node _connection in _node.neighborNodes)
        {
          Node[] complimentPair = { _connection, _node };
          if (!alreadyDrawn.ContainsKey(complimentPair))
          {
            Debug.DrawLine(_node.pos, _connection.pos, Color.blue);
            alreadyDrawn.Add(complimentPair, true);
          }

        }
      }
    }






    private void PopulateParty(int _numGuests)
    {
      for(int i = 0; i < numGuests; i++)
      {
        Node randomNode = availableNodes[Random.Range(0, availableNodes.Count)];
        Instantiate(partyGuest, randomNode.pos, Quaternion.identity);
        unavailableNodes.Add(randomNode);
        availableNodes.Remove(randomNode);
      }
    }
  }
}
