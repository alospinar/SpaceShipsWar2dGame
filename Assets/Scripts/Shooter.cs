using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Base Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFireRate = 0.2f;
    
    [Header("IA Variables")]
    [SerializeField] bool useAI;
    [SerializeField] float minimuFireRate= 0.2f;
    [SerializeField] float fireRateVariance= 0f;

    [HideInInspector] public bool isFiring;
    Coroutine fireCoroutine;
    AudioManager audioManager;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        if(useAI)
        {
            isFiring = true;
        }
    }
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && fireCoroutine == null)
        {
            //Repatedly fire projectiles
            fireCoroutine = StartCoroutine(FireContinuosly());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            //STOP firing projectiles
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            
            GameObject projectile = Instantiate(projectilePrefab,transform.position,Quaternion.identity);

            projectile.transform.rotation = transform.rotation;

            Rigidbody2D  projectileRB= projectile.GetComponent<Rigidbody2D>();
            projectileRB.linearVelocity= transform.up * projectileSpeed;
            
            Destroy(projectile,projectileLifetime);
            
            float waitTime = Random.Range(baseFireRate - fireRateVariance,
            baseFireRate + fireRateVariance);
            waitTime = Mathf.Clamp(waitTime, minimuFireRate, float.MaxValue);

            audioManager.PlayShootingSFX();

            yield return new WaitForSeconds(waitTime) ;
        }

        
    }



}
