using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    GameObject targets;
    CombatCtrl combatCtrl;
    public GameObject barLife;
    public GameObject select;
    public SpriteRenderer sr;

    float ScaleI;
    int maxlife;
    public int life;
    public int atack;
    public int speed;
    int target;

    public bool type;

    private void Start()
    {
        ScaleI = barLife.transform.localScale.x;
        maxlife = life;
        combatCtrl = GameObject.Find("combatCtrl").GetComponent<CombatCtrl>();
        if (type)
        {
            targets = GameObject.Find("Enemies");
        }
        else
        {
            targets = GameObject.Find("Players");
        }
        
        Debug.Log("Target seleccionado: " + target); // Verifica qué número tiene el target
        Debug.Log("Número de hijos en targets: " + targets.transform.childCount); // Verifica cuántos hay
    }

    public void Atack()
    {
        StartCoroutine(AnimAtack());
        if (type)
        {
            target = combatCtrl.EnemySelect;
        }
        else
        {
            target = combatCtrl.PlayerSelect;
        }
        if (combatCtrl.EnemyN >= 0 && combatCtrl.PlayersN >= 0)
        {
            targets.transform.GetChild(target).GetComponent<character>().Damage(atack);
        }
    }

    public void Damage(int atack)
    {
        life -= atack;
        StartCoroutine(AnimDmg(atack));
        if (life <= 0)
        {
            if (type)
            {
                combatCtrl.PlayersN--;
            }
            else
            {
                combatCtrl.EnemyN--;
            }
            Destroy(gameObject);
        }
    }


    public void Select(bool select)
    {
        this.select.SetActive(select);
    }

    IEnumerator AnimAtack()
    {
        float mov = 0.3f;
        if (!type) mov *= -1;
        transform.position = new Vector3(transform.position.x + mov, transform.position.y, transform.position.z);
        yield return new WaitForSecondsRealtime(0.2f);
        transform.position = new Vector3(transform.position.x - mov, transform.position.y, transform.position.z);
    }

    IEnumerator AnimDmg(float damage)
    {

        for (int i = 0; i < 10; i++)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
