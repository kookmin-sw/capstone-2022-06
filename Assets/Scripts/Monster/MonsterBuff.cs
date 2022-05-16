using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBuff : MonoBehaviour
{
    public float timer;
    public float LimitTimer;

    ObjectStat parentStat;

    // Start is called before the first frame update
    void Start()
    {
        parentStat = transform.parent.gameObject.GetComponent<ObjectStat>();

        BuffCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= LimitTimer)
            DestroyEffect();

        timer += Time.deltaTime;
    }

    void BuffCharacter()
    {
        parentStat.Status.atk += 25;
        parentStat.Status.ap += 40;
    }

    void DestroyEffect()
    {
        parentStat.Status.atk -= 25;
        parentStat.Status.ap -= 40;

        Managers.Resource.Destroy(this.gameObject);
    }
}
