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
    void Start()
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

      GenerateGrid();
      Instantiate(player, new Vector2(0, 0), Quaternion.identity);
      PopulateParty(numGuests);


    }

    void Update()
    {
      
    }

    private void GenerateGrid()
    {
      grid = new Node[numPointsX, numPointsY];
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

    private void PopulateParty(int _numGuests)
    {
      for(int i = 0; i < numGuests; i++)
      {
        Node randomNode = availableNodes[(int)(Random.Range(0, availableNodes.Count))];
        Instantiate(partyGuest, randomNode.pos, Quaternion.identity);
        unavailableNodes.Add(randomNode);
        availableNodes.Remove(randomNode);
      }
    }
  }
}
