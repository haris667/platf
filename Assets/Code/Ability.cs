using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityRaphaelType
{
    fastShot,
    spreadShot
}
public enum AbilityBorisType
{

}
public class Ability : MonoBehaviour
{
    public AbilityRaphaelType           abilityRaphaelType;

    public void ActivateRaphaelAbility()
    {
        switch (abilityRaphaelType)
        {
            case AbilityRaphaelType.spreadShot:
                Projectile p = Raphael.S.MakeProjectile(0);
                Projectile p1 = Raphael.S.MakeProjectile(45);
                Projectile p2 = Raphael.S.MakeProjectile(-45);
                break;
        }
    }
}
