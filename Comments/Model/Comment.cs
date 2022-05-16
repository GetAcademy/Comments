namespace Comments.Model
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }

        public Comment()
        {
            Id = Guid.NewGuid();
        }
    }
}
