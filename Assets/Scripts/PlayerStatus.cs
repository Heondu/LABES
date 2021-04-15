[System.Serializable]
public class PlayerStatus
{
    public int HP = 0;
    public int maxHP = 1000;
    public int mana = 0;
    public int maxMana = 100;
    public float exp = 0;
    public int level = 1;

    public Status strength;
    public Status agility;
    public Status intelligence;
    public Status endurance;
    public Status damage;
    public Status defence;
    public Status allResist;
    public Status fireResist;
    public Status coldResist;
    public Status darkResist;
    public Status lightResist;
    public Status fireDamage;
    public Status coldDamage;
    public Status darkDamage;
    public Status lightDamage;
    public Status fixDamage;
    public Status critChance;
    public Status critResist;
    public Status critDamage;
    public Status avoidance;
    public Status accuracy;
    public Status reduceMana;
    public Status reduceCool;
    public Status experience;

    private const float multValue = 0.05f;

    public void CalculateDerivedStatus()
    {
        damage.RemoveAllModifiersFromSource(strength);
        fixDamage.RemoveAllModifiersFromSource(strength);
        critChance.RemoveAllModifiersFromSource(agility);
        avoidance.RemoveAllModifiersFromSource(agility);
        accuracy.RemoveAllModifiersFromSource(agility);
        reduceMana.RemoveAllModifiersFromSource(intelligence);
        reduceCool.RemoveAllModifiersFromSource(intelligence);
        defence.RemoveAllModifiersFromSource(endurance);
        allResist.RemoveAllModifiersFromSource(endurance);

        damage.AddModifier(new StatusModifier(strength.Value, StatusModType.Flat, strength));
        fixDamage.AddModifier(new StatusModifier(strength.Value, StatusModType.Flat, strength));
        critChance.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat, agility));
        avoidance.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat, agility));
        accuracy.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat, agility));
        reduceMana.AddModifier(new StatusModifier(intelligence.Value, StatusModType.Flat, intelligence));
        reduceCool.AddModifier(new StatusModifier(intelligence.Value, StatusModType.Flat, intelligence));
        defence.AddModifier(new StatusModifier(endurance.Value, StatusModType.Flat, endurance));
        allResist.AddModifier(new StatusModifier(endurance.Value, StatusModType.Flat, endurance));

        fixDamage.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, strength));
        critChance.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, agility));
        avoidance.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, agility));
        accuracy.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, agility));
        reduceMana.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, intelligence));
        reduceCool.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, intelligence));
        allResist.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, endurance));
    }

    public Status GetStatus(string name)
    {
        switch (name)
        {
            case "strength": return strength;
            case "agility": return agility;
            case "intelligence": return intelligence;
            case "endurance": return endurance;
            case "damage": return damage;
            case "defence": return defence;
            case "allResist": return allResist;
            case "fireResist": return fireResist;
            case "coldResist": return coldResist;
            case "darkResist": return darkResist;
            case "lightResist": return lightResist;
            case "fireDamage": return fireDamage;
            case "coldDamage": return coldDamage;
            case "darkDamage": return darkDamage;
            case "lightDamage": return lightDamage;
            case "fixDamage": return fixDamage;
            case "critChance": return critChance;
            case "critResist": return critResist;
            case "critDamage": return critDamage;
            case "avoidance": return avoidance;
            case "accuracy": return accuracy;
            case "reduceMana": return reduceMana;
            case "reduceCool": return reduceCool;
            case "experience": return experience;
        }
        return null;
    }

    public object GetValue(string name)
    {
        switch (name)
        {
            case "HP": return HP;
            case "maxHP": return maxHP;
            case "mana": return mana;
            case "maxMana": return maxMana;
            case "exp": return exp;
            case "level": return level;
        }
        return null;
    }
}