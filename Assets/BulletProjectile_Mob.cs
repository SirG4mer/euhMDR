using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile_Mob : MonoBehaviour
{

    private Rigidbody bulletRigidbody;
    private int p=0;

    private void Awake(){
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        float speed=5f;
        bulletRigidbody.velocity=transform.forward * speed;

    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="HERRO")
        {
            other.transform.GetComponent<Controller>().ApplyDammage();

        }
        
        if(gameObject.name!="Mob_Bullet"){
            Destroy(gameObject);

        }
    }
    private void OnTriggerEnter(Collider other) {
        // if(other.GetComponent<BulletTarget>() != null){
        //     //Hit target
        // }else{
        //     //Hit someth
        // }
        /*oooooooooooooooooooooooooooooooooooo");
        Destroy(other.gameObject);*/
        
        
    }

    // Update is called once per frame

}
