using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(ParticleSystem))]
public class FireworkParticleSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _launchSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private float _volume;

    private ParticleSystem _particleSystem;
    private Particle[] _particles;

    void Start()
    {       
        _particleSystem = GetComponent<ParticleSystem>();
        _particles = new Particle[_particleSystem.main.maxParticles];      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _particleSystem.Play();
            _launchSound.Play();
        }
        int particleCount = _particleSystem.GetParticles(_particles);
        for (int i = 0; i < particleCount; i++)
        {
            if (_particles[i].remainingLifetime <= 0.02f)
            {
                Vector3 particlePosition = transform.TransformPoint(_particles[i].position);
                AudioSource.PlayClipAtPoint(_explosionSound, particlePosition, _volume);
            }
            _particleSystem.Stop(); 
        }
    }
}

