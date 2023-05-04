using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{

    public UnityEvent onPlayerDeath;

    public CharacterStats characterData;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;
    Animator anim;
    
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("I-Frames")]
    public float invincibilityDuration = 0.5f;
    float invincibilityTimer;
    bool isInvincible;

    float rezChance = 1.1f;

    VisualElement mui;
    ProgressBar healthBar;
    ProgressBar ultBar;
    Label numSouls;

    Label abilityLabel;
    float ultProgress;

    PlayerController pc;

    sfxController sfx;

    public bool CanRezEnemy()
    {
        float random = Random.Range(0, 1f);
        return rezChance > random;
    }

    public enum AffectedStat { None, ProjectileSpeed, Speed, Damage, Health };

    private IEnumerator ModifyStatForDuration(AffectedStat which, float value, float duration)
    {
        switch (which)
        {
            case AffectedStat.None: {
                yield return null;
            } break;
            case AffectedStat.ProjectileSpeed: {
                yield return null;
            } break;
            case AffectedStat.Speed: {
                Debug.Log("Spee stat was " + currentMoveSpeed);
                currentMoveSpeed *= value;
                Debug.Log("Speed stat is now " + currentMoveSpeed + " for " + duration + " seconds");
                yield return new WaitForSeconds(duration);
                currentMoveSpeed = characterData.MoveSpeed;
                Debug.Log("Speed has reverted to normal");
            } break;
            case AffectedStat.Damage: {
                yield return null;
            } break;
            case AffectedStat.Health: {
                yield return null;
            } break;
        }
    }
    public void ModifyStat(AffectedStat which, float value, float duration)
    {
        StartCoroutine(ModifyStatForDuration(which, value, duration));
    }

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        mui = GetComponent<UIDocument>().rootVisualElement;
        healthBar = mui.Q<ProgressBar>("HealthBar");
        ultBar = mui.Q<ProgressBar>("SoulRelease");
        numSouls = mui.Q<Label>("NumSouls");
        abilityLabel = mui.Q<Label>("Ability");
        inventory = GetComponent<InventoryManager>();
        
        

        for (int i = 0; i < 6; i++)
        {
            inventory.weaponSlots[i] = null;
            inventory.passiveItemSlots[i] = null;
        }

        currentHealth = characterData.MaxHealth;
        healthBar.highValue = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;
    }

    public InventoryManager GetInventory()
    {
        return inventory;
    }

    private void Start()
    {
        sfx = FindObjectOfType<sfxController>();
        anim = GetComponent<Animator>();
        experienceCap = levelRanges[0].experienceCapIncrease;
        healthBar.value = currentHealth;
        ultProgress = ultBar.highValue;
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.Log("inventory slots are full");
            return;
        }
        GameObject actualWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        actualWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, actualWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.Log("inventory slots are full");
            return;
        }
        GameObject actualPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        actualPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, actualPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            ++level;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void RestoreHealth(float amt)
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += amt;
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
            healthBar.value = currentHealth;
        }
    }

    public bool CanUlt()
    {
        return ultProgress >= ultBar.highValue;
    }

    public void UseUlt()
    {
        ultProgress = ultBar.lowValue;
        for (int i = 0; i < inventory.souls.Count; i++)
        {
            EnemyStats es = inventory.souls[i].GetComponent<EnemyStats>();

        }
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
        if (ultBar != null)
        {
            if (ultProgress <= ultBar.highValue)
            {
                ultProgress += Time.deltaTime;
                ultBar.value = ultProgress;
            }
            if (pc.DidUlt())
            {
                ultProgress = 0f;
                ultBar.value = ultProgress;
            }
        }

        if (inventory.abilities.Count > 0)
        {
            string abilities = "";
            foreach (var item in inventory.abilities)
            {
                abilities += item.name + " ";
            }
            Debug.Log(abilities);
            abilityLabel.text = abilities;
        }



        numSouls.text = "" + inventory.souls.Count;
    }

    public void TakeDamage(float dmg)
    {
        if(!isInvincible)
        {
            currentHealth -= dmg;
            healthBar.value = currentHealth;
            sfx.PlaySFX(SFX.PlayerHurt);
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (currentHealth <= 0)
            {
                Debug.Log("Player died");
                die();
            }
        }
    }

    void Recover()
    {
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
            healthBar.value = currentHealth;
        }
    }

    public bool IsDead()
    {
        return currentHealth < 0;
    }

    public void die()
    {
            anim.SetBool("IsDead", true);
            sfx.PlaySFX(SFX.Die);
            Destroy(this.gameObject, 2);
            onPlayerDeath.Invoke();
    }


}
