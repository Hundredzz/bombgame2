using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySkillProfile",menuName = "EnemySkill")]

public class EnemySkillProfile : ScriptableObject
{
    public static Dictionary<string, EnemySkillProfile> enemySkill_profile = new Dictionary<string, EnemySkillProfile>();

    public GameObject bullet;
    public GameObject[] skillFX;
    public Quaternion FXrotate;
    public float fxDelay;
    public Vector3 FXpos;
    public Vector3 bulletOffset;
    public float bulletDelay;
    public float bulletCharge;
    public float bulletForce;
    public float moveDistance;
    public Vector3 moveDirection;
    public bool customPos;
    public bool ATK;
    public bool move; //has mobility
    public float moveDelay;
    public float freezeTime;
    public bool buff; //has buff effect
    public bool shoot; //projectile type
    public bool snap;
    public string[] reqComb; 
    public string animName;
    public string buffType;
    public float buffTime;
    public float multiply;
    public float skillTime;
    public float moveTime;
    public int max;

}
