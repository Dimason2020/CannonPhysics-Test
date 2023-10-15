using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CannonWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem shootParticle;
    [Inject] private MasterObjectPooler masterObjectPooler;
    [Inject] private InputHandler inputHandler;

    private void Start()
    {
        inputHandler.OnMouseClicked += Shoot;
    }

    public void Shoot() 
    {
        Projectile bullet = (Projectile)masterObjectPooler.GetObjectComponent<IPoolable>(projectilePool.PoolName);
        bullet.Init(shootPoint.position, shootPoint.rotation, shootPoint.forward * inputHandler.PowerValue);

        shootParticle.Play();
    }
}
