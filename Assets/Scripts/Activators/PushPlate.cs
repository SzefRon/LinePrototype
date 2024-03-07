using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlate : MonoBehaviour
{
    [SerializeField] public Material inactivated_material;
    [SerializeField] public Material activated_material;

    private int collisions = 0;

    private bool active = false;

    public bool Active 
    {
        get { return active; } 
        set 
        { 
            active = value; 
            if(active)
            {
                var mr = GetComponentInChildren<MeshRenderer>();
                mr.material = activated_material;
            }
            else
            {
                var mr = GetComponentInChildren<MeshRenderer>();
                mr.material = inactivated_material;
            }
        } 
    }
    public int Collisions { get => collisions; set { collisions = value; if (collisions > 0) { Active = true; } else { Active = false; }; } }



    void Activate()
    {
        var mr = GetComponentInChildren<MeshRenderer>();
        mr.material = activated_material;
    }

    void Inactivate()
    {
        var mr = GetComponentInChildren<MeshRenderer>();
        mr.material = inactivated_material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collisions++;
    }

    private void OnCollisionExit(Collision collision)
    {
        Collisions--;        
    }



    // Start is called before the first frame update
    void Start()
    {
        var mr = GetComponentInChildren<MeshRenderer>();
        mr.material = inactivated_material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
