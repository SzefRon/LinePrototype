using UnityEngine;

public class ChokeChecker : MonoBehaviour
{
    [SerializeField] int raycasts;
    [SerializeField] float chokeDistance;
    [SerializeField] float percentage;

    [SerializeField] GameObject canvas;

    int angleStep;
    public bool previousIsChoked;
    public bool isChoked;

    public int collisionsWithSegment;
    public int collisionsWithMonster;

    public GameObject minigame;

    // Start is called before the first frame update
    void Start()
    {
        angleStep = 360 / raycasts;
        previousIsChoked = false;
        isChoked = false;
        collisionsWithMonster = 0;
        collisionsWithSegment = 0;
        minigame.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        previousIsChoked = isChoked;
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
                if (hit.transform.tag == "Segment" || hit.transform.tag == "FlameEffect")
                {
                    collisionsWithSegment++;
                }
                else if (hit.transform.tag == "Monster")
                {
                    collisionsWithMonster++;
                }

                Debug.DrawRay(begining, newAngle * hit.distance, Color.yellow);
            }
        }

        if(collisionsWithSegment > 0)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }

        chokeCount = collisionsWithMonster + collisionsWithSegment;

        //Debug.Log(chokeCount);

        if (chokeCount >= raycasts * percentage)
        {
            minigame.SetActive(true);
            //Debug.Log("choked!!!");
            //GetComponent<EffectComponent>().ApplyRopeEffect(SegmentUpgrades.Choke);
            isChoked = true;
        }
        else
        {
            isChoked = false;
        }

        if (isChoked)
        {
            if (!previousIsChoked)
            {
                ChokeList.chokedObjects.Add(gameObject);
            }
        }
        else
        {
            if (previousIsChoked)
            {
                ChokeList.chokedObjects.Remove(gameObject);
            }
        }
    }
}
