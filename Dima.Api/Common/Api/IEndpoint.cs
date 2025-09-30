namespace Dima.Api.Common.Api
{
    public interface IEndpoint
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}

//Comentários para meu controle pessoal
//Interface que define mapeamento de rotas da Minimal API, todos enpoints herdam dela, e que utilizem seu metodo map para mapear as rotas )
