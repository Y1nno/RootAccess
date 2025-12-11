using UnityEngine;

public class AbilityProgressManager : MonoBehaviour
{
    public bool canEliDoubleJump = false;
    public bool canOliviaShoot = false;

    private Jumper jumper;
    private GameObject capsule;
    private FireProjectile shooter;

    private void Awake()
    {
        capsule = transform.Find("Capsule").gameObject;
        

        jumper = capsule.GetComponent<Jumper>();
        jumper.ChangeDoubleJump(canEliDoubleJump);
        shooter = capsule.GetComponent<FireProjectile>();
        shooter.ChangeShootingAbility(canOliviaShoot);
    }

    public void ChangeAbilityBool(string abilityName, bool? newValue)
    {
        switch (abilityName.ToLower())
        {
            case "doublejump":
                jumper.ChangeDoubleJump(newValue);
                canEliDoubleJump = jumper.doubleJumpAllowed;
                break;
            case "shooting":
                shooter.ChangeShootingAbility(newValue);
                canOliviaShoot = shooter.canShoot;
                break;
            default: break;
        }
    }
}
