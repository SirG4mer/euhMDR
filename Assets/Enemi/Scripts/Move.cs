using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float _speed = 1.0f;
    public float time;
    public int rotation;

    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_rigidBody.velocity.magnitude < _speed)
        {
            
            float value = Random.Range(0.0f, 1.0f);
            if(time < 5 && time >= 0)
            {
                if (value != 0 && (rotation == 90 || rotation == -90))
                    _rigidBody.AddForce(value * Time.fixedDeltaTime * 1000.0f, 0, 0);
                if (value != 0 && (rotation == 0))
                    _rigidBody.AddForce(0, 0, value * Time.fixedDeltaTime * 1000.0f);
                time += Time.deltaTime;
                if (time > 5)
                    time = -5.0f; ;
            }
            else
            {
                value = -value;
                if (value != 0 && (rotation == 90 || rotation == -90))
                    _rigidBody.AddForce(value * Time.fixedDeltaTime * 1000.0f, 0, 0);
                if (value != 0 && (rotation == 0))
                    _rigidBody.AddForce(0, 0, value * Time.fixedDeltaTime * 1000.0f);
                time += Time.deltaTime;

            }
            
            

        }
    }
}
