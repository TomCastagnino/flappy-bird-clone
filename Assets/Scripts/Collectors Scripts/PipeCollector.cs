using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {

    private GameObject[] pipeHolder;
    private float distance = 2.75f;
    private float lastPipeX;
    private float pipeMinY = -2.17f;
    private float pipeMaxY = 1f;


    private void Awake()
    {
        pipeHolder = GameObject.FindGameObjectsWithTag("PipeHolder");
        SetPipes();
    }

    private void SetPipes()
    {
        for (int i = 0; i < pipeHolder.Length; i++)
        {
            Vector3 temp = pipeHolder[i].transform.position;
            temp.y = SetRandomY();
            pipeHolder[i].transform.position = temp;
        }
        lastPipeX = FindLastElementX(pipeHolder);
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

    private float SetRandomY()
    {
        return Random.Range(pipeMinY, pipeMaxY);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            Vector3 tempPosition = target.transform.position;
            tempPosition.x = lastPipeX + distance;
            tempPosition.y = SetRandomY();
            target.transform.position = tempPosition;
            lastPipeX = tempPosition.x;
        }
    }
}
