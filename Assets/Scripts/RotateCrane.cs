using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCrane : MonoBehaviour {

    float x = 0;

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0, 4 * Mathf.PerlinNoise(x, 0) - 2));

        x += 0.01f;
    }
}
