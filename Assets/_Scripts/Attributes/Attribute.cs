using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nathan.Attributes
{
    [Serializable]
    public class Attribute
    {
        public float BaseValue;
        public virtual float Value {
            get {
                if (isDirty || lastBaseValue != BaseValue) {
                    lastBaseValue = BaseValue;
                    _value = CalculateValue();
                    isDirty = false;
                }

                return _value;
            }
        }
        public readonly ReadOnlyCollection<AttributeModifier> Modifiers;

        protected readonly List<AttributeModifier> modifiers;
        protected bool isDirty = true;
        protected float _value;
        protected float lastBaseValue = float.MinValue;

        public Attribute()
        {
            modifiers = new List<AttributeModifier>();
            Modifiers = modifiers.AsReadOnly();
        }

        public Attribute(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(AttributeModifier mod)
        {
            isDirty = true;
            modifiers.Add(mod);
            modifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(AttributeModifier mod)
        {
            if (modifiers.Remove(mod)) {
                isDirty = true;
                return isDirty;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = modifiers.Count - 1; i >= 0; i--) {
                if (modifiers[i].Source == source) {
                    isDirty = true;
                    didRemove = true;
                    modifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual float CalculateValue()
        {
            float value = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < modifiers.Count; i++) {
                AttributeModifier mod = modifiers[i];

                if (mod.Type == AttributeModifierType.Flat) {
                    value += mod.Value;
                    continue;
                }

                if (mod.Type == AttributeModifierType.PercentageAdd) {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= modifiers.Count || modifiers[i + 1].Type != AttributeModifierType.PercentageAdd) {
                        value *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }

                if (mod.Type == AttributeModifierType.PercentageMult) {
                    value *= 1 + mod.Value;
                }
            }

            return (float) Math.Round(value, 4);
        }

        protected virtual int CompareModifierOrder(AttributeModifier a, AttributeModifier b)
        {
            if (a.Order < b.Order) {
                return -1;
            }

            if (a.Order > b.Order) {
                return 1;
            }

            return 0;
        }
    }
}
