using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

// control = 1x
// instantiate = 100x

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject destroy1;
    [SerializeField] GameObject destroy2;
    [SerializeField] bool instantiate;
    [SerializeField] int iter;
    [SerializeField] bool extreme;
    GameObject[] destroyObjects;
    int timer;
    int iterObjects;

    Stopwatch stopwatch;

    private void Start()
    {
        stopwatch = new Stopwatch();
        destroyObjects = new GameObject[10];
        for (int i=0; i<10;i++)
        {
            destroyObjects[i] = Instantiate(destroy1, transform.position, Quaternion.identity) as GameObject;
        }

    }


    private void Update()
    {
        timer--;
        if (timer < 0)
        {
            timer = 30;
            stopwatch.Start();

            int i = iter - 1;
            if (extreme) i = 0;
            for (; i < iter; i++)
            {
                if (instantiate)
                {
                    Instantiate(destroy2, transform.position, Quaternion.identity);
                }
                else
                {
                    Iter();
                }
            }
            Debug.Log(stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }

    void Iter()
    {
        iterObjects = (iterObjects + 1) % 10;
        destroyObjects[iterObjects].SetActive(true);
        destroyObjects[iterObjects].transform.position = transform.position;
    }


}
