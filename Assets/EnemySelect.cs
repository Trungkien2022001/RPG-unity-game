using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : MonoBehaviour
{

    public Collider2D enemySelect;


    public void SetSelect(bool flag) {
        enemySelect.enabled = flag;
    }
}
