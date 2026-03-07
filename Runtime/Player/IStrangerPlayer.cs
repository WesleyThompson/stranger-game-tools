namespace StrangerGameTools.Player
{
    public interface IStrangerPlayer
    {
        bool CanLook { get; set; }
        bool CanMove { get; set; }
        bool CanJump { get; set; }
        bool IsControllable { get; set; }
        float MoveSpeed { get; set; }

        void Setup();
    }
}
