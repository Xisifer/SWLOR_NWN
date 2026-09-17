using FluentAssertions;
using NUnit.Framework;
using SWLOR.Game.Server.Feature.AppearanceDefinition.RacialAppearance;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.Service.LanguageService;
using SWLOR.NWN.API.NWScript.Enum;
using SWLOR.NWN.API.NWScript.Enum.Creature;

namespace SWLOR.Game.Server.Tests.Feature;

public class GunganRaceTests
{
    [TestCase(Gender.Male)]
    [TestCase(Gender.Female)]
    public void DefaultAppearance_IsSelectableAndWithinSpeciesHeightRange(Gender gender)
    {
        Race.LoadRaces();
        var appearance = Race.GetDefaultAppearance(RacialType.Gungan, gender);
        appearance.AppearanceType.Should().Be(AppearanceType.Gungan);
        RacialAppearanceRegistry.TryGet(appearance.AppearanceType, out var definition).Should().BeTrue();
        var heads = gender == Gender.Male ? definition.MaleHeads : definition.FemaleHeads;
        heads.Should().HaveCountGreaterThanOrEqualTo(3).And.Contain(appearance.HeadId);
        appearance.Scale.Should().BeInRange(definition.MinimumScale, definition.MaximumScale);
        definition.MaximumScale.Should().BeLessThan(new WookieeRacialAppearanceDefinition().MaximumScale);
    }

    [Test]
    public void Translator_ObscuresBothCasesAndPreservesPunctuation()
    {
        var translator = new TranslatorGungan();
        translator.Translate("Hello, 123!\n").Should().NotBe("Hello, 123!\n");
        translator.Translate(", 123!\n").Should().Be(", 123!\n");
        translator.Translate("ABC").ToLowerInvariant().Should().Be(translator.Translate("abc"));
    }
}
