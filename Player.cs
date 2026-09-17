using LLSJProjectAlpha;

namespace LLSJ_Project_Alpha.Entities;

public partial class Player
{
    public string Name { get; set; } = "";
    public int CurrentHitPoints { get; set; }
    public int MaximumHitPoints { get; set; }
    public Weapon? CurrentWeapon { get; set; }
    public Location? CurrentLocation { get; set; }
}