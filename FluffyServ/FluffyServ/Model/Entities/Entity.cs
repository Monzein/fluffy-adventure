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

        public int Id { get => id; }
        public string Name { get => name; }

        private static int NextId()
        {
            idCpt++;
            return idCpt;
        }

        public override string ToString()
        {
            return "{\"Id\":\"" + Id + "\",\"Name\":\"" + Name + "\"}";
        }

        public Entity(string name)
        {
            this.id = NextId();
            this.name = name;
        }
        /*
        */
    }
}
