using UnityEngine;

public class PlayerArrowBehavior : MonoBehaviour
{
    private Transform pesto;

    void Start()
    {
        pesto = transform.parent.Find("Pesto");
        if (pesto == null)
            Debug.LogError("'Pesto' not found under parent: " + transform.parent.name);
    }

    void Update()
    {
        if (pesto != null)
        {
            transform.position = pesto.position + new Vector3(0, 0.7f, 0);
        }
    }
}
