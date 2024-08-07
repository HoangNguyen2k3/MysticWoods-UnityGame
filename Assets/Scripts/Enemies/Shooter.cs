using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour,IEnemy
{
    [SerializeField] private GameObject bulletPrefab; //bulletPrefab for enemy
    [SerializeField] private float bulletMoveSpeed; //declare bullet speed
    [SerializeField] private int burstCount;//Num of burst
    [SerializeField] private float timeBetweenBurst;//time between 2 burst
    [SerializeField] private float restTime=1f;//time enemy can attack next section

    [SerializeField] private int projectilePerBurst; //Num of bullet in one burst
    [SerializeField] [Range(0,359)] private float angleSpread;//angle of bullet
    [SerializeField] private float startingDistant = 0.1f; //Distance start fire bullet

    [SerializeField] private bool stagger;
    [Tooltip("Stagger must be enable for oscillate to function properly")]
    [SerializeField] private bool oscillate;

    private bool isShooting = false;

    private void OnValidate()
    {
        if (oscillate) { stagger = true; }
        if (!oscillate) { stagger = false; }
        if (projectilePerBurst < 1) { projectilePerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBurst < 0.1f) { timeBetweenBurst = 0.1f; }
        if (restTime < 0.1f) { restTime = 0.1f; }
        if (startingDistant < 0.1f) { startingDistant = 0.1f; }
        if (angleSpread == 0) { projectilePerBurst = 1; }
        if (bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }
    }

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRountine());
        }
    }
    //Attack continuous normal
    /* private IEnumerator ShootRountine()
     {
         isShooting=true;
         for(int i = 0; i < burstCount; i++)
         {
             Vector2 targetDirection = Playercontroller.Instance.transform.position - transform.position;//Make direction to attack player

             GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);// Create new bullet
            newBullet.transform.right = targetDirection;// Create direction for bullet
            if(newBullet.TryGetComponent(out Projectile projectile))
             {
                 projectile.UpdateMoveSpeed(bulletMoveSpeed);
             }
            yield return new WaitForSeconds(timeBetweenBurst);
         }
         yield return new WaitForSeconds(restTime);
         isShooting = false;
     }*/
    //Attack circle 
    /*   private IEnumerator ShootRountine()
       {
           isShooting = true;
           float startAngle, currentAngle, angleStep;
           TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);


           for (int i = 0; i < burstCount; i++)
           {
               for (int j = 0; j < projectilePerBurst; j++)
               {
               Vector2 pos = FindBulletSpawnPos(currentAngle);
               GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
               newBullet.transform.right = newBullet.transform.position-transform.position;

               if (newBullet.TryGetComponent(out Projectile projectile))
               {
                   projectile.UpdateMoveSpeed(bulletMoveSpeed);
               }
                   currentAngle += angleStep;
           }
               currentAngle = startAngle;
               yield return new WaitForSeconds(timeBetweenBurst);
               TargetConeOfInfluence(out startAngle,out currentAngle, out angleStep);
           }
           yield return new WaitForSeconds(restTime);
           isShooting = false;
       }*/
    private IEnumerator ShootRountine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (stagger) { timeBetweenProjectiles = timeBetweenBurst / projectilePerBurst; }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }


            for (int j = 0; j < projectilePerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;


                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;

                if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currentAngle = startAngle;

            if (!stagger) { yield return new WaitForSeconds(timeBetweenBurst); }
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = Playercontroller.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilePerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }


    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistant * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistant * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }

}
