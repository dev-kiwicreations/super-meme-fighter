using UnityEngine;
using System.Collections.Generic;
using UFE3D;

public class DefaultVersusModeScreen : VersusModeScreen
{
    private void Start()
    {
        this.SelectPlayerVersusCpu();
    }
    public override void OnShow()
    {
        base.OnShow();
    }
}
