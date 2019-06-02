using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour 
{

  /*
   * Singleton pattern
   */
  private static GridManager s_Instance = null;

  private GridManager() { }

  public static GridManager instance
  {
    get
    {
      if(s_Instance == null)
      {
        s_Instance = FindObjectOfType(typeof(GridManager)) as GridManager;
        if(s_Instance == null)
        {
          Debug.Log("Could not locate a GridManager Object");
        }
      }
      return s_Instance;
    }
  }
  /*
   * **********************
   */  


  public int numRows;
  public int numColumns;
  public float cellSize;
  public bool showGrid = true;
  public bool showObstacleBlocks = true;

  private Vector2 origin = new Vector2();
  private GameObject[] obstacleList;
  public Node[,] grid { get; set; }
  public Vector2 Origin
  {
    get { return origin; }
  }


  private void Awake()
  {
    obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
    CalculateObstacles();
  }

  private void CalculateObstacles()
  {
    grid = new Node[numColumns, numRows];
    int index = 0;

    for(int i = 0; i <numColumns; i++)
    {
      for(int j = 0; j < numRows; j++)
      {
        Vector2 cellPos = GetGridCellCenter(index);
        Node node = new Node(cellPos);
        grid[i, j] = node;
        index++;
      }
    }


    /*
     * Mark obstcales
     */
    if(obstacleList != null && obstacleList.Length > 0)
    {
      foreach(GameObject data in obstacleList)
      {
        int indexCell = GetGridIndex(data.transform.position);
        int col = GetColumn(indexCell);
        int row = GetRow(indexCell);
        grid[row, col].MarkAsObstacle();
      }
    }
    /*
     * 
     */
  }

  public Vector2 GetGridCellCenter(int index)
  {
    Vector2 cellPosition = GetGridCellPosition(index);
    cellPosition.x += (cellSize / 2.0f);
    cellPosition.y += (cellSize / 2.0f);
    return cellPosition;
  }

  public Vector2 GetGridCellPosition(int index)
  {
    int row = GetRow(index);
    int col = GetColumn(index);
    float xPosInGrid = col * cellSize;
    float yPosInGrid = row * cellSize;

    return Origin + new Vector2(xPosInGrid, yPosInGrid);
  }

  public int GetRow(int index)
  {
    int row = index / numColumns;
    return row;
  }

  public int GetColumn(int index)
  {
    int col = index % numColumns;
    return col;
  }

  public int GetGridIndex(Vector2 pos)
  {
    if (!IsInBounds(pos))
    {
      return -1;
    }

    pos -= Origin;
    int col = (int)(pos.x / cellSize);
    int row = (int)(pos.y / cellSize);
    return (row * numColumns + col);
  }

  public bool IsInBounds(Vector2 pos)
  {
    float width = numColumns * cellSize;
    float height = numRows * cellSize;
    return (pos.x >= Origin.x && pos.x <= Origin.x + width &&
      pos.y <= Origin.y + height && pos.y >= Origin.y);
  }

  public void GetNeighbors(Node node, ArrayList neighbors)
  {
    Vector2 neighborPos = node.pos;
    int neighborIndex = GetGridIndex(neighborPos);

    int row = GetRow(neighborIndex);
    int col = GetColumn(neighborIndex);

    //Bottom Neighbor
    int neighborRow = row - 1;
    int neighborCol = col;
    AssignNeighbor(neighborRow, neighborCol, neighbors);

    //Top Neighbor
    neighborRow = row + 1;
    neighborCol = col;
    AssignNeighbor(neighborRow, neighborCol, neighbors);

    //Left Neighbor
    neighborRow = row;
    neighborCol = col - 1;
    AssignNeighbor(neighborRow, neighborCol, neighbors);

    //Right Neighbor
    neighborRow = row;
    neighborCol = col + 1;
    AssignNeighbor(neighborRow, neighborCol, neighbors);
  }

   void AssignNeighbor(int row, int column, ArrayList neighbors)
  {
    if(row != -1 && column != -1 &&
        row < numRows && column < numColumns)
    {
      Node newNode = grid[row, column];
      if (!newNode.isObstacle)
      {
        neighbors.Add(newNode);
      }
    }
  }


  /*
   * Gizmos Section
   */

  private void OnDrawGizmos()
  {
    if (showGrid)
    {
      DebugDrawGrid(transform.position, numRows, numColumns, cellSize, Color.blue);
    }
    if (showObstacleBlocks)
    {
      Vector2 gridCellSize = new Vector2(cellSize, cellSize);
      if(obstacleList != null && obstacleList.Length > 0)
      {
        foreach(GameObject data in obstacleList)
        {
          Gizmos.DrawCube(GetGridCellCenter(GetGridIndex(data.transform.position)), gridCellSize);
        }
      }
    }
  }


  public void DebugDrawGrid(Vector2 origin, int _numRows, int _numColumns, float _cellSize, Color color)
  {
    float width = (_numColumns * _cellSize);
    float height = (_numRows * _cellSize);

    //Draw horizontal grid lines
    for(int i = 0; i < _numRows; i++)
    {
      Vector2 startPosition = origin + (i * _cellSize * new Vector2(0.0f, 1.0f));
      Vector2 endPosition = (startPosition + width * new Vector2(1.0f, 0.0f));
      Debug.DrawLine(startPosition, endPosition, color);
    }
    //Draw vertical grid lines
    for (int i = 0; i < _numRows; i++)
    {
      Vector2 startPosition = origin + (i * _cellSize * new Vector2(1.0f, 0.0f));
      Vector2 endPosition = (startPosition + width * new Vector2(0.0f, 1.0f));
      Debug.DrawLine(startPosition, endPosition, color);
    }

  }
}
