namespace StrangerGameTools.Player
{
    public interface IStrangerPlayer
    {
        bool CanLook { get; set; }
        bool CanJump { get; set; }
        bool IsControllable { get; set; }
        bool IsGrounded { get; set; }
        float MoveSpeed { get; set; }

        void Setup();
    }
}
