using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    public int wallDamage = 1;
    public int pointsPerKill = 10;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int score;
    private int life;

	// Use this for initialization
	protected override void Start ()
    {
        animator = GetComponent<Animator>();

        score = GameManager.instance.playerScore;
        life = GameManager.instance.playerLife;

        base.Start();
	}

    private void OnDisable()
    {
        GameManager.instance.playerScore = score;
        GameManager.instance.playerLife = life;
    }


    void Update () {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0) //checking for inputs, won't allow diagonal movement
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false; //ends level after 1 sec delay upon stepping on exit
        }

        else if (collision.tag == "Item")
        {
            // to be implemented once item implementation is done
            collision.gameObject.SetActive(false);
        }


    }

    protected override void AttemptMove <T> (int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;

        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
  
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
    private void CheckIfGameOver()
    {
        if (life <= 0)
            GameManager.instance.GameOver();
    }

    public void LoseLife (int loss)
    {
        animator.SetTrigger("playerHit");
        life = life - loss;
        CheckIfGameOver();
    }
}
