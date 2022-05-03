using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    public Transform LegTarget;
    public Transform ArmTarget;
    float lastArmz;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 tmp = ArmTarget.position;
        lastArmz = tmp.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = LegTarget.position;
        target.z = lastArmz;
        ArmTarget.position = target;
    }
}
