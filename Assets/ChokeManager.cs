using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChokeManager : MonoBehaviour
{
    [SerializeField] private float inputWindow = 0.2f;
    private bool zeroIsChoking = false;
    private bool oneIsChoking = false;

    public void PullRope(int a)
    {
        if(a == 0)
        {
            if (oneIsChoking)
            {
                ChokeSuccessful();
            }
            else if (!zeroIsChoking)
            {
                zeroIsChoking = true;
                StartCoroutine(Choke(0));
            }
        }
        else if (a == 1) 
        {
            if (zeroIsChoking)
            {
                ChokeSuccessful();
            }
            else if (!oneIsChoking)
            {
                oneIsChoking = true;
                StartCoroutine(Choke(1));
            }
        }
    }

    private IEnumerator Choke(int index)
    {
        if (index == 0)
        {
            yield return new WaitForSeconds(inputWindow);
            zeroIsChoking = false;
        }
        else if (index == 1)
        {
            yield return new WaitForSeconds(inputWindow);
            oneIsChoking = false;
        }
    }

    private void ChokeSuccessful()
    {
            Debug.Log("Choke Successful");
        ChokeList.DealDmgToObjectsInList();
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
