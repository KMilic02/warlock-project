using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Test", menuName = "Spells/TestSpell", order = 1)]
public class test_spell : BaseSpell
{
    public GameObject projectilePrefab;
    public GameObject explosionPrefab;

    public override void castSpell(Player caster, Vector3 target)
    {
        base.castSpell(caster, target);
        var projectileInstance = Instantiate(projectilePrefab, caster.transform.position, Quaternion.LookRotation(target - caster.transform.position));
    }
}
