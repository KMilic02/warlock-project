using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    protected char hotkey;
    protected SpellType spellType;

    protected int spellAoe = 0; //0 means it's not an AOE spell

    public virtual void castSpell()
    {

    }

}
