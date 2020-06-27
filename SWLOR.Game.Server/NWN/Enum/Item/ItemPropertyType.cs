namespace SWLOR.Game.Server.NWN.Enum.Item
{
    public enum ItemPropertyType
    {
        Invalid = -1,
        AbilityBonus = 0,
        ACBonus = 1,
        ACBonusVsAlignmentGroup = 2,
        ACBonusVsDamageType = 3,
        ACBonusVsRacialGroup = 4,
        ACBonusVsSpecificAlignment = 5,
        EnhancementBonus = 6,
        EnhancementBonusVsAlignmentGroup = 7,
        EnhancementBonusVsRacialGroup = 8,
        EnhancementBonusVsSpecificAlignement = 9,
        DecreasedEnhancementModifier = 10,
        BaseItemWeightReduction = 11,
        BonusFeat = 12,
        BonusSpellSlotOfLevelN = 13,
        CastSpell = 15,
        DamageBonus = 16,
        DamageBonusVsAlignmentGroup = 17,
        DamageBonusVsRacialGroup = 18,
        DamageBonusVsSpecificAlignment = 19,
        ImmunityDamageType = 20,
        DecreasedDamage = 21,
        DamageReduction = 22,
        DamageResistance = 23,
        DamageVulnerability = 24,
        Darkvision = 26,
        DecreasedAbilityScore = 27,
        DecreasedAC = 28,
        DecreasedSkillModifier = 29,
        EnhancedContainerReducedWeight = 32,
        ExtraMeleeDamageType = 33,
        ExtraRangedDamageType = 34,
        Haste = 35,
        HolyAvenger = 36,
        ImmunityMiscellaneous = 37,
        ImprovedEvasion = 38,
        SpellResistance = 39,
        SavingThrowBonus = 40,
        SavingThrowBonusSpecific = 41,
        Keen = 43,
        Light = 44,
        Mighty = 45,
        MindBlank = 46,
        NoDamage = 47,
        OnHitProperties = 48,
        DecreasedSavingThrows = 49,
        DecreasedSavingThrowsSpecific = 50,
        Regeneration = 51,
        SkillBonus = 52,
        ImmunitySpecificSpell = 53,
        ImmunitySpellSchool = 54,
        ThievesTools = 55,
        AttackBonus = 56,
        AttackBonusVsAlignmentGroup = 57,
        AttackBonusVsRacialGroup = 58,
        AttackBonusVsSpecificAlignment = 59,
        DecreasedAttackModifier = 60,
        UnlimitedAmmunition = 61,
        UseLimitationAlignmentGroup = 62,
        UseLimitationClass = 63,
        UseLimitationRacialType = 64,
        UseLimitationSpecificAlignment = 65,
        UseLimitationTileset = 66,
        RegenerationVampiric = 67,
        Trap = 70,
        TrueSeeing = 71,
        OnMonsterHit = 72,
        TurnResistance = 73,
        MassiveCriticals = 74,
        FreedomOfMovement = 75,
        Poison = 76, // no longer working, poison is now a onHit  subtype
        MonsterDamage = 77,
        ImmunitySpellsByLevel = 78,
        SpecialWalk = 79,
        HealersKit = 80,
        WeightIncrease = 81,
        OnHitCastSpell = 82,
        Visualeffect = 83,
        ArcaneSpellFailure = 84,
        Material = 85,
        Quality = 86,
        Additional = 87,

        StarshipWeaponsBonus = 90,
        StarshipShieldsBonus = 91,
        StarshipStronidiumBonus = 92,
        StarshipFuelBonus = 93,
        StarshipStealthBonus = 94,
        StarshipScanningBonus = 95,
        StarshipSpeedBonus = 96,
        StarshipRangeBonus = 97,
        PilotingBonus = 98,
        ScavengingBonus = 99,
        RecommendedLevel = 100,
        RestBonus = 101,
        HarvestingBonus = 102,
        CastingSpeed = 103,
        ComponentType = 104,
        CraftBonusWeaponsmith = 105,
        CraftBonusArmorsmith = 106,
        CraftBonusCooking = 107,
        Durability = 108,
        MaxDurability = 109,
        AssociatedSkill = 110,
        CraftTierLevel = 111,
        CraftBonusEngineering = 112,
        HPBonus = 113,
        FPBonus = 114,
        EnmityRate = 115,
        ItemType = 116,
        ArmorClass = 117,
        DarkPotencyBonus = 118,     // Deprecated
        LightPotencyBonus = 119,    // Deprecated
        MindPotencyBonus = 120,     // Deprecated
        LuckBonus = 121,
        MeditateBonus = 122,
        MedicineBonus = 123,
        HPRegenBonus = 124,
        FPRegenBonus = 125,
        BaseAttackBonus = 126,
        ModSlotRed = 127,
        ModSlotBlue = 128,
        ModSlotGreen = 129,
        ModSlotYellow = 130,
        ModSlotPrismatic = 131,
        RedMod = 132,
        BlueMod = 133,
        GreenMod = 134,
        YellowMod = 135,
        LevelIncrease = 136,
        ComponentBonus = 137,
        ComponentItemTypeRestriction = 138,
        CraftBonusFabrication = 139,
        StructureBonus = 140,
        ScanningBonus = 141,
        ElectricalPotencyBonus = 142, // Deprecated
        ForcePotencyBonus = 143,      // Deprecated
        ForceAccuracyBonus = 144,     // Deprecated
        ForceDefenseBonus = 145,      // Deprecated
        ElectricalDefenseBonus = 146, // Deprecated
        MindDefenseBonus = 147,       // Deprecated
        LightDefenseBonus = 148,      // Deprecated
        DarkDefenseBonus = 149        // Deprecated
    }
}