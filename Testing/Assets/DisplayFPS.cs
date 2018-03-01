using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class DisplayFPS : MonoBehaviour {
    Stopwatch stopwatch = new Stopwatch();
    // Update is called once per frame
    private void Update()
    {
        stopwatch.Start();
    }

    void LateUpdate () {
        stopwatch.Stop();
      //  Debug.Log("FPS: " + (1.0f / Time.deltaTime).ToString() + " | ms: " + stopwatch.ElapsedMilliseconds.ToString());
        stopwatch.Reset();
	}
}
