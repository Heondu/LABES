﻿using System.Collections.Generic;

[System.Serializable]
public class StatusData
{
    public float strength;
    public float agility;
    public float intelligence;
    public float endurance;
    public float damage;
    public float defence;
    public float allResist;
    public float fireResist;
    public float coldResist;
    public float darkResist;
    public float lightResist;
    public float fireDamage;
    public float coldDamage;
    public float darkDamage;
    public float lightDamage;
    public float fixDamage;
    public float critChance;
    public float avoidance;
    public float accuracy;
    public float reduceMana;
    public float reduceCool;
    public float critResist;
    public float critDamage;
    public float experience;
    public float itemRange;
    public float maxHp;
    public float HP;
    public float maxMana;
    public float mana;
    public float exp;
    public float level;
    public float strengthPer;
    public float agilityPer;
    public float intelligencePer;
    public float endurancePer;
    public float damagePer;
    public float defencePer;
    public float allResistPer;
    public float fireResistPer;
    public float coldResistPer;
    public float darkResistPer;
    public float lightResistPer;
    public float fireDamagePer;
    public float coldDamagePer;
    public float darkDamagePer;
    public float lightDamagePer;
    public float fixDamagePer;
    public float critChancePer;
    public float avoidancePer;
    public float accuracyPer;
    public float reduceManaPer;
    public float reduceCoolPer;
    public float critResistPer;
    public float critDamagePer;
    public float experiencePer;
    public float itemRangePer;
    public float maxHpPer;
    public float hpPer;
    public float maxManaPer;
    public float manaPer;

    public void DicToVar(Dictionary<string, float> status)
    {
        strength = status["strength"];
        agility = status["agility"];
        intelligence = status["intelligence"];
        endurance = status["endurance"];
        damage = status["damage"];
        defence = status["defence"];
        allResist = status["allResist"];
        fireResist = status["fireResist"];
        coldResist = status["coldResist"];
        darkResist = status["darkResist"];
        lightResist = status["lightResist"];
        fireDamage = status["fireDamage"];
        coldDamage = status["coldDamage"];
        darkDamage = status["darkDamage"];
        lightDamage = status["lightDamage"];
        fixDamage = status["fixDamage"];
        critChance = status["critChance"];
        avoidance = status["avoidance"];
        accuracy = status["accuracy"];
        reduceMana = status["reduceMana"];
        reduceCool = status["reduceCool"];
        critResist = status["critResist"];
        critDamage = status["critDamage"];
        experience = status["experience"];
        itemRange = status["itemRange"];
        maxHp = status["maxHp"];
        HP = status["HP"];
        maxMana = status["maxMana"];
        mana = status["mana"];
        exp = status["exp"];
        level = status["level"];
        strengthPer = status["strength%"];
        agilityPer = status["agility%"];
        intelligencePer = status["intelligence%"];
        endurancePer = status["endurance%"];
        damagePer = status["damage%"];
        defencePer = status["defence%"];
        allResistPer = status["allResist%"];
        fireResistPer = status["fireResist%"];
        coldResistPer = status["coldResist%"];
        darkResistPer = status["darkResist%"];
        lightResistPer = status["lightResist%"];
        fireDamagePer = status["fireDamage%"];
        coldDamagePer = status["coldDamage%"];
        darkDamagePer = status["darkDamage%"];
        lightDamagePer = status["lightDamage%"];
        fixDamagePer = status["fixDamage%"];
        critChancePer = status["critChance%"];
        avoidancePer = status["avoidance%"];
        accuracyPer = status["accuracy%"];
        reduceManaPer = status["reduceMana%"];
        reduceCoolPer = status["reduceCool%"];
        critResistPer = status["critResist%"];
        critDamagePer = status["critDamage%"];
        experiencePer = status["experience%"];
        itemRangePer = status["itemRange%"];
        maxHpPer = status["maxHp%"];
        hpPer = status["HP%"];
        maxManaPer = status["maxMana%"];
        manaPer = status["mana%"];
    }

    public Dictionary<string, float> VarToDic()
    {
        Dictionary<string, float> status = new Dictionary<string, float>();
        status["strength"] = strength;
        status["agility"] = agility;
        status["intelligence"] = intelligence;
        status["endurance"] = endurance;
        status["damage"] = damage;
        status["defence"] = defence;
        status["allResist"] = allResist;
        status["fireResist"] = fireResist;
        status["coldResist"] = coldResist;
        status["darkResist"] = darkResist;
        status["lightResist"] = lightResist;
        status["fireDamage"] = fireDamage;
        status["coldDamage"] = coldDamage;
        status["darkDamage"] = darkDamage;
        status["lightDamage"] = lightDamage;
        status["fixDamage"] = fixDamage;
        status["critChance"] = critChance;
        status["avoidance"] = avoidance;
        status["accuracy"] = accuracy;
        status["reduceMana"] = reduceMana;
        status["reduceCool"] = reduceCool;
        status["critResist"] = critResist;
        status["critDamage"] = critDamage;
        status["experience"] = experience;
        status["itemRange"] = itemRange;
        status["maxHp"] = maxHp;
        status["HP"] = HP;
        status["maxMana"] = maxMana;
        status["mana"] = mana;
        status["exp"] = exp;
        status["level"] = level;
        status["strength%"] = strengthPer;
        status["agility%"] = agilityPer;
        status["intelligence%"] = intelligencePer;
        status["endurance%"] = endurancePer;
        status["damage%"] = damagePer;
        status["defence%"] = defencePer;
        status["allResist%"] = allResistPer;
        status["fireResist%"] = fireResistPer;
        status["coldResist%"] = coldResistPer;
        status["darkResist%"] = darkResistPer;
        status["lightResist%"] = lightResistPer;
        status["fireDamage%"] = fireDamagePer;
        status["coldDamage%"] = coldDamagePer;
        status["darkDamage%"] = darkDamagePer;
        status["lightDamage%"] = lightDamagePer;
        status["fixDamage%"] = fixDamagePer;
        status["critChance%"] = critChancePer;
        status["avoidance%"] = avoidancePer;
        status["accuracy%"] = accuracyPer;
        status["reduceMana%"] = reduceManaPer;
        status["reduceCool%"] = reduceCoolPer;
        status["critResist%"] = critResistPer;
        status["critDamage%"] = critDamagePer;
        status["experience%"] = experiencePer;
        status["itemRange%"] = itemRangePer;
        status["maxHp%"] = maxHpPer;
        status["HP%"] = hpPer;
        status["maxMana%"] = maxManaPer;
        status["mana%"] = manaPer;
        return status;
    }
}
