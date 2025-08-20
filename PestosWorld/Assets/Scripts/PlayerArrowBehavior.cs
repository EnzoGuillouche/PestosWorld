using UnityEngine;

public class PlayerArrowBehavior : MonoBehaviour
{
    public GameObject pointedObject;
    private float offset = 0.0f;

    void Start()
    {
        offset = 0.7f;
        pointedObject = GameObject.Find("Pesto 1");
        if (pointedObject == null)
            Debug.LogError("'Pointed Game Object' not found");
    }

    void Update()
    {
        if (pointedObject != null)
        {
            transform.position = pointedObject.transform.position + new Vector3(0, offset, 0);
        }
    }

    public void UpdateGameObject(GameObject objectToReplace)
    {
        pointedObject = objectToReplace;
        if (pointedObject.CompareTag("Destination"))
            offset = 0.2f;
        else
            offset = 0.7f;
    }
}
