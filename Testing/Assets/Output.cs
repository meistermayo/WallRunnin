using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Output : MonoBehaviour {
    Stopwatch stopwatch;
    [SerializeField] bool get;
    [SerializeField] bool extreme;
    [SerializeField] int iter;
    Component_Test component;

    // reference = 1x
    // get = 10x

    private void Start()
    {
        component = GetComponent<Component_Test>();
        stopwatch = new Stopwatch();
    }

    private void Update()
    {
        TestGetComponent();
    }

    void TestGetComponent()
    {
        stopwatch.Start();
        int i = iter-1;
        if (extreme)
            i = 0;
        for (; i<iter; i++)
        {
            if (get)
                GetComponent<Component_Test>().Access();
            else
                component.Access();
        }
        Debug.Log(stopwatch.ElapsedMilliseconds);
        stopwatch.Stop();
        stopwatch.Reset();

    }


}
