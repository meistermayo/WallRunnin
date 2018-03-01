using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour {
    [SerializeField] bool deac;
    
    public void OnEnable()
    {
        if (!deac)
            Destroy(gameObject, 5f);
        else {
            StopAllCoroutines();
            StartCoroutine(Deac());
        }
    }

    IEnumerator Deac()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
