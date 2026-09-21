using LLSJProjectAlpha;

namespace LLSJ_Project_Alpha.Entities;

public partial class Player
{
    // Compatibility aliases used by the exploration code.
    public int CurrentHitPoints { get => CurrentHP; set => CurrentHP = value; }
    public int MaximumHitPoints { get => MaxHP; set => MaxHP = value; }
    public Weapon? CurrentWeapon { get; set; }
    public Location? CurrentLocation { get; set; }
}
