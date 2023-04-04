using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject ball = Instantiate(weapon.Prefab);
        ball.transform.position = transform.position;
        ball.GetComponent<BallBehaviour>().DirectionChecker(pc.LastMoveDir);
    }
}
