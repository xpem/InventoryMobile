using Models.Responses;

namespace Models
{
    public class BLLResponse
    {
        public bool Success { get; set; }

        public Object? Content { get; set; }

        public ErrorTypes? Error { get; set; }

        public string ErrorMessage => Error switch
        {
            ErrorTypes.ServerUnavaliable => "Servidor indisponível",
            ErrorTypes.WrongEmailOrPassword => "Senha ou email inválidos",
            _ => throw new NotImplementedException("Erro não mapeado")
        };
    }    
}
