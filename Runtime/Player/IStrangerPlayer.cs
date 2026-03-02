namespace StrangerGameTools.Player
{
    public interface IStrangerPlayer
    {
        protected bool canLook {get; set;}
        protected bool canJump {get; set;}
        protected bool isControllable {get; set;}
        protected bool isGrounded {get; set;}
        protected float moveSpeed {get; set;}

        public abstract void Setup();
    }
}
