using System.Collections;
using System.Collections.Generic;
using UFE3D;
using UnityEngine;

public class BoomerangInvoker : MonoBehaviour
{
    private void OnDisable()
    {
        this.GetComponent<ProjectileMoveScript>().InstantiateBoomerang();
    }
}
