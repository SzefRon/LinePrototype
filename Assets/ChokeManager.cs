using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ChokeManager : MonoBehaviour
{
    [SerializeField] private float inputWindow = 0.2f;
    private bool zeroIsChoking = false;
    private bool oneIsChoking = false;

    public Material material;
    public Color normalColor;
    public Color chokingColor;

    public void PullRope(int a)
    {
        if (a == 0)
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
        StartCoroutine(FlashLine());   
        
    }

    /*IEnumerator FlashLine()
    {
        var rope = FindAnyObjectByType<RopeGenerator>().smallSegments;
        foreach (var segment in rope)
        {
            segment.GetComponent<SmallSegment>().Succes();
        }
        yield return new WaitForSeconds(0.2f);
        foreach (var segment in rope)
        {
            segment.GetComponent<SmallSegment>().Normal();
        }
    }*/

    IEnumerator FlashLine()
    {
        material.color = chokingColor;
        yield return new WaitForSeconds(inputWindow);
        material.color = normalColor;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
