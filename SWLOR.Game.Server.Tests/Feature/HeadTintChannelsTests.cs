using FluentAssertions;
using NUnit.Framework;
using SWLOR.Game.Server.Feature.AppearanceDefinition.TintMap;
using SWLOR.NWN.API.NWScript.Enum.Item;

namespace SWLOR.Game.Server.Tests.Feature;

[TestFixture]
public class HeadTintChannelsTests
{
    private static TintMapMaterialSelection Selection(string model, bool item, params TintMapLayerType[] layers) =>
        new(model, new TintMapMaterialDefinition("Test", "test", layers), 1, 1, item, AppearanceArmor.Invalid);

    [Test]
    public void NoHeadKeepsOnlyNativeCreatureChannels()
    {
        HeadTintChannels.GetLayers(new[] {
            Selection("pmh0_chest303", false, TintMapLayerType.Cloth1),
            Selection("helm_001", true, TintMapLayerType.Metal2)
        }).Should().Equal(TintMapLayerType.Skin, TintMapLayerType.Hair,
            TintMapLayerType.Tattoo1, TintMapLayerType.Tattoo2);
    }

    [Test]
    public void MultipleHeadMaterialsExposeOnlyTheirUnionInStableOrder()
    {
        HeadTintChannels.GetLayers(new[] {
            Selection("pfh0_head311", false, TintMapLayerType.Metal1, TintMapLayerType.Cloth1),
            Selection("PFH0_HEAD311", false, TintMapLayerType.Cloth1, TintMapLayerType.Cloth2, TintMapLayerType.Leather1),
            Selection("pmh0_chest303", true, TintMapLayerType.Metal2)
        }).Should().Equal(TintMapLayerType.Skin, TintMapLayerType.Hair,
            TintMapLayerType.Tattoo1, TintMapLayerType.Tattoo2,
            TintMapLayerType.Cloth1, TintMapLayerType.Cloth2, TintMapLayerType.Leather1, TintMapLayerType.Metal1);
    }

    [Test]
    public void HeadSwitchRetainsSemanticChannelInsteadOfOldListIndex()
    {
        var oldLayers = HeadTintChannels.GetLayers(new[] {Selection("pfh0_head311", false,
            TintMapLayerType.Cloth1, TintMapLayerType.Metal1)});
        var next = HeadTintChannels.GetLayers(new[] {Selection("pmh0_head323", false, TintMapLayerType.Metal1)});
        HeadTintChannels.GetSelectedIndex(oldLayers, TintMapLayerType.Metal1).Should().Be(5);
        HeadTintChannels.GetSelectedIndex(next, TintMapLayerType.Metal1).Should().Be(4);
        HeadTintChannels.GetSelectedIndex(next, TintMapLayerType.Cloth1).Should().Be(0);
    }
}
