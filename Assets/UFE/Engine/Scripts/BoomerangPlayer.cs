using System.Collections;
using System.Collections.Generic;
using UFE3D;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangPlayer : MonoBehaviour
{
    public Transform TargetPos;
    public float force;
    public bool posSet;
    public bool firstMove;

    public void Start()
    {
        ProjectileMoveScript.TriggerBoomerang += setTargetPos;
        ProjectileMoveScript.SetPositionTarget();
    }

    private void Update()
    {
        if (!posSet)
        {
            return;
        }
        if (Vector3.Distance(TargetPos.position, this.transform.position) <= 2.5f)
        {
            Destroy(gameObject);
        }
        else
        {
            float Distance = Mathf.Abs(Vector3.Distance(this.transform.position, TargetPos.position));
            float speed;
            if (Distance < 2)
            {
                speed = Distance / 0.1f;
            }
            else
            {

                speed = Distance / 0.4f;
            }
            Vector3 finalPos = new Vector3(TargetPos.position.x,TargetPos.position.y + 1.75f,TargetPos.position.z);
            transform.position= Vector3.MoveTowards(this.transform.position, finalPos, speed * Time.deltaTime);
            Debug.Log("MovingBack");
            if (!firstMove)
            {
                firstMove = true;
                Destroy(gameObject, 1.0f);
            }
            //MoveBackToPlayer();
        }
    }

    public void setTargetPos(Transform pos)
    {
        TargetPos = pos;
        Debug.Log("Pos Set" + pos);
        posSet = true;
        ProjectileMoveScript.TriggerBoomerang -= setTargetPos;

    }
}