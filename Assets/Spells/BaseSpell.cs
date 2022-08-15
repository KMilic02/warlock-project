using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : ScriptableObject
{
    [SerializeField] protected char hotkey;
    [SerializeField] protected SpellType spellType;
    [SerializeField] protected float castTime;

    [SerializeField] protected int spellAoe = 0; //0 means it's not an AOE spell
    [SerializeField] protected float cooldown;

    public virtual void castSpell(Player caster, Vector3 target)
    {
        
    }

    public char getHotkey()
    {
        return hotkey;
    }
}
