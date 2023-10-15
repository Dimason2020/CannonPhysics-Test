using System.Linq;
using UnityEngine;

public class RandomMeshHandler : MonoBehaviour
{
    [SerializeField] private MeshFilter projectileMesh;
    [SerializeField] private float meshDistortion = 0.1f;

    private readonly Vector3[] vertices = new[]
{
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, +0.5f, -0.5f),
        new Vector3(+0.5f, +0.5f, -0.5f),
        new Vector3(+0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, +0.5f, +0.5f),
        new Vector3(+0.5f, +0.5f, +0.5f),
        new Vector3(+0.5f, -0.5f, +0.5f),
        new Vector3(-0.5f, -0.5f, +0.5f),
    };
    private readonly int[] triangles = new int[]
    {
        0, 1, 2, 0, 2, 3,
        4, 5, 6, 4, 6, 7,
        8, 9, 10, 8, 10, 11,
        12, 13, 14, 12, 14, 15,
        16, 17, 18, 16, 18, 19,
        20, 21, 22, 20, 22, 23,
    };

    public void UpdateMesh()
    {
        Mesh mesh = projectileMesh.mesh;
        Vector3[] distorted = vertices
            .Select(x => x + Random.insideUnitSphere * meshDistortion).ToArray();

        Vector3[] newVertices = new Vector3[]
        {
            distorted[0], distorted[1], distorted[2], distorted[3],
            distorted[3], distorted[2], distorted[5], distorted[6],
            distorted[6], distorted[5], distorted[4], distorted[7],
            distorted[7], distorted[4], distorted[1], distorted[0],
            distorted[1], distorted[4], distorted[5], distorted[2],
            distorted[0], distorted[3], distorted[6], distorted[7],
        };

        mesh.vertices = newVertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
