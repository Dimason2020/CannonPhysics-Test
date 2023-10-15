using QFSW.MOP2;
using UnityEngine;
using Zenject;

public class ExplosionParticle : MonoBehaviour, IPoolable
{
    [Inject] private MasterObjectPooler masterObjectPooler;
    [SerializeField] private ObjectPool particlePool;

    private void OnParticleSystemStopped()
    {
        Release();
    }

    public void Release()
    {
        masterObjectPooler.Release(gameObject, particlePool.PoolName);
    }
}
