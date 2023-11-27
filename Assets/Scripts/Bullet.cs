using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ----------On Hit----------
    public enum impacttag {Player,Enemy};
    public impacttag tag;
    public GameObject[] impact;
    public string customTag;

    //----------Awake Animation----------
    public GameObject[] AwakeVFX;
    public float awakeFxDeley;
    public bool IsShoot;
    
    void Start()
    {
        // if(IsAwake == true)
        // {
        //     StartCoroutine(fx(awakeFxDeley));
        // }
    }

    void OnTriggerEnter(Collider col)
    {
        if((col.gameObject.tag == tag.ToString()||col.gameObject.tag == customTag)&&IsShoot==false)
        {
            foreach(GameObject fx in impact)
            {
                GameObject spawn = Instantiate(fx,col.gameObject.transform.position,this.gameObject.transform.rotation);
                spawn.SetActive(true);
            }
        }
        else if((col.gameObject.tag == tag.ToString()||col.gameObject.tag == customTag)&&IsShoot==true)
        {
            foreach(GameObject fx in impact)
            {
                GameObject spawn = Instantiate(fx,col.gameObject.transform.position,col.gameObject.transform.rotation);
                spawn.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    public IEnumerator fx(float time)
    {
        yield return new WaitForSeconds(time);
        foreach(GameObject f in AwakeVFX)
        {
            if(f != null)
            {
                GameObject spawned = Instantiate(f,this.transform.position,this.transform.rotation);
                spawned.SetActive(true);
                spawned.transform.SetParent(this.transform);
            }
        }
    }

    public IEnumerator shootMove(Transform target,float moveDistance,float delay)
    {
        yield return new WaitForSeconds(delay);
        while(true)
        {
            this.transform.position += Vector3.MoveTowards(this.transform.position,target.position,moveDistance);
            Debug.Log(this.transform.position);
            yield return new WaitForSeconds(0.01f);
        }
        
    }
}
