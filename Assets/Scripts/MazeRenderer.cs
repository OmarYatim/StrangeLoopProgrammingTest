using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] private int MazeSize;
    [SerializeField] private float WallSize = 1;
    [SerializeField] private Transform MazeParent;
    [SerializeField] private Transform WallPrefab;

    private Vector2 EntryPoint = new Vector2(0, 0);

    private Vector3 MazeCenter;

    // Start is called before the first frame update
    void Start()
    {
        MazeCenter = new Vector3(MazeSize / 2 * 10, 0, MazeSize / 2 * 10);
        var Maze = MazeGenerator.CreateMaze(MazeSize);
        DrawMaze(Maze);
    }

    private void DrawMaze(WallState[,] maze)
    {
        GameObject MazePlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        NavMeshSurface Surface = MazePlane.AddComponent(typeof(NavMeshSurface)) as NavMeshSurface;
        MazePlane.transform.localPosition = MazeCenter;
        MazePlane.transform.SetParent(MazeParent);
        MazePlane.transform.localScale = new Vector3(MazeSize, 1, MazeSize);


        for (int i = 0; i < MazeSize; i++)
        {
            for(int j = 0; j < MazeSize; j++)
            {
                var cell = maze[i, j];
                var position = new Vector3(i* MazeSize + WallSize / 2, 0,j * MazeSize + WallSize / 2);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(WallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, WallSize / 2);
                    topWall.localScale = new Vector3(WallSize, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(WallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-WallSize / 2, 0, 0);
                    leftWall.localScale = new Vector3(WallSize, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == MazeSize - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(WallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+WallSize / 2, 0, 0);
                        rightWall.localScale = new Vector3(WallSize, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(WallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -WallSize / 2);
                        bottomWall.localScale = new Vector3(WallSize, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }
        }

        Surface.BuildNavMesh();
    }

}
