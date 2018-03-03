using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_Test : MonoBehaviour {
    bool testBool;

    public void Access()
    {
        testBool = !testBool;
    }
}
