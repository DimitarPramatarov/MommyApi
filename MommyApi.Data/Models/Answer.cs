namespace MommyApi.Data.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public string Text { get; set; }

        public bool CorrectAnswer { get; set; } = false;



    }
}
