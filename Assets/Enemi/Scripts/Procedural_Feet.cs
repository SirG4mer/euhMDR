using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural_Feet : MonoBehaviour
{

    static Vector3[] CastOnSurface(Vector3 point, float halfRange, Vector3 up)
    {
        Vector3[] res = new Vector3[2];
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(point.x, point.y + halfRange, point.z), -up);

        if (Physics.Raycast(ray, out hit, 2f * halfRange))
        {
            res[0] = hit.point;
            res[1] = hit.normal;
        }
        else
        {
            res[0] = point;
        }
        return res;
    }

    public Transform RightFoot;
    public Transform LeftFoot;
    public float speed = 0.9f;
    public float stepHeight = 0.1f;
    public float StepDistance = 1.0f;
    public bool right_foot_first = true;

    private Vector3 initRightFoot;
    private Vector3 initLeftFoot;
    private Vector3 lastRightFootPos;
    private Vector3 lastLeftFootPos;
    private Vector3 initBodyPos;
    private Vector3 lastBodyPos;
    private Vector3 nextPosition;

    private float foot_distance;
    private float current_distance_right;
    private float current_distance_left;
    private float foot_height;
    private float moving = 1.0f;

    private int forward = 0;
    private int moving_right = 0;
    private int lastForward = 0;
    private int last_moving_right = 0;
    private bool move_right_foot;

    //Retourne vrai si le personnage doit prendre un pas.
    bool step()
    {
        if(move_right_foot)
        {

            current_distance_right = Vector3.Distance(lastBodyPos, lastRightFootPos);
            if (current_distance_right < 0)
                current_distance_right = -current_distance_right;
            if (current_distance_right > foot_distance)
                return true;
            else
                return false;
        }
        else
        {
            current_distance_left = Vector3.Distance(lastBodyPos, lastLeftFootPos);
            if (current_distance_left < 0)
                current_distance_left = -current_distance_left;
            if (current_distance_left > foot_distance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }

    //Calcule si le personnage est en train de se deplacer.
    void isMoving()
    {
        lastForward = forward;
        last_moving_right = moving_right;
        Vector3 currentPos = transform.position;
        if(lastBodyPos.x == currentPos.x)
        {
            forward = 0;
        }
        else
        {
            if (lastBodyPos.x > 0)
            {
                if (currentPos.x > 0)
                {
                    if ((lastBodyPos.x - currentPos.x) > 0)
                        forward = -1;
                    else
                        forward = 1;
                }
                else
                {
                    forward = -1;
                }
            }
            else
            {
                if (currentPos.x > 0)
                {
                    forward = 1;
                }
                else
                {
                    if ((lastBodyPos.x - currentPos.x) > 0)
                    {
                        forward = -1;
                    }
                    else
                    {
                        forward = 1;
                    }
                }
            }
        }
        if (lastBodyPos.z == currentPos.z)
        {
            moving_right = 0;
        }
        else
        {
            if (lastBodyPos.z > 0)
            {
                if (currentPos.z > 0)
                {
                    if ((lastBodyPos.z - currentPos.z) > 0)
                        moving_right = -1;
                    else
                        moving_right = 1;
                }
                else
                {
                    moving_right = -1;
                }
            }
            else
            {
                if (currentPos.z > 0)
                {
                    moving_right = 1;
                }
                else
                {
                    if ((lastBodyPos.z - currentPos.z) > 0)
                    {
                        moving_right = -1;
                    }
                    else
                    {
                        moving_right = 1;
                    }
                }
            }
        }
        
    }

    //Gere le calcul pour le prochain pas.
    void nextStep()
    {
        isMoving();
        if (moving >= 1)
        {
            Vector3 velocity = transform.position - lastBodyPos;
            Vector3 lastFootPos;
            if (move_right_foot)
                lastFootPos = lastRightFootPos;
            else
                lastFootPos = lastLeftFootPos;
            Vector3 currentBodyPos = transform.position;
            nextPosition.y = currentBodyPos.y - foot_height;
            if (forward == 1)
                nextPosition.x = currentBodyPos.x + 2 * StepDistance;
            else if (forward == -1)
                nextPosition.x = currentBodyPos.x - 2 * StepDistance;
            else
                nextPosition.x = lastFootPos.x;
            if (moving_right == 1)
                nextPosition.z = currentBodyPos.z + StepDistance;
            else if (moving_right == -1)
                nextPosition.z = currentBodyPos.z - StepDistance;
            else
                nextPosition.z = lastFootPos.z;
            
            moving = 0;

            Vector3[] posAndNormal = CastOnSurface(nextPosition, 2f, transform.up);
            nextPosition = posAndNormal[0];

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if(move_right_foot)
                    RightFoot.rotation = Quaternion.LookRotation(Vector3.Cross(transform.TransformDirection(Vector3.right), hit.normal));
                else
                    LeftFoot.rotation = Quaternion.LookRotation(Vector3.Cross(transform.TransformDirection(Vector3.right), hit.normal));
            }
        }
        FootMovement();
    }

    //S'occupe des placements de pieds.
    void FootMovement()
    {
        Vector3 bodyPos = transform.position;
        float height = lastBodyPos.y - bodyPos.y;
        if (moving < 1)
        {
            if (move_right_foot)
            {
                Vector3 footPosition = Vector3.Lerp(lastRightFootPos, nextPosition, moving);
                if (forward == 1 || moving_right == 1)
                    footPosition.y += Mathf.Sin(moving * Mathf.PI) * stepHeight;
                else
                    footPosition.y += Mathf.Sin(moving * Mathf.PI) * stepHeight;
                RightFoot.position = footPosition;
                lastRightFootPos = footPosition;


                lastLeftFootPos.y = lastLeftFootPos.y + height;
                LeftFoot.position = lastLeftFootPos;
                lastBodyPos = transform.position;
            }
            else
            {
                Vector3 footPosition = Vector3.Lerp(lastLeftFootPos, nextPosition, moving);
                if (forward == 1 || moving_right == 1)
                    footPosition.y += Mathf.Sin(moving * Mathf.PI) * stepHeight;
                else
                    footPosition.y += Mathf.Sin(moving * Mathf.PI) * stepHeight;
                LeftFoot.position = footPosition;
                lastLeftFootPos = footPosition;


                lastRightFootPos.y = lastRightFootPos.y + height;
                RightFoot.position = lastRightFootPos;
                lastBodyPos = transform.position;
            }
            
            moving += Time.deltaTime * speed;
            if(moving >= 1)
                move_right_foot = !move_right_foot;
        }
        else
        {
            lastRightFootPos.y = lastRightFootPos.y + height;
            lastLeftFootPos.y = lastLeftFootPos.y + height;
            RightFoot.position = lastRightFootPos;
            LeftFoot.position = lastLeftFootPos;
            lastBodyPos = transform.position;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        initRightFoot = RightFoot.position;
        initLeftFoot = LeftFoot.position;
        lastRightFootPos = RightFoot.position;
        lastLeftFootPos = LeftFoot.position;
        initBodyPos = transform.position;
        lastBodyPos = transform.position;

        Vector3 tmp;
        if (right_foot_first)
            tmp = initRightFoot;
        else
            tmp = initLeftFoot;
        tmp.x = tmp.x + StepDistance;

        foot_distance = Vector3.Distance(initBodyPos, tmp) + 0.5f;
        if (foot_distance < 0)
            foot_distance = -foot_distance;
        foot_height = initBodyPos.y - initRightFoot.y;

        move_right_foot = !right_foot_first;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (step())
        {
            nextStep();
        }
        else
        {
            FootMovement();
        }
    }

    //Dessine les targets dans la vue scene.
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(RightFoot.position, 0.2f);
        Gizmos.DrawWireSphere(LeftFoot.position, 0.2f);
    }
}
