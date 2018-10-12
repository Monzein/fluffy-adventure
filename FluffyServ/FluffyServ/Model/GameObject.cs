
namespace FluffyServ.Model
{
    public abstract class GameObject
    {
        private static int idCpt = 0;

        private int id;
        private string name;
        private double space;
        private double mass;

        public GameObject(string name, double space, double mass)
        {
            this.id = NextId();
            this.name = name;
            this.space = space;
            this.mass = mass;
        }

        public int Id { get => id; }
        public string Name { get => name; }
        public double Space { get => space; }
        public double Mass { get => mass; }

        private static int NextId()
        {
            idCpt++;
            return idCpt;
        }
    }
}
