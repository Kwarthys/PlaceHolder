using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementRotator : MonoBehaviour {

    float x = 0;

    [SerializeField]
    GameObject toRotate;

	// Update is called once per frame
	void Update ()
    {
        if(toRotate != null)
        {
            toRotate.transform.Rotate(new Vector3(0, 0, 4 * Mathf.PerlinNoise(x, 0) - 2));

            x += 0.01f;
        }
    }
}
