using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    [SerializeField] int raycasts;
    [SerializeField] float chokeDistance;
    [SerializeField] float percentage;
    int angleStep;

    [SerializeField] public float healAmount;
    public bool hasHealed;

    [SerializeField] public Material hasntHealedMaterial;
    [SerializeField] public Material healedMaterial;
    [SerializeField] public ParticleSystem ps;

    public List<GameObject> players = new();

    int collisionsWithSegment;
    void Start()
    {
        angleStep = 360 / raycasts;
        hasHealed = false;

        var pcs = FindObjectsByType<PlayerController>(0);

        foreach ( PlayerController pc in pcs ) 
        {
            players.Add(pc.gameObject);
        }
    }

    private void FixedUpdate()
    {
        
        if (!hasHealed)
        {
            GetComponent<Renderer>().material = hasntHealedMaterial;
            var forward = transform.right;
            var begining = new Vector3(transform.position.x, 1.0f, transform.position.z);

            int currentStep = angleStep;
            for (int i = 0; i < raycasts; i++)
            {
                var newAngle = Quaternion.AngleAxis(i * angleStep, Vector3.up) * forward;

                currentStep += angleStep;
                Debug.DrawRay(begining, newAngle * 10.0f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(begining, newAngle, out hit, chokeDistance))
                {
                    if (hit.transform.tag == "Segment" || hit.transform.tag == "FlameEffect")
                    {
                        collisionsWithSegment++;
                    }

                    
                }
            }

            if (collisionsWithSegment >= raycasts * percentage)
            {
                Heal();
            }
        }
        else
        {
            GetComponent<Renderer>().material = healedMaterial;
        }
        
    }

    void Heal()
    {
        foreach(var player in players) 
        {
            player.GetComponent<HealthComponent>().ForceHeal(healAmount);
        }
        hasHealed = true;
        ps.Emit(50);
    }
}
