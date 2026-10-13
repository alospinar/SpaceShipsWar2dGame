using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int scoreValue = 50;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioManager audioManager;

    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = FindFirstObjectByType<AudioManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if we hit a damage dealer
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if( damageDealer != null)
        {
            //redice health
            TakeDamage(damageDealer.GetDamage());
            PlayHitParticles();
            damageDealer.Hit();
            audioManager.PlayDamageSFX();
            

            if(applyCameraShake)
            {
                cameraShake.play();
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if(isPlayer)
        {
            levelManager.LoadGameOver();
        }
        else
        {
            scoreKeeper.ModifyScore(scoreValue);
        }
        Destroy(gameObject); 
    }

    void PlayHitParticles()
    {
        if(hitParticles != null)
        {
            ParticleSystem  particles =Instantiate(hitParticles,transform.position,Quaternion.identity);
            Destroy(particles,particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
