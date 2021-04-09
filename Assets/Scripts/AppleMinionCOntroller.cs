using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class AppleMinionCOntroller : MonoBehaviour
{
    private GameObject MeleePlayer, RangedPlayer;
    private GameObject target;
    private Vector3 PreyDir;

    public float minionSpeed;
    public AudioSource GameAudioSource;
    public AudioClip AppleMinionExplosionSound;
    public AtkStruct[] attacks;
    public SpriteRenderer AppleMinnionSprite;
    public Animator AppleMinnionAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        MeleePlayer = GameObject.FindWithTag("MeleePlayer");
        RangedPlayer = GameObject.FindWithTag("RangedPlayer");
        

    }


    void FindPrey()
    {
        if(MeleePlayer != null)
        {
            target = MeleePlayer;
        }
        else if( RangedPlayer != null)
        {
            target = RangedPlayer;
        }
    }

    void Update()
    {
        FindPrey();
    }

    
    void FixedUpdate()
    {
        PreyDir = (target.transform.position - gameObject.transform.position);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, (minionSpeed * Time.deltaTime));
        if(PreyDir.x > 0.0f)
        {
            AppleMinnionSprite.flipX = true;
        }
        else if(PreyDir.x < 0.0f)
        {
            AppleMinnionSprite.flipX = false;
        }
        
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MeleeStrike")
        {
            Destroy(gameObject);
        }
        else
        {
            PreyDir = (target.transform.position - gameObject.transform.position);
            GameAudioSource.PlayOneShot(AppleMinionExplosionSound, 0.5f);
            GameObject atk = PlayerController.Attack(attacks[0].atkObj, gameObject.transform.position, PreyDir, attacks[0].atkDistance, gameObject.transform.rotation);
            atk = null;
            Destroy(gameObject);
        }
    }
}
