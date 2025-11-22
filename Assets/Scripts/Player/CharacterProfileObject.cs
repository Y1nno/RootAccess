using UnityEngine;

[CreateAssetMenu(menuName = "Game/Character Profile", fileName = "NewCharacterProfile")]
public class CharacterProfile : ScriptableObject
{
    [Header("Identity")]
    public string characterName;

    [Header("Visuals")]
    public RuntimeAnimatorController animatorController;
    public Sprite idleSprite;

    [Header("Collision / Hitboxes")]
    public Vector2 colliderSize = Vector2.one;
    public Vector2 colliderOffset = Vector2.zero;

    [Header("Movement tuning (starter)")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Ability toggles (starter)")]
    public bool canDash;
    public bool canDoubleJump;
}
