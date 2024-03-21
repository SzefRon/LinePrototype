using UnityEngine;

public class RopeModificationStation : MonoBehaviour
{
    [SerializeField] GameObject upgradePicker;
    [SerializeField] GameObject enginieer;
    [SerializeField] float range;

    float distance;

    private void Update()
    {
        distance = (Vector3.Distance(transform.position, enginieer.transform.position));
        if (distance > range)
        {
            upgradePicker.active = false;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                upgradePicker.active = !upgradePicker.active;
            }
        }
    }
}
