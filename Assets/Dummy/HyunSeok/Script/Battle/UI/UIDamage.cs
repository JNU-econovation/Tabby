using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamage : MonoBehaviour
{
    [SerializeField]
    Text dmgText;

    public Color normalColor;
    public Color criticalColor;
    public Color missColor;

    bool isCritical;
    bool isMiss;

    private void Awake()
    {

    }

    public void SetDmg(float dmg, bool isCritical, bool isMiss)
    {
        transform.position += new Vector3(Random.Range(-1f, 0f), 0.75f, 0f);
        this.isCritical = isCritical;
        this.isMiss = isMiss;
        if (!isMiss)
            dmgText.text = ((int)dmg).ToString();
        else
            dmgText.text = "Miss";
        StartCoroutine(Off());
    }

    IEnumerator Off()
    {
        if(isMiss)
            dmgText.color = missColor;
        else if (isCritical)
            dmgText.color = criticalColor;
        else
            dmgText.color = normalColor;
        float time = 0f;
        while (time < 0.6f)
        {
            time += Time.deltaTime;
            transform.position += new Vector3(0f, 2f * Time.deltaTime, 0f);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
