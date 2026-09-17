namespace SWLOR.Game.Server.Feature.AppearanceDefinition.RacialAppearance
{
    public class GunganRacialAppearanceDefinition : RacialAppearanceBaseDefinition
    {
        public override float MinimumScale => 1.0f;
        public override float MaximumScale => 1.4f;
        public override int[] MaleHeads { get; } = { 300, 301, 302, 303, 304 };
        public override int[] FemaleHeads { get; } = { 300, 301, 302, 303, 304 };
        // Elf body parts; the human-only hand and limb options do not apply.
        public override int[] RightForearm { get; } = { 1, 2 };
        public override int[] LeftForearm { get; } = { 1, 2 };
        public override int[] RightHand { get; } = { 1, 2 };
        public override int[] LeftHand { get; } = { 1, 2 };
        public override int[] RightThigh { get; } = { 1, 2 };
        public override int[] LeftThigh { get; } = { 1, 2 };
    }
}
