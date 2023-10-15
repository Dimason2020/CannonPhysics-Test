using QFSW.MOP2;
using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour, IPoolable
{
    [Inject] private MasterObjectPooler masterObjectPooler;

    [SerializeField] private RandomMeshHandler randomMeshHandler;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private ObjectPool particlesPool;
    [SerializeField] private int bouncesToExplosion = 2;
    private int collidingCount;

    private void Start()
    {
        physicsBody.OnProjectileCollided += OnCollided;
    }

    public void Init(Vector3 position, Quaternion rotation, Vector3 push)
    {
        transform.position  = position;
        transform.rotation  = rotation;
        physicsBody.MoveVelocity     = push;
        collidingCount = 0;

        randomMeshHandler.UpdateMesh();
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