namespace StrangerGameTools.Save
{
    /// <summary>
    /// Interface for objects that can be saved and loaded using the Stranger Game Tools save system.
    /// Implement this interface to enable serialization of game objects to persistent storage.
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// Gets the unique key used to identify this saveable object.
        /// This key should be unique across all saveable objects in the game.
        /// </summary>
        string SaveKey { get; }

        /// <summary>
        /// Saves the current state of the object to a generic object that can be serialized.
        /// </summary>
        /// <returns>An object containing the serialized state of this saveable object.</returns>
        object SaveToObject();

        /// <summary>
        /// Loads the state of the object from a previously serialized object.
        /// </summary>
        /// <param name="data">The serialized data to load from.</param>
        void LoadFromObject(object data);
    }

    /// <summary>
    /// Generic version of ISaveable that provides type-safe save and load operations.
    /// Use this interface when you want strongly-typed serialization without casting.
    /// </summary>
    /// <typeparam name="T">The type of data to save and load.</typeparam>
    public interface ISaveable<T> : ISaveable
    {
        /// <summary>
        /// Saves the current state of the object to a strongly-typed object.
        /// </summary>
        /// <returns>The serialized state of this saveable object.</returns>
        T Save();

        /// <summary>
        /// Loads the state of the object from a strongly-typed serialized object.
        /// </summary>
        /// <param name="data">The serialized data to load from.</param>
        void Load(T data);
    }
}
