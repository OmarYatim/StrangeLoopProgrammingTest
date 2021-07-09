using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NavMeshBuilder : MonoBehaviour
{
    [HideInInspector] public NavMeshSurface SurfaceMesh;

    public static NavMeshBuilder Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SurfaceMesh.BuildNavMesh();
    }

}
