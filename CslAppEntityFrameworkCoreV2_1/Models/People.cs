namespace Models
{
    public class People
    {
        public People(int id, string name, bool status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        public int Id { get; }
        public string Name { get; }
        public bool Status { get; }

        public string NameStatus
        {
            get
            {
                return $"{Name} - {Status}";
            }
        }

        public static People Create(int id, string name, bool status)
            => new People(id, name, status);
    }
}
