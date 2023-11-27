using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBahav : MonoBehaviour
{
    [Header("----------Movement----------")]
    public EnemyStat enemy;
    public EnemyMovement movement;
    public Animator anim;
    public GameObject body;
    public float atkRange;
    private int normalAttackCount;
    private int skillAttackCount;

    [Header("----------Skill----------")]
    public EnemySkillProfile[] skillPro;
    public GameObject flash;

    private AudioSystem audio;

    void Start()
    {
        body = this.gameObject;
        audio = GetComponent<AudioSystem>();
        enemy = GetComponent<EnemyStat>();
        // movement = GetComponent<EnemyMovement>();
        StartCoroutine(attackEnum());
        Debug.Log("ATTACK!!!!");
    }

    IEnumerator attackEnum()
    {
        yield return new WaitForSeconds(0.2f);
        movement = GetComponent<EnemyMovement>();
        while(true)
        {
            if(skillAttackCount < 1)
            {
                if(normalAttackCount <= 1)
                {
                    if(Vector3.Distance(this.gameObject.transform.position,movement.target.position) <= atkRange)
                    {
                        enemy.Attack(false);
                        normalAttackCount += 1;
                    }
                }
                else if(normalAttackCount > 1)
                {
                    if(Vector3.Distance(this.gameObject.transform.position,movement.target.position) <= atkRange)
                    {
                        enemy.Attack(true);
                        normalAttackCount = 0;
                        skillAttackCount += 1;
                    }
                }
                yield return new WaitForSeconds(Random.Range(enemy.atkSpd+1f,enemy.atkSpd+3f));
            }
            else if(skillAttackCount >= 1)
            {
                if(enemy.nextPhase == false)
                {
                    int skillNumber = Random.Range(0,3);
                    useSkill(skillNumber);
                    yield return new WaitForSeconds(skillPro[skillNumber].skillTime);
                    skillAttackCount = 0;
                }
                else
                {
                    int skillNumber = Random.Range(0,3);
                    useSkill(skillNumber+3);
                    yield return new WaitForSeconds(skillPro[skillNumber+3].skillTime);
                    skillAttackCount = 0;
                }
            }
        }
    }

    void useSkill(int skillIndex)
    {
        StartCoroutine(enemy.clearSkill(skillPro[skillIndex].skillTime));
        StartCoroutine(movement.stun(skillPro[skillIndex].freezeTime));
        if(skillIndex == 0 || skillIndex-3 == 0)
        {
            anim.SetTrigger("skill1");
        }
        else if(skillIndex == 1 || skillIndex-3 == 1)
        {
            anim.SetTrigger("skill2");
        }
        else if(skillIndex == 2 || skillIndex-3 == 2)
        {
            anim.SetTrigger("skill3");
        }
        enemy.multi = skillPro[skillIndex].multiply;
        enemy.IsSkill = true;
        GameObject s = Instantiate(flash,body.transform.position,body.transform.rotation);
        s.SetActive(true);
        audio.PlayAudio("charge");

        if(skillPro[skillIndex].buff == true) //buff
        {
            // player.SetBuff(skillPro.buffType,skillPro.buffTime);
        }

        if(skillPro[skillIndex].move == true) //moveskill
        {
            StartCoroutine(moveSkill(skillPro[skillIndex].moveTime,skillIndex));
        }

        if(skillPro[skillIndex].shoot == true) //projectileType
        {
            StartCoroutine(shoot(skillIndex));
        }

        foreach(GameObject skillfx in skillPro[skillIndex].skillFX) //skillFX
        {
            if(skillfx != null)
            {
                StartCoroutine(spawnFX(skillPro[skillIndex],skillfx,skillPro[skillIndex].fxDelay));
            }
        }
    }
    
    IEnumerator moveSkill(float t,int index)
    {
        yield return new WaitForSeconds(skillPro[index].moveDelay);
        while(t >= 0)
        {
            body.transform.position += body.transform.forward * skillPro[index].moveDirection.z*0.1f;
            body.transform.position += body.transform.right * skillPro[index].moveDirection.x*0.1f;
            t -= Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator shoot(int index)
    {
        yield return new WaitForSeconds(skillPro[index].bulletCharge);

        GameObject spawned = Instantiate(skillPro[index].bullet,body.transform.position+skillPro[index].bulletOffset,Quaternion.identity);
        spawned.SetActive(true);
        Rigidbody rigid = spawned.GetComponent<Rigidbody>();
        Debug.Log(skillPro[index].bulletCharge);

        yield return new WaitForSeconds(skillPro[index].bulletDelay);

        spawned.transform.LookAt(movement.target);
        rigid.useGravity = true;
        rigid.AddForce(spawned.transform.forward*skillPro[index].bulletForce,ForceMode.Impulse);
        Debug.Log(skillPro[index].bulletDelay);
    }

    IEnumerator spawnFX(EnemySkillProfile pro,GameObject skillfx,float time)
    {
        yield return new WaitForSeconds(time);
        GameObject spawned = Instantiate(skillfx,body.transform.position,body.transform.rotation);
        spawned.SetActive(true);
        if(pro.customPos == true)
        {
            spawned.transform.rotation = body.transform.rotation * pro.FXrotate;
            spawned.transform.position = body.transform.position;
            spawned.transform.SetParent(body.transform);
            spawned.transform.position = body.transform.position + pro.FXpos;
        }
        else
        {
            spawned.transform.SetParent(body.transform);
        }
        Destroy(spawned,pro.skillTime);
    }
}
