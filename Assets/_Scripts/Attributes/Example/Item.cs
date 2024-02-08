namespace Nathan.Attributes
{
    public class Item
    {
        public void Equip(Character c)
        {
            c.Strength.AddModifier(new AttributeModifier(10, AttributeModifierType.Flat, this));
            c.Strength.AddModifier(new AttributeModifier(0.1f, AttributeModifierType.PercentageAdd, this));
            c.Agility.AddModifier(new AttributeModifier(0.5f, AttributeModifierType.PercentageMult, this));
        }

        public void Unequip(Character c)
        {
            c.Strength.RemoveAllModifiersFromSource(this);
        }
    }
}
