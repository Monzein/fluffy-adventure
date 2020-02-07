namespace FluffyServ.Model.Entities.GameItems.Equipables
{
    public class BodyArmor : Equipable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="picture"></param>
        /// <param name="space"></param>
        /// <param name="mass"></param>
        /// <param name="defense"></param>
        public BodyArmor(string name, string picture, double space, double mass, int defense) 
            : base(name, picture, space, mass, defense)
        {

        }
    }
}
