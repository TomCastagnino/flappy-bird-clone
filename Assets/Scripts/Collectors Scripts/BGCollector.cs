using UnityEngine;

public class BGCollector : MonoBehaviour {

    private float numberOfBackgroundObjects = 5f;

    //Calculo la posición X del objeto y a eso le sumo el ancho del objeto multiplicado por el número de backgroundObjects
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Background" || target.tag == "Ground")
        {
            Vector3 tempPosition = target.transform.position;
            float width = ((BoxCollider2D)target).size.x;
            tempPosition.x += width * (numberOfBackgroundObjects - 1);
            target.transform.position = tempPosition;
        }
    }
}


/*

public class BGCollector : MonoBehaviour
{

    private GameObject[] backgrounds;
    private GameObject[] grounds;
    private float lastBackgroundX;
    private float lastGroundX;

    // Use this for initialization
    void Awake()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");

        lastBackgroundX = FindLastElementX(backgrounds);
        lastGroundX = FindLastElementX(grounds);
    }

    public float FindLastElementX(GameObject[] array)
    {
        float temp = array[0].transform.position.x;
        for (int i = 1; i < array.Length; i++)
        {
            if (temp < array[i].transform.position.x)
            {
                temp = array[i].transform.position.x;
            }
        }
        return temp;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Background")
        {
            Vector3 tempPosition = target.transform.position;
            float width = ((BoxCollider2D)target).size.x;
            tempPosition.x = lastBackgroundX + width;
            target.transform.position = tempPosition;
            lastBackgroundX = tempPosition.x;
        }
        else if (target.tag == "Ground")
        {
            Vector3 tempPosition = target.transform.position;
            float width = ((BoxCollider2D)target).size.x;
            tempPosition.x = lastGroundX + width;
            target.transform.position = tempPosition;
            lastGroundX = tempPosition.x;
        }
    }
}

*/

