using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class UI_StatBar : MonoBehaviour
    {
        private Slider slider;
        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }
        public virtual void setStat(int newValue)
        {
            slider.value = newValue;
        }
        public virtual void setMaxStat(int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }
        



    }
}