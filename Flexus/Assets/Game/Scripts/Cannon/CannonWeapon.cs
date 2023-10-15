using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CannonWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem shootParticle;
    [Inject] private InputHandler inputHandler;

    private void Start()
    {
        inputHandler.OnMouseClicked += Shoot;
    }

    public void Shoot() 
    {
        Projectile bullet = Instantiate(projectile);
        bullet.Init(shootPoint.position, shootPoint.rotation, shootPoint.forward * inputHandler.PowerValue);

        shootParticle.Play();
    }
}
