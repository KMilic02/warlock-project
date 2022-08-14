using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter
{
    Player player;

    protected int maxHealth;
    protected int health;

    protected List<BaseSpell> spells = new List<BaseSpell>();

    public BaseCharacter(int maxHealth, List<BaseSpell> spells)
    {
        this.spells = spells;
        this.maxHealth = maxHealth;
    }

    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}
