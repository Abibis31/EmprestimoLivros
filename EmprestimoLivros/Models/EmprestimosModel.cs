namespace EmprestimoLivros.Models
{
    public class EmprestimosModel
    {
        public int Id { get; set; }
        public string Recebedor { get; set; }
        public string Forcenedor { get; set; }
        public string LivroEmprestado { get; set; }

        public DateTime dataUltimaAtualização { get; set; } = DateTime.Now;
    } 
}
