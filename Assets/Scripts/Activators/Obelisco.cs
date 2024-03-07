using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obelisco : MonoBehaviour, IActivator
{
    [SerializeField] public int collisionsToActivation;
    [SerializeField] public SegmentType segmentType;
    [SerializeField] public Activable target;

    [SerializeField] public Material inactivated_material;
    [SerializeField] public Material activated_material;

    private uint collisions = 0;

    private bool activable = true;
    private bool activated = false;

    public Activable Activable { get => target;  set { target = value; } }

    // Start is called before the first frame update
    void Start()
    {
        var mr = GetComponentInChildren<MeshRenderer>();  
        mr.material = inactivated_material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("ShockLayer"))
        {
            collisions++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("ShockLayer"))
        {
            collisions--;
        }
    }

    public void Activate()
    {
        Debug.Log("ACTIVATE!");
        if(target != null) 
        {
            target.Activate();  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(activable && (collisions >= collisionsToActivation))
        {
            Activate();
            var mr = GetComponentInChildren<MeshRenderer>();
            mr.material = activated_material;
            activable = false;
        }
    }
}
