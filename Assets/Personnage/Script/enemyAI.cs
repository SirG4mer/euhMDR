using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    //Distance joueur ennemi
    private float Distance;

    // Distance entre l'ennemi et sa position de base
    private float DistanceBase;
    private Vector3 basePositions;

    // Cible de l'ennemi
    public Transform Target;

    

   
    // Agent de navigation
    private UnityEngine.AI.NavMeshAgent agent;

    // Animations de l'ennemi
    Animation animations;

    // Vie de l'ennemi
    public float enemyHealth=10;
    private bool isDead = false;
    private bool canShoot=true;
    public int speedshoot;
    private int lastshoot=0;

    public Transform pfBulletProjectile;
    public Transform spawnBulletPosition;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animation>();
        basePositions = transform.position;
        //agent.destination = Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isDead)
        {
            lastshoot++;
            Distance = Vector3.Distance(Target.position, transform.position);
            DistanceBase = Vector3.Distance(basePositions, transform.position);


            // Quand l'ennemi est proche mais pas assez pour attaquer
            if (Distance <=30)
            {
                chase();
            }else{
                idle();
            }

            // Quand le joueur s'est Ã©chappÃ©
            

        }
       

    }

    private void chase()
    {
        agent.destination = Target.position;
        if (lastshoot>=speedshoot)
        {
            attaque();
            lastshoot = 0;
        }
    }

    private void idle()
    {
        agent.destination = basePositions;
    }

    public void attaque(){
        Vector3 aimDir= (Target.position-spawnBulletPosition.position).normalized;
        Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.down));
    }

    public void ApplyDammage(){
        enemyHealth--;
        if(enemyHealth<=0)
        {
            Dead();
        }
    }

    public void ApplySuperDammage(){
        enemyHealth=0;
            Dead();
    }

    public void Dead(){
        Destroy(gameObject);
    }

    public void BackBase() {
        //agent.destination = basePositions;
    }

}
