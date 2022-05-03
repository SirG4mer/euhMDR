using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    private Rigidbody bulletRigidbody;
    private int p=0;

    private void Awake(){
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        float speed=10f;
        bulletRigidbody.velocity=transform.forward * speed;

    }

    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.tag=="ENNEMI")
        {
            other.transform.GetComponent<enemyAI>().ApplyDammage();

        }
        
        if(gameObject.name!="bulletNormal"){
            Destroy(gameObject);

        }
    }
    private void OnTriggerEnter(Collider other) {
        // if(other.GetComponent<BulletTarget>() != null){
        //     //Hit target
        // }else{
        //     //Hit someth
        // }
        /*
        Debug.Log("poooooooooooooooooooooooooooooooooooooo");
        Destroy(other.gameObject);*/
        
        
    }

    // Update is called once per frame

}
