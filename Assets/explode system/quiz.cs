using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quiz : MonoBehaviour
{
    public ThrowingTutorial script;
    private int index = 0;
    [SerializeField] private Sprite[] question;
    [SerializeField] private int num;
    [SerializeField] private int[] multiple;
    [SerializeField] private Image slot;
    [SerializeField] private Transform wallbreak;
    private int Fixindex = 0;
    private int damagecal;
    void Start()
    {
        Fixindex = RandomNum();
        slot.sprite = question[Fixindex];
        damagecal = (int)Mathf.Pow(num,multiple[Fixindex]);
    }

    public int RandomNum()
    {
        System.Random random = new System.Random();
        index = random.Next(0, 4);
        return index;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bomb")
        {
            if(script.bombBaseDamage == damagecal)
            {
                Instantiate(wallbreak, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

    }
}
