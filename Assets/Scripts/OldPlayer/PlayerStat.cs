using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public class PlayerStat : MonoBehaviour
{
    public SliderScriprt hpSlider;
    public SliderScriprt shieldSlider;
    public SliderScriprt manaSlider;
    public EnemyStat enemy;
    public AudioSystem audio;
    public Animator anim;
    public GameObject buffFX;
    public GameObject healFX;
    public GameObject defFX;
    public GameObject[] activeFX;
    public GameObject hpUi;
    public PlayableDirector playerWin;
    public Transform spawnPoint;
    public enum Buff{none,regen,atkBuff,defBuff,};
    public Buff[] buff;
    public float[] buffTime;
    public bool avoid;
    public bool IsHeavy;
    public string[] ComboOrder;
    public int comboCount;
    public float maxHP;
    public float hp;
    public float maxShield;
    public float shield;
    public float baseDef;
    public float def;
    public float baseATK;
    public float atk;
    // -----------------skill------------
    public bool IsSkill;
    public float multi;
    public float clearTime;

    private float oriSPD;
    // -----------------mana------------
    private float maxMana = 4;
    public float mana;

    public CameraShake camShake;

    void Start()
    {
        hp = maxHP;
        shield = maxShield;
        def = baseDef;
        atk = baseATK;
        mana = maxMana/2;
        audio = GetComponent<AudioSystem>();
        hpSlider.SetmaxValue(maxHP);
        shieldSlider.SetmaxValue(maxShield);
        manaSlider.SetmaxValue(maxMana);
        enemy = FindObjectOfType<EnemyStat>();
        camShake = FindObjectOfType<CameraShake>();
        playerWin = GameObject.Find("PlayerwinTimeLine").GetComponent<PlayableDirector>();
        spawnPoint = GameObject.Find("PlayerSpawn").GetComponent<Transform>();
        StartCoroutine(buffEffect());
    }

    void Update()
    {
        manaSlider.SetValue(mana);
        for(int i = 0;i < 2;i++)
        {
            if(buffTime[i]>0)
            {
                buffTime[i] -= Time.deltaTime;
            }
            else if(buff[i] != Buff.none)
            {
                if(buff[i] == Buff.atkBuff && buffTime[i]<=0)
                {
                    buffFX.SetActive(false);
                    atk = baseATK;
                }
                else if(buff[i] == Buff.defBuff)
                {
                    defFX.SetActive(false);
                    def = baseDef;
                }
                else if(buff[i] == Buff.regen)
                {
                    healFX.SetActive(false);
                }
                buff[i] = Buff.none;
            }
        }

        if(mana < maxMana)
        {
            mana += Time.deltaTime/7;
        }

        if(shield <= 0)
        {
            StartCoroutine(resetShield());
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "EnemyHit" && avoid == false && IsSkill == false && enemy.IsSkill == false)
        {
            takeDMG(enemy.atk,enemy.IsHeavy);
            anim.Play("GetHit");
            camShake.Shaking(8,0.5f);
        }
        else if(col.gameObject.tag == "EnemyHit" && avoid == false && IsSkill == false && enemy.IsSkill == true)
        {
            takeDMG(enemy.atk*enemy.multi);
            anim.Play("GetHit");
            camShake.Shaking(8,0.5f);
        }
        else if(col.gameObject.tag == "EnemyKnockback" && avoid == false)
        {
            takeDMG(enemy.atk*enemy.multi);
            anim.Play("GetHit");
            camShake.Shaking(8,0.5f);
        }
    }

    IEnumerator resetShield()
    {
        yield return new WaitForSeconds(3f);
        while(shield < maxShield)
        {
            shield += Time.deltaTime;
            shieldSlider.SetValue(shield);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator clearSkill(float T)
    {
        yield return new WaitForSeconds(T);
        IsSkill = false;
    }

    public IEnumerator buffEffect()
    {
        while(true)
        {
            for(int i=0;i<3;i++)
            {
                switch (buff[i]) 
                {
                    case Buff.regen:
                        if(hp < maxHP)
                        {
                            hp += maxHP*0.05f;
                        }
                        else
                        {
                            hp = maxHP;
                        }
                        healFX.SetActive(true);
                        hpSlider.SetValue(hp);
                        break;
                    case Buff.atkBuff:
                        atk = baseATK+5;
                        buffFX.SetActive(true);
                        break;
                    case Buff.defBuff:
                        def = baseDef+5;
                        defFX.SetActive(true);
                        break;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator stopfollow(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);
    }

    public void SetBuff(string name,float T)
    {
        for(int i = 0;i<3;i++)
        {
            if(buff[i] == Buff.none)
            {
                buff[i] = (Buff)System.Enum.Parse(typeof(Buff),name);
                buffTime[i] = T;
                break;
            }
        }
    }
    
    public void Attack(bool heavy)
    {
       if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_L")||anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_H")||anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
       {
            return;
       }
       else
       {
            IsHeavy = heavy;
            mana += 0.05f;
            if(comboCount == 5)
            {
                comboCount = 0;
                for(int i = 0;i <= 4;i++)
                {
                    ComboOrder[i] = "";
                }
            }

            if(IsHeavy == true)
            {
                anim.SetTrigger("ATK_H");
                StartCoroutine(stopfollow(0.5f));
                ComboOrder[comboCount] = "H";
            }
            if(IsHeavy == false)
            {
                anim.SetTrigger("ATK");
                StartCoroutine(stopfollow(0.3f));
                ComboOrder[comboCount] = "L";
            }

            comboCount += 1;
       }

        if(enemy.hp <= 0)
        {
            Win();
        }
    }
    
    public void takeDMG(float inTake,bool heavy)
    {
        if(avoid == false)
        {
            if(shield > 0)
            {
                if(heavy == true)
                {
                    hp -= (inTake*2f)-def;
                    shield -= 5;
                    audio.PlayAudio("heavy");
                    StartCoroutine(stopfollow(0.3f));
                }
                else
                {
                    hp -= inTake-def;
                    shield -= 5;
                    audio.PlayAudio("light");
                    StartCoroutine(stopfollow(0.1f));
                }
            }
            
            else
            {   
                if(heavy == true)
                {
                    hp -= (inTake*2f);
                    audio.PlayAudio("heavy");
                    StartCoroutine(stopfollow(0.3f));
                }
                else
                {
                    hp -= inTake;
                    audio.PlayAudio("light");
                    StartCoroutine(stopfollow(0.1f));
                }
            }

            hpSlider.SetValue(hp);
            shieldSlider.SetValue(shield);

            if(hp <= 0)
            {
                Die();
                enemy.Win();
            }
        }
    }

    public void takeDMG(float inTake)
    {
        if(avoid == false)
        {
            if(shield > 0)
            {
                if((inTake-def) > 0)
                {
                    hp -= (inTake-def);
                }
                else
                {
                    hp -= 1;
                }
                shield -= 5;
                audio.PlayAudio("skillHit");
                StartCoroutine(stopfollow(0.1f));
            }
            else
            {   
                hp -= inTake;
                shield -= 5;
                audio.PlayAudio("skillHit");
                StartCoroutine(stopfollow(0.1f));
            }

            hpSlider.SetValue(hp);
            shieldSlider.SetValue(shield);

            if(hp <= 0)
            {
                Die();
                enemy.Win();
            }
        }
    }

    public void Die()
    {
        this.transform.position = spawnPoint.position;
        this.transform.rotation = spawnPoint.rotation;
        hpUi.SetActive(false);
        anim.SetTrigger("Die");
        StopAllCoroutines();
        this.enabled = false;
    }

    public void Win()
    {
        this.transform.position = spawnPoint.position;
        this.transform.rotation = spawnPoint.rotation;
        hpUi.SetActive(false);
        enemy.Die();
        playerWin.Play();
        anim.SetTrigger("Win");
        StopAllCoroutines();
        this.enabled = false;
    }
}

