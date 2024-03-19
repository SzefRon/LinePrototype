using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChokeManager : MonoBehaviour
{
    public   int chokeFrames;

    public int ctrZero = 0;
    public int ctrOne = 0;

    public bool zeroIsChoking = false;
    public bool oneIsChoking = false;

    public void PullRope(int a)
    {
        if(a == 0)
        {
            zeroIsChoking = true;
            ctrZero = chokeFrames;
        }
        else if (a == 1) 
        {
            oneIsChoking = true;
            ctrOne = chokeFrames;
        }
    }

    void Choke()
    {
        if(zeroIsChoking && oneIsChoking)
        {
            Debug.Log("CHOKE!!!");
            ctrZero = 0;
            ctrOne = 0;
            zeroIsChoking = false;
            oneIsChoking = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ctrZero > 0)
        {
            ctrZero--;
        }
        else
        {
            zeroIsChoking = false;
        }

        if (ctrOne > 0)
        {
            ctrOne--;
        }
        else
        {
            oneIsChoking = false;   
        }
        Choke();
    }
}
