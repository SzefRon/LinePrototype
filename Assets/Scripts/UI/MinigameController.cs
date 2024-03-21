using UnityEngine;

public class MinigameController : MonoBehaviour
{
    public bool isMinigameActive = false;
    public bool chocking = false;
    public int numberOfChokings = 3;
    public bool CHOKED = false;
    private bool chocking1 = false;
    private bool chocking2 = false;
    private bool alreadyChoked = false;

    public GameObject Player1Rect;
    public GameObject Player2Rect;

    public RectTransform square1;
    public RectTransform square2;

    public float speed1 = 1.0f;
    public float speed2 = 1.0f;
    public RectTransform Bar;



    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (isColliding() && !alreadyChoked)
        {
            Debug.Log("collision!!!");
            Debug.Log("Number of chockings: " + numberOfChokings);

            if (chocking1 && chocking2)
            {
                ChokeList.DealDmgToObjectsInList();
                numberOfChokings--;
                alreadyChoked = true;
            }
        }

        if (numberOfChokings == 0)
        {
            CHOKED = true;
            Debug.Log("CHOCKED!!!");
        }

        MoveSquares();

    }


    public void MoveSquares()
    {
        square1.transform.Translate(speed1 * Time.deltaTime, 0, 0);
        square2.transform.Translate(speed2 * Time.deltaTime, 0, 0);

        if (square1.anchoredPosition.x < Bar.anchoredPosition.x - 256 || square1.anchoredPosition.x > Bar.anchoredPosition.x + 256)
        {
            speed1 *= -1;

        }
        if (square2.anchoredPosition.x < Bar.anchoredPosition.x - 256 || square2.anchoredPosition.x > Bar.anchoredPosition.x + 256)
        {
            speed2 *= -1;
        }
    }

    public bool isColliding()
    {
        if (Vector2.Distance(square1.anchoredPosition, square2.anchoredPosition) < 200)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("The 1 is chocking!!!");
                chocking1 = true;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("The 2 is chocking!!!");
                chocking2 = true;
            }

            if (chocking1 && chocking2)
            {
                return true;
            }

            return false;
        }
        else
        {
            chocking1 = false;
            chocking2 = false;
            alreadyChoked = false;

            return false;
        }
    }
}
