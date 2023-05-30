using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(15, 45, 5) * Time.deltaTime * 3f);
    }
}
