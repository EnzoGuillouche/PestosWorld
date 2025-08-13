using UnityEngine;

public class PlayerArrowBehavior : MonoBehaviour
{
    public GameObject pointedObject;

    void Start()
    {
        pointedObject = GameObject.Find("Pesto 1");
        if (pointedObject == null)
            Debug.LogError("'Pointed Game Object' not found");
    }

    void Update()
    {
        if (pointedObject != null)
        {
            transform.position = pointedObject.transform.position + new Vector3(0, 0.7f, 0);
        }
    }

    public void UpdateGameObject(GameObject objectToReplace)
    {
        pointedObject = objectToReplace;
    }
}
