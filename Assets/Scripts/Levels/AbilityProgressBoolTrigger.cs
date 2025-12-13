using UnityEngine;

public class AbilityProgressBoolTrigger : MonoBehaviour
{
    const int fusionatorPartsNeeded = 3;

    [Tooltip("The ID of the ability we want to change")]
    public string id;

    public enum NullableBool { Toggle, True, False }

    [Tooltip("True / False sets the ability, toggle inverts the current value")]
    public NullableBool value = NullableBool.Toggle;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        var mgr = collision.gameObject.transform.parent.GetComponent<AbilityProgressManager>();

        switch (value)
        {
            case NullableBool.True:
                mgr.fusionatorParts += 1;
                if (fusionatorPartsNeeded <= mgr.fusionatorParts && !mgr.canOliviaShoot)
                {
                    mgr.ChangeAbilityBool(id, true);
                }
                break;

            case NullableBool.False:
                mgr.ChangeAbilityBool(id, false);
                break;

            case NullableBool.Toggle:
            default:
                mgr.ChangeAbilityBool(id, null);
                break;
        }
    }
}
