using UnityEngine;

public class PlayerCharacterSwapper : MonoBehaviour
{
    [Header("Input")]
    public KeyCode swapKey = KeyCode.Tab;

    [Header("Profiles (order matters)")]
    [SerializeField] private CharacterProfile[] profiles;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CapsuleCollider2D bodyCollider;

    public int currentIndex = 0;
    private CharacterProfile Current => profiles != null && profiles.Length > 0
        ? profiles[currentIndex]
        : null;

    private void Reset()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        if (profiles == null || profiles.Length == 0)
        {
            Debug.LogWarning("[Swapper] No profiles assigned.");
            return;
        }

        ApplyProfile(Current);
    }

    private void Update()
    {
           
        if (profiles == null || profiles.Length <= 1) return;

        if (Input.GetKeyDown(swapKey))
        {
            SwapToNextProfile();
        }
    }

    private void SwapToNextProfile()
    {
        int oldIndex = currentIndex;
        currentIndex = (currentIndex + 1) % profiles.Length;

        var oldProfile = profiles[oldIndex];
        var newProfile = Current;

        ApplyProfile(newProfile);

        //Debug.Log($"[Swapper] Swapped from '{oldProfile.characterName}' to '{newProfile.characterName}' (index {oldIndex} -> {currentIndex}).");
    }

    private void ApplyProfile(CharacterProfile profile)
    {
        if (profile == null)
        {
            //Debug.LogWarning("[Swapper] Tried to apply a null profile.");
            return;
        }

        // Visuals
        if (animator != null && profile.animatorController != null)
            animator.runtimeAnimatorController = profile.animatorController;
        animator.SetInteger("ProfileID", currentIndex);
        animator.SetTrigger("Swapping");

        if (spriteRenderer != null && profile.idleSprite != null)
            spriteRenderer.sprite = profile.idleSprite;

        // Collision / hitbox
        if (bodyCollider != null)
        {
            bodyCollider.size = profile.colliderSize;
            bodyCollider.offset = profile.colliderOffset;
        }
        //Debug.Log($"[Swapper] Applied profile '{profile.characterName}'.");
    }
}
