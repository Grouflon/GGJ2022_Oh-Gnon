using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class OneShotFX : MonoBehaviour
{
    private void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        var mainModule = ps.main;
        Destroy(gameObject, mainModule.startLifetime.constant + 0.1f);
    }
}