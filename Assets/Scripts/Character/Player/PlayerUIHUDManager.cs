using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerUIHUDManager : MonoBehaviour
    {
        [SerializeField] UI_StatBar staminaBar;
        [SerializeField] UI_StatBar healthBar;
        public void SetMaxHealthValue(float oldvalue, float newValues)
        {
          //  healthBar.SetStat(Mathf.RoundToInt(newValues));
        }


        public void SetMaxHealthValues(int maxHealth)
        {
            healthBar.setMaxStat(maxHealth);
        }




        public void SetNewStaminaValue(float oldValue,float newValue)
        {
            staminaBar.setStat(Mathf.RoundToInt(newValue));
        }
        public void SetMaxStaminaValue(int maxStamina)
        {
            staminaBar.setMaxStat(maxStamina);
        }
    }
}