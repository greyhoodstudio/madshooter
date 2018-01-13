using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class LobbyGrid : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    //grid settings
    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridSize;

    //initialization
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        MakeLobbyGrid();
        UpdateMesh();
    }

    void MakeLobbyGrid()
    {
        //set array sizes
        vertices = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];

        //set tracker integers
        int v = 0;
        int t = 0;

        //set vertex offset
        float vertexOffset = cellSize * 0.5f;

        //generate cells
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                //set cell offset
                Vector3 cellOffset = new Vector3(x * cellSize, y * cellSize);

                //populate vertex array
                vertices[v] = new Vector3 (-vertexOffset, -vertexOffset, 0) + cellOffset + gridOffset;
                vertices[v+1] = new Vector3 (-vertexOffset, vertexOffset, 0) + cellOffset + gridOffset;
                vertices[v+2] = new Vector3 (vertexOffset, -vertexOffset, 0) + cellOffset + gridOffset;
                vertices[v+3] = new Vector3 (vertexOffset, vertexOffset, 0) + cellOffset + gridOffset;

                //populate triangle array
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;

                //update tracker integers
                v += 4;
                t += 6;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }
}
