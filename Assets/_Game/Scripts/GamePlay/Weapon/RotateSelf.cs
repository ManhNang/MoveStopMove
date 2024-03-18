using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateSelf : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(Vector3.up * 0.5f, Space.Self);
    }
}
