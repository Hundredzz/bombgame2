using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyStat enemy;
    
    public Transform target;
    public Animator anim;
    public GameObject knockbackFX;
    public bool Left;
    public bool Front;
    public bool move;
    public bool Isdodge;
    public bool Isfollow;
    public float oriSpeed;
    public float speed;
    public float damping;
    public float rotateSmoothTime;

    private Transform trans;
    private Rigidbody rb;
    private int side;
    private int front;
    private int m;
    
    private float SPD;
    Vector3 movement;
    
    void Start()
    {
        enemy = GetComponent<EnemyStat>();
        target = FindObjectOfType<PlayerStat>().GetComponent<Transform>();
        trans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveEnum());
        // StartCoroutine(LookAtRoutine());
        speed = oriSpeed;
        SPD = speed;
    }

    private float fMov = 0f;
    private float sMov = 0f;
    private Vector3 oldPosition;
    void Update()
    {
        anim.SetBool("Ismove",move);
        if(Left == true && Front == true && move == true) //LF
        {
            fMov = 1;
            sMov = -1;
            anim.SetFloat("Horizontal",-1f);
        }
        else if(Left == false && Front == true && move == true) //RF
        {
            fMov = 1;
            sMov = 1;
            anim.SetFloat("Horizontal",1f);
        }

        else if(Left == true && Front == false && move == true) //LB
        {
            fMov = -1;
            sMov = -1;
            anim.SetFloat("Vertical",1f);
        }
        else if(Left == false && Front == false && move == true) //RN
        {
            fMov = -1;
            sMov = 1;
            anim.SetFloat("Vertical",-1f);
        }
        else
        {
            fMov = 0f;
            sMov = 0f;
        }
        if(enemy.IsSkill == false)
        {
            rb.velocity = transform.forward*speed*fMov + transform.right*speed*sMov;
            anim.SetFloat("Horizontal",sMov);
            anim.SetFloat("Vertical",fMov);
        }
    
        if(Vector3.Distance(this.gameObject.transform.position,target.position) > 13)
        {
            Front = true;
        }

        // if(Isfollow == true)
        // {
        //     trans.LookAt(new Vector3(target.position.x,this.transform.position.y,target.position.z));
        // }

        oldPosition = target.position;

        if(speed == 0)
        {
            move = false;
        }
    }

    void LateUpdate () 
    {
        if(Isfollow == true)
        {
            var target_rot = Quaternion.LookRotation(new Vector3(target.position.x,transform.position.y,target.position.z) - transform.position);
            var delta = Quaternion.Angle(transform.rotation, target_rot);
            if (delta > 0.0f)
            {
                var t = Mathf.SmoothDampAngle(delta, 0.0f, ref damping, rotateSmoothTime);
                t = 1.0f - t/delta;
                transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, t);
            }
        }
            
    }

    IEnumerator LookAtRoutine()
    {
        while(true)
        {
            if(Isfollow == true)
            {
                Vector3 position = Vector3.Lerp (oldPosition,new Vector3(target.position.x,this.transform.position.y,target.position.z), Time.deltaTime * damping);
                transform.LookAt (position);
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    IEnumerator MoveEnum()
    {
        EnemyBahav behave = GetComponent<EnemyBahav>();
        while(true)
        {
            if(Vector3.Distance(this.gameObject.transform.position,target.position) > behave.atkRange)
            {
                side = Random.Range(1,3);
                front = Random.Range(1,3);
                m = Random.Range(1,7);

                if(m <= 5)
                {
                    move = true;
                }
                else
                {
                    move = false;
                }

                if(side == 1) //left
                {
                    Left = true;
                }
                else if(side == 2) //right
                {
                    Left = false; 
                }

                if(front == 1) //forward
                {
                    Front = true;
                }
                else if(front == 2) //back
                {
                    Front = false; 
                }
            }
            else
            {
                move = false;
            }
            yield return new WaitForSeconds(Random.Range(3,5));
        }
    }

    public IEnumerator stun(float stunTime)
    {
        speed = 0;
        Isfollow = false;
        yield return new WaitForSeconds(stunTime);
        speed = oriSpeed;
        Isfollow = true;
    }

    public IEnumerator stun(float stunTime,string immune)
    {
        speed = 0;
        Isfollow = false;
        enemy.avoid = true;
        yield return new WaitForSeconds(stunTime);
        speed = oriSpeed;
        Isfollow = true;
        enemy.avoid = false;
    }

    public IEnumerator stopfollow(float stopTime)
    {
        Isfollow = false;
        yield return new WaitForSeconds(stopTime);
        Isfollow = true;
    }

    public IEnumerator enemyKnockback()
    {
        float t = 0.35f;
        while(t >= 0)
        {
            rb.velocity = this.transform.forward*(-50f);
            Quaternion rotate = this.transform.rotation*new Quaternion(0f,180f,0f,0f);
            Vector3 point = new Vector3(this.transform.position.x,1f,this.transform.position.z);
            GameObject spawned = Instantiate(knockbackFX,point,rotate);
            spawned.SetActive(true);
            t -= Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        
    }
}
