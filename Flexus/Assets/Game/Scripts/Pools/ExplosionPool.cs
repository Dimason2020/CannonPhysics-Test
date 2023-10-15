using UnityEngine;

public class ExplosionPool : Pool<ParticleSystem>
{
    private static ExplosionPool _instance;

    public static ExplosionPool Instance
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<ExplosionPool>();
            return _instance;
        }
    }
}