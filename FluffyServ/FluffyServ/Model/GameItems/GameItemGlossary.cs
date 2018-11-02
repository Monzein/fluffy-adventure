namespace FluffyServ.Model.GameItems
{
    /// <summary>
    /// A glossary of all items in the game.
    /// TODO: Fill it from a config file.
    /// </summary>
    public class GameItemGlossary
    {
        public static UsableItem FRUIT_SALAD = new UsableItem("Salade de fruit", 2, 2, 0, 10);
        public static UsableItem BANDAGE = new UsableItem("Bandage", 1, 1, 5, 0);
        public static UsableItem MASO = new UsableItem("Maso", 1, 1, -10, 0);

        /// <summary>
        /// Get the GameItem by his name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameItem Parse(string name)
        {
            GameItem result = Resource.Parse(name);
            if (result == null)
            {
                if(name == FRUIT_SALAD.Name)
                {
                    return FRUIT_SALAD;
                }else if (name == BANDAGE.Name)
                {
                    return BANDAGE;
                }
                if (name == MASO.Name)
                {
                    return MASO;
                }
            }

            return result;
        }
    }
}
