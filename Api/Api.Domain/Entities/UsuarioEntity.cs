namespace Api.Domain.Entities
{
    public class UsuarioEntity : BaseEntity
    {
        public string DesNome { get; set; }
        public string DesImagem { get; set; }
        public string DesEmail { get; set; }
        public string DesSenha { get; set; }
        public string DesTelefone { get; set; }
        public string DesEspecialidade { get; set; }
        public string DesExperiencia { get; set; }
        public int Admin { get; set; }
        public int Local { get; set; }
    }
}
