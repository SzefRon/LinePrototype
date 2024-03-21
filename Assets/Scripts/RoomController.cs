using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    Material mat;
    float fadeAmount = 0;
    float originalOpacity;
    float fadeSpeed = 0.5f;

    bool isOpen = false;
    bool onlyOne = false;

    public List<GameObject> enemies = new List<GameObject>();
    //public List<GameObject> previousEnemies = new List<GameObject>();
    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            mat = enemy.GetComponent<Renderer>().material;
            originalOpacity = mat.color.a;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies.RemoveAll(item => item == null);
        if (isOpen)
            foreach (GameObject enemy in enemies)
            {
                if (enemy is not null)
                {
                    enemy.SetActive(true);
                }

            }
        else
            foreach (GameObject enemy in enemies)
            {
                if (enemy is not null)
                {
                    enemy.SetActive(false);
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onlyOne)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                onlyOne = true;
                isOpen = !isOpen;
                Debug.Log("Room is open");
            }
        }
    }

    void Fade()
    {
        Color color = mat.color;
        Color smoothcolor = new Color(color.r, color.g, color.b, 0);
        Mathf.Lerp(color.a, fadeAmount, fadeSpeed * Time.deltaTime);
        mat.color = smoothcolor;
    }

    void ResetFade()
    {
        Color color = mat.color;
        Color smoothcolor = new Color(color.r, color.g, color.b, 1);
        Mathf.Lerp(color.a, originalOpacity, fadeSpeed * Time.deltaTime);
        mat.color = smoothcolor;
    }
}
