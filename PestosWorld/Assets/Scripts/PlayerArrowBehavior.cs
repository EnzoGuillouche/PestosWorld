using UnityEngine;

public class PlayerArrowBehavior : MonoBehaviour
{
    private GameObject pesto;

    void Start()
    {
        pesto = GameObject.Find("Pesto");
        if (pesto == null)
            Debug.LogError("'Pesto' not found");
    }

    void Update()
    {
        if (pesto != null)
        {
            transform.position = pesto.transform.position + new Vector3(0, 0.7f, 0);
        }
    }
}
