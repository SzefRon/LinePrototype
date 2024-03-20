using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class MinionScript2 : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject enemyTarget;

    public float currentSpeed = 0;
    public float followSpeed;
    public float numRaycast;

    public float masterDistance;
    public float enemyFollowRange;

    float angleStep;

    void Awake()
    {
        angleStep = 360.0f / numRaycast;
        PlayerController[] pcs = Object.FindObjectsByType<PlayerController>(0);
        player1 = pcs[0].gameObject;
        player2 = pcs[1].gameObject;
        enemyTarget = null;
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < numRaycast; i++)
        {
            var newAngle = Quaternion.AngleAxis(i * angleStep, Vector3.up) * transform.forward;
            Debug.DrawRay(transform.position, newAngle * enemyFollowRange, Color.yellow);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, newAngle, out hit, enemyFollowRange))
            {
                if(hit.transform.tag == "Monster")
                {
                    enemyTarget = hit.transform.gameObject;
                    break;
                }
                else
                {
                    enemyTarget = null; 
                }
            }
        }

        if(enemyTarget is not null)
        {
            Vector3 direction = (enemyTarget.transform.position - transform.position).normalized;
            var targetPos = transform.position + direction * followSpeed;
            var newPos = Vector3.Lerp(transform.position, targetPos, 0.1f);
            transform.position = newPos;
        }
        else
        {
            var p1d = Vector3.Distance(player1.transform.position, transform.position);
            var p2d = Vector3.Distance(player2.transform.position, transform.position);

            var distance = Mathf.Min(p1d, p2d);
            Vector3 followPoint;

            if(p1d < p2d) 
            {
                followPoint = player1.transform.position;
            } 
            else
            {
                followPoint = player2.transform.position;
            }

            if(distance > masterDistance) 
            {
                Vector3 direction = (followPoint - transform.position).normalized;
                var targetPos = transform.position + direction * followSpeed;
                var newPos = Vector3.Lerp(transform.position, targetPos, 0.1f);
                transform.position = newPos;
            }
            
        }
    }
}
