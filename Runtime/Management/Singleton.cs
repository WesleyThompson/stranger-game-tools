namespace StrangerGameTools.Management
{
    /// <summary>
    /// A generic base class for singleton objects.
    /// </summary>
    /// <typeparam name="T">The type of the singleton class.</typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets the singleton instance of the class.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new T();
                    }
                }
                return _instance;
            }
        }

        // Prevent direct instantiation from outside
        protected Singleton() { }
    }
}
