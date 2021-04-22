using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateShellCounter : MonoBehaviour
{
    private GameObject bossSpreadShells;
    private GameObject boss;
    public  Rigidbody2D rb2D;
    private Vector2 angle;
    private float spriteAngle, angleDif;
    public float speed;
    public AtkStruct attack;

    private SpreadAtkController bossShellsController;

    // Start is called before the first frame update   
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        bossSpreadShells = GameObject.FindWithTag("BossSpreadShells");
        bossShellsController = bossSpreadShells.GetComponent<SpreadAtkController>();
        angle = new Vector2(rb2D.transform.right.x, rb2D.transform.right.y);
        angleDif = Random.Range(-10, 10);
    }

    void FixedUpdate()
    {
        rb2D.velocity = angle * speed * Time.deltaTime;
        rb2D.transform.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);
        spriteAngle += angleDif;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bossShellsController.removeShell();
        Destroy(gameObject);
    }
}
