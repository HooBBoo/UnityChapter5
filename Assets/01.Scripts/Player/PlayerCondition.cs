using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
    void TakeMentalDamage(int damage);
}

    public class PlayerCondition : MonoBehaviour, IDamagable
    {
        public UICondition uiCondition;

        Condition health { get { return uiCondition.health; } }
        Condition mentalHealth { get { return uiCondition.mentalHealth; } }

        public float noMentalHealthDecay;

        public float curMentalHealth = 300f; // 현재 정신 건강 수치
        public float maxMentalHealth = 500f; // 최대 정신 건강 수치

        public event Action onApplyBlessing;

    public void IncreaseMentalHealth(float value)
        {
        mentalHealth.Add(value);
        Debug.Log($"현재 정신 건강: {mentalHealth.curValue}");
        onApplyBlessing?.Invoke();
    }

    public event Action onTakeDamage;

        void Update()
        {
            if (mentalHealth.curValue == 0f)
            {
                health.Subtract(noMentalHealthDecay * Time.deltaTime);
            }

            if (health.curValue == 0f)
            {
                Die();
            }
        }

        public void TakePhysicalDamage(int damage)
        {
            health.Subtract(damage);
            onTakeDamage?.Invoke();
        }

        public void TakeMentalDamage(int damage)
        {
            mentalHealth.Subtract(damage);
            onTakeDamage?.Invoke();
        }

        public void Die()
        {
            Debug.Log("깩꼬닥 죽었다");
        }
    }
