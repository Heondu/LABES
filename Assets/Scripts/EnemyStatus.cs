[System.Serializable]
public class EnemyStatus
{
    public int HP;
    public int maxHP;

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

    private const float multValue = 0.05f;

    public void CalculateDerivedStatus()
    {
        damage.AddModifier(new StatusModifier(strength.Value, StatusModType.Flat));
        fixDamage.AddModifier(new StatusModifier(strength.Value, StatusModType.Flat));
        critChance.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat));
        avoidance.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat));
        accuracy.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat));
        reduceMana.AddModifier(new StatusModifier(intelligence.Value, StatusModType.Flat));
        reduceCool.AddModifier(new StatusModifier(intelligence.Value, StatusModType.Flat));
        defence.AddModifier(new StatusModifier(endurance.Value, StatusModType.Flat));
        allResist.AddModifier(new StatusModifier(endurance.Value, StatusModType.Flat));

        fixDamage.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd));
        critChance.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd));
        avoidance.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd));
        accuracy.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd));
        reduceMana.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd));
        reduceCool.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd));
        allResist.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd));
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
        }
        return null;
    }
}
