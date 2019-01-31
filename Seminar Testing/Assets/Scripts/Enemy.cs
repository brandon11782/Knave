using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MovingObject {

    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;

	protected override void Start ()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);

        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)  // checks if the enemy and player are roughly within the same column (x value)
            yDir = target.position.y > transform.position.y ? 1 : -1;  // moves enemy up or down depending on player location
        else
            xDir = target.position.x > transform.position.x ? 1 : -1; // moves enemy left or right if within same rough row (y val)

        AttemptMove<Player> (xDir, yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;

        hitPlayer.LoseLife(playerDamage);
    }
}
