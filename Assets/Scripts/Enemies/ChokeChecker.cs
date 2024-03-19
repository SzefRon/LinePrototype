using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChokeChecker : MonoBehaviour
{
    [SerializeField] int raycasts;
    [SerializeField] float chokeDistance;
    [SerializeField] float percentage;
    int angleStep;
    public bool isChoked;
    
    public int collisionsWithSegment;
    public int collisionsWithMonster;

    public MinigameController minigameController;

    // Start is called before the first frame update
    void Start()
    {
        angleStep = 360 / raycasts;
        isChoked = false;
        collisionsWithMonster = 0;
        collisionsWithSegment = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        collisionsWithMonster = 0;
        collisionsWithSegment = 0;
        int layerMask = 288;
        
        int chokeCount = 0;

        var forward = transform.forward;
        var begining = new Vector3(transform.position.x, 1.0f, transform.position.z);

        int currentStep = angleStep;
        for (int i = 0; i < raycasts; i++)
        {
            var newAngle = Quaternion.AngleAxis(i * angleStep, Vector3.up) * forward;
            
            currentStep += angleStep;

            RaycastHit hit;
            if (Physics.Raycast(begining, newAngle, out hit, chokeDistance))
            {
                if(hit.transform.tag == "Segment")
                {
                    collisionsWithSegment++;
                }
                else if(hit.transform.tag == "Monster")
                {
                    collisionsWithMonster++;
                }

                Debug.DrawRay(begining, newAngle * hit.distance, Color.yellow);
            }
        }
        
        chokeCount = collisionsWithMonster + collisionsWithSegment;

        //Debug.Log(chokeCount);

        if (chokeCount >= raycasts * percentage ) 
        {
            Debug.Log("choked!!!");
            GetComponent<EffectComponent>().ApplyRopeEffect(SegmentUpgrades.Choke);
        }
    }
}
