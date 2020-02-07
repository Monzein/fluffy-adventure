using Newtonsoft.Json;

namespace FluffyServ.Model.Entities
{
    /// <summary>
    /// Character or item on a cell
    /// </summary>
    public abstract class Entity
    {
        private static int idCpt = 0;

        private int id;
        private string name;
        private string picture;

        public int Id { get => id; }
        public string Name { get => name; }
        public string Picture { get => picture; }

        private static int NextId()
        {
            idCpt++;
            return idCpt;
        }

        public override string ToString()
        {
            return "{\"Id\":\"" + Id + "\",\"Name\":\"" + Name + "\",\"Picture\":\"" + Picture + "\"}";
        }

        public Entity(string name, string picture)
        {
            this.id = NextId();
            this.name = name;
            this.picture = picture;
        }
        /*
        */
    }
}
