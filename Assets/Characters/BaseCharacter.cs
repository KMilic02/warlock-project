using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : ScriptableObject
{
    Player player;

    protected int maxHealth;
    protected int health;

    [SerializeField] protected List<BaseSpell> spells = new List<BaseSpell>();

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

    public BaseSpell getSpellByHotkey(char hotkey)
    {
        return spells.Find(spell => spell.getHotkey() == hotkey);
    }

    public void setPlayer(Player player)
    {
        this.player = player;
    }

    public void castSpell(BaseSpell spell)
    {
        spell.castSpell(player, Utilities.getClickEventPosition(player.getCamera(), player.getPlane()));
    }
}
