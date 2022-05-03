using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDecors: MonoBehaviour
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
        
        if(other.gameObject.tag!="ENNEMI" && other.gameObject.tag!="sol" && other.gameObject.tag!="MainCamera")
        {
            Destroy(other.gameObject);
        }
        if(gameObject.name!="bulletDecors"){
            Destroy(gameObject);

        }
        
    }
}
