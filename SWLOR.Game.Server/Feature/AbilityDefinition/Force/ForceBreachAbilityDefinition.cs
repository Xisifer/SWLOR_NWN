﻿//using Random = SWLOR.Game.Server.Service.Random;

using System.Collections.Generic;
using SWLOR.Game.Server.Core;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.VisualEffect;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.Service.AbilityService;
using SWLOR.Game.Server.Service.CombatService;
using SWLOR.Game.Server.Service.PerkService;
using SWLOR.Game.Server.Service.SkillService;
using static SWLOR.Game.Server.Core.NWScript.NWScript;

namespace SWLOR.Game.Server.Feature.AbilityDefinition.Force
{
    public class ForceBreachAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            ForceBreach1(builder);
            ForceBreach2(builder);
            ForceBreach3(builder);
            ForceBreach4(builder);

            return builder.Build();
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0.0f;

            switch (level)
            {
                case 1:
                    dmg = 6.0f;
                    break;
                case 2:
                    dmg = 8.5f;
                    break;
                case 3:
                    dmg = 12.0f;
                    break;
                case 4:
                    dmg = 13.5f;
                    break;
            }
            
            var willpower = GetAbilityModifier(AbilityType.Willpower, activator);
            var defense = Stat.GetDefense(target, CombatDamageType.Force);
            var targetWillpower = GetAbilityModifier(AbilityType.Willpower, target);
            var damage = Combat.CalculateDamage(dmg, willpower, defense, targetWillpower, false);
            
            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage), target);
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Silence), target);
            });
            
            Enmity.ModifyEnmityOnAll(activator, 1);
            CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
        }

        private static void ForceBreach1(AbilityBuilder builder)
        {
            builder.Create(FeatType.ForceBreach1, PerkType.ForceBreach)
                .Name("Force Breach I")
                .HasRecastDelay(RecastGroup.ForceBreach, 30f)
                .HasActivationDelay(2.0f)
                .RequirementFP(2)
                .IsCastedAbility()
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction(ImpactAction);
        }

        private static void ForceBreach2(AbilityBuilder builder)
        {
            builder.Create(FeatType.ForceBreach2, PerkType.ForceBreach)
                .Name("Force Breach II")
                .HasRecastDelay(RecastGroup.ForceBreach, 30f)
                .HasActivationDelay(2.0f)
                .RequirementFP(3)
                .IsCastedAbility()
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction(ImpactAction);
        }

        private static void ForceBreach3(AbilityBuilder builder)
        {
            builder.Create(FeatType.ForceBreach3, PerkType.ForceBreach)
                .Name("Force Breach III")
                .HasRecastDelay(RecastGroup.ForceBreach, 30f)
                .HasActivationDelay(2.0f)
                .RequirementFP(4)
                .IsCastedAbility()
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction(ImpactAction);
        }

        private static void ForceBreach4(AbilityBuilder builder)
        {
            builder.Create(FeatType.ForceBreach4, PerkType.ForceBreach)
                .Name("Force Breach IV")
                .HasRecastDelay(RecastGroup.ForceBreach, 30f)
                .HasActivationDelay(4.0f)
                .RequirementFP(5)
                .IsCastedAbility()
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction(ImpactAction);
        }
    }
}