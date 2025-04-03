namespace Kucheu.StardewValleyFishing
{
    public enum TackleType
    {
        None,
        TrapBobber, //Causes fish to escape slower when you aren't reeling them in.
        CorkBobber, //Slightly increases the size of your "fishing bar". +24px per bobber
        LeadBobber, //Adds weight to your "fishing bar", preventing it from bouncing along the bottom.
        BarbedHook, //Makes your catch more secure, causing the "fishing bar" to cling to your catch. Works best on slow, weak fish.
    }
}