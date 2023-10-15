using QFSW.MOP2;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour, IPoolable
{
    [Inject] private MasterObjectPooler masterObjectPooler;
    [SerializeField] private MeshFilter projectileMesh;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private ObjectPool particlesPool;

    [SerializeField] private int bouncesToExplosion = 2;
    [SerializeField] private float meshDistortion = 0.1f;

    private int collidingCount;

    private readonly Vector3[] _vertices = new[]
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
    private readonly int[] _triangles = new int[]
    {
        0, 1, 2, 0, 2, 3,
        4, 5, 6, 4, 6, 7,
        8, 9, 10, 8, 10, 11,
        12, 13, 14, 12, 14, 15,
        16, 17, 18, 16, 18, 19,
        20, 21, 22, 20, 22, 23,
    };

    private void Awake()
    {
        physicsBody.OnProjectileCollided += OnCollided;
    }

    public void Init(Vector3 position, Quaternion rotation, Vector3 push)
    {
        transform.position  = position;
        transform.rotation  = rotation;
        physicsBody.MoveVelocity     = push;
        collidingCount = 0;

        UpdateMesh();
    }

    private void UpdateMesh()
    {
        var mesh = projectileMesh.mesh;

        var distorted = _vertices.Select(x => x + Random.insideUnitSphere * meshDistortion).ToArray();

        var vertices = new Vector3[]
        {
            distorted[0], distorted[1], distorted[2], distorted[3],
            distorted[3], distorted[2], distorted[5], distorted[6],
            distorted[6], distorted[5], distorted[4], distorted[7],
            distorted[7], distorted[4], distorted[1], distorted[0],
            distorted[1], distorted[4], distorted[5], distorted[2],
            distorted[0], distorted[3], distorted[6], distorted[7],
        };

        mesh.vertices = vertices;
        mesh.triangles = _triangles;
        mesh.RecalculateNormals();
    }

    private void OnCollided(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Wall>(out var stand)) 
            stand.DrawHit(hit);

        collidingCount++;
        
        if (collidingCount >= bouncesToExplosion) 
            Explode();
    }

    private void Explode()
    {
        masterObjectPooler.GetObjectComponent<ExplosionParticle>(particlesPool.PoolName, transform.position, Quaternion.identity);
        Release();
    }

    public void Release()
    {
        masterObjectPooler.Release(gameObject, projectilePool.PoolName);
    }
}