using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class EnemyStat : MonoBehaviour
{
    public SliderScriprt hpSlider;
    public SliderScriprt shieldSlider;
    public PlayerStat player;
    public EnemyMovement mov;

    public ThrowingTutorial player_bomb;
    public CameraShake camShake;
    public Animator anim;
    public PlayableDirector enemyWin;
    public Transform spawnPoint;
    public SkinnedMeshRenderer mesh;
    public EnemyBahav behave;
    public Material mat;
    public AudioSystem audio;
    public LightManager lightManager;
    public Material skyMat;
    public bool nextPhase;
    public bool avoid;
    public bool IsHeavy;
    public bool IsSkill;
    public float maxHP;
    public float hp;
    public float maxShield;
    public float shield;
    public float baseDef;
    public float def;
    public float baseATK;
    public float atk;
    public float atkSpd;
    public float multi;
    public float clearTime;
    
    void Start()
    {
        hp = maxHP;
        shield = maxShield;
        def = baseDef;
        atk = baseATK;
        multi = 1;
        hpSlider.SetmaxValue(maxHP);
        shieldSlider.SetmaxValue(maxShield);
        player = FindObjectOfType<PlayerStat>();
        mov = FindObjectOfType<EnemyMovement>();
        camShake = FindObjectOfType<CameraShake>();
        behave = GetComponent<EnemyBahav>();
        enemyWin = GameObject.Find("EnemywinTimeLine").GetComponent<PlayableDirector>();
        spawnPoint = GameObject.Find("EnemySpawn").GetComponent<Transform>();
        lightManager = FindObjectOfType<LightManager>().GetComponent<LightManager>();
        audio = GetComponent<AudioSystem>();
    }

    void Update()
    {
    
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "PlayerHit" && avoid == false && player.IsSkill == false)
        {
            takeDMG(player.atk,player.IsHeavy);
            camShake.Shaking(6,0.5f);
        }
        else if(col.gameObject.tag == "PlayerHit" && avoid == false && player.IsSkill == true)
        {
            if(IsSkill == false)
            {
                anim.Play("GetHit");
            }
            takeDMG(player.atk*player.multi);
            camShake.Shaking(6,0.5f);
        }
        else if(col.gameObject.tag == "PlayerKnockback" && avoid == false && player.IsSkill == true)
        {
            if(IsSkill == false)
            {
                anim.Play("GetHit");
                StartCoroutine(mov.enemyKnockback());
            }
            takeDMG(player.atk*player.multi);
            camShake.Shaking(6,0.5f);
            
        }
        else if(col.gameObject.tag == "bomb")
        {
            anim.Play("GetHit");
            hp -= player_bomb.bombtier1;
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
        multi = 1;
        IsSkill = false;
    }

    public void Attack(bool heavy)
    {
        IsHeavy = heavy;
        if(IsHeavy == true)
        {
            anim.SetTrigger("ATK_H");
            StartCoroutine(mov.stun(atkSpd));
            return;
        }
        else if(IsHeavy == false)
        {
            anim.SetTrigger("ATK");
            StartCoroutine(mov.stun(atkSpd));
            return;
        }
    }

    public void takeDMG(float inTake,bool heavy)
    {
        if(avoid == false)
        {
            if(shield > 0)
            {
                if(avoid == false)
                {
                    if(heavy == true)
                    {
                        audio.PlayAudio("heavy");
                        hp -= (inTake*2f)-def;
                        shield -= 5;
                        camShake.Shaking(2,0.25f);
                    }
                    else
                    {
                        audio.PlayAudio("light");
                        hp -= inTake-def;
                        shield -= 5;
                        camShake.Shaking(1,0.25f);
                    }
                }
            }
            else
            {   
                if(heavy == true)
                {
                    audio.PlayAudio("heavy");
                    hp -= (inTake*2.5f);
                    camShake.Shaking(2,0.25f);
                }
                else
                {
                    audio.PlayAudio("light");
                    hp -= inTake;
                    camShake.Shaking(1,0.25f);
                }
            }

            hpSlider.SetValue(hp);
            shieldSlider.SetValue(shield);

            if(shield <= 0)
            {
                StartCoroutine(resetShield());
            }

            if(nextPhase == false && hp <= (maxHP/2))
            {
                NextPhase();
            }
        }
    }

    public void takeDMG(float inTake)
    {
        if(avoid == false)
        {
            if(shield > 0)
            {
                audio.PlayAudio("skillHit");
                hp -= (inTake*2f)-def;
                shield -= 5;
                camShake.Shaking(2,0.25f);
            }
            else
            {   
                audio.PlayAudio("skillHit");
                hp -= (inTake*2f);
                shield -= 5;
                camShake.Shaking(2,0.25f);
            }

            hpSlider.SetValue(hp);
            shieldSlider.SetValue(shield);

            if(nextPhase == false && hp <= (maxHP/2))
            {
                NextPhase();
            }
        }
    }

    void NextPhase()
    {
        nextPhase = true;
        RenderSettings.skybox = skyMat;
        lightManager.lightSnap(3f);
        
        if(mat != null)
        {
            mesh.material = mat;
            Debug.Log("transform");
        }

        anim.SetTrigger("Transform");
        anim.SetBool("IsPhase2",true);
        atkSpd -= 0.5f;
        mov.oriSpeed += 2f;
        mov.speed = mov.oriSpeed;
        baseATK *= 1.5f;
        atk = baseATK;
        def *= 1.5f;
        StartCoroutine(mov.stun(3f,"immune"));
    }

    public void Die()
    {
        this.transform.position = new Vector3 (spawnPoint.position.x,transform.position.y,spawnPoint.position.z);
        this.transform.rotation = spawnPoint.rotation;
        anim.SetTrigger("Die");
        mov.StopAllCoroutines();
        mov.enabled = false;
        FindObjectOfType<EnemyBahav>().StopAllCoroutines();
        FindObjectOfType<EnemyBahav>().enabled = false;
        this.enabled = false;
        StopAllCoroutines();
        behave.StopAllCoroutines();
        StartCoroutine(Remove());
        this.enabled = false;
    }

    public void Win()
    {
        this.transform.position = new Vector3(spawnPoint.position.x,transform.position.y,spawnPoint.position.z);
        this.transform.rotation = spawnPoint.rotation;
        player.Die();
        enemyWin.Play();
        anim.SetTrigger("Win");
        mov.StopAllCoroutines();
        mov.enabled = false;
        FindObjectOfType<EnemyBahav>().StopAllCoroutines();
        FindObjectOfType<EnemyBahav>().enabled = false;
        mov.enabled = false;
        StopAllCoroutines();
        behave.StopAllCoroutines();
        this.enabled = false;
    }

    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Die();
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
}