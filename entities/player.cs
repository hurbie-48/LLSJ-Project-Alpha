namespace LLSJ_Project_Alpha.Entities
{
    public class Player
    {
        public string name;
        public int currentHitPoints, maximumHitPoints;
        // @TODO: create field for current weapon
        // @TODO: create field for current location

        public Player()
        {
            int currentHitPoints = 100;
            int maximumHitPoints = 100;
        }
    }
}