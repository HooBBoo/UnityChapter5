using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition mentalHealth;

    private void Start()
    {
        CharacterManager.Instance.player.condition.uiCondition = this;
    }
}