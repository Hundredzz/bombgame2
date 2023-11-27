using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


[CreateAssetMenu(fileName = "skillprofile",menuName = "NewSkill")]

public class SkillProfile : ScriptableObject
{
    public static Dictionary<string, SkillProfile> skill_profile = new Dictionary<string, SkillProfile>();

    public AnimationClip clip;
    public Animator anim;
    public Sprite sprite;
    public GameObject bullet;
    public GameObject[] skillFX;
    public Quaternion FXrotate;
    public Vector3 FXpos;
    public Vector3 bulletOffset;
    public float manaUse;
    public float bulletDelay;
    public float bulletCharge;
    public float moveDistance;
    public bool customPos;
    public bool ATK;
    public bool move; //has mobility
    public bool buff; //has buff effect
    public bool shoot; //projectile type
    public bool snap;
    public bool zoom;
    public string[] reqComb; 
    public string animName;
    public string buffType;
    public float buffTime;
    public float multiply;
    public float skillTime;
    public float moveTime;
    public int max;

}
