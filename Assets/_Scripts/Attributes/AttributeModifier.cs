namespace Nathan.Attributes
{
    public class AttributeModifier
    {
        public readonly float Value;
        public readonly AttributeModifierType Type;
        public readonly int Order;
        public readonly object Source;

        public AttributeModifier(float value, AttributeModifierType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public AttributeModifier(float value, AttributeModifierType type) : this(value, type, (int) type, null) { }

        public AttributeModifier(float value, AttributeModifierType type, int order) : this(value, type, order, null) { }

        public AttributeModifier(float value, AttributeModifierType type, object source) : this(value, type, (int) type, source) { }
    }
}
