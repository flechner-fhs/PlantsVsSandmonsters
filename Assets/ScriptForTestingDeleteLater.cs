using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForTestingDeleteLater : MonoBehaviour
{
    public float frames = 0;
    void Update()
    {
        frames += Time.deltaTime;
        if (frames > 2)
        {
            this.transform.Rotate(new Vector3(20, 20, 20));
            frames = 0;
        }
    }
}
