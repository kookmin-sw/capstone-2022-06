using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DeadPanel : UI_Scene
{
    private float deadTime;
    private float waitDeath;

    enum Texts
    {
        Timer
    }

    public override void Init()
    {
        deadTime = Time.time;
        waitDeath = deadTime + 20f;

        Bind<Text>(typeof(Texts));

        StartCoroutine(TimerCoroutine());
    }

    /// <sumamry>
    /// 제한 시간에 다 다를 때까지 남은 시간을 갱신하는 코루틴
    /// yield를 빠져나오면 스스로를 파괴합니다.
    /// </summary>
    IEnumerator TimerCoroutine()
    {
        yield return new WaitUntil(() => {
            deadTime = Time.time;
            float diff = waitDeath - deadTime;
            GetText((int)Texts.Timer).text = $"{Mathf.Max(0, (int)diff)}";
            return diff <= 0;
        });

        Destroy(gameObject);
    }
}
