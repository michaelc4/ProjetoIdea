namespace Api.Domain.Presenters
{
    public class UsuarioPresenter
    {
        public string Id { get; set; }
        public string DesNome { get; set; }
        public string DesImagem { get; set; }
        public string DesEmail { get; set; }
        public string DesTelefone { get; set; }
        public string DesEspecialidade { get; set; }
        public string DesExperiencia { get; set; }
        public int Admin { get; set; }
        public int Local { get; set; }
    }
}
